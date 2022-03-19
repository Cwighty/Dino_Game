namespace DinoGame
{
    public interface IDrawable
    {
        List<DrawPoint> DrawPoints
        {
            get;
        }
        public bool IsVisible { get; set; }
    }
    public struct DrawPoint
    {
        public int X;
        public int Y;
        public char Character;
        public DrawPoint(int x, int y, char character)
        {
            X = x;
            Y = y;
            Character = character;
        }
    }
}
