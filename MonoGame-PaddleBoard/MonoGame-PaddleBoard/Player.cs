using MonoGamePaddleBoard.Source;

namespace MonoGamePaddleBoard
{
    public class Player
    {

        public int Score { get; set; }
        public string Name { get; set; }
        public int Controller { get; set; }
        public Paddle Paddle { get; set; }
        public Player(Paddle paddle, int controller = -1)
        {
            Controller = controller;

            Paddle = paddle;
        }
    }
}
