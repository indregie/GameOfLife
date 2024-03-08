namespace GameOfLife.Logic.Interfaces;

public interface IBoardService
{
    void GenerateRandomBoard(int boardHeight, int boardLenght);
    void StartBackgroundMainLoop();
    void StopBackgroundMainLoop();
    void UpdateBoard();
    int CalculateAliveCells();
    void DrawBoard();
    void SaveToFile(string savePath);
    void LoadFromFile(string fileName);
}