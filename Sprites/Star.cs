namespace DinoGame
{
    internal class Star : BackgroundObject
    {
        private int id;
        public Star(int displayWidth, int displayHeight, int layer)
        {
            Random random = new Random();

            this.IsVisible = true;

            this.originX = random.Next(800);
            this.originY = random.Next(1, displayHeight);
            this.id = random.Next(1, 4);
            this.layer = layer;
            this.DrawPoints = Display.ConvertSpriteToDrawPoints(@$"{Resource.Sprites}\Star{id}.txt", originX, originY);
        }

        public override void MoveLeft()
        {
            if (this.DrawPoints[DrawPoints.Count - 1].X >= 0)
            {
                originX -= layer;
                this.DrawPoints = Display.ConvertSpriteToDrawPoints(@$"{Resource.Sprites}\Star{id}.txt", originX, originY);
            }
            else
            {
                this.IsVisible = false;
            }
        }
    }

}