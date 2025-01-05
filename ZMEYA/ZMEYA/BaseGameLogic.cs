using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMEYA
{
    internal abstract class BaseGameLogic : ConsoleInput.IArrowListener
    {
        protected float time { get; private set; }
        protected int screenWidth { get; private set; }
        protected int screenHeight { get; private set; }
        protected BaseGameState? currentState { get; private set; }
        public abstract void OnArrowUp();
        public abstract void OnArrowDown();
        public abstract void OnArrowLeft();
        public abstract void OnArrowRight();

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            time += deltaTime;
            screenHeight = renderer.height;
            screenWidth = renderer.width;
            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);
            Update(deltaTime);
        }
        public abstract ConsoleColor[] CreatePalette();
        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }
        protected void ChangeState(BaseGameState? state)
        {
            currentState?.Reset();
            currentState = state;
        }
        public abstract void Update(float deltaTime);
    }
}
