using GameOfLife.Logic;
using GameOfLife.Logic.Interfaces;
using GameOfLife.UI.Interfaces;

namespace GameOfLife.UI;
/// <summary>
/// Represents a service for managing user interactions with game backend logic.
/// </summary>
public class GameManager : IGameManager
{
    private readonly IBoardService _logicService;
    private readonly IFileService _fileService;
    private Dictionary<int, BoardService> _boards = new Dictionary<int, BoardService>();
    List<int> _selectedGames = new List<int>();

    public GameManager(IBoardService logicService, IFileService fileService)
    {
        _logicService = logicService;
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

            Console.WriteLine($"{gamesNumber} of games are now ready to start executing.");

            Console.WriteLine($"Please select which ones out of {gamesNumber} you want to display on screen." +
                $"\nEnter the number of maximum 8 games in this manner <1 2 102> and press Enter.");
            string? inputGamesToDisplay = Console.ReadLine();

            if ( string.IsNullOrEmpty(inputGamesToDisplay) )
            {
                throw new Exception("Value provided can not be empty.\n");
            }
          
            string[] inputArray = inputGamesToDisplay.Split(' ');

            //butinai reikia validacijos, kad inputas ateitu is gamesNumber
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (int.TryParse(inputArray[i], out int gameNumber))
                {
                   _selectedGames.Add(gameNumber);
                }
                else
                {
                    Console.WriteLine($"Invalid input at the position {i + 1}.");
                    return;
                }
            }

            for (int i = 0; i < gamesNumber; i++)
            {
                var board = new BoardService();
                board.GenerateRandomBoard(boardHeight, boardLenght);
                board.StartBackgroundMainLoop();
                _boards.Add(i, board);
            }

            MainLoop();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Manages workflow of loading game from a file.
    /// </summary>
    //public void LoadGame()
    //{
    //    try
    //    {
    //        Console.WriteLine("Please provide your file name with .txt extension:");
    //        string? fileName = Console.ReadLine();

    //        if (fileName is null)
    //        {
    //            throw new Exception("File name cannot be null.");
    //        }

    //        bool[,]? board = _fileService.LoadFromFile(fileName.ToLower());

    //        if (board is null)
    //        {
    //            throw new Exception(fileName);
    //        }
    //        MainLoop(board);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //    }
    //}

    /// <summary>
    /// Manages main logic common to both New and Load games.
    /// </summary>
    /// <param name="board">Board to be updated</param>
    private void MainLoop()
    {

        int numOfCells = 0;

        //Console.SetCursorPosition(0, board.GetLength(0) + 2);
        Console.Clear();

        while (true)
        {
            Thread.Sleep(1000);
            for (int i = 0; i < _boards.Count; i++)
            {
                int aliveCells = _boards[i].CalculateAliveCells();
                numOfCells += aliveCells;
            }

            Console.Clear();
            Console.WriteLine($"Number of alive cells in all games is {numOfCells}");
            Console.WriteLine("If you want games to be saved to file, press S.");
            Console.WriteLine("If you want to quit this and return back to main menu, press Q.\n");

            foreach (var item in _selectedGames)
            {
                _boards[item].DrawBoard();
            }


            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                switch (char.ToLower(key.KeyChar))
                {
                    case 's':
                        //_fileService.SaveToFile(board);
                        break;
                    case 'q':
                        Console.Clear();
                        return;
                }
            }


        }
    }

}
