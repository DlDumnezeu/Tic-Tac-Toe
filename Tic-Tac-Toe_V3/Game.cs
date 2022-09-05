using System;

namespace Tic_Tac_Toe_V3
{
    public class Game
    {
        private static char[,] grid;
        private static Players players;
        private static char winSign;
        private static int moves;
        private static bool ended;

        public bool Ended { get => ended; set => ended = value; }
        public char WinSign { get => winSign; set => winSign = value; }

        public Game(char pSign, char aSign)
        {
            players = new Players(pSign, aSign);
            winSign = ' ';
            grid = new char[3, 3];
            moves = 0;
            ended = false;
            initializegrid();
        }

        public void playerMove(int x, int y, bool pva)
        {
            if (pva)
            {
                grid[x, y] = players.Player;
                if (gameOver(checkWin()))
                {
                    ended = true;
                }
            }
            else
            {
                grid[x, y] = players.Player;
                if (gameOver(checkWin()))
                {
                    ended = true;
                }
                char aux = players.Player;
                players.Player = players.Ai;
                players.Ai = aux;

            }
        }

        public int aiLvl2()
        {
            int auxI = 0;
            int auxJ = 0;
            bool stopPlayer = false; ;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[i, j] == ' ')
                    {
                        grid[i, j] = players.Ai;
                        if (gameOver(checkWin()))
                        {
                            ended = true;
                            return i * 3 + j + 1;
                        }
                        else moves--;
                        grid[i, j] = players.Player;
                        moves--;
                        if (gameOver(checkWin()))
                        {
                            stopPlayer = true;
                            auxI = i;
                            auxJ = j;
                            WinSign = ' ';
                        }
                        grid[i, j] = ' ';
                    }
                }
            }
            if (stopPlayer)
            {
                moves++;
                grid[auxI, auxJ] = players.Ai;
                return auxI * 3 + auxJ + 1;
            }
            else
            {
                return aiLvl1();
            }
        }

        private static void initializegrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid[i, j] = players.None;
                }
            }
        }

        public int aiLvl1()
        {
            Random rnd = new Random();
            int x = rnd.Next(3);
            int y = rnd.Next(3);
            while (!grid[x, y].Equals(' '))
            {
                x = rnd.Next(3);
                y = rnd.Next(3);
            }
            grid[x, y] = players.Ai;
            if (gameOver(checkWin()))
            {
                ended = true;
            }
            return x * 3 + y + 1;
        }

        private static WinLine checkWin()
        {
            WinLine wl = new WinLine();
            if (grid[0, 0] == grid[1, 1] && grid[0, 0] == grid[2, 2] && grid[0, 0] != ' ')
            {
                winSign = grid[0, 0];
                wl.Diag = 1;
                return wl;
            }
            if (grid[0, 2] == grid[1, 1] && grid[0, 2] == grid[2, 0] && grid[0, 2] != ' ')
            {
                winSign = grid[0, 2];
                wl.Diag = 2;
                return wl;
            }

            for (int i = 0; i < 3; i++)
            {
                if (grid[i, 0] == grid[i, 1] && grid[i, 0] == grid[i, 2] && grid[i, 0] != ' ')
                {
                    winSign = grid[i, 0];
                    wl.Row = i + 1;
                    return wl;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (grid[0, i] == grid[1, i] && grid[0, i] == grid[2, i] && grid[0, i] != ' ')
                {
                    winSign = grid[0, i];
                    wl.Column = i + 1;
                    return wl;
                }
            }

            return wl;
        }

        private static bool gameOver(WinLine wl)
        {
            moves++;
            if (wl.Diag != 0) return true;
            if (wl.Column != 0) return true;
            if (wl.Row != 0) return true;
            if (moves == 9) return true;
            return false;
        }

        public int aiLvl3()
        {
            int bestScore = int.MinValue;
            int auxI = 0;
            int auxJ = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[i, j] == ' ')
                    {
                        grid[i, j] = players.Ai;
                        int auxM = moves;
                        int score = miniMax(grid, 0, false);
                        moves = auxM;
                        grid[i, j] = ' ';
                        if (score > bestScore)
                        {
                            bestScore = score;
                            auxI = i;
                            auxJ = j;
                        }
                    }
                }
            }
            grid[auxI, auxJ] = players.Ai;
            if (gameOver(checkWin()))
            {
                ended = true;
            }
            return auxI * 3 + auxJ + 1;
        }

        private static int miniMax(char[,] grid, int depth, bool isAi)
        {
            int bestscore = int.MinValue;
            if (gameOver(checkWin()))
            {
                if (winSign == players.Ai)
                {
                    winSign = ' ';
                    return 1;
                }
                else if (winSign == players.Player)
                {
                    winSign = ' ';
                    return -1;
                }
                else return 0;
            }
            int score = int.MinValue;
            if (isAi)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (grid[i, j] == ' ')
                        {
                            grid[i, j] = players.Ai;
                            int auxM = moves;
                            score = miniMax(grid, depth + 1, false);
                            moves = auxM;
                            grid[i, j] = ' ';
                            if (score > bestscore)
                            {
                                bestscore = score;
                            }
                        }
                    }
                }
            }
            else
            {
                bestscore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (grid[i, j] == ' ')
                        {
                            grid[i, j] = players.Player;
                            int auxM = moves;
                            score = miniMax(grid, depth + 1, true);
                            moves = auxM;
                            grid[i, j] = ' ';
                            if (score < bestscore)
                            {
                                bestscore = score;
                            }
                        }
                    }
                }

            }
            return bestscore;
        }


    }
}
