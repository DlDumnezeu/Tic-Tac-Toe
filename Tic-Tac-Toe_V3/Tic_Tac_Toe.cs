using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tic_Tac_Toe_V3
{
    public partial class Tic_Tac_Toe : Form
    {
        private Bitmap[] aX = new Bitmap[16];
        private Bitmap[] aO = new Bitmap[16];
        private int picCounter;
        private Timer tX = new Timer();
        private Timer tO = new Timer();
        private int currentButton;
        private Bitmap[] picDif = new Bitmap[3];
        private int difCounter;
        private string pvp;
        private string pva;
        private bool pFirst;
        private char currentPlayerSign;
        private bool isPva;


        private static Game game;

        private void initializeAnimation()
        {
            aX[0] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X0);
            aX[1] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X1);
            aX[2] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X2);
            aX[3] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X3);
            aX[4] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X4);
            aX[5] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X5);
            aX[6] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X6);
            aX[7] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X7);
            aX[8] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X8);
            aX[9] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X9);
            aX[10] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X10);
            aX[11] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X11);
            aX[12] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X12);
            aX[13] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X13);
            aX[14] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X14);
            aX[15] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.X15);

            aO[0] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O0);
            aO[1] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O1);
            aO[2] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O2);
            aO[3] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O3);
            aO[4] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O4);
            aO[5] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O5);
            aO[6] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O6);
            aO[7] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O7);
            aO[8] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O8);
            aO[9] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O9);
            aO[10] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O10);
            aO[11] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O11);
            aO[12] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O12);
            aO[13] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O13);
            aO[14] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O14);
            aO[15] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.O15);
        }
        public Tic_Tac_Toe()
        {
            game = new Game('X', 'O');
            isPva = true;
            difCounter = 0;
            picCounter = 0;
            InitializeComponent();
            initializeAnimation();
            tX.Interval = 20;
            tX.Tick += new EventHandler(onTickX);
            tO.Interval = 20;
            tO.Tick += new EventHandler(onTickO);



            picDif[0] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.Dif1);
            picDif[1] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.Dif2);
            picDif[2] = new Bitmap(Tic_Tac_Toe_V3.Properties.Resources.Dif3);

            difPic.Image = picDif[0];
            pvp = "Player VS Player";
            pva = "Player VS Ai";
            pFirst = true;
            currentPlayerSign = 'X';
        }

        private void Reset()
        {
            if (labelGameMode.Text == pva)
            {
                isPva = true;
                SignBtn.Text = "Change Your Sign";
                if (pFirst) game = new Game('X', 'O');
                else
                {
                    game = new Game('O', 'X');
                    if (difCounter == 0)
                    {
                        currentButton = game.aiLvl1();
                    }
                    else if (difCounter == 1)
                    {
                        currentButton = game.aiLvl2();
                    }
                    else
                    {
                        currentButton = game.aiLvl3();
                    }
                    tX.Start();
                }
            }
            else
            {
                isPva = false;
                SignBtn.Text = "Current Player:";
                SignPic1.Image = aX[15];
                game = new Game('X', 'O');
                currentPlayerSign = 'X';
            }
            B1.Image = null;
            B2.Image = null;
            B3.Image = null;
            B4.Image = null;
            B5.Image = null;
            B6.Image = null;
            B7.Image = null;
            B8.Image = null;
            B9.Image = null;
            winPic.Image = null;
            winLabel.Text = null;
        }

        private void onButtonClickX(int button)
        {
            picCounter = 0;
            if (!isPva)
                SignPic1.Image = aO[15];
            if (pFirst || labelGameMode.Text == pvp)
            {
                int aux = button - 1;
                int x = aux / 3;
                int y = aux % 3;
                game.playerMove(x, y, isPva);
                if(labelGameMode.Text == pvp)
                {
                    if (currentPlayerSign == 'X') currentPlayerSign = 'O';
                    else currentPlayerSign = 'X';
                }
                currentButton = button;
            }
            else
            {
                if (difCounter == 0)
                {
                    currentButton = game.aiLvl1();
                }
                else if (difCounter == 1)
                {
                    currentButton = game.aiLvl2();
                }
                else
                {
                    currentButton = game.aiLvl3();
                }
            }
            tX.Start();
            if (game.Ended == true)
            {
                if (game.WinSign == 'X')
                {
                    winPic.Image = aX[15];
                    winLabel.Text = "Wins";
                }
                else if (game.WinSign == 'O')
                {
                    winPic.Image = aO[15];
                    winLabel.Text = "Wins";
                }
                else
                {
                    winLabel.Text = "Draw";
                }
            }
        }

        private void onButtonClickO(int button)
        {
            picCounter = 0;
            if (!isPva)
                SignPic1.Image = aX[15];
            if (!pFirst || labelGameMode.Text == pvp)
            {
                int aux = button - 1;
                int x = aux / 3;
                int y = aux % 3;
                game.playerMove(x, y, isPva);
                if(labelGameMode.Text == pvp)
                {
                    if (currentPlayerSign == 'X') currentPlayerSign = 'O';
                    else currentPlayerSign = 'X';
                }

                currentButton = button;
            }
            else
            {
                if (difCounter == 0)
                {
                    currentButton = game.aiLvl1();
                }
                else if (difCounter == 1)
                {
                    currentButton = game.aiLvl2();
                }
                else
                {
                    currentButton = game.aiLvl3();
                }
            }
            tO.Start();
            if (game.Ended == true)
            {
                if (game.WinSign == 'X')
                {
                    winPic.Image = aX[15];
                    winLabel.Text = "Wins";
                }
                else if (game.WinSign == 'O')
                {
                    winPic.Image = aO[15];
                    winLabel.Text = "Wins";
                }
                else
                {
                    winLabel.Text = "Draw";
                }
            }
        }

        private void onTickX(Object sender, EventArgs e)
        {
            switch (currentButton)
            {
                case 1:
                    B1.Image = aX[++picCounter];
                    break;
                case 2:
                    B2.Image = aX[++picCounter];
                    break;
                case 3:
                    B3.Image = aX[++picCounter];
                    break;
                case 4:
                    B4.Image = aX[++picCounter];
                    break;
                case 5:
                    B5.Image = aX[++picCounter];
                    break;
                case 6:
                    B6.Image = aX[++picCounter];
                    break;
                case 7:
                    B7.Image = aX[++picCounter];
                    break;
                case 8:
                    B8.Image = aX[++picCounter];
                    break;
                case 9:
                    B9.Image = aX[++picCounter];
                    break;
            }
            if (picCounter == 15)
            {
                picCounter = 0;
                tX.Stop();
                tX.Dispose();
                if (!game.Ended && pFirst && labelGameMode.Text == pva)
                {
                    onButtonClickO(1);
                }
            }
        }
        private void onTickO(Object sender, EventArgs e)
        {
            switch (currentButton)
            {
                case 1:
                    B1.Image = aO[++picCounter];
                    break;
                case 2:
                    B2.Image = aO[++picCounter];
                    break;
                case 3:
                    B3.Image = aO[++picCounter];
                    break;
                case 4:
                    B4.Image = aO[++picCounter];
                    break;
                case 5:
                    B5.Image = aO[++picCounter];
                    break;
                case 6:
                    B6.Image = aO[++picCounter];
                    break;
                case 7:
                    B7.Image = aO[++picCounter];
                    break;
                case 8:
                    B8.Image = aO[++picCounter];
                    break;
                case 9:
                    B9.Image = aO[++picCounter];
                    break;
            }
            if (picCounter == 15)
            {
                picCounter = 0;
                tO.Stop();
                tO.Dispose();
                if (!game.Ended && !pFirst && labelGameMode.Text == pva)
                {
                    onButtonClickX(1);
                }
            }
        }

        private void B1_Click(object sender, EventArgs e)
        {
            if (B1.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(1);
                    else onButtonClickO(1);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(1);
                    }
                    else
                    {
                        onButtonClickO(1);
                    }
                }
            }
        }

        private void B2_Click(object sender, EventArgs e)
        {
            if (B2.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(2);
                    else onButtonClickO(2);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(2);
                    }
                    else
                    {
                        onButtonClickO(2);
                    }
                }
            }
        }
        private void B3_Click(object sender, EventArgs e)
        {
            if (B3.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(3);
                    else onButtonClickO(3);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(3);
                    }
                    else
                    {
                        onButtonClickO(3);
                    }
                }

            }
        }

        private void B4_Click(object sender, EventArgs e)
        {
            if (B4.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(4);
                    else onButtonClickO(4);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(4);
                    }
                    else
                    {
                        onButtonClickO(4);
                    }
                }
            }
        }

        private void B5_Click(object sender, EventArgs e)
        {
            if (B5.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(5);
                    else onButtonClickO(5);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(5);
                    }
                    else
                    {
                        onButtonClickO(5);
                    }
                }
            }
        }

        private void B6_Click(object sender, EventArgs e)
        {
            if (B6.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(6);
                    else onButtonClickO(6);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(6);
                    }
                    else
                    {
                        onButtonClickO(6);
                    }
                }
            }
        }

        private void B7_Click(object sender, EventArgs e)
        {
            if (B7.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(7);
                    else onButtonClickO(7);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(7);
                    }
                    else
                    {
                        onButtonClickO(7);
                    }
                }
            }
        }

        private void B8_Click(object sender, EventArgs e)
        {
            if (B8.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(8);
                    else onButtonClickO(8);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(8);
                    }
                    else
                    {
                        onButtonClickO(8);
                    }
                }
            }
        }

        private void B9_Click(object sender, EventArgs e)
        {
            if (B9.Image == null && game.Ended == false && picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    if (pFirst) onButtonClickX(9);
                    else onButtonClickO(9);
                }
                else
                {
                    if (currentPlayerSign == 'X')
                    {
                        onButtonClickX(9);
                    }
                    else
                    {
                        onButtonClickO(9);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isPva)
            {
                if (picCounter == 0)
                {
                    if (difCounter == 2) difCounter = 0;
                    else difCounter++;
                    difPic.Image = picDif[difCounter];
                    Reset();
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (picCounter == 0)
                Reset();
        }

        private void GameModeBtn_Click(object sender, EventArgs e)
        {
            if (picCounter == 0)
            {
                if (labelGameMode.Text == pva)
                {
                    labelGameMode.Text = pvp;
                    Reset();
                }
                else
                {
                    labelGameMode.Text = pva;
                    currentPlayerSign = 'X';
                    Reset();
                }
                SignBtn_Click(sender, e);
            }
        }

        private void SignBtn_Click(object sender, EventArgs e)
        {
            if (picCounter == 0)
            {
                if (isPva)
                {
                    if (currentPlayerSign == 'X')
                    {
                        pFirst = false;
                        currentPlayerSign = 'O';
                        Reset();
                        SignPic1.Image = aO[15];
                    }
                    else
                    {
                        pFirst = true;
                        currentPlayerSign = 'X';
                        Reset();
                        SignPic1.Image = aX[15];
                    }
                }
            }
        }
    }
}
