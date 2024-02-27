﻿namespace GameOfLife;

public class DrawingService
{
    private double _probability = 0.8;
    Random _random = new Random();

    public bool[,] GenerateRandomBoard(int boardHeight, int boardLenght)
    {
        bool[,] board = new bool[boardHeight, boardLenght];

        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardLenght; j++)
            {
                double randomVal = _random.NextDouble();
                board[i, j] = randomVal > _probability;
            }
        }
        return board;
    }

    public void DrawBoard(bool[,] board)
    {
        Console.Clear();

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write(board[i, j] ? '*' : '-');
            }
            Console.WriteLine();
        }
    }

    public bool[,] UpdateBoard(bool[,] board)
    {
        bool[,] updatedBoard = new bool[board.GetLength(0), board.GetLength(1)];

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                updatedBoard[i, j] = CalculateCell(board, i, j);
            }
        }
        return updatedBoard;
    }

    private bool CalculateCell(bool[,] board, int i, int j)
    {
        return !board[i,j];
    }
}
