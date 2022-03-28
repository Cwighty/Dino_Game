namespace DinoGame
{
    public class Dino : CollisionObject
    {
        public Dino(int x, int y) : base(x, y, 3, 3)
        {
            this.IsVisible = true;
            this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X + 1, Y, '|'), new DrawPoint(X, Y, '┘'), new DrawPoint(X + 1, Y + 1, '█'), new DrawPoint(X + 1, Y + 2, '▄'), new DrawPoint(X, Y + 1, '▄'), new DrawPoint(X + 2, Y + 2, '▄') };
        }
        public char Head { get; set; }
        public char Body { get; set; }
        public bool isInAir { get; set; }
        public bool isDucking { get; set; }

        
        public void Move(int spaces)
        {
            if (Y >= 0)
                Y += spaces;
        }

        public void Jump(int jumpFrame)
        {
            int[] frames = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1 };
            if(jumpFrame < 20)
                this.Move(frames[jumpFrame]);
        }

        public void AnimateLegs()
        {
            if (!this.isDucking)
            {
                if (this.DrawPoints.Contains(new DrawPoint(X + 1, Y, '┘')))
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X + 1, Y, '|'), new DrawPoint(X, Y, '┘'), new DrawPoint(X + 1, Y + 1, '█'), new DrawPoint(X + 1, Y + 2, '▄'), new DrawPoint(X, Y + 1, '▄'), new DrawPoint(X + 2, Y + 2, '▄') };
                }
                else
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X + 1, Y, '┘'), new DrawPoint(X, Y, '/'), new DrawPoint(X + 1, Y + 1, '█'), new DrawPoint(X + 1, Y + 2, '▄'), new DrawPoint(X, Y + 1, '▄'), new DrawPoint(X + 2, Y + 2, '▄') };
                }
            }
        }

        public void Duck()
        {
            this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '▄'), new DrawPoint(X - 1, Y, '▄'), new DrawPoint(X + 1, Y, '▄') };
            Height = 1;
        }
    }
}
