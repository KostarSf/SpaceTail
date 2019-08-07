using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceTail
{
    public class TextSprite
    {
        int x;
        int y;

        Screen.TextAlign textAlign = Screen.TextAlign.Left;
        Screen.ScreenAlign screenAlign = Screen.ScreenAlign.TopLeft;

        private List<StringBuilder> value;
        private int spriteWidth;
        private int spriteHeight;

        ConsoleColor textColor;
        ConsoleColor backColor;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public List<StringBuilder> Value { get => value; set => this.value = value; }
        public int Width { get => spriteWidth; set => spriteWidth = value; }
        public int Height { get => spriteHeight; set => spriteHeight = value; }
        internal Screen.TextAlign TextAlign { get => textAlign; set => textAlign = value; }
        internal Screen.ScreenAlign ScreenAlign { get => screenAlign; set => screenAlign = value; }

        public TextSprite(string value, int x, int y) : this(value, x, y, Input.GetDefaultForegroundColor()) { }

        public TextSprite(string value, int x, int y, ConsoleColor textColor) : this(value, x, y, textColor, Input.GetDefaultBackgroundColor()) { }

        public TextSprite(string value, int x, int y, ConsoleColor textColor, ConsoleColor backColor)
        {
            SetValue(value);
            SetCoords(x, y);
            SetColors(textColor, backColor);
            SetAlign(Screen.TextAlign.Left);
        }

        public TextSprite(List<string> value, int x, int y) : this(value, x, y, Input.GetDefaultForegroundColor()) { }

        public TextSprite(List<string> value, int x, int y, ConsoleColor textColor) : this(value, x, y, textColor, Input.GetDefaultBackgroundColor()) { }

        public TextSprite(List<string> value, int x, int y, ConsoleColor textColor, ConsoleColor backColor)
        {
            SetValue(value);
            SetCoords(x, y);
            SetColors(textColor, backColor);
        }

        public TextSprite SetColors(ConsoleColor textColor, ConsoleColor backColor)
        {
            this.textColor = textColor;
            this.backColor = backColor;

            return this;
        }

        public ConsoleColor[] GetColors()
        {
            ConsoleColor[] colors = { textColor, backColor };
            return colors;
        }

        internal TextSprite SetAlign(Screen.TextAlign align)
        {
            TextAlign = align;
            return this;
        }

        public void SetValue(string value)
        {
            var newValue = new List<string>();
            newValue.Add(value);
            SetValue(newValue);
        }

        public void SetValue(List<string> value)
        {
            if (Value == null)
            {
                Value = new List<StringBuilder>();
            }
            else
            {
                Value.Clear();
            }

            foreach (var line in value)
            {
                Value.Add(new StringBuilder(line));
            }

            Width = 0;
            Height = Value.Count;

            foreach(var line in Value)
            {
                if (line.Length > Width)
                {
                    Width = line.Length;
                }
            }
        }

        public void SetCoords(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal TextSprite SetScreenAlign(Screen.ScreenAlign align)
        {
            ScreenAlign = align;
            return this;
        }

        internal void MakeBlank()
        {
            foreach (var line in Value)
            {
                int lineLength = line.Length;
                line.Clear();
                line.Append(Input.GetEmptyLine(lineLength));
            }
        }

        public string GetLine(int lineIndex)
        {
            if (lineIndex >= 0 && lineIndex < Height)
                return Value[lineIndex].ToString();
            else
                return $"[wrong line index: {lineIndex}]";
        }
    }
}
