namespace DinoGame
{
    public partial class Background : IDrawable
    {
        private int width;
        private int height;

        private List<DrawPoint> drawPoints;
        public List<DrawPoint> DrawPoints => drawPoints;

        private Moon moon;
        private List<Star> stars;
        private List<BackgroundObject> backgroundObjects;

        public bool IsDay;

        private bool WasDay;
        public bool IsVisible { get; set; }

        public Background(int displayWidth, int displayHeight)
        {
            width = displayWidth;
            height = displayHeight;

            drawPoints = new List<DrawPoint>();
            backgroundObjects = new List<BackgroundObject>();

            IsVisible = true;
            IsDay = false;
            WasDay = false;
            stars = new List<Star>();
            moon = new Moon(100, 15, 1);

            generateBackgroundObjects();

            foreach (IDrawable drawable in backgroundObjects)
            {
                this.drawPoints.AddRange(drawable.DrawPoints);
            }
        }

        private void generateBackgroundObjects()
        {
            if (!IsDay)
            {
                stars.Clear();
                for (int i = 0; i < 80; i++)
                    stars.Add(new Star(this.width, this.height, 1));
                backgroundObjects.AddRange(stars);

                moon = new Moon(100, 15, 1);
                backgroundObjects.Insert(0, moon);

            }

            for (int i = 0; i < 10; i++)
                backgroundObjects.Add(new Cloud(this.width, this.height, 2));
        }

        public void MoveLeft()
        {
            if (WasDay && !IsDay)
            {
                generateBackgroundObjects();
            }
            if (IsDay)
            {
                moon.IsVisible = false;
                foreach (var star in stars)
                {
                    if (star.IsVisible)
                        star.IsVisible = false;
                }
            }
            else
            {
                moon.IsVisible = true;
                foreach (var star in stars)
                {
                    if (!star.IsVisible)
                        star.IsVisible = true;
                }
            }

            backgroundObjects = checkVisiblity(backgroundObjects);

            if (backgroundObjects.Count < 10)
                generateBackgroundObjects();

            foreach (var obj in backgroundObjects)
                obj.MoveLeft();

            this.DrawPoints.Clear();
            foreach (IDrawable drawable in backgroundObjects)
            {
                this.DrawPoints.AddRange(drawable.DrawPoints);
            }

            WasDay = IsDay;
        }

        private static List<BackgroundObject> checkVisiblity(List<BackgroundObject> objects)
        {
            List<BackgroundObject> visibles = new List<BackgroundObject>();
            foreach (BackgroundObject obj in objects)
            {
                if (obj.IsVisible)
                {
                    visibles.Add(obj);
                }
            }
            return visibles;
        }
    }

}