using GameOfLife;

Console.WriteLine("Welcome to the game of life!\nPlease write down the maximum height of the desk input:");
try
{
    string? inputBoardHeight = Console.ReadLine();

    Console.WriteLine("Please write down the maximum lenght of the desk input:");
    string? inputBoardLenght = Console.ReadLine();

    if (string.IsNullOrEmpty(inputBoardHeight) || string.IsNullOrEmpty(inputBoardLenght))
    {
        throw new Exception("Please provide valid input.");
    }

    int boardHeight = int.Parse(inputBoardHeight!);
    int boardLenght = int.Parse(inputBoardLenght!);

    Console.WriteLine($"Board: {boardHeight}, {boardLenght}");

    DrawingService drawing = new DrawingService();
    var board = drawing.GenerateRandomBoard(boardHeight, boardLenght);

    while (true)
    {
        drawing.DrawBoard(board);
        Thread.Sleep(1000);
        board = drawing.UpdateBoard(board);
    }
}
catch (Exception ex) 
{
    Console.WriteLine(ex.Message);
}
