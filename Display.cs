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
        }
        
        public void DrawNextFrame(List<IDrawable> objects)
        {
            var tempList = new List<DrawPoint>();
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
                    Console.SetCursorPosition(point.X, this.height - point.Y);
                    Console.Write(point.Character);
                }
        }

    }
}
