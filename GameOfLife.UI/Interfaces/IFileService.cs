namespace GameOfLife.UI.Interfaces
{
    public interface IFileService
    {
        bool[,] LoadFromFile(string fileName);
        void SaveToFile(bool[,] board);
    }
}