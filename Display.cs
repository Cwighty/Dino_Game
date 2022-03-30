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
            char[] groundTextures = new char[] { '░', '▒', '▓' };
            char[] generated = new char[displayWidth];
            for (int i = 0; i < this.width; i++)
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
            
            char[][] currentFrame = new char[this.height][];
            for (int row = 0; row < this.height; row++)
            {
                currentFrame[row] = new char[this.width];
                for(int col = 0; col < this.width; col++)
                {
                    currentFrame[row][col] = ' ';
                }
            }

            /*foreach (var point in nextFrame)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(point.X, this.height - point.Y);
                Console.Write(point.Character);
            }*/
            foreach (var point in nextFrame)
            {
                if(point.X >= 0 && point.Y >= 0 && point.X < this.width && point.Y < this.height)
                    currentFrame[point.Y][point.X] = point.Character;
            }
            Console.SetCursorPosition(0, 1);
            for (int i = this.height - 1; i >=0; i--)
            {
                var line = new string(currentFrame[i]);
                Console.WriteLine(line);
            }
        }

        public void PrintStartScreen()
        {
            string text = System.IO.File.ReadAllText(@$"{Resource.Sprites}\startscreen.txt");
            Console.WriteLine(text);
        }

        public void PrintGround()
        {
            string newGround = new string(ground);
            Console.SetCursorPosition(0, this.height + 1);
            Console.Write(newGround);
        }

        public static List<DrawPoint> ConvertSpriteToDrawPoints(string path, int x, int y)
        {
            var sr = new StreamReader(path);
            List<string> lines = new List<string>();
            List<DrawPoint> points = new List<DrawPoint>();
            while (!sr.EndOfStream)
            {
                lines.Add(sr.ReadLine());
            }
            int tempY = y + lines.Count;
            int tempX = x;
            foreach (var line in lines)
            {
                x = tempX;
                foreach (char c in line)
                {
                    points.Add(new DrawPoint(x, y, c));
                    x++;
                }
                y--;
            }
            return points;
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

        public void DisplayHighScore(int highscore)
        {
            if (highscore > 0)
            {
                Console.SetCursorPosition(15, 0);
                Console.Write("High Score: " + highscore);
            }
        }

        public bool AskToRestart()
        {
            Console.SetCursorPosition(width / 3 + 5, height / 3);
            Console.Write("GAME OVER");
            Console.SetCursorPosition(width / 3, (height / 3)+1);
            Console.Write("Press Space To Restart");
            while (Console.ReadKey().Key != ConsoleKey.Spacebar) { }
            return true;
        }
    }

}
