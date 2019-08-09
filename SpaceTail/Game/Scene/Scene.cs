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
    }
}
