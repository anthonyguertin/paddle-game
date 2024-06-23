using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePaddleBoard.Source
{
    public class Paddle : Sprite
    {
        public Paddle(float speed)
        {
            Speed = speed;
        }
        public override void CheckBounds()
        {

        }

        public override void RegisterMovement(GameTime gameTime, Player player, Ball ball)
        {
            var kstate = Keyboard.GetState();
            var lastKey = kstate.IsKeyDown(Keys.Down);

            if (player.Controller != -1)
            {
                if (kstate.IsKeyDown(Keys.Up))
                {
                    Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (kstate.IsKeyDown(Keys.Down))
                {
                    Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                return;
            }

            // --- AI

            if (ball.Position.Y > Position.Y)
            {
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (ball.Position.Y < Position.Y)
            {
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            } else
            {
                // TODO: Whatever else the AI Paddle should do.
            }
        }
    }
}