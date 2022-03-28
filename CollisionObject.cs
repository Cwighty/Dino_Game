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

        public HitBox GetHitBox()
        {
            return new HitBox(X, Y, Width, Height);
        }

        public CollisionObject(int x, int y, int width, int height)
        {
            drawPoints = new List<DrawPoint>();
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }
     }

    public class HitBox
    {
        public List<(int, int)> Bounds { get; private set; }
        public HitBox(int x, int y, int width, int height)
        {
            Bounds = new List<(int, int)>();
            for(int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    Bounds.Add((i, j));
                }
            }
        }

        public bool Overlaps(HitBox otherHitBox)
        {
            foreach(var point in this.Bounds)
            {
                if (otherHitBox.Bounds.Contains(point))
                    return true;
            }
            return false;
        }
    }
}


