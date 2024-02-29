using GameOfLife.UI.Interfaces;

namespace GameOfLife.UI;

public class FileService : IFileService
{
    public void SaveToFile(bool[,] board)
    {
        try
        {
            string fileName = string.Format($"{DateTime.Now:yyyyMMdd}.txt");
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        writer.Write(board[i, j] ? '*' : '-');
                    }
                    writer.WriteLine();
                }
            }
            Console.WriteLine($"Board view saved to: {fileName} at {DateTime.Now}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public bool[,] LoadFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException(fileName);
        }

        var lines = File.ReadAllLines(fileName);

        int boardHeight = lines.Count();
        int boardLength = lines.First().Length;

        bool[,] board = new bool[boardHeight, boardLength];

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                char cell = lines[i][j];
                board[i, j] = cell == '*';
            }
        }
        return board;
    }
}
