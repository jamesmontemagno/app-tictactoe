using System.Collections.Generic;
namespace TicTacToe
{
    public static class GameLogic
    {
        public static bool IsWinningBoard(string[,] board, string X)
        {
            string _ = null;

            var winningCombos = new List<string[,]>
                {

                new[,] {
                    { X, X, X },
                    { _, _, _ },
                    { _, _, _ }
                },

                new[,] {
                    { _, _, _ },
                    { X, X, X },
                    { _, _, _ }
                },

                new[,] {
                    { _, _, _ },
                    { _, _, _ },
                    { X, X, X }
                },

                new[,] {
                    { X, _, _ },
                    { X, _, _ },
                    { X, _, _ }
                },

                new[,] {
                    { _, X, _ },
                    { _, X, _ },
                    { _, X, _ }
                },

                new[,] {
                    { _, _, X },
                    { _, _, X },
                    { _, _, X }
                },


                new[,] {
                    { X, _, _ },
                    { _, X, _ },
                    { _, _, X }
                },


                new[,] {
                    { _, _, X },
                    { _, X, _ },
                    { X, _, _ }
                }
              };

            foreach (var winningCombo in winningCombos)
            {
                var threeToWin = 0;
                for (var x = 0; x < 3; x++)
                    for (var y = 0; y < 3; y++)
                        if (winningCombo[x, y] == X)
                            if (winningCombo[x, y] == board[x, y])
                                threeToWin++;

                if (threeToWin == 3)
                    return true;
            }

            return false;
        }
    }
}
