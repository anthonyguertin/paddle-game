using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoGamePaddleBoard.Source
{
    public abstract class Sprite
    {
        public Vector2 Position;
        public Texture2D Texture;
        public float Speed;

        public virtual void CheckBounds()
        {

        }

        public virtual void RegisterMovement(GameTime gameTime, Player player, Ball ball)
        {
/*            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
            {
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }*/
        }
    }
}
