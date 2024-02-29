using GameOfLife.Logic;
using GameOfLife.Logic.Interfaces;
using GameOfLife.UI;
using GameOfLife.UI.Interfaces;

while (true)
{
    Console.WriteLine("Welcome to the game of life!\nPlease choose what you want to do next by entering 1, 2 or 3.");
    Console.WriteLine("1. Start a new game.");
    Console.WriteLine("2. Load game from file.");
    Console.WriteLine("3. Quit the application.");
    string? option = Console.ReadLine();

    switch (option)
    {
        case "1":
            NewGame();
            break;
        case "2":
            break;
        case "3":
            return;
        default:
            Console.WriteLine("Please choose between 1, 2 or 3.");
            break;
    }
}

void NewGame()
{
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

        IGameLogicService logic = new GameLogicService();
        var board = logic.GenerateRandomBoard(boardHeight, boardLenght);
        IDrawingService drawing = new DrawingService();
        int numOfIterations = 0;
        int numOfCells = 0;

        Console.SetCursorPosition(0, boardHeight + 2);
        drawing.DrawBoard(board);
        Thread.Sleep(1000);
        board = logic.UpdateBoard(board);
        numOfIterations++;
        numOfCells = logic.CalculateAliveCells(board);
        Console.WriteLine($"Number of iterations is {numOfIterations}. \nCurrent number of alive cells is {numOfCells}.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

