namespace GameOfLife.Logic.Interfaces;

public interface IFileService
{
    bool[,]? LoadFromFile(string fileName);
    void SaveToFile(bool[,] board);
}