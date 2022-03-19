namespace DinoGame
{
    public class Cactus : CollisionObject
    {
        public Cactus(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.IsVisible = true;
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
