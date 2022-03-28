namespace DinoGame
{
    public class Display
    {
        List<DrawPoint> nextFrame;
        List<DrawPoint> currentFrame;
        int height, width;
        char[] ground;

        public Display(int height, int width)
        {
            this.height = height;
            this.width = width;
            nextFrame = new List<DrawPoint>();
            currentFrame = new List<DrawPoint>();
            ground = generateGround(width);
        }

        private char[] generateGround(int displayWidth)
        {
            var random = new Random();
            char[] groundTextures = new char[] { '░', '▒', '▓'};
            char[] generated = new char[displayWidth];
            for (int i =0; i < this.width; i++)
            {
                generated[i] = groundTextures[random.Next(groundTextures.Length)];
            }
            return generated;
        }

        public void DrawNextFrame(List<IDrawable> objects)
        {
            nextFrame = new List<DrawPoint>();
            foreach (IDrawable obj in objects)
            {
                foreach (DrawPoint point in obj.DrawPoints)
                {
                    nextFrame.Add(point);
                }
            }
        }
        public void PrintCurrentFrame()
        {
                ground = rotateArray(ground);
                PrintGround();
                foreach (var point in nextFrame)
                {
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(point.X, this.height - point.Y);
                    Console.Write(point.Character);
                }
        }

        public void PrintStartScreen()
        {
            Console.SetCursorPosition(this.width / 5, this.height / 2);
            Console.WriteLine("Press the spacebar to start the game.");
            Console.SetCursorPosition(this.width / 14, this.height / 2 + 1);
            Console.WriteLine("Use the up and down arrows to control the dinosaur.");
        }

        public void PrintGround()
        {
            string newGround = new string(ground);
            Console.SetCursorPosition(0, this.height+1);
            Console.Write(newGround);
        }

        private char[] rotateArray(char[] curr)
        {
            var newArray = new char[curr.Count()];
            int i = 1;
            do
            {
                newArray[i] = curr[(i + 1) % curr.Count()];
                i = (i + 1) % curr.Count();
            }
            while (i != 1);
            return newArray;
        }
        public void DisplayScore(int score)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Score: " + score);
        }
    }
}
