using GameOfLife.Logic;
using GameOfLife.UI;

Console.WriteLine("Welcome to the game of life!\nPlease write down the maximum height of your game board:");
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

    GameLogicService logic = new GameLogicService();
    var board = logic.GenerateRandomBoard(boardHeight, boardLenght);
    DrawingService drawing = new DrawingService();

    while (true)
    {
        drawing.DrawBoard(board);
        Thread.Sleep(1000);
        board = logic.UpdateBoard(board);
    }
}
catch (Exception ex) 
{
    Console.WriteLine(ex.Message);
}
