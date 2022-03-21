namespace DinoGame
{
    public class Dino : CollisionObject
    {
        public Dino(int x, int y, int height, int width) : base(x, y, height, width)
        {
            this.IsVisible = true;
        }
        public char Head { get; set; }
        public char Body { get; set; }
        public bool isInAir { get; set; }


        public void Jump()
        {
            //at spacebar or up arrowkey press:

            isInAir = true;

            //update head and body postions for jump period

            isInAir = false;
        }
    }
}
