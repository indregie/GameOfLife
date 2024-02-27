
Console.WriteLine("Welcome to the game of life!\nPlease write down the maximum height of the desk input:");
try
{
    string? inputDeskHeight = Console.ReadLine();

    Console.WriteLine("Please write down the maximum lenght of the desk input:");
    string? inputDeskLenght = Console.ReadLine();

    if (string.IsNullOrEmpty(inputDeskHeight) || string.IsNullOrEmpty(inputDeskLenght))
    {
        throw new Exception("Please provide valid input.");
    }

    int deskHeight = int.Parse(inputDeskHeight!);
    int deskLenght = int.Parse(inputDeskLenght!);

    int[,] board = new int[deskHeight, deskLenght];
    Console.WriteLine($"board: {deskHeight}, {deskLenght}");
}
catch (Exception ex) 
{
    Console.WriteLine(ex.Message);
}
