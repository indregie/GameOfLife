using GameOfLife.Logic;
using GameOfLife.Logic.Interfaces;
using GameOfLife.UI.Interfaces;
using System.IO;

namespace GameOfLife.UI;
/// <summary>
/// Represents a service for managing user interactions with game backend logic.
/// </summary>
public class GameManager : IGameManager
{
    private Dictionary<int, IBoardService> _boards = new Dictionary<int, IBoardService>();
    List<int> _selectedGames = new List<int>();
    private int _numOfCells = 0;
    private int _numberOfGames = 0;

    /// <summary>
    /// Manages workflow when initiating new game is chosen.
    /// </summary>
    public void NewGame()
    {
        _numOfCells = 0; 
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
            int numberOfGames = int.Parse(inputGamesNumber!);

            if (numberOfGames > 1000)
            {
                throw new Exception("You can only run up to 1000 games.\n");
            }
            _numberOfGames = numberOfGames;
            Console.WriteLine($"\n{numberOfGames} of games are now ready to start executing.");           

            for (int i = 1; i <= numberOfGames; i++)
            {
                var board = new BoardService(i);
                board.GenerateRandomBoard(boardHeight, boardLenght);
                board.StartBackgroundMainLoop();
                _boards.Add(i, board);
            }

            SelectGames();

            MainLoop();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void SelectGames()
    {
        Console.WriteLine($"Please select which ones out of {_numberOfGames} you want to display on screen." +
            $"\nEnter the number of maximum 8 games in this manner <1 2 102> and press Enter.");
        string? inputGamesToDisplay = Console.ReadLine();

        if (string.IsNullOrEmpty(inputGamesToDisplay))
        {
            throw new Exception("Value provided can not be empty.\n");
        }

        string[] inputArray = inputGamesToDisplay.Split(' ');

        if (inputArray.Length > 8)
        {
            throw new Exception("You can only select up to 8 games to display.\n");
        }

        foreach (string game in inputArray)
        {
            if (int.TryParse(game, out int gameNumber))
            {
                if (!Enumerable.Range(1, _numberOfGames).Contains(gameNumber))
                {
                    throw new Exception($"Game {game} does not exist. You are running {_numberOfGames} of games.");
                }
                _selectedGames.Add(gameNumber);
            }
            else
            {
                Console.WriteLine($"Invalid input {game}.");
                return;
            }
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
    private void MainLoop()
    {
        Console.Clear();

        while (true)
        {
            try
            {
                Thread.Sleep(1000);
                foreach (var board in _boards.Values)
                {
                    int aliveCells = board.CalculateAliveCells();
                    _numOfCells += aliveCells;
                }

                Console.Clear();
                Console.WriteLine($"Number of alive cells in all games is {_numOfCells}");
                Console.WriteLine("If you want games to be saved to file, press S.");
                Console.WriteLine("If you want to change games iterating on screen, press C.");
                Console.WriteLine("If you want to quit this and return back to main menu, press Q.\n");

                foreach (var boardNumber in _selectedGames)
                {
                    Console.WriteLine($"Board number {boardNumber}.");
                    _boards[boardNumber].DrawBoard();
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                    switch (char.ToLower(key.KeyChar))
                    {
                        case 's':
                            foreach (var board in _boards.Values)
                            {
                                board.SaveToFile();
                            }
                            Console.WriteLine($"Files saved to folder {DateTime.Now.Date} on your desktop.");
                            break;
                        case 'c':
                            Console.Clear();
                            _selectedGames.Clear();
                            SelectGames();
                            break;
                        case 'q':
                            foreach (var boardNumber in _selectedGames)
                            {
                                _boards[boardNumber].StopBackgroundMainLoop();
                            }
                            _boards.Clear();
                            _selectedGames.Clear();
                            Console.Clear();
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
    }
}
