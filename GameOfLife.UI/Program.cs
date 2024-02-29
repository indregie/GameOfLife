using GameOfLife.Logic;
using GameOfLife.Logic.Interfaces;
using GameOfLife.UI;
using GameOfLife.UI.Interfaces;

IGameLogicService _logic = new GameLogicService();

while (true)
{
    Console.WriteLine("Welcome to the game of life!\nPlease choose what you want to do next by entering 1, 2 or Q and pressing Enter.");
    Console.WriteLine("1. Start a new game.");
    Console.WriteLine("2. Load game from file.");
    Console.WriteLine("Q. Quit the application.");
    string? option = Console.ReadLine();

    switch (option?.ToLower())
    {
        case "1":
            NewGame();
            break;
        case "2":
            Console.WriteLine("Please provide file name in this format: XXXXXXXX.txt");
            string? fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new Exception("File name cannot be null.");
            }
            LoadGame(fileName);
            break;
        case "q":
            return;
        default:
            Console.WriteLine("Please choose between 1, 2 or Q.");
            break;
    }
}

void NewGame()
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

void LoadGame(string fileName)
{
    IFileService fileService = new FileService();
    bool[,] board = fileService.LoadFromFile(fileName);
    MainLoop(board);
}

void MainLoop(bool[,] board)
{
    IDrawingService drawing = new DrawingService();
    int numOfIterations = 0;
    int numOfCells = 0;

    Console.SetCursorPosition(0, board.GetLength(0) + 2);

    while (true)
    {
        drawing.DrawBoard(board);
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
                    FileService fileService = new FileService();
                    fileService.SaveToFile(board);
                    break;
                case 'q':
                    Console.Clear();
                    return;
            }
        }
    }
}
