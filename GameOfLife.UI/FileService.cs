namespace GameOfLife.UI;

public class FileService
{
    public void SaveToFile(bool[,] board)
    {
        try
        {
            string fileName = string.Format($"{DateTime.Now:yyyyMMddHHmmss}.txt");
            Console.WriteLine(fileName);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        writer.WriteLine(board[i, j] ? '*' : '-');
                    }
                    writer.WriteLine();
                }
                writer.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
