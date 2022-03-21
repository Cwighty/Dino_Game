namespace DinoGame
{
    public class Game
    {
        static int displayWidth = 60;
        static int displayHeight = 20;
        static int gameSpeed = 100;
        static int score = 0;
        static List<CollisionObject> collisionObjects = new List<CollisionObject>();
        public static void Main(string[] args)
        {
            var display = new Display(displayHeight, displayWidth);
            var objectsToDraw = new List<IDrawable>();
            var rand = new Random();
            //TODO: add your cactus/birds/dinos to the collisionObject list to print them out
            // You can create an object at the position you want it : See the bird class for how to implement IDrawable
            var bird = new Bird(displayWidth, rand.Next(displayHeight-5, displayHeight));
            var cactus = new Cactus(displayWidth, 1, 3, 1);
            objectsToDraw.Add(bird);
            objectsToDraw.Add(cactus);

            display.PrintStartScreen();
            // TODO: Wait for spacebar press here
            Thread.Sleep(100);
            //
            while(true)
            {
                objectsToDraw = CheckVisiblity(objectsToDraw);
                Console.Clear();

                DisplayScore();
                display.DrawNextFrame(objectsToDraw);
                display.PrintCurrentFrame();

                Thread.Sleep(4000/gameSpeed);
                //You can move you object here
                cactus.moveLeft();
                bird.moveLeft();

                score++;
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

        public static void DisplayScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Score: " + score);
        }
    }
}
