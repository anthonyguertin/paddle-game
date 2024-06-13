namespace MonoGamePaddleBoard
{
    public class Player
    {

        public int Score { get; set; }
        public string Name { get; set; }
        public int Controller { get; set; }

        public Player(int controller = -1) 
        {
            Controller = controller;
        }
    }
}
