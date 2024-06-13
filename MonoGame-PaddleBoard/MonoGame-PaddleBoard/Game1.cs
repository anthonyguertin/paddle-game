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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Player1 = new Player(1);
            Player2 = new Player();

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Ball.Texture = Content.Load<Texture2D>("ball");
            Paddle1.Texture = Content.Load<Texture2D>("paddle");
            Paddle2.Texture = Content.Load<Texture2D>("paddle-green");
            Board = new Board(
                Ball.Texture.Height / 2,
                _graphics.PreferredBackBufferWidth - Ball.Texture.Width / 2,
                _graphics.PreferredBackBufferHeight - Ball.Texture.Height / 2,
                Ball.Texture.Width / 2
            );
            Ball.GenerateRandomDirection(Board, Paddle1);

        }
        private void CheckCollision(Ball ball)
        {
            //ToDO: Ensure the paddle board height falls within the region the ball hits first
            var ballBottom = Ball.Position.Y + Ball.Texture.Height;
            var ballTop = Ball.Position.Y;

            var paddleTop = Paddle1.Position.Y;
            var paddleBottom = Paddle1.Position.Y + Paddle1.Texture.Height;

            var paddleRight = Paddle1.Position.X + Paddle1.Texture.Width;
            
            if (paddleTop < ballTop && ballTop < paddleBottom)
            {
                // For Y
                if (paddleRight >= ball.Position.X)
                {
                    //For X
                    ball.Position.X = Paddle1.Position.X + Paddle1.Texture.Width + Ball.Texture.Width / 2;
                    ball.CurrentDirection = ball.GenerateRandomDirection(Board, Paddle1);
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
        private bool Collision { get; set; }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            var kstate = Keyboard.GetState();

            Ball.RegisterMovement(gameTime);
            //Ball.CheckBounds(_graphics, gameTime);

            Paddle1.RegisterMovement(gameTime, Player1, Ball);
            Paddle2.RegisterMovement(gameTime, Player2, Ball);


            CheckCollision(Ball);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

/*            BallPosition = new Vector2 (
                _graphics.PreferredBackBufferWidth/2 - BallTexture.Width,
                _graphics.PreferredBackBufferHeight/2 - BallTexture.Height);*/

            _spriteBatch.Begin();
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
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
