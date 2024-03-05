using GameOfLife.Logic;
using GameOfLife.Logic.Interfaces;
using GameOfLife.UI;
using GameOfLife.UI.Interfaces;

IBoardService logicService = new BoardService();
IDrawingService drawingService = new DrawingService();
IFileService fileService = new FileService();
IGameManager manager = new GameManager(logicService, drawingService, fileService);

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
            manager.NewGame();
            break;
        case "2":
            //manager.LoadGame();
            break;
        case "q":
            return;
        default:
            Console.WriteLine("Please choose between 1, 2 or Q.");
            break;
    }
}
