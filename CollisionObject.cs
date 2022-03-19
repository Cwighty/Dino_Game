namespace DinoGame
{
    public class CollisionObject : IDrawable
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public int Height { get; internal set; }
        public int Width { get; internal set; }

        private List<DrawPoint> drawPoints;

        public List<DrawPoint> DrawPoints {get => drawPoints; internal set => drawPoints = value; }
        public bool IsVisible { get; set; }

        public (int, int) GetPosition()
        {
            return (this.X, this.Y);
        }

        public CollisionObject(int x, int y, int height, int width)
        {
            DrawPoints = new List<DrawPoint>();
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }
     }
}
