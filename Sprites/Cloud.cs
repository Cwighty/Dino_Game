namespace DinoGame
{
    public partial class Background
    {
        internal class Cloud : BackgroundObject
        {
            private int id;
            public Cloud(int displayWidth, int displayHeight, int layer)
            {
                Random random = new Random();
                this.IsVisible = true;
                this.originX = displayWidth + random.Next(500);
                this.originY = random.Next(8, displayHeight - 5);
                this.id = random.Next(1, 4);
                this.layer = layer;
                this.DrawPoints = Display.ConvertSpriteToDrawPoints(@$"{Resource.Sprites}\Cloud{id}.txt", originX, originY);
            }

            public override void MoveLeft()
            {
                if (this.DrawPoints[DrawPoints.Count - 1].X >= 0)
                {
                    originX -= layer;
                    this.DrawPoints = Display.ConvertSpriteToDrawPoints(@$"{Resource.Sprites}\Cloud{id}.txt", originX, originY);
                }
                else
                {
                    this.IsVisible = false;
                }
            }
        }
    }

}