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
            //TODO: add your cactus/birds/dinos to the collisionObject list to print them out
            // You can create an object at the position you want it : See the bird class for how to implement IDrawable
            var bird = new Bird(displayWidth, 10, 1, 2);
            objectsToDraw.Add(bird);

            while(true)
            {
                objectsToDraw = CheckVisiblity(objectsToDraw);
                Console.Clear();
                display.DrawNextFrame(objectsToDraw);
                display.PrintCurrentFrame();
                Thread.Sleep(4000/gameSpeed);
                //You can move you object here
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
    }
}
