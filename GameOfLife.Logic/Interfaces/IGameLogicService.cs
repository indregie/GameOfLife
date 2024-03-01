namespace GameOfLife.Logic.Interfaces
{
    public interface IGameLogicService
    {
        bool[,] GenerateRandomBoard(int boardHeight, int boardLenght);
        bool[,] UpdateBoard(bool[,] board);
        int CalculateAliveCells(bool[,] board);
    }
}