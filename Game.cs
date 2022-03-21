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
            Thread.Sleep(2000);
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
        public string getObject()
        {
            Random rnd = new Random();
            int type = rnd.Next(2);
            if (type == 0) //bird 
            {
                int height == rnd.Next(1, 3)
                return Bird.Bird(height);
            }
            else //cactus
            {
                int width = rnd.Next(6);
                if (width <= 3)
                {
                    return Cactus.Cactus(int height = rnd.Next(5));
                }
                if (width <= 5)
                {
                    return Cactus.Cactus(int height = rnd.Next(5)) + Cactus.Cactus(int height = rnd.Next(5));
                }
                if (width == 6)
                {
                    return Cactus.Cactus(int height = rnd.Next(5)) + Cactus.Cactus(int height = rnd.Next(5)) + Cactus.Cactus(int height = rnd.Next(5));

                }

            }

        }
    }
}
