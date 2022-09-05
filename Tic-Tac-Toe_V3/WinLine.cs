namespace Tic_Tac_Toe_V3
{
    public class WinLine
    {
        private int diag;
        private int row;
        private int column;

        public WinLine()
        {
            Diag = 0;
            Row = 0;
            Column = 0;
        }

        public int Diag { get => diag; set => diag = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
    }
}
