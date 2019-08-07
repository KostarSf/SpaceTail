using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceTail
{
    class CharLine
    {
        private List<CharPixel> charPixels;

        internal List<CharPixel> CharPixels { get => charPixels; set => charPixels = value; }
        public int Length { get; internal set; }

        public CharLine(int charPixelsCount)
        {
            charPixels = new List<CharPixel>(charPixelsCount);
            for (int i = 0; i < charPixels.Capacity; i++)
            {
                charPixels.Add(new CharPixel());
            }

            Length = charPixels.Count;
        }

        public void PutLine(string line, int startIndex)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (startIndex + i < charPixels.Count && startIndex + i >= 0)
                {
                    charPixels[startIndex + i].SetChar(line[i]);
                }
            }
        }

        public void PutLine(string line, int startIndex, ConsoleColor charColor)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (startIndex + i < line.Length)
                {
                    charPixels[startIndex + i].SetChar(line[i]);
                    charPixels[startIndex + i].SetCharColor(charColor);
                }
            }
        }

        public void PutLine(string line, int startIndex, ConsoleColor charColor, ConsoleColor backColor)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (startIndex + i < line.Length)
                {
                    charPixels[startIndex + i].SetCharPixel(line[i], charColor, backColor);
                }
            }
        }

        internal void PutReverseLine(string line, int reverseStartIndex, bool reverseLine)
        {
            if (reverseLine)
            {
                line = Input.ReverseLine(line);
            }
            for (int i = 0; i < line.Length; i++)
            {
                int startIndex = Length - reverseStartIndex;
                if (startIndex - i < Length && startIndex - i >= 0)
                {
                    charPixels[startIndex - i].SetChar(line[i]);
                }
            }
        }

        internal void PutReverseLine(string line, int startIndex, bool reverseLine, ConsoleColor charColor)
        {
            if (reverseLine)
            {
                line = Input.ReverseLine(line);
            }
            for (int i = 0; i < line.Length; i++)
            {
                if (startIndex - i < line.Length && startIndex - i >= 0)
                {
                    charPixels[startIndex - i].SetChar(line[i]);
                    charPixels[startIndex - i].SetCharColor(charColor);
                }
            }
        }

        internal void PutReverseLine(string line, int startIndex, bool reverseLine, ConsoleColor charColor, ConsoleColor backColor)
        {
            if (reverseLine)
            {
                line = Input.ReverseLine(line);
            }
            for (int i = 0; i < line.Length; i++)
            {
                if (startIndex - i < line.Length && startIndex - i >= 0)
                {
                    charPixels[startIndex - i].SetCharPixel(line[i], charColor, backColor);
                }
            }
        }

        internal void Draw(int linePosition)
        {
            Screen.DrawLine(0, linePosition, GetStringLine());
        }

        public string GetStringLine()
        {
            var result = new StringBuilder();

            foreach (var charPixel in charPixels)
            {
                result.Append(charPixel.Char);
            }

            return result.ToString();
        }

        internal void Clear()
        {
            foreach (var charPixel in charPixels)
            {
                charPixel.Reset();
            }
        }

        internal void SetLength(int length)
        {
            if (Length == length) return;

            Length = length;

            if (charPixels.Count < Length)
            {
                while (charPixels.Count < Length)
                {
                    charPixels.Add(new CharPixel());
                }
            }
            else if (charPixels.Count > Length)
            {
                charPixels.RemoveRange(Length, charPixels.Count - Length);
            }
        }
    }
}
