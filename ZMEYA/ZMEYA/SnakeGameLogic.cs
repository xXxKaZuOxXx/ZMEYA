using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMEYA
{
    internal class SnakeGameLogic : BaseGameLogic
    {

        SnakeGameplayState gameplayState = new SnakeGameplayState();

        public override void OnArrowUp()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void OnArrowDown()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(SnakeDir.Right);
        }
        public override void Update(float deltaTime)
        {
            gameplayState.Update(deltaTime);
        }
        public void GotoGameplay()
        {
            ChangeState(gameplayState);
            gameplayState.Reset();
        }
    }
}
