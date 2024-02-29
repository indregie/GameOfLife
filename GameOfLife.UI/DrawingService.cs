using GameOfLife.UI.Interfaces;

namespace GameOfLife.UI;
/// <summary>
/// Represents a service for generating, drawing and updating a Game of life board.
/// </summary>
public class DrawingService : IDrawingService
{
    private int _initialLeft;
    private int _initialTop;

    public DrawingService()
    {
        _initialLeft = Console.CursorLeft;
        _initialTop = Console.CursorTop;
    }
    /// <summary>
    /// Draws Game of life board on the console.
    /// </summary>
    /// <param name="board">Board to be drawn.</param>
    public void DrawBoard(bool[,] board)
    {
        Console.SetCursorPosition(_initialLeft, _initialTop);
        
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write(board[i, j] ? '*' : '-');
            }
            Console.WriteLine();
        }
    }
}
