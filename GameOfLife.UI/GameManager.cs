using GameOfLife.Logic.Interfaces;
using GameOfLife.UI.Interfaces;

namespace GameOfLife.UI;
/// <summary>
/// Represents a service for managing user interactions with game backend logic.
/// </summary>
public class GameManager : IGameManager
{
    private readonly IGameLogicService _logicService;
    private readonly IDrawingService _drawingService;
    private readonly IFileService _fileService;
    private Dictionary<int, bool[,]> _boards = new Dictionary<int, bool[,]>();  

    public GameManager(IGameLogicService logicService, IDrawingService drawingService, IFileService fileService)
    {
        _logicService = logicService;
        _drawingService = drawingService;
        _fileService = fileService;
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

            Console.WriteLine("Please write down how many games do you want to run (up to 1000):");
            string? inputGamesNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(inputBoardHeight) || string.IsNullOrEmpty(inputBoardLenght) 
                || string.IsNullOrEmpty(inputGamesNumber))
            {
                throw new Exception("Value provided can not be empty.\n");
            }

            int boardHeight = int.Parse(inputBoardHeight!);
            int boardLenght = int.Parse(inputBoardLenght!);
            int gamesNumber = int.Parse(inputGamesNumber!);

            //Generate random boards
            for (int i = 0; i < gamesNumber; i++)
            {
                bool[,] board = _logicService.GenerateRandomBoard(boardHeight, boardLenght);
                _boards.Add(i, board);
            }

            Console.WriteLine($"{gamesNumber} of games are now ready to start executing.");

            Console.WriteLine($"Please select which ones out of {gamesNumber} you want to display on screen." +
                $"\nEnter the number of maximum 8 games in this manner <1 2 102> and press Enter.");
            string? inputGamesToDisplay = Console.ReadLine();
            //MainLoop(board);
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
            Console.WriteLine("Please provide your file name with .txt extension:");
            string? fileName = Console.ReadLine();

            if (fileName is null)
            {
                throw new Exception("File name cannot be null.");
            }

            bool[,]? board = _fileService.LoadFromFile(fileName.ToLower());

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
        Console.Clear();

        while (true)
        {
            _drawingService.DrawBoard(board);
            Thread.Sleep(1000);
            board = _logicService.UpdateBoard(board);
            numOfIterations++;
            numOfCells = _logicService.CalculateAliveCells(board);

            Console.WriteLine($"Number of iterations is {numOfIterations}. \nCurrent number of alive cells is {numOfCells}.\n");
            Console.WriteLine("If you want this game to be saved to file, press S.");
            Console.WriteLine("If you want to quit this game and return back to main menu, press Q.\n");

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                switch (char.ToLower(key.KeyChar))
                {
                    case 's':
                        _fileService.SaveToFile(board);
                        break;
                    case 'q':
                        Console.Clear();
                        return;
                }
            }
        }
    }

}
