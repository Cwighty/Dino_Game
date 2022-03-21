namespace DinoGame
{
    public class Cactus : CollisionObject
    {
        public Cactus(int x) : base(x, 0, 0, 0)
        {
            this.IsVisible = true;
            var rand = new Random();
            this.Height = rand.Next(1, 3);
            this.Width = rand.Next(1, 4);
        }

        public void moveLeft()
        {

            if (X > 0)
            {
                X--;
                if (Height == 1 && Width == 1)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '╣') };
                }
                if (Height == 2 && Width == 1)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '╣'), new DrawPoint(X, Y + 1, '║') };
                }
                if (Height == 1 && Width == 2)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '╣'), new DrawPoint(X + 1, Y, '╣') };
                }
                if (Height == 2 && Width == 2)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '╚'), new DrawPoint(X + 1, Y, '╣'), new DrawPoint(X, Y + 1, ' '), new DrawPoint(X + 1, Y + 1, '║') };
                }
                if (Height == 1 && Width == 3)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '╚'), new DrawPoint(X + 1, Y, '╣'), new DrawPoint(X + 2, Y, '╣') };
                }
                if (Height == 2 && Width == 3)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '╚'), new DrawPoint(X + 1, Y, '╣'), new DrawPoint(X + 2, Y, '╠'), new DrawPoint(X, Y + 1, ' '), new DrawPoint(X + 1, Y + 1, '║'), new DrawPoint(X + 2, Y + 1, '║') };
                }
            }
            else
            {
                IsVisible = false;
            }
        }
    }
}
