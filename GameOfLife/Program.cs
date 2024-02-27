
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

    //int[,] board = new int[deskHeight, deskLenght];
    Console.WriteLine($"Board: {boardHeight}, {boardLenght}");

    DrawingService drawing = new DrawingService();
    drawing.DrawBoard(boardHeight, boardLenght);

}
catch (Exception ex) 
{
    Console.WriteLine(ex.Message);
}
