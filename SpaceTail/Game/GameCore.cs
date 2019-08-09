using SpaceTail.Game.Scenes;
using System;
using System.Diagnostics;
using System.Threading;

namespace SpaceTail.Game
{
    internal class GameCore
    {
        public delegate void EventContainer();
        public delegate void LongEventContainer(long ticksPassed);

        public event EventContainer OnGameStart;
        public event EventContainer OnGameStop;

        public event EventContainer OnFrameDraw;
        public event LongEventContainer OnLogicUpdate;

        private InputManager input;

        private Scene _currentScene;
        private static bool _gameIsRunning;

        private static int fps = 20;
        private static int targetCycleTime = 1000 / fps;

        internal Scene CurrentScene { get => _currentScene; }
        public static bool GameIsRunning { get => _gameIsRunning; }

        internal void Init()
        {
            Screen.Title = "SpaceTail";
            Screen.Init();

            input = new InputManager();

            OnGameStart += GameCycle;
            OnGameStart += input.OnGameStart;

            OnGameStop += input.OnGameStop;
        }

        private void GameCycle()
        {
            var cycleTimer = new Stopwatch();
            int currentCycleTime;

            long ticksPassed = 0;

            while (GameIsRunning)
            {
                cycleTimer.Restart();

                OnLogicUpdate(ticksPassed);
                OnFrameDraw();

                ticksPassed++;

                cycleTimer.Stop();
                currentCycleTime = (int)cycleTimer.ElapsedMilliseconds;

                if (currentCycleTime < targetCycleTime)
                {
                    Thread.Sleep(targetCycleTime - currentCycleTime);
                }
            }
        }

        internal void Start()
        {
            _gameIsRunning = true;
            OnGameStart();
        }

        internal void Stop()
        {
            _gameIsRunning = false;
            OnGameStop();
        }

        internal void SetScene(Scene scene)
        {
            if (CurrentScene != null)
            {
                OnLogicUpdate -= _currentScene.OnLogicUpdate;
                OnFrameDraw -= _currentScene.OnFrameDraw;
                input.OnKeyPress -= _currentScene.OnKeyPressed;
            }

            _currentScene = scene;

            OnLogicUpdate += _currentScene.OnLogicUpdate;
            OnFrameDraw += _currentScene.OnFrameDraw;
            input.OnKeyPress += _currentScene.OnKeyPressed;
        }
    }
}
