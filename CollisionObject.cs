namespace DinoGame
{
    public class CollisionObject : IDrawable
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public int Height { get; internal set; }
        public int Width { get; internal set; }

        private List<DrawPoint> drawPoints;

        public List<DrawPoint> DrawPoints => drawPoints;

        public (int, int) GetPosition()
        {
            return (this.X, this.Y);
        }
     }
}
