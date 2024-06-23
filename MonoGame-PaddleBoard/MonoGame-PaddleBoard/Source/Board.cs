using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePaddleBoard.Source
{
    public class Board
    {
        public enum Wall { Top, Right, Bottom, Left, None }
        // TODO: Not actually the wall locations
        public int TopWall { get; set; }
        public int RightWall { get; set; }
        public int BottomWall { get; set; }
        public int LeftWall { get; set; }

        private static int TextHeight = 5;
        public Board(int topWall, int rightWall, int bottomWall, int leftWall)
        {
            TopWall = topWall;
            RightWall = rightWall;
            BottomWall = bottomWall;
            LeftWall = leftWall;
        }
        public void CheckBounds(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            //graphics.GraphicsDevice
        }

        public void RegisterMouse(Vector2 settings, Vector2 settingsLength)
        {
            var mouseState = Mouse.GetState();
            var beginText = settings.X;
            var endText = settingsLength.X + beginText;
            var textHeight = settings.Y + TextHeight;

            if (mouseState.X >= beginText && mouseState.X <= endText)
            {
                if (mouseState.Y <= settings.Y && settings.Y >= TextHeight)
                {
                    
                }
            }
        }
    }
}
