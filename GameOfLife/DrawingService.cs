namespace GameOfLife;

public class DrawingService
{
    private double _probability = 0.8;
    Random _random = new Random();
    public void DrawBoard(int boardHeight, int boardLenght)
    {
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardLenght; j++)
            {
                double randomVal = _random.NextDouble();
                char character = (randomVal < _probability) ? '-' : '*';
                Console.Write(character);
            }
            Console.WriteLine();
        }
        Console.ReadLine();
    }
}
