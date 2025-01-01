using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMEYA
{
    internal abstract class BaseGameLogic : ConsoleInput.IArrowListener
    {
        protected BaseGameState? currentState { get; private set; }
        public abstract void OnArrowUp();
        public abstract void OnArrowDown();
        public abstract void OnArrowLeft();
        public abstract void OnArrowRight();
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
