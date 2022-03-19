namespace DinoGame
{
    public class Display
    {
        List<DrawPoint> nextFrame;
        List<DrawPoint> currentFrame;
        int height, width;

        public Display(int height, int width)
        {
            this.height = height;
            this.width = width;
            nextFrame = new List<DrawPoint>();
            currentFrame = new List<DrawPoint>();
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
        }

        }
}
