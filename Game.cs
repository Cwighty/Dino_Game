namespace DinoGame
{
    public class Game
    {
        static int displayWidth = 60;
        static int displayHeight = 20;
        static int gameSpeed = 1000;
        static int score = 0;
        static List<CollisionObject> collisionObjects = new List<CollisionObject>();
        private static bool jumping;
        private static int jumpFrame;

        public static void Main(string[] args)
        {
            var display = new Display(displayHeight, displayWidth);
            var objectsToDraw = new List<IDrawable>();
            var rand = new Random();
            //TODO: add your cactus/birds/dinos to the collisionObject list to print them out
            // You can create an object at the position you want it : See the bird class for how to implement IDrawable
            var bird = new Bird(displayWidth, rand.Next(displayHeight-5, displayHeight));
            var cactus = new Cactus(displayWidth, 0, 3, 1);
            var dino = new Dino(5, 0, 2, 1);
            objectsToDraw.Add(bird);
            objectsToDraw.Add(cactus);
            objectsToDraw.Add(dino);

            display.PrintStartScreen();
            while (Console.ReadKey().Key != ConsoleKey.Spacebar) { }

            //
            while (true)
            {
                objectsToDraw = CheckVisiblity(objectsToDraw);
                Console.Clear();
                DisplayScore();
                display.DrawNextFrame(objectsToDraw);
                display.PrintCurrentFrame();
                Thread.Sleep(4000/gameSpeed);
                bird.moveLeft();
                cactus.moveLeft();
                //You can move you object here
                if (Console.KeyAvailable)
                {
                    jumping = true;
                    Console.ReadKey(true);
                }

                if (jumping)
                {
                    Jump(jumpFrame, dino);
                    jumpFrame++;
                    if(jumpFrame == 10)
                    {
                        jumping = false;
                        jumpFrame = 0;
                    }
                }

                score++;
            }

        }

        private static void Jump(int jumpFrame, Dino dino)
        {
            int[] frames = new int[] { 2, 2, 1, 1, 0,0 ,-1, -1, -2, -2 };
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

        public static void DisplayScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Score: " + score);
        }
    }
}
