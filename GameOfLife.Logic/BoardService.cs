using GameOfLife.Logic.Interfaces;

namespace GameOfLife.Logic;
/// <summary>
/// Represents a service for generating, drawing and updating a Game of life board.
/// </summary>
public class BoardService : IBoardService
{
    private double _probability = 0.8;
    private Random _random = new Random();
    private bool[,] _board;
    private int _numOfIterations = 0;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    /// <summary>
    /// Generates a random Game of life board with specific dimensions.
    /// </summary>
    /// <param name="boardHeight">The height of the board.</param>
    /// <param name="boardLenght">The length of the board.</param>
    public void GenerateRandomBoard(int boardHeight, int boardLenght)
    {
        _board = new bool[boardHeight, boardLenght];

        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardLenght; j++)
            {
                double randomVal = _random.NextDouble();
                _board[i, j] = randomVal > _probability;
            }
        }
    }

    public void StartBackgroundMainLoop()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() => StartMainLoop(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
    }

    public void StopBackgroundMainLoop()
    {
        _cancellationTokenSource?.Cancel();
    }

    private async Task StartMainLoop(CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000, cancellationToken);
            UpdateBoard();
        }
    }

    /// <summary>
    /// Updates board following the rules of Game of life.
    /// </summary>
    public void UpdateBoard()
    {
        bool[,] updatedBoard = new bool[_board.GetLength(0), _board.GetLength(1)];

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                updatedBoard[i, j] = CalculateCell(_board, i, j);
            }
        }
        _board = updatedBoard;
        _numOfIterations++;
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
    /// <returns>Number of alive cells on the current board.</returns>
    public int CalculateAliveCells()
    {
        int count = 0;

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (_board[i, j])
                {
                    count++;
                }
            }
        }
        return count;
    }

    /// <summary>
    /// Draws Game of life board on the console.
    /// </summary>
    public void DrawBoard()
    {
        var board = _board;

        Console.WriteLine($"Number of iterations is {_numOfIterations}.");
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write(board[i, j] ? '*' : '-');
            }
            Console.WriteLine();
        }
    }
}
