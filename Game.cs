using System.Media;

namespace DinoGame
{
    public static class Game
    {
        private static int displayWidth = 100;
        private static int displayHeight = 20;
        private static int gameSpeed = 1000;

        private static int score = 0;
        private static int spawnTimer = 0;

        private static List<IDrawable> objectsToDraw = new List<IDrawable>();
        private static Display display = new Display(displayHeight, displayWidth);
        private static Background background = new Background(displayWidth, displayHeight);
        private static Dino dino = new Dino(5, 0);

        private static SoundPlayer player = new SoundPlayer(@"./sound93.wav");
        private static SoundPlayer music = new SoundPlayer(@"Sound\8bittune.wav");

        public static void Main(string[] args)
        {
            Console.Clear();

            var rand = new Random();

            if (args.Contains("-music"))
            {
                music.PlayLooping();
            }

            objectsToDraw.Add(background);
            objectsToDraw.Add(dino);

            display.PrintStartScreen();
            while (Console.ReadKey().Key != ConsoleKey.Spacebar) { }

            //Main loop
            while (true)
            {
                if (isCollision(objectsToDraw))
                {
                    if (!args.Contains("-music"))
                    {
                        player = new SoundPlayer(@"Sound\gameoversound.wav");
                        player.Play();
                    }

                    display.GameOverScreen();

                    if (display.AskToRestart())
                    {
                        reinitializeGame();
                    }
                    else
                    {
                        break;
                    }
                }

                if (score % 2000 == 0)
                {
                    ToggleDayAndNight();
                }

                //Checkpoint sound
                if (score % 1000 == 0 && score != 0)
                {
                    if (!args.Contains("-music"))
                    {
                        player = new SoundPlayer(@"Sound\scoreSound.wav");
                        player.Play();
                    }
                }

                drawScreen();
                moveObjects();

                //Spawn lottery
                if (timeToSpawn(spawnTimer))
                {
                    objectsToDraw.Add(getObstacle());
                    spawnTimer = 0;
                }

                doKeyboardControl(args);

                doDuck();
                
                doJump();
                
                dino.AnimateLegs();

                score++;
                spawnTimer++;
            }

            display.GameOverScreen();

        }

        private static void doKeyboardControl(string[] args)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Spacebar || key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    dino.IsJumping = true;
                    if (!args.Contains("-music"))
                    {
                        player = new SoundPlayer(@"Sound\jumpSound.wav");
                        player.Play();
                    }
                }
                if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && !dino.IsJumping)
                {
                    dino.IsDucking = true;
                    dino.Duck();

                }
                if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && dino.IsJumping)
                {
                    dino.FallFaster = true;
                }
            }

            //Clear input buffer
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }

        private static void moveObjects()
        {
            //Move background
            if (score % 4 == 0)
            {
                background.MoveLeft();
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
        }

        private static void drawScreen()
        {
            objectsToDraw = CheckVisiblity(objectsToDraw);
            Console.Clear();
            display.DisplayScore(score);
            display.DrawNextFrame(objectsToDraw);
            display.PrintCurrentFrame();
            Thread.Sleep(4000 / gameSpeed);
        }

        private static void reinitializeGame()
        {
            objectsToDraw.Clear();
            objectsToDraw.Add(background);
            objectsToDraw.Add(dino);
            score = 0;
        }

        private static void doDuck()
        {
            if (dino.IsDucking)
            {
                dino.Duck();
                dino.DuckFrame++;
                if (dino.DuckFrame == 20)
                {
                    dino.IsDucking = false;
                    dino.Height = 3;
                    dino.DuckFrame = 0;
                }
            }
        }

        private static void doJump()
        {
            if (dino.IsJumping)
            {
                if (dino.FallFaster)
                {
                    dino.Jump(dino.JumpFrame);
                    if (dino.JumpFrame < 20)
                        dino.JumpFrame++;
                    dino.Jump(dino.JumpFrame);
                    if (dino.JumpFrame < 20)
                        dino.JumpFrame++;
                }
                else
                {
                    dino.Jump(dino.JumpFrame);
                    dino.JumpFrame++;
                }
                if (dino.JumpFrame >= 20)
                {
                    dino.IsJumping = false;
                    dino.JumpFrame = 0;
                    dino.FallFaster = false;
                }
            }
        }

        private static bool isCollision(List<IDrawable> drawables)
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
