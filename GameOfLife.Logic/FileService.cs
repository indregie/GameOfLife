using GameOfLife.Logic.Interfaces;

namespace GameOfLife.Logic;
/// <summary>
/// Represents a service for working with external files.
/// </summary>
public class FileService : IFileService
{
    /// <summary>
    /// Saves given board to a file.
    /// </summary>
    /// <param name="board">Board to be saved.</param>
    public void SaveToFile(bool[,] board)
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

    /// <summary>
    /// Loads data from the file and displays on console.
    /// </summary>
    /// <param name="fileName">File from which data is loaded.</param>
    /// <returns>Board constructed from what was in the file.</returns>
    /// <exception cref="FileNotFoundException">Is thrown if no file with provided name was found in directory.</exception>
    public bool[,]? LoadFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"{fileName} can not be found.");
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
