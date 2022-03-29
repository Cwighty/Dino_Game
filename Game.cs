using System.Media;

namespace DinoGame
{
    public class Game
    {
        static int displayWidth = 100;
        static int displayHeight = 20;
        static int gameSpeed = 500;
        static int score = 0;
        static int spawnTimer = 0;
        static List<CollisionObject> collisionObjects = new List<CollisionObject>();
        private static bool jumping;
        private static int jumpFrame;
        private static bool fallFaster;
        private static int duckCounter;
        static Dino dino = new Dino(5, 0);
        private static Background background = new Background(displayWidth, displayHeight);
        private static SoundPlayer player = new SoundPlayer(@"./sound93.wav");
        private static SoundPlayer music = new SoundPlayer(@"Sound\8bittune.wav");

        public static void Main(string[] args)
        {
            music.PlayLooping();
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
                    //player = new SoundPlayer(@"Sound\gameoversound.wav");
                    //player.Play();
                    display.GameOverScreen();
                    if (display.AskToRestart())
                    {
                        objectsToDraw.Clear();
                        objectsToDraw.Add(background);
                        objectsToDraw.Add(dino);
                        score = 0;
                    }
                    else
                    {
                        break;
                    }
                }

                //Drawing
                if (score % 2000 == 0)
                {
                    ToggleDayAndNight();
                }

                if (score % 1000 == 0 && score != 0)
                {
                    //player = new SoundPlayer(@"Sound\scoreSound.wav");
                    //player.Play();               
                }

                objectsToDraw = CheckVisiblity(objectsToDraw);
                Console.Clear();
                display.DisplayScore(score);
                display.DrawNextFrame(objectsToDraw);
                display.PrintCurrentFrame();
                Thread.Sleep(4000 / gameSpeed);

                if (score % 4 == 0)
                {
                    background.MoveLeft();
                }

                //Spawn lottery
                if (timeToSpawn(spawnTimer))
                {
                    objectsToDraw.Add(getObstacle());
                    spawnTimer = 0;
                }

                // Move the birds and the cacti left
                foreach (var o in objectsToDraw)
                {
                    if (o is Bird)
                    {
                        ((Bird)o).MoveLeft();
                    }
                    else if (o is Cactus)
                    {
                        ((Cactus)o).MoveLeft();
                    }
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Spacebar || key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                    {
                        jumping = true;
                        //player = new SoundPlayer(@"Sound\jumpSound.wav");
                        //player.Play();
                    }
                    if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && !jumping)
                    {
                        dino.isDucking = true;
                        dino.Duck();

                    }
                    if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && jumping)
                    {
                        fallFaster = true;
                    }
                
                }

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(false);
                }

                if (dino.isDucking)
                {
                    dino.Duck();
                    duckCounter++;
                    if (duckCounter == 20)
                    {
                        dino.isDucking = false;
                        dino.Height = 3;
                        duckCounter = 0;
                    }
                }

                if (jumping)
                {
                    if (fallFaster)
                    {
                        dino.Jump(jumpFrame);
                        if (jumpFrame < 20)
                            jumpFrame++;
                        dino.Jump(jumpFrame);
                        if (jumpFrame < 20)
                            jumpFrame++;
                    }
                    else
                    {
                        dino.Jump(jumpFrame);
                        jumpFrame++;
                    }
                    if (jumpFrame >= 20)
                    {
                        jumping = false;
                        jumpFrame = 0;
                        fallFaster = false;
                    }
                }

                dino.AnimateLegs();
                score++;
                spawnTimer++;
            }

            display.GameOverScreen();

        }

        private static bool checkCollision(List<IDrawable> drawables)
        {
            foreach (var o in drawables)
            {
                if (o is Bird)
                {
                    if (((Bird)o).GetHitBox().Overlaps(dino.GetHitBox()))
                    {
                        return true;
                    }
                }
                else if (o is Cactus)
                {
                    if (((Cactus)o).GetHitBox().Overlaps(dino.GetHitBox()))
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
            if (rand.Next(0, 5) == 0)
            {
                // Occasionally close together spawn
                if (time >= rand.Next(35, 50))
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
