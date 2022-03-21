namespace DinoGame
{
    public class Dino : CollisionObject
    {
        public Dino(int x, int y, int height, int width) : base(x, y, height, width)
        {
            this.IsVisible = true;
            this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '['), new DrawPoint(X, Y + 1, 'M'), new DrawPoint(X-1, Y, '≈') };
        }
        public char Head { get; set; }
        public char Body { get; set; }
        public bool isInAir { get; set; }

        
        public void Move(int spaces)
        {
                Y += spaces;
                this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, 'ß'), new DrawPoint(X, Y + 1, 'M'), new DrawPoint(X - 1, Y, '/') };
           
        }
    }
}
