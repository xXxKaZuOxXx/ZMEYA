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
        const char squareSymbol  = '■';
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
        public override void Reset()
        {
            body.Clear();
            var middleY = fieldHeight / 2;
            var middleX = fieldWidth / 2;
         
            Cell cell = new Cell(middleX, middleY);
            currentDir = SnakeDir.Left;
            body.Add(cell);
            timeToMove = 0f;

        }
        public override void Draw(ConsoleRenderer renderer)
        {

            renderer.SetPixel(body[0].X, body[0].Y, squareSymbol, 3);
            
        }

        public override void Update(float deltaTime)
        {
            timeToMove -= deltaTime;
            if (timeToMove > 0f)
            {
                return;
            }
            
            timeToMove = 1f / 5f;
            
            Cell head = body[0];
            Cell nextCell = ShiftTo(head, currentDir);
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
