namespace DinoGame
{
    public partial class Background
    {
        internal class Moon : BackgroundObject
        {
            public Moon(int originX, int originY, int layer)
            {

                this.IsVisible = true;
                this.originX = originX;
                this.originY = originY;
                this.layer = layer;
                this.DrawPoints = Display.ConvertSpriteToDrawPoints(@$"{Resource.Sprites}\Moon.txt", originX, originY);
            }

            public override void MoveLeft()
            {
                originX--;
                if (this.DrawPoints[DrawPoints.Count - 1].X >= -2)
                {
                    this.DrawPoints = Display.ConvertSpriteToDrawPoints(@$"{Resource.Sprites}\Moon.txt", originX, originY);
                }
                else
                {
                    this.IsVisible = false;
                }
            }
        }
    }

}