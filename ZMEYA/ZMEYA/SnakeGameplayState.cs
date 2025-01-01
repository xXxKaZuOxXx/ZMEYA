using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       
        public struct Cell
        {
            public int X; public int Y;
            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }

        }
        private SnakeDir currentDir = SnakeDir.Up;
        private float timeToMove = 0f;
        private List<Cell> body = new List<Cell>();
        public override void Reset()
        {
            body.Clear();
            currentDir = SnakeDir.Up;
            Cell cell = new Cell(0,0); 
            body.Add(cell);
            timeToMove = 0f;

        }

        public override void Update(float deltaTime)
        {
            timeToMove -= deltaTime;
            if (timeToMove > 0f)
            {
                return;
            }
            
            timeToMove = 1 / 5;
            
            Cell head = body[0];
            Cell nextCell = ShiftTo(head, currentDir);
            body.Remove(body[body.Count - 1]);
            body.Insert(0, nextCell);
            Console.WriteLine($"X: {body[0].X} Y: {body[0].Y}");

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
                    return new Cell(from.X, from.Y + 1);
                case SnakeDir.Down:
                    return new Cell(from.X, from.Y - 1);
                case SnakeDir.Left:
                    return new Cell(from.X - 1, from.Y);
                case SnakeDir.Right:
                    return new Cell(from.X + 1, from.Y);
            }

            return from;
        }
    }
}
