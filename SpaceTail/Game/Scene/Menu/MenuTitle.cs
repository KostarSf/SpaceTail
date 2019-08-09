using SpaceTail.Game.Scenes;

namespace SpaceTail.Game
{
    internal class MenuTitle : MenuItem
    {
        private string _text;

        private string _decoration;
        private int _decorationMargin;

        private int _maxWidth;
        private bool _wordWrap;

        public string Text { get => _text;}
        public string Decoration { get => _decoration;}
        public int DecorationMargin { get => _decorationMargin;}
        public int MaxWidth { get => _maxWidth; }
        public bool WordWrap { get => _wordWrap;}

        public MenuTitle(string titleText)
        {
            _text = titleText;
            _decoration = "-";
            _decorationMargin = 1;
            _maxWidth = 34;
            _wordWrap = false;
        }

        internal MenuTitle SetDecoration(string decorationPattern)
        {
            _decoration = decorationPattern;

            return this;
        }

        internal MenuTitle SetDecorationMargin(int size)
        {
            _decorationMargin = size;

            return this;
        }

        internal MenuTitle SetMaxWidth(int width)
        {
            _maxWidth = width;

            return this;
        }

        internal MenuTitle SetWordWrap(bool state)
        {
            _wordWrap = state;

            return this;
        }
    }
}
