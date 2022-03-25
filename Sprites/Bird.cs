namespace DinoGame
{
    public class Bird : CollisionObject
    {
        public Bird(int lengthOfDisplay, int topOfDisplay) :base(lengthOfDisplay, topOfDisplay, 1, 2)
        {
            //this.DrawPoints = new List<DrawPoint>() { new DrawPoint(x, y, 'e') };
            this.IsVisible = true;
            var rand = new Random();
            this.Y = rand.Next(1, rand.Next(1, topOfDisplay));
        }

        public void MoveLeft()
        {
            // SAMPLE CODE FOR MOVING AND UPDATING DRAW POINT
            
            if(X>0)
            {
                X -= 1;
                if (X % 8 == 0 || X % 8 == 1 || X % 8 == 2 || X % 8 == 3)
                {
                    this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, 'V'), new DrawPoint(X + 1, Y, 'V') };
                }
                else
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
