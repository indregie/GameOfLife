namespace GameOfLife.Logic.Interfaces;

public interface IBoardService
{
    void GenerateRandomBoard(int boardHeight, int boardLenght);
    void StartBackgroundMainLoop();
    void UpdateBoard();
    int CalculateAliveCells();
}