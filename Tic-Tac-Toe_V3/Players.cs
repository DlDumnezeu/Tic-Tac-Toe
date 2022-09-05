namespace Tic_Tac_Toe_V3
{
    public class Players
    {
        private char player;
        private char ai;
        private const char none = ' ';


        public char None { get => none; }
        public char Player { get => player; set => player = value; }
        public char Ai { get => ai; set => ai = value; }

        public Players() { }

        public Players(char player, char ai)
        {
            this.player = player;
            this.ai = ai;
        }
    }
}
