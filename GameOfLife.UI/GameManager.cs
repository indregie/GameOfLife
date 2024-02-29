using GameOfLife.Logic.Interfaces;
using GameOfLife.UI.Interfaces;

namespace GameOfLife.UI;
/// <summary>
/// Represents a service for managing user interactions with game backend logic.
/// </summary>
public class GameManager : IGameManager
{
    private readonly IGameLogicService _logic;
    private readonly IDrawingService _drawing;
    private readonly IFileService _file;

    public GameManager(IGameLogicService logic, IDrawingService drawing, IFileService file)
    {
        _logic = logic;
        _drawing = drawing;
        _file = file;
    }

    /// <summary>
    /// Manages workflow when initiating new game is chosen.
    /// </summary>
    public void NewGame()
    {
        Console.Clear();
        Console.WriteLine("Please write down the maximum height of your game board:");
        try
        {
            string? inputBoardHeight = Console.ReadLine();

            Console.WriteLine("Please write down the maximum lenght of your game board:");
            string? inputBoardLenght = Console.ReadLine();

            if (string.IsNullOrEmpty(inputBoardHeight) || string.IsNullOrEmpty(inputBoardLenght))
            {
                throw new Exception("Value provided can not be empty.");
            }

            int boardHeight = int.Parse(inputBoardHeight!);
            int boardLenght = int.Parse(inputBoardLenght!);

            Console.WriteLine($"Board: {boardHeight}, {boardLenght}");

            bool[,] board = _logic.GenerateRandomBoard(boardHeight, boardLenght);
            MainLoop(board);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Manages workflow of loading game from a file.
    /// </summary>
    public void LoadGame()
    {
        try
        {
            Console.WriteLine("Please provide file name in this format: XXXXXXXX.txt");
            string? fileName = Console.ReadLine();

            if (fileName is null)
            {
                throw new Exception("File name cannot be null.");
            }

            bool[,]? board = _file.LoadFromFile(fileName.ToLower());

            if (board is null)
            {
                throw new Exception(fileName);
            }
            MainLoop(board);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Manages main logic common to both New and Load games.
    /// </summary>
    /// <param name="board">Board to be updated</param>
    private void MainLoop(bool[,] board)
    {
        int numOfIterations = 0;
        int numOfCells = 0;

        Console.SetCursorPosition(0, board.GetLength(0) + 2);

        while (true)
        {
            _drawing.DrawBoard(board);
            Thread.Sleep(1000);
            board = _logic.UpdateBoard(board);
            numOfIterations++;
            numOfCells = _logic.CalculateAliveCells(board);
            Console.WriteLine($"Number of iterations is {numOfIterations}. \nCurrent number of alive cells is {numOfCells}.\n");
            Console.WriteLine("If you want this game to be saved to file, press S.");
            Console.WriteLine("If you want to quit this game and return back to main menu, press Q.\n");

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                switch (char.ToLower(key.KeyChar))
                {
                    case 's':
                        _file.SaveToFile(board);
                        break;
                    case 'q':
                        Console.Clear();
                        return;
                }
            }
        }
    }

}
