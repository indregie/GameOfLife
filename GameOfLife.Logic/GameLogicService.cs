using GameOfLife.Logic.Interfaces;

namespace GameOfLife.Logic;
/// <summary>
/// Represents a service for generating, drawing and updating a Game of life board.
/// </summary>
public class GameLogicService : IGameLogicService
{
    private double _probability = 0.8;
    private Random _random = new Random();

    /// <summary>
    /// Generates a random Game of life board with specific dimensions.
    /// </summary>
    /// <param name="boardHeight">The height of the board.</param>
    /// <param name="boardLenght">The length of the board.</param>
    /// <returns>Randomly generated initial Game of life board.</returns>
    public bool[,] GenerateRandomBoard(int boardHeight, int boardLenght)
    {
        bool[,] board = new bool[boardHeight, boardLenght];

        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardLenght; j++)
            {
                double randomVal = _random.NextDouble();
                board[i, j] = randomVal > _probability;
            }
        }
        return board;
    }

    /// <summary>
    /// Updates board following the rules of Game of life.
    /// </summary>
    /// <param name="board">Board to be updated.</param>
    /// <returns>Updated board</returns>
    public bool[,] UpdateBoard(bool[,] board)
    {
        bool[,] updatedBoard = new bool[board.GetLength(0), board.GetLength(1)];

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                updatedBoard[i, j] = CalculateCell(board, i, j);
            }
        }
        return updatedBoard;
    }

    /// <summary>
    /// Calculates the upcoming state of each cell in given Game of life board.
    /// </summary>
    /// <param name="board">Board to be updated.</param>
    /// <param name="i">X offset of the cell.</param>
    /// <param name="j">Y offset of the cell.</param>
    /// <returns>The next state of each cell in the board (dead or alive)</returns>
    private bool CalculateCell(bool[,] board, int i, int j)
    {
        int numOfNeighbours = CalculateNeighbours(board, i, j);

        if (numOfNeighbours < 2)
        {
            return false;
        }
        if (numOfNeighbours > 3)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Calculates the number of alive cells (of bool value true) surrounding the given cell.
    /// </summary>
    /// <param name="board">Given board of the game.</param>
    /// <param name="i">X offset of the cell.</param>
    /// <param name="j">Y offset of the cell.</param>
    /// <returns>Number of alive cells surrounding given cell.</returns>
    private int CalculateNeighbours(bool[,] board, int i, int j)
    {
        int[] offsetX = { -1, 0, 1, 1, 1, 0, -1, -1 };
        int[] offsetY = { 1, 1, 1, 0, -1, -1, -1, 0 };
        int count = 0;

        for (int k = 0; k < offsetX.Length; k++)
        {
            int x = i + offsetX[k];
            int y = j + offsetY[k];

            if (x < 0 || y < 0)
            {
                continue;
            }

            if (x >= board.GetLength(0) || y >= board.GetLength(1))
            {
                continue;
            }

            bool value = board[x, y];
            count += value ? 1 : 0;
        }
        return count;
    }

    /// <summary>
    /// Calculates the number of alive cells (of bool value true) on given board.
    /// </summary>
    /// <param name="board">Given board of the game.</param>
    /// <returns>Number of alive cells on the current board.</returns>
    public int CalculateAliveCells(bool[,] board)
    {
        int count = 0;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j])
                {
                    count++;
                }
            }
        }
        return count;
    }
}
