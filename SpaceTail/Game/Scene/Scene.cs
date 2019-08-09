using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceTail.Game.Scenes
{
    class Scene
    {
        private string _name;

        public string Name { get => _name; }

        public Scene(string sceneName)
        {
            _name = sceneName;
        }

        public virtual void OnLogicUpdate(long ticksPassed)
        {

        }

        public virtual void OnKeyPressed(ConsoleKeyInfo key)
        {

        }

        public virtual void OnFrameDraw(/* Frame frame */)
        {

        }
    }
}
