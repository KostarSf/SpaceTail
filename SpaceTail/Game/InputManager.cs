using System;
using System.Threading.Tasks;

namespace SpaceTail.Game
{
    internal class InputManager
    {
        private Task inputTask;

        public delegate void EventContainer(ConsoleKeyInfo key);

        public event EventContainer OnKeyPress;

        public InputManager()
        {
            inputTask = new Task(InputAsync);
            inputTask.Start();
        }

        private InputManager Start()
        {
            inputTask.Start();

            return this;
        }

        internal void Stop()
        {
            inputTask.Dispose();
        }

        public void InputAsync()
        {
            Console.WriteLine("It works");
            while (true)
            {
                OnKeyPress(Console.ReadKey(true));
                //Console.ReadKey(false);
            }
        }

        public void OnGameStart()
        {
            //inputTask.Start();
            //Console.WriteLine("OnGameStart");
        }

        public void OnGameStop()
        {
            inputTask.Dispose();
        }
    }
}
