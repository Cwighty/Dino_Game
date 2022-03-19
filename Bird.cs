namespace DinoGame
{
    public class Bird : CollisionObject
    {
        public Bird(int x, int y, int height, int width) :base(x, y, height, width)
        {
            //this.DrawPoints = new List<DrawPoint>() { new DrawPoint(x, y, 'e') };
        }

        public void moveLeft()
        {
            // SAMPLE CODE FOR MOVING AND UPDATING DRAW POINT
            /*
            if(X>0)
            {
                X--;
                this.DrawPoints = new List<DrawPoint>() { new DrawPoint(X, Y, 'e') };
            }
            */
        }
    }
}
