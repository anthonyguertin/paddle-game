using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace MonoGamePaddleBoard.Source
{
    public class Ball : Sprite
    {
        public int PaddleCollisionCount { get; set; }
        public enum Direction {
            Up = 0,
            UpRight = 1,
            Right = 2,
            RightDown = 3,
            Down = 4,
            DownLeft = 5,
            Left = 6,
            LeftUp = 7,
            All =-1
        }

        public Direction CurrentDirection { get; set; }

        public bool Collision;
        public Ball(float speed)
        {
            Speed = speed;
        }

        private Direction GetRicochet(int r, Board.Wall wall)
        {
            switch (wall)
            {
                case Board.Wall.Top:
                    if (r == 0)
                    {
                        return Direction.RightDown;
                    }
                    else if (r == 1)
                    {
                        return Direction.DownLeft;
                    }
                    break;
                case Board.Wall.Right:
                    if (r == 0)
                    {
                        return Direction.LeftUp;
                    }
                    else if (r == 1)
                    {
                        return Direction.Left;
                    }
                    else if (r == 2)
                    {
                        return Direction.DownLeft;
                    }
                    break;
                case Board.Wall.Bottom:
                    if (r == 0)
                    {
                        return Direction.LeftUp;
                    }
                    else if (r == 1)
                    {
                        return Direction.UpRight;
                    }
                    break;

                case Board.Wall.Left:
                    if (r == 0)
                    {
                        return Direction.UpRight;
                    }
                    else if (r == 1)
                    {
                        return Direction.Right;
                    }
                    else if (r == 2)
                    {
                        return Direction.RightDown;
                    }

                    break;
                case Board.Wall.None:
                    if (r == 0)
                    {
                        return Direction.UpRight;
                    }
                    else if (r == 1)
                    {
                        return Direction.Right;
                    }
                    else if (r == 2)
                    {
                        return Direction.RightDown;
                    }
                    else
                    {
                        return Direction.All;
                    }

                    break;
            }
            return 0;
        }

        public Direction GenerateRandomDirection(Board board, Paddle paddle)
        {
            var random = new Random();
            var result = Direction.All;
            if (Position.X == board.LeftWall)
            {
                var n = random.Next(0, 3);
                var r = GetRicochet(n, Board.Wall.Left);
                result = r;

                return r;
            }
            if (Position.X == board.RightWall)
            {
                var n = random.Next(0, 3);
                var r = GetRicochet(n, Board.Wall.Right);
                result = r;

                return r;
            }

            if (Position.Y == board.TopWall)
            {
                var n = random.Next(0, 2);
                var r = GetRicochet(n, Board.Wall.Top);
                result = r;

                return r;
            }

            if (Position.Y == board.BottomWall)
            {
                var n = random.Next(0, 2);
                var r = GetRicochet(n, Board.Wall.Bottom);
                result = r;

                return r;
            }

            if (Position.X == paddle.Position.X + paddle.Texture.Width + Texture.Width / 2)
            {
                var n = random.Next(0, 3);
                var r = GetRicochet(n, Board.Wall.Left);
                result = r;

                return r;
            }

            if (Position.X == paddle.Position.X - paddle.Texture.Width - Texture.Width / 2)
            {
                var n = random.Next(0, 3);
                var r = GetRicochet(n, Board.Wall.Right);
                result = r;

                return r;
            }
/* TODO else
            var all = random.Next(0, 4);
            var ric = GetRicochet(all, Board.Wall.Right);
            result = ric;
*/
                return result;
        }

        public override void RegisterMovement(GameTime gameTime, Player player, Ball ball)
        {
            
            //GenerateRandomDirection();
            if (CurrentDirection == Direction.Up)
            {
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(CurrentDirection == Direction.Right)
            {
                Position.X += Speed *(float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (CurrentDirection == Direction.Down)
            {
                Position.Y += Speed *(float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (CurrentDirection == Direction.Left)
            {
                Position.X -= Speed* (float)gameTime.ElapsedGameTime.TotalSeconds;
            }



            if (CurrentDirection == Direction.UpRight)
            {
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (CurrentDirection == Direction.RightDown)
            {
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (CurrentDirection == Direction.DownLeft)
            {
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (CurrentDirection == Direction.LeftUp)
            {
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }
        public void CheckBounds(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            if (Position.X > graphics.PreferredBackBufferWidth - Texture.Width / 2)
            {
                Position.X = graphics.PreferredBackBufferWidth - Texture.Width / 2;
            }
            if (Position.X < 0 + Texture.Width / 2)
            {
                Position.X = Texture.Width / 2;
            }
            if (Position.Y > graphics.PreferredBackBufferHeight - Texture.Height / 2)
            {
                Position.Y = graphics.PreferredBackBufferHeight - Texture.Height / 2;
            }
            if (Position.Y < 0 + Texture.Height / 2)
            {
                Position.Y = Texture.Height / 2;
            }
        }
    }
}
