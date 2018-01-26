namespace Tick_Tack_Toe
{
    public class Game
    {
        public const int GAME_WIDTH_FIELD = 5;
        public const int GAME_HEIGTH_FIELD = 5;
        public int countStroke = 1;

        public enum CellStateCondition
        {
            Zero,
            Cross,
            Empty
        }

        public CellStateCondition[,] GameField = new CellStateCondition[GAME_HEIGTH_FIELD, GAME_WIDTH_FIELD];

        public bool GameIsWin
        {
            get
            {
                return CheckWinOrNotEnded(CellStateCondition.Zero, GameField) ||
                       CheckWinOrNotEnded(CellStateCondition.Cross, GameField);
            }
        }

        public void GameFieldClean()
        {
            for (int i = 0; i < GAME_HEIGTH_FIELD; i++)
            {
                for (int j = 0; j < GAME_WIDTH_FIELD; j++)
                {
                    GameField[i, j] = CellStateCondition.Empty;
                }
            }
            countStroke = 1;
        }

        public bool CheckWinOrNotEnded(CellStateCondition playerMove, CellStateCondition[,] GameField)
        {
            if (CheckWinVertical(playerMove, GameField) || CheckWinHorizontal(playerMove, GameField) ||
                CheckWinFirstDiagonal(playerMove, GameField) || CheckWinSecondDiagonal(playerMove, GameField))
            {
                return true;
            }
            return false;
        }

        private bool CheckWinHorizontal(CellStateCondition playerMove, CellStateCondition[,] GameField)
        {
            int countForHorizontalCheck = 0;
            int slipVertical = 0;
            while (slipVertical < GAME_WIDTH_FIELD)
            {
                for (int i = 0; i < GAME_WIDTH_FIELD; i++)
                {
                    if (GameField[slipVertical, i] == playerMove)
                    {
                        countForHorizontalCheck++;
                    }
                }
                if ((countForHorizontalCheck == GAME_WIDTH_FIELD) || (CheckFieldHaveEmptyCells() == false))
                {
                    return true;
                }
                else
                {
                    slipVertical++;
                    countForHorizontalCheck = 0;
                }
            }
            return false;
        }

        private bool CheckWinVertical(CellStateCondition playerMove, CellStateCondition[,] GameField)
        {
            int slipHorizontal = 0;
            int countForVerticalCheck = 0;
            while (slipHorizontal < GAME_HEIGTH_FIELD)
            {
                for (int i = 0; i < GAME_HEIGTH_FIELD; i++)
                {
                    if (GameField[i, slipHorizontal] == playerMove)
                    {
                        countForVerticalCheck++;
                    }
                }
                if ((countForVerticalCheck == GAME_HEIGTH_FIELD) || (CheckFieldHaveEmptyCells() == false))
                {
                    return true;
                }
                else
                {
                    slipHorizontal++;
                    countForVerticalCheck = 0;
                }
            }
            return false;
        }

        private bool CheckWinFirstDiagonal(CellStateCondition playerMove, CellStateCondition[,] GameField)
        {
            int countFirstDiagonal = 0;
            for (int i = 0; i < GAME_WIDTH_FIELD; i++)
            {
                for (int j = 0; j < GAME_WIDTH_FIELD; j++)
                {
                    if ((i == j) && (GameField[i, j] == playerMove))
                    {
                        countFirstDiagonal++;
                    }
                }
            }
            if ((countFirstDiagonal == GAME_HEIGTH_FIELD) || (CheckFieldHaveEmptyCells() == false))
            {
                return true;
            }
            return false;
        }

        private bool CheckWinSecondDiagonal(CellStateCondition playerMove, CellStateCondition[,] GameField)
        {
            int countSecondDiagonal = 0;
            for (int i = 0; i < GAME_HEIGTH_FIELD; i++)
            {
                if (GameField[i, GAME_WIDTH_FIELD - 1 - i] == playerMove)
                {
                    countSecondDiagonal++;
                }
            }
            if ((countSecondDiagonal == GAME_HEIGTH_FIELD) || (CheckFieldHaveEmptyCells() == false))
            {
                return true;
            }
            return false;
        }

        public bool CheckFieldHaveEmptyCells()
        {
            for (int i = 0; i < GAME_HEIGTH_FIELD; i++)
            {
                for (int j = 0; j < GAME_WIDTH_FIELD; j++)
                {
                    if (GameField[i, j] == CellStateCondition.Empty)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckCellIsNotEmpty(int Row, int Column)
        {
            if (GameField[Row, Column] != CellStateCondition.Empty)
            {
                return true;
            }
            return false;
        }

        public CellStateCondition SequencingStroke()
        {
            if (countStroke % 2 == 0)
            {
                return CellStateCondition.Zero;
            }

            return CellStateCondition.Cross;
        }
    }
}
