namespace DinoGame
{
    public class Bird : CollisionObject
    {
        public Bird(int x, int y, int height, int width) :base(x, y, height, width)
        {
            //this.DrawPoints = new List<DrawPoint>() { new DrawPoint(x, y, 'e') };
            this.IsVisible = true;
        }

        public void moveLeft()
        {
            // SAMPLE CODE FOR MOVING AND UPDATING DRAW POINT
            
            if(X>0)
            {
                X--;
                if (X % 2 == 0)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, 'V'), new DrawPoint(X + 1, Y, 'V') };
                }
                if (X % 2 == 1)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, '~'), new DrawPoint(X + 1, Y, '~') };
                }
            }
            else
            {
                IsVisible = false;
            }
        }
    }
}
