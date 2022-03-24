namespace DinoGame
{
    public class Background : IDrawable
    {
        private List<DrawPoint> drawPoints;

        private int backgroundPosition;

        private List<IDrawable> backgroundDrawables;

        private Moon moon;
        private List<Cloud> clouds;

        private int width;
        private int height;

        public Background(int displayWidth, int displayHeight)
        {
            this.drawPoints = new List<DrawPoint>();
            backgroundDrawables = new List<IDrawable>();
            backgroundPosition = 0;
            width = displayWidth;
            height = displayHeight;
            this.IsVisible = true;
            moon = new Moon(100, 15);
            clouds = new List<Cloud>();
            for (int i = 0; i < 10; i++)
                clouds.Add(new Cloud(displayWidth, displayHeight));

            backgroundDrawables.Add(moon);
            backgroundDrawables.AddRange(clouds);

            foreach (IDrawable drawable in backgroundDrawables)
            {
                this.drawPoints.AddRange(drawable.DrawPoints);
            }
        }

        public List<DrawPoint> DrawPoints => drawPoints;


        public bool IsVisible { get; set; }

        public void MoveLeft()
        {
            moon.MoveLeft();
            foreach (Cloud cloud in clouds)
                cloud.MoveLeft();
            this.DrawPoints.Clear();
            foreach (IDrawable drawable in backgroundDrawables)
            {
                this.drawPoints.AddRange(drawable.DrawPoints);
            }
        }

        internal class Moon : IDrawable
        {
            private int originX;
            private int originY;
            private List<DrawPoint> drawPoints;

            public Moon(int originX, int originY)
            {
                this.IsVisible = true;
                this.originX = originX;
                this.originY = originY;
                this.drawPoints = Display.ConvertSpriteToDrawPoints(@"C:\Users\agent\source\repos\Dino_Game\Moon.txt", originX, originY);
            }

            public List<DrawPoint> DrawPoints => drawPoints;

            public bool IsVisible { get; set; }

            public void MoveLeft()
            {   
                if (this.drawPoints[drawPoints.Count-1].X >= 0)
                {
                    originX--;
                    this.drawPoints = Display.ConvertSpriteToDrawPoints(@"C:\Users\agent\source\repos\Dino_Game\Moon.txt", originX, originY);
                }
                else
                {
                    this.IsVisible = false;
                }
            }
        }

        internal class Cloud : IDrawable
        {
            private int originX;
            private int originY;
            private int id;
            private List<DrawPoint> drawPoints;

            public Cloud(int displayWidth, int displayHeight)
            {
                Random random = new Random();
                this.IsVisible = true;
                this.originX = displayWidth + random.Next(100);
                this.originY = random.Next(8, displayHeight - 5);
                this.id = random.Next(1, 4);
                this.drawPoints = Display.ConvertSpriteToDrawPoints(@$"C:\Users\agent\source\repos\Dino_Game\Cloud{id}.txt", originX, originY);
            }

            public List<DrawPoint> DrawPoints => drawPoints;

            public bool IsVisible { get; set; }

            public void MoveLeft()
            {
                if (this.drawPoints[drawPoints.Count-1].X >= 0)
                {
                    originX -= 2;
                    this.drawPoints = Display.ConvertSpriteToDrawPoints(@$"C:\Users\agent\source\repos\Dino_Game\Cloud{id}.txt", originX, originY);
                }
                else
                {
                    this.IsVisible = false;
                }
            }
        }
    }

}
