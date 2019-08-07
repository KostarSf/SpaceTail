using System;

namespace SpaceTail
{
    class CharPixel
    {
        private char @char;
        private ConsoleColor charColor;
        private ConsoleColor backColor;

        public char Char { get => @char; set => @char = value; }
        public ConsoleColor CharColor { get => charColor; set => charColor = value; }
        public ConsoleColor BackColor { get => backColor; set => backColor = value; }

        public CharPixel() : this(' ')
        {
        }

        public CharPixel(char @char) : this(@char, Console.ForegroundColor, Console.BackgroundColor)
        {
        }

        public CharPixel(char @char, ConsoleColor charColor, ConsoleColor backColor)
        {
            this.@char = @char;
            this.charColor = charColor;
            this.backColor = backColor;
        }

        public void SetCharPixel(char @char, ConsoleColor charColor, ConsoleColor backColor)
        {
            SetChar(@char);
            SetColors(charColor, backColor);
        }

        public void SetChar(char @char)
        {
            this.@char = @char;
        }

        public void SetCharColor(ConsoleColor charColor)
        {
            this.charColor = charColor;
        }

        public void SetBackColor(ConsoleColor backColor)
        {
            this.backColor = backColor;
        }

        public void SetColors(ConsoleColor charColor, ConsoleColor backColor)
        {
            SetCharColor(charColor);
            SetBackColor(backColor);
        }

        public void Reset()
        {
            SetCharPixel(' ', Input.GetDefaultForegroundColor(), Input.GetDefaultBackgroundColor());
        }

        public bool IsSameColor(CharPixel charPixel)
        {
            return (charColor == charPixel.CharColor && backColor == charPixel.BackColor);
        }

        public bool IsSameChar(CharPixel charPixel)
        {
            return (@char == charPixel.Char);
        }

        public bool IsSame(CharPixel charPixel)
        {
            return (IsSameChar(charPixel) && IsSameColor(charPixel));
        }
    }
}
