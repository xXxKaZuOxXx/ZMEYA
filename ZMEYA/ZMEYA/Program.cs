
using ZMEYA;

internal class Program
{
    const float targetFrameTime = 1f / 90f;
    static void Main(string[] args)
    {
        
        var gameLogic = new SnakeGameLogic();

        var pallete = gameLogic.CreatePalette();

        var renderer0 = new ConsoleRenderer(pallete);
        var renderer1 = new ConsoleRenderer(pallete);
        
        var input = new ConsoleInput();
        gameLogic.InitializeInput(input);
        var prevRenderer = renderer0;
        var currRenderer = renderer1;
        var lastFrameTime = DateTime.Now;
        //gameLogic.GotoGameplay();
        while (true)
        {
            //input.Update();
            var frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
            input.Update();
            gameLogic.DrawNewState(deltaTime, currRenderer);
            lastFrameTime = frameStartTime;
            if (!currRenderer.Equals(prevRenderer)) currRenderer.Render();

            var tmp = prevRenderer;
            prevRenderer = currRenderer;
            currRenderer = tmp;
            currRenderer.Clear();

            //lastFrameTime = frameStartTime;
            var nextFrameTime = frameStartTime + TimeSpan.FromSeconds(targetFrameTime);
            var endFrameTime = DateTime.Now;
            //Thread.Sleep(300);
            if (nextFrameTime > endFrameTime)
            {
                Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
    }
}
