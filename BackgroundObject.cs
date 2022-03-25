namespace DinoGame
{
    public abstract class BackgroundObject : IDrawable
    {
        internal int originX;
        internal int originY;
        //The higher the layer, the closer to the forground it appears
        internal int layer;

        private List<DrawPoint> drawPoints;
        public List<DrawPoint> DrawPoints { get => drawPoints; internal set => drawPoints = value; }
        public bool IsVisible { get; set; }

        public abstract void MoveLeft();

        public BackgroundObject()
        {
            drawPoints = new List<DrawPoint>();
            layer = 0;
        }
    }

}