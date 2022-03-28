namespace DinoGame
{
    public class Dino : CollisionObject
    {
        public Dino(int x, int y, int height, int width) : base(x, y, height, width)
        {
            this.IsVisible = true;
            this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '|'), new DrawPoint(X - 1, Y, '┘'), new DrawPoint(X, Y + 1, '█'), new DrawPoint(X, Y + 2, '▄'), new DrawPoint(X - 1, Y + 1, '▄'), new DrawPoint(X + 1, Y + 2, '▄') };
        }
        public char Head { get; set; }
        public char Body { get; set; }
        public bool isInAir { get; set; }

        
        public void Move(int spaces)
        {
            Y += spaces;
        }

        public void Jump(int jumpFrame)
        {
            int[] frames = new int[] { 2, 2, 1, 1, 0, 0, -1, -1, -2, -2 };
            this.Move(frames[jumpFrame]);
        }

        public void AnimateLegs()
        {
            if (DrawPoints.Contains(new DrawPoint(X, Y, '┘')))
            {
                this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '|'), new DrawPoint(X - 1, Y, '┘'), new DrawPoint(X, Y + 1, '█'), new DrawPoint(X, Y + 2, '▄'), new DrawPoint(X - 1, Y + 1, '▄'), new DrawPoint(X + 1, Y + 2, '▄') };
            }
            else
            {
                this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '┘'), new DrawPoint(X - 1, Y, '/'), new DrawPoint(X, Y + 1, '█'), new DrawPoint(X, Y + 2, '▄'), new DrawPoint(X - 1, Y + 1, '▄'), new DrawPoint(X + 1, Y + 2, '▄') };
            }
        }
    }
}
