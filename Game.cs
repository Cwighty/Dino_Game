namespace DinoGame
{
    public class Game
    {
        static int displayWidth = 100;
        static int displayHeight = 20;
        static int gameSpeed = 4000;
        static int score = 0;
        static int spawnTimer = 0;
        static List<CollisionObject> collisionObjects = new List<CollisionObject>();
        static Dino dino;
        private static bool jumping;
        private static int jumpFrame;
        private static Background background;

        public static void Main(string[] args)
        {
            var display = new Display(displayHeight, displayWidth);
            var objectsToDraw = new List<IDrawable>();
            var rand = new Random();
            //TODO: add your cactus/birds/dinos to the collisionObject list to print them out
            // You can create an object at the position you want it : See the bird class for how to implement IDrawable
            background = new Background(displayWidth, displayHeight);
            objectsToDraw.Add(background);
            dino = new Dino(5, 0);
            objectsToDraw.Add(dino);

            display.PrintStartScreen();
            while (Console.ReadKey().Key != ConsoleKey.Spacebar) { }

            //
            while (true)
            {
                if (checkCollision(objectsToDraw))
                {
                    Thread.Sleep(1000);
                }
                //Drawing
                if (score % 2000 == 0) {
                    ToggleDayAndNight();
                }

                objectsToDraw = CheckVisiblity(objectsToDraw);
                Console.Clear();
                display.DisplayScore(score);
                display.DrawNextFrame(objectsToDraw);
                display.PrintCurrentFrame();
                Thread.Sleep(4000/gameSpeed);

                if (score % 4 == 0)
                {
                    background.MoveLeft();
                }
                //Spawn lottery
                if(timeToSpawn(spawnTimer))
                {
                    objectsToDraw.Add(getObstacle());
                    spawnTimer = 0;
                }

                // Move the birds and the cacti left
                //if(score % 3 == 0)
                //{

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
                //}

                if (Console.KeyAvailable)
                {
                    jumping = true;
                    Console.ReadKey(true);
                }

                if (jumping)
                {
                    dino.Jump(jumpFrame);
                    jumpFrame++;
                    if(jumpFrame == 16)
                    {
                        jumping = false;
                        jumpFrame = 0;
                    }
                }


                dino.AnimateLegs();
                score++;
                spawnTimer++;
            }

        }

        private static bool checkCollision(List<IDrawable> drawables)
        {
            foreach (var o in drawables)
            {
                if (o is Bird)
                {
                    if(((Bird)o).GetHitBox().Overlaps(dino.GetHitBox()))
                    {
                        return true;
                    }
                }
                else if (o is Cactus)
                {
                    if(((Cactus)o).GetHitBox().Overlaps(dino.GetHitBox()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void ToggleDayAndNight()
        {
            if (Console.BackgroundColor == ConsoleColor.Black)
            {
                background.IsDay = true;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                background.IsDay = false;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void Jump(int jumpFrame, Dino dino)
        {
            int[] frames = new int[] { 2,2, 2, 1,1, 1, 0,0,0,0 ,-1, -1,-1,-2, -2, -2 };
            dino.Move(frames[jumpFrame]);
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
