namespace GameOfLife;

public class DrawingService
{
    private double _probability = 0.8;
    Random _random = new Random();
    public void DrawBoard(int boardHeight, int boardLenght)
    {
        char[,] board = new char[boardHeight, boardLenght];

        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardLenght; j++)
            {
                double randomVal = _random.NextDouble();
                board[i, j] = (randomVal < _probability) ? '-' : '*';
                Console.Write(board[i, j]);
            }
            Console.WriteLine();
        }
        Console.ReadLine();
    }
}
