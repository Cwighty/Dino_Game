using System.Windows.Input;

namespace DinoGame
{
    public class Game
    {
        static int displayWidth = 100;
        static int displayHeight = 20;
        static int gameSpeed = 1000;
        static int score = 0;
        static int spawnTimer = 0;
        static List<CollisionObject> collisionObjects = new List<CollisionObject>();
        private static bool jumping;
        private static bool ducking;
        private static int jumpFrame;

        public static void Main(string[] args)
        {
            var display = new Display(displayHeight, displayWidth);
            var objectsToDraw = new List<IDrawable>();
            var rand = new Random();
            //TODO: add your cactus/birds/dinos to the collisionObject list to print them out
            // You can create an object at the position you want it : See the bird class for how to implement IDrawable
            var dino = new Dino(5, 0, 2, 1);
            objectsToDraw.Add(dino);

            display.PrintStartScreen();
            while (Console.ReadKey().Key != ConsoleKey.Spacebar) { }

            //
            while (true)
            {
                //Drawing
                objectsToDraw = CheckVisiblity(objectsToDraw);
                Console.Clear();
                display.DisplayScore(score);
                display.DrawNextFrame(objectsToDraw);
                display.PrintCurrentFrame();
                Thread.Sleep(4000/gameSpeed);

                //Spawn lottery
                if(timeToSpawn(spawnTimer))
                {
                    objectsToDraw.Add(getObstacle());
                    spawnTimer = 0;
                }

                // Move the birds and the cacti left
                foreach(var o in objectsToDraw)
                {
                    if(o is Bird)
                    {
                        ((Bird) o).MoveLeft();
                    }
                    else if (o is Cactus)
                    {
                        ((Cactus) o).MoveLeft();
                    }
                }

                if(Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Spacebar || key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                   jumping = true;
                }
                if(key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    ducking = true;
                }

                }

                while(Console.KeyAvailable)
                {
                    Console.ReadKey(false);
                }
                
                

                if (jumping)
                {
                    dino.Jump(jumpFrame);
                    jumpFrame++;
                    if(jumpFrame == 20)
                    {
                        jumping = false;
                        jumpFrame = 0;
                    }
                }

                if(ducking)
                {
            
                    dino.Duck();
                }

                dino.AnimateLegs();
                score++;
                spawnTimer++;
            }

        }

        public static List<IDrawable> CheckVisiblity(List<IDrawable> otd)
        {
            List<IDrawable> visiblity = new List<IDrawable>();
            foreach (IDrawable obj in otd)
            {
                if (obj.IsVisible)
                {
                    visiblity.Add(obj);
                }
            }
            return visiblity;
        }

        private static CollisionObject getObstacle()
        {
            Random rnd = new Random();
            int type = rnd.Next(3);
            if (type == 0) //bird 
            {
                return new Bird(displayWidth, displayHeight);
            }
            else
            {
                return new Cactus(displayWidth);
            }

        }
        private static bool timeToSpawn(int time)
        {
            var rand = new Random();
            if (rand.Next(0,5) == 0)
            {
                // Occasionally close together spawn
                if (time >= rand.Next(20, 50))
                {
                    return true;
                }
            }
            if (time >= rand.Next(50, 150))
            {
                // Average spawn with some distance between
                return true;
            }
            return false;
        }
    }
}
