using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZMEYA.SnakeGameplayState;

namespace ZMEYA
{
    internal enum SnakeDir
    {
        Up, 
        Down, 
        Left, 
        Right
    }
    internal class SnakeGameplayState : BaseGameState
    {
        public int fieldWidth { get; set; }
        public int fieldHeight { get; set; }
        public bool gameOver { get; private set; }
        public bool hasWon { get; private set; }
        public int level { get; set; }

        const char squareSymbol  = '■';
        const char circleSymbol = '0';
        private Cell apple = new Cell();
        Random random = new Random();
        public struct Cell
        {
            public int X; public int Y;
            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }

        }
        private SnakeDir currentDir = SnakeDir.Left;
        private float timeToMove = 0f;
        private List<Cell> body = new List<Cell>();

        public void GenerateApple()
        {
            Cell cell = new Cell();
            cell.X = random.Next(fieldWidth);
            cell.Y = random.Next(fieldHeight);

            if (body[0].Equals(cell))
            {
                if (cell.Y > fieldHeight / 2)
                    cell.Y--;
                else
                    cell.Y++;
            }
            apple = cell;
        }

        public override void Reset()
        {
            body.Clear();
            var middleY = fieldHeight / 2;
            var middleX = fieldWidth / 2;
         
            Cell cell = new Cell(middleX, middleY);
            apple = new(middleX - 5, middleY + 6);
            currentDir = SnakeDir.Left;
            body.Add(cell);
            timeToMove = 0f;
            gameOver = false;
            hasWon = false;

        }
        public override bool IsDone()
        {
            return hasWon || gameOver;
        }
        public override void Draw(ConsoleRenderer renderer)
        {

            renderer.DrawString($"Score: {body.Count - 1}", 3, 2, ConsoleColor.White);
            renderer.DrawString($"Level: {level}", 3, 1, ConsoleColor.White);
            foreach (Cell cell in body)
            {
                renderer.SetPixel(cell.X, cell.Y, squareSymbol, 3);
            }
            renderer.SetPixel(apple.X, apple.Y, circleSymbol, 1);

        }

        public override void Update(float deltaTime)
        {
            timeToMove -= deltaTime;
            if (timeToMove > 0f || gameOver)
            {
                return;
            }


            timeToMove = 1f / (5f + level);
            
            Cell head = body[0];
            Cell nextCell = ShiftTo(head, currentDir);

            if (nextCell.Equals(apple))
            {
                body.Insert(0, apple);
                if(body.Count >= level + 2)
                {
                    hasWon = true;
                }
                GenerateApple();
                return;
            }

            if (nextCell.X < 0 || nextCell.Y < 0 || nextCell.X >= fieldWidth || nextCell.Y >= fieldHeight)
            {
                gameOver = true;
                return;
            }

            body.RemoveAt(body.Count - 1);
            body.Insert(0, nextCell);
            //Console.WriteLine($"X: {body[0].X} Y: {body[0].Y}");

        }
        public void SetDirection(SnakeDir dir)
        {
            currentDir = dir;
        }
        private Cell ShiftTo(Cell from, SnakeDir toDir)
        {
            switch (toDir)
            {
                case SnakeDir.Up:
                    return new Cell(from.X, from.Y - 1);
                case SnakeDir.Down:
                    return new Cell(from.X, from.Y + 1);
                case SnakeDir.Left:
                    return new Cell(from.X - 1, from.Y);
                case SnakeDir.Right:
                    return new Cell(from.X + 1, from.Y);
            }

            return from;
        }
    }
}
