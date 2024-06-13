using Microsoft.Xna.Framework;

namespace MonoGamePaddleBoard.Source
{
    public class Board
    {
        public enum Wall { Top, Right, Bottom, Left }
        // TODO: Not actually the wall locations
        public int TopWall { get; set; }
        public int RightWall { get; set; }
        public int BottomWall { get; set; }
        public int LeftWall { get; set; }
        public Board(int topWall, int rightWall, int bottomWall, int leftWall)
        {
            TopWall = topWall;
            RightWall = rightWall;
            BottomWall = bottomWall;
            LeftWall = leftWall;
        }
        public void CheckBounds(GraphicsDeviceManager graphics, GameTime gameTime)
        {

        }
    }
}
