using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGamePaddleBoard.Source;
using static MonoGamePaddleBoard.Source.Ball;
using System;
using System.Threading;
using MonoGamePaddleBoard;

namespace MonoGame_PaddleBoard
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Ball Ball;
        private Board Board;
        private Paddle Paddle1;
        private Paddle Paddle2;
        private Player Player1;
        private Player Player2;
        private SpriteFont SpriteFont1;
        private bool Scored { get; set; }
        private bool P1Scored { get; set; }
        private bool P2Scored { get; set; }
        private Vector2 TextSize;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Ball = new Ball(150f);
            Ball.Position = new Vector2(
                _graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);

            Paddle1 = new Paddle(100f);
            Paddle1.Position = new Vector2(
                _graphics.PreferredBackBufferWidth / 8,
                _graphics.PreferredBackBufferHeight / 2);

            Paddle2 = new Paddle(100f);
            Paddle2.Position = new Vector2(
                _graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 8,
                _graphics.PreferredBackBufferHeight / 2
                );

            Player1 = new Player(Paddle1, 1);
            Player2 = new Player(Paddle2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Ball.Texture = Content.Load<Texture2D>("ball");
            Paddle1.Texture = Content.Load<Texture2D>("paddle-red");
            Paddle2.Texture = Content.Load<Texture2D>("paddle-green");
            //106 706
            SpriteFont1 = Content.Load<SpriteFont>("font-ariel");
            TextSize = SpriteFont1.MeasureString("Score    ");
            TextSize = new Vector2(TextSize.X / 2, TextSize.Y / 2);

            Board = new Board(
                Ball.Texture.Height / 2,
                _graphics.PreferredBackBufferWidth - Ball.Texture.Width / 2,
                _graphics.PreferredBackBufferHeight - Ball.Texture.Height / 2,
                Ball.Texture.Width / 2
            );
            Ball.GenerateRandomDirection(Board, Paddle1);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            var kstate = Keyboard.GetState();

            Ball.RegisterMovement(gameTime, Player1, Ball);
            //Ball.CheckBounds(_graphics, gameTime);

            Paddle1.RegisterMovement(gameTime, Player1, Ball);
            Paddle2.RegisterMovement(gameTime, Player2, Ball);


            CheckCollision(Ball);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

/*            BallPosition = new Vector2 (
                _graphics.PreferredBackBufferWidth/2 - BallTexture.Width,
                _graphics.PreferredBackBufferHeight/2 - BallTexture.Height);*/
            //Castle Defense
            
            _spriteBatch.Begin();
            _spriteBatch.DrawString(
                SpriteFont1,
                $"Score {Player1.Score}:{Player2.Score}",
                new Vector2(_graphics.PreferredBackBufferWidth / 2, TextSize.Y),
                Color.Red
            );
            _spriteBatch.Draw(
                Ball.Texture,
                Ball.Position,
                null,
                Color.White,
                0f,
                new Vector2(
                    Ball.Texture.Width / 2,
                    Ball.Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );

            _spriteBatch.Draw(
                Paddle1.Texture,
                Paddle1.Position,
                null,
                Color.White,
                0f,
                new Vector2(Paddle1.Texture.Width / 2,
                Paddle1.Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );

            _spriteBatch.Draw(
                Paddle2.Texture,
                Paddle2.Position,
                null,
                Color.White,
                0f,
                new Vector2(Paddle2.Texture.Width / 2,
                Paddle2.Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void CheckCollision(Ball ball)
        {
            //ToDO: Ensure the paddle board height falls within the region the ball hits first
            var ballBottom = Ball.Position.Y + Ball.Texture.Height;
            var ballTop = Ball.Position.Y;

            var paddleTop = Paddle1.Position.Y;
            var paddleBottom = Paddle1.Position.Y + Paddle1.Texture.Height;

            var paddleRight = Paddle1.Position.X + Paddle1.Texture.Width;
            // For Y
            if (paddleRight >= ball.Position.X - ball.Texture.Width/2)
            {
                if ((paddleTop < ballTop && ballTop < paddleBottom) || (paddleTop < ballBottom && ballBottom < paddleBottom))
                {
                    //For X
                    ball.Position.X = Paddle1.Position.X + Paddle1.Texture.Width + Ball.Texture.Width / 2;
                    ball.CurrentDirection = ball.GenerateRandomDirection(Board, Paddle1);
                    ball.PaddleCollisionCount++;
                    ball.Speed += 10f;
                }
            }
            var p1Goal = Math.Round(Player1.Paddle.Position.X + Player1.Paddle.Texture.Width, 0, MidpointRounding.AwayFromZero);
            var p2Goal = Math.Round(Player2.Paddle.Position.X, 0, MidpointRounding.AwayFromZero);
            // Passed the paddle

          if (p1Goal < ball.Position.X)
            {
                P1Scored = false;
            }
            if (!P1Scored)
            {
                if (p1Goal > ball.Position.X)
                {
                    Player1.Score++;
                    P1Scored = true;
                }
            }

            if (p2Goal > ball.Position.X) {
                P2Scored = false;
            }
            if (!P2Scored)
            {
                if (p2Goal < ball.Position.X)
                {
                    Player2.Score++;
                    P2Scored = true;
                }
            }
            /*            if (paddleTop >= ballTop && paddleBottom > ballBottom && paddleTop <= ballTop)
                        {
                            if (Paddle1.Position.X < Ball.Position.X &&
                            Ball.Position.X < Paddle1.Position.X + Paddle1.Texture.Width)
                            {
                                ball.Position.X = Paddle1.Position.X + Paddle1.Texture.Width + Ball.Texture.Width/2 ;
                                ball.CurrentDirection = ball.GenerateRandomDirection(Board, Paddle1);
                            }
                        }*/
            if (Ball.Position.X < Board.LeftWall)
            {
                // Left Wall
                ball.Position.X = Ball.Texture.Width / 2;
                ball.CurrentDirection = ball.GenerateRandomDirection(Board, Paddle1);
            }
            if (Ball.Position.X > Board.RightWall)
            {
                //Right Wall
                ball.Position.X = _graphics.PreferredBackBufferWidth - Ball.Texture.Width / 2;
                Ball.CurrentDirection = ball.GenerateRandomDirection(Board, Paddle1);
            }
            if (Ball.Position.Y > Board.BottomWall)
            {
                // Bottom Wall
                ball.Position.Y = _graphics.PreferredBackBufferHeight - Ball.Texture.Height / 2;
                Ball.CurrentDirection = ball.GenerateRandomDirection(Board, Paddle1);
            }

            if (Ball.Position.Y < Board.TopWall)
            {
                // Top Wall
                Ball.Position.Y = Ball.Texture.Height / 2;
                Ball.CurrentDirection = ball.GenerateRandomDirection(Board, Paddle1);
            }

        }
    }
}
