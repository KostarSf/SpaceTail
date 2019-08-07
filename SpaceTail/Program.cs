using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceTail
{
    class TestPlayer
    {
        public static int X = 1;
        public static int Y = 1;

        public static char Symbol = 'X';
        public TestPlayer(int x, int y)
        {
            X = x;
            Y = y;
            Symbol = 'X';
        }

        public void SetCoords(int x, int y)
        {
            //this.X = x;
            //this.Y = y;
        }
    }
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

        
    }
    class FrameBorder
    {
        char leftBorder;
        char rightBorder;
        char topBorder;
        char bottomBorder;

        bool useCorners;
        char[] corners;

        char marginChar = ' ';

        int marginSize = 1;

        public int Size { get => marginSize + 1; }

        public FrameBorder(char l, char r, char t, char b)
        {
            leftBorder = l;
            rightBorder = r;
            topBorder = t;
            bottomBorder = b;
        }

        public void AddCorners(char lt, char rt, char lb, char rb)
        {
            useCorners = true;
            corners = new char[] { lt, rt, lb, rb };
        }

        public Frame AddBorderToFrame(Frame frame)
        {   /*
            int index;
            //Вертикальные
            
            foreach (var line in frame.FrameLines)
            {

                for (index = 0; index < marginSize + 1; index++)
                {
                    line[index] = marginChar;
                }
                line[index] = right;

                for (index = 0; index < marginSize; index++)
                {
                    line[line.Length - 1 - index] = marginChar;
                }
                line[line.Length - 1 - index] = left;
            }
            */

            foreach (var line in frame.FrameLines)
            {
                line.PutLine(Input.PutChars(marginChar, marginSize + 1) + leftBorder, 0);
                line.PutReverseLine(Input.PutChars(marginChar, marginSize + 1) + rightBorder, 0, false);
            }

            //Горизонтальные

            for (int startIndex = marginSize + 1; startIndex < frame.Width - marginSize; startIndex++)
            {
                if (marginSize < frame.FrameLines.Count)
                {
                    frame.PutColumn(Input.PutChars(marginChar, marginSize) + topBorder, 0, startIndex);
                    if (useCorners)
                    {
                        frame.FrameLines[marginSize].PutLine(corners[0].ToString(), marginSize + 1);
                        frame.FrameLines[marginSize].PutReverseLine(corners[1].ToString(), marginSize + 1, false);
                    }
                    frame.PutReverseColumn(Input.PutChars(marginChar, marginSize) + bottomBorder, 0, startIndex);

                    if (useCorners)
                    {
                        frame.PutReverseColumn(corners[2].ToString(), marginSize, marginSize + 1);
                        frame.PutReverseColumn(corners[3].ToString(), marginSize, frame.Width - marginSize - 1);
                    }
                }
            }




            /*
            for (index = 0; index < marginSize; index++)
            {
                for (int i = 0; i < frame.FrameLines[index].Length; i++)
                {
                    frame.FrameLines[index][i] = marginChar;
                }
            }
            
            for (int i = marginSize + 1; i < frame.FrameLines[index].Length - (marginSize + 1); i++)
            {
                frame.FrameLines[index][i] = top;
            }

            for (index = 0; index < marginSize - 1; index++)
            {
                for (int i = 0; i < frame.FrameLines[frame.FrameLines.Count - 1 - index].Length; i++)
                {
                    frame.FrameLines[frame.FrameLines.Count - 1 - index][i] = marginChar;
                }
            }
            for (int i = marginSize + 1; i < frame.FrameLines[index].Length - (marginSize + 1); i++)
            {
                frame.FrameLines[frame.FrameLines.Count - 1 - index][i] = bottom;
            }
            */

            //Углы
            /*
            if (useCorners)
            {
                frame.FrameLines[marginSize][marginSize + 1] = corners[0];
                frame.FrameLines[marginSize][frame.FrameLines[marginSize].Length - 2] = corners[1];
                frame.FrameLines[frame.FrameLines.Count - 1][marginSize + 1] = corners[2];
                frame.FrameLines[frame.FrameLines.Count - 1][frame.FrameLines[marginSize + 1].Length - 2] = corners[3];
            }*/
            return frame;
        }

        public void AddMargins(int marginSize)
        {
            this.marginSize = marginSize;
        }
    }

    class Frame
    {
        int frameWidth;
        int frameHeight;

        private List<CharLine> frameLines;
        FrameBorder border;
        bool useBorders;

        public List<CharLine> FrameLines { get => frameLines; set => frameLines = value; }
        public List<TextSprite> SpriteList;
        public int Width { get => frameWidth; set => frameWidth = value; }
        public int Height { get => frameHeight; set => frameHeight = value; }

        public Frame()
        {
            FitSizesToWindow();
            SetBordersPattern('║', '║', '═', '═').AddCorners('╔', '╗', '╚', '╝');
        }

        public void SetSizes(int width, int height)
        {
            int lastLineCount = Height + 1;

            Width = width;
            Height = height + 1;

            if (FrameLines == null)
            {
                frameLines = new List<CharLine>(Height);
                for (int line = 0; line < Height; line++)
                {
                    frameLines.Add(new CharLine(Width));
                }
            }
            else
            {
                //может быть поменять и не чистить полностью, а прибавлять/удалять
                //долго обновляется
                frameLines.Clear();
                for (int line = 0; line < Height; line++)
                {
                    frameLines.Add(new CharLine(Width));
                }
            }
        }

        public void FitSizesToWindow()
        {
            SetSizes(Console.BufferWidth - 1, Console.BufferHeight - 1);
        }

        public Frame CompareTo(Frame frame)
        {
            Frame resultFrame = new Frame();

            return resultFrame;
        }

        public FrameBorder SetBordersPattern(char left, char right, char top, char bottom)
        {
            useBorders = true;
            return border = new FrameBorder(left, right, top, bottom);
        }

        public void Draw()
        {
            if (useBorders)
            {
                border.AddBorderToFrame(this);
            }
            /*
            for (int i = 0; i < Height; i++)
            {
                Screen.DrawLine(0, i, frameLines[i].ToString());
            } */
            foreach (var line in FrameLines)
            {
                line.Draw(FrameLines.IndexOf(line));
            }
        }

        public void AddLine(int stardIndex, int lineIndex, object obj)
        {   /*
            string line = obj.ToString();
            for (int i = 0; i < line.Length; i++)
            {
                frameLines[y][x + i] = line[i];
            }   
            */
            frameLines[lineIndex].PutLine(obj.ToString(), stardIndex);
            try
            {
                
            } catch (Exception e)
            {
                Game.GetSecretEnding(e);
            }
        }

        internal void PutColumn(string line, int lineIndex, int startIndex)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (lineIndex + i < frameLines.Count)
                {
                    frameLines[lineIndex + i].PutLine(line[i].ToString(), startIndex);
                }
            }
        }

        internal void PutReverseColumn(string line, int reverseLineIndex, int startIndex)
        {
            for (int i = 0; i < line.Length; i++)
            {
                int lineIndex = Height - 1 - reverseLineIndex;
                frameLines[lineIndex - i].PutLine(line[i].ToString(), startIndex);
            }
        }

        internal void Clear()
        {
            if (SpriteList != null)
            {
                SetSpriteList(ClearSpriteList(SpriteList));
            }

            //if (SpriteList != null)
            //{
            //    foreach (var sprite in SpriteList)
            //    {
            //        if (frameLines.Count > sprite.Y + border.Size - 1)
            //        {
            //            frameLines[sprite.Y + border.Size - 1].PutLine(Input.GetEmptyLine(sprite.Value[0].Length), sprite.X + border.Size);
            //        }
            //    }
            //}



            /*
            foreach (var line in FrameLines)
            {
                line.Clear();
            }
            */

            //frameLines.Clear();
            //for (int line = 0; line < Height; line++)
            //{
            //    frameLines.Add(new CharLine(Width));
            //}
        }

        private List<TextSprite> ClearSpriteList(List<TextSprite> spriteList)
        {
            foreach (var sprite in spriteList)
            {
                sprite.MakeBlank();
            }
            return spriteList;
        }

        internal void SetSpriteList(List<TextSprite> spriteList)
        {
            SpriteList = spriteList;
            foreach (var sprite in SpriteList)
            {
                switch (sprite.TextAlign)
                {
                    case Screen.TextAlign.Left:
                        for (int i = 0; i < sprite.Value.Count && sprite.Y + i + border.Size - 1 < frameLines.Count; i++)
                        {
                            int margin = sprite.Width - sprite.Value[i].Length; ;
                            switch(sprite.ScreenAlign)
                            {
                                case Screen.ScreenAlign.TopLeft:
                                    frameLines[sprite.Y + i + border.Size - 1].PutLine(sprite.GetLine(i), sprite.X + border.Size);
                                    break;
                                case Screen.ScreenAlign.TopCenter:
                                    frameLines[sprite.Y + i + border.Size - 1].PutLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + 1);
                                    break;
                                case Screen.ScreenAlign.TopRight:
                                    frameLines[sprite.Y + i + border.Size - 1].PutReverseLine(sprite.GetLine(i), sprite.X + border.Size + margin, true);
                                    break;
                                case Screen.ScreenAlign.BottomLeft:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutLine(sprite.GetLine(i), sprite.X + border.Size);
                                    }
                                    break;
                                case Screen.ScreenAlign.BottomCenter:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + 1);
                                    }
                                    break;
                                case Screen.ScreenAlign.BottomRight:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        margin = sprite.Width - sprite.Value[i].Length;
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutReverseLine(sprite.GetLine(i), sprite.X + border.Size + margin, true);
                                    }
                                    break;
                                case Screen.ScreenAlign.MiddleLeft:
                                    frameLines[Height/2 - sprite.Height/2 + i + sprite.Y].PutLine(sprite.GetLine(i), sprite.X + border.Size);
                                    break;
                                case Screen.ScreenAlign.MiddleCenter:
                                    frameLines[Height/2 - sprite.Height/2 + i + sprite.Y].PutLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + 1);
                                    break;
                                case Screen.ScreenAlign.MiddleRight:
                                    frameLines[Height/2 - sprite.Height/2 + i + sprite.Y].PutReverseLine(sprite.GetLine(i), sprite.X + border.Size + margin, true);
                                    break;
                            }
                        }
                        break;

                    case Screen.TextAlign.Right:
                        for (int i = 0; i < sprite.Value.Count && sprite.Y + i + border.Size - 1 < frameLines.Count; i++)
                        {
                            switch (sprite.ScreenAlign)
                            {
                                case Screen.ScreenAlign.TopLeft:
                                    frameLines[sprite.Y + i + border.Size - 1].PutReverseLine(sprite.GetLine(i), Width - (sprite.X + border.Size) - sprite.Width + 1, true);
                                    break;
                                case Screen.ScreenAlign.TopCenter:
                                    frameLines[sprite.Y + i + border.Size - 1].PutReverseLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + 1, true);
                                    break;
                                case Screen.ScreenAlign.TopRight:
                                    frameLines[sprite.Y + i + border.Size - 1].PutReverseLine(sprite.GetLine(i), (sprite.X + border.Size), true);
                                    break;
                                case Screen.ScreenAlign.BottomLeft:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutReverseLine(sprite.GetLine(i), Width - (sprite.X + border.Size) - sprite.Width + 1, true);
                                    }
                                    break;
                                case Screen.ScreenAlign.BottomCenter:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutReverseLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + 1, true);
                                    }
                                    break;
                                case Screen.ScreenAlign.BottomRight:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutReverseLine(sprite.GetLine(i), (sprite.X + border.Size), true);
                                    }
                                    break;
                                case Screen.ScreenAlign.MiddleLeft:
                                    frameLines[Height / 2 - sprite.Height / 2 + i + sprite.Y].PutReverseLine(sprite.GetLine(i), Width - (sprite.X + border.Size) - sprite.Width + 1, true);
                                    break;
                                case Screen.ScreenAlign.MiddleCenter:
                                    frameLines[Height / 2 - sprite.Height / 2 + i + sprite.Y].PutReverseLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + 1, true);
                                    break;
                                case Screen.ScreenAlign.MiddleRight:
                                    frameLines[Height / 2 - sprite.Height / 2 + i + sprite.Y].PutReverseLine(sprite.GetLine(i), (sprite.X + border.Size), true);
                                    break;
                            }
                        }
                        break;

                    case Screen.TextAlign.Center:
                        for (int i = 0; i < sprite.Value.Count && sprite.Y + i + border.Size - 1 < frameLines.Count; i++)
                        {
                            int leftMargin = (sprite.Width - sprite.Value[i].Length) / 2;
                            switch (sprite.ScreenAlign)
                            {
                                case Screen.ScreenAlign.TopLeft:
                                    frameLines[sprite.Y + i + border.Size - 1].PutLine(sprite.GetLine(i), sprite.X + border.Size + leftMargin);
                                    break;
                                case Screen.ScreenAlign.TopCenter:
                                    frameLines[sprite.Y + i + border.Size - 1].PutLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + leftMargin + 1);
                                    break;
                                case Screen.ScreenAlign.TopRight:
                                    frameLines[sprite.Y + i + border.Size - 1].PutReverseLine(sprite.GetLine(i), (sprite.X + border.Size) + leftMargin, true);
                                    break;
                                case Screen.ScreenAlign.BottomLeft:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutLine(sprite.GetLine(i), sprite.X + border.Size + leftMargin);
                                    }
                                    break;
                                case Screen.ScreenAlign.BottomCenter:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + leftMargin + 1);
                                    }
                                    break;
                                case Screen.ScreenAlign.BottomRight:
                                    if ((Height - sprite.Y + i - border.Size + 1 - sprite.Height) >= 0)
                                    {
                                        frameLines[Height - sprite.Y + i - border.Size + 1 - sprite.Height].PutReverseLine(sprite.GetLine(i), (sprite.X + border.Size) + leftMargin, true);
                                    }
                                    break;
                                case Screen.ScreenAlign.MiddleLeft:
                                    frameLines[Height / 2 - sprite.Height / 2 + i + sprite.Y].PutLine(sprite.GetLine(i), sprite.X + border.Size + leftMargin);
                                    break;
                                case Screen.ScreenAlign.MiddleCenter:
                                    frameLines[Height / 2 - sprite.Height / 2 + i + sprite.Y].PutLine(sprite.GetLine(i), sprite.X + Width / 2 - sprite.Width / 2 + leftMargin + 1);
                                    break;
                                case Screen.ScreenAlign.MiddleRight:
                                    frameLines[Height / 2 - sprite.Height / 2 + i + sprite.Y].PutReverseLine(sprite.GetLine(i), (sprite.X + border.Size) + leftMargin, true);
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }

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

        private void SetColors(ConsoleColor textColor, ConsoleColor backColor)
        {
            this.textColor = textColor;
            this.backColor = backColor;
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

    class Game
    {
        static Task inputCycle;
        static int inputCount = 0;
        static int exceptionCount = 0;
        static int lastWindowWidth = 0;
        static int lastWindowHeight = 0;
        static long elapsedTime;

        static string fpsOffset = "";
        public static bool GameIsRunning;
        static int[] consoleSizes = { 0, 0 };

        static int fps = 20;
        static int targetCycleTime = 1000 / fps;

        static List<int> fpsSmooth = new List<int>();

        static int cycleCounter = 0;
        static List<TextSprite> SpriteList = new List<TextSprite>();

        static List<string> testArray = new List<string>();

        public static void StartGame()
        {
            testArray.Add("SpaceTail");
            testArray.Add("The story of one pony");

            Game.SetFPS(20);
            fpsSmooth = Input.PutInts(0, 40);

            GameIsRunning = true;
            inputCount = 0;
            exceptionCount = 0;
            Screen.Clear();

            var frame = new Frame();

            Stopwatch cycleTimer = new Stopwatch();
            int currentCycleTime;

            Stopwatch fpsTimer = new Stopwatch();

            TestPlayer player = new TestPlayer(5, 10);

            consoleSizes[0] = Console.WindowWidth;
            consoleSizes[1] = Console.WindowHeight;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetBufferSize(consoleSizes[0], consoleSizes[1]);
            }

            inputCycle = new Task(UpdateInput);
            inputCycle.Start();

            while (GameIsRunning)
            {
                fpsTimer.Restart();
                cycleTimer.Restart();

                //UpdateLogic();
                DrawFrame(frame);

                cycleTimer.Stop();
                currentCycleTime = (int)cycleTimer.ElapsedMilliseconds;

                if (currentCycleTime < targetCycleTime)
                {
                    Thread.Sleep(targetCycleTime - currentCycleTime);
                }

                //Console.SetCursorPosition(2, 1);
                cycleCounter++;
                
                switch (elapsedTime.ToString().Length)
                {
                    case 1:
                        fpsOffset = "  ";
                        break;
                    case 2:
                        fpsOffset = " ";
                        break;
                }

                fpsSmooth.RemoveAt(0);
                fpsSmooth.Add((int)(1000 / fpsTimer.ElapsedMilliseconds));

                elapsedTime = Input.Average(fpsSmooth);
            }

            inputCycle.Wait();
            inputCycle.Dispose();
            Program.DrawConsoleMenu(Program.Menu.DevMenu);
        }

        private static void UpdateInput()
        {
            ConsoleKeyInfo pressedKey;

            while (GameIsRunning)
            {
                pressedKey = Console.ReadKey(true);
                inputCount++;

                if (pressedKey.Key == ConsoleKey.Q)
                {
                    GameIsRunning = false;
                }

                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (TestPlayer.Y > 1)
                            TestPlayer.Y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (TestPlayer.Y < Console.BufferHeight - 4)
                            TestPlayer.Y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (TestPlayer.X > 1)
                            TestPlayer.X--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (TestPlayer.X < Console.BufferWidth - 6)
                            TestPlayer.X++;
                        break;

                    case ConsoleKey.D1:
                        Game.SetFPS(10);
                        break;
                    case ConsoleKey.D2:
                        Game.SetFPS(20);
                        break;
                    case ConsoleKey.D3:
                        Game.SetFPS(30);
                        break;
                    case ConsoleKey.D6:
                        Game.SetFPS(60);
                        break;
                    case ConsoleKey.D9:
                        Game.SetFPS(100);
                        break;
                    case ConsoleKey.D0:
                        Game.SetFPS(1000);
                        break;
                }
            }
        }

        private static void SetFPS(int value)
        {
            Game.fps = value;
            Game.targetCycleTime = 1000 / fps;
        }

        private static void DrawFrame(Frame frame)
        {
            Screen.DisableCursor();

            frame.Clear();

            Game.SpriteList.Clear();
            /*
            Game.SpriteList.Add(new TextSprite(Game.GetFPS(frame), 2, 1));
            Game.SpriteList.Add(new TextSprite("'q' to quit", 2, 2));
            Game.SpriteList.Add(new TextSprite("1, 2, 3, 6, 9, 0 - time control", 2, 3));
            Game.SpriteList.Add(new TextSprite("arrows - the 'X' control", 2, 4));

            Game.SpriteList.Add(new TextSprite("by KostarSf", 2, 1).SetScreenAlign(Screen.ScreenAlign.BottomLeft));
            Game.SpriteList.Add(new TextSprite(Program.appVersion, 2, 1).SetScreenAlign(Screen.ScreenAlign.BottomRight));

            Game.SpriteList.Add(new TextSprite(testArray, 0, 0).SetAlign(Screen.TextAlign.Center).SetScreenAlign(Screen.ScreenAlign.MiddleCenter));

            
            */

            Game.SpriteList.Add(new TextSprite(testArray, 1, 1).SetScreenAlign(Screen.ScreenAlign.TopLeft).SetAlign(Screen.TextAlign.Left));
            Game.SpriteList.Add(new TextSprite(testArray, 0, 1).SetScreenAlign(Screen.ScreenAlign.TopCenter).SetAlign(Screen.TextAlign.Center));
            Game.SpriteList.Add(new TextSprite(testArray, 1, 1).SetScreenAlign(Screen.ScreenAlign.TopRight).SetAlign(Screen.TextAlign.Right));

            Game.SpriteList.Add(new TextSprite(testArray, 1, 0).SetScreenAlign(Screen.ScreenAlign.MiddleLeft).SetAlign(Screen.TextAlign.Center));
            Game.SpriteList.Add(new TextSprite(testArray, 0, 0).SetScreenAlign(Screen.ScreenAlign.MiddleCenter).SetAlign(Screen.TextAlign.Right));
            Game.SpriteList.Add(new TextSprite(testArray, 1, 0).SetScreenAlign(Screen.ScreenAlign.MiddleRight).SetAlign(Screen.TextAlign.Left));

            Game.SpriteList.Add(new TextSprite(testArray, 1, 1).SetScreenAlign(Screen.ScreenAlign.BottomLeft).SetAlign(Screen.TextAlign.Right));
            Game.SpriteList.Add(new TextSprite(testArray, 0, 1).SetScreenAlign(Screen.ScreenAlign.BottomCenter).SetAlign(Screen.TextAlign.Left));
            Game.SpriteList.Add(new TextSprite(testArray, 1, 1).SetScreenAlign(Screen.ScreenAlign.BottomRight).SetAlign(Screen.TextAlign.Center));

            Game.SpriteList.Add(new TextSprite(TestPlayer.Symbol.ToString(), TestPlayer.X, TestPlayer.Y));

            //Слежение за обновлением размера консоли
            try
            {
                if (Console.WindowWidth != lastWindowWidth)
                {
                    lastWindowWidth = Console.WindowWidth;
                    Console.Clear(); //Убрать, когда появится буфер кадров
                    frame.FitSizesToWindow();
                }

                if (Console.WindowHeight != lastWindowHeight)
                {
                    lastWindowHeight = Console.WindowHeight;
                    frame.FitSizesToWindow();
                }

                if (Console.BufferHeight > Console.WindowHeight)
                {
                    Console.Clear();
                    Console.BufferHeight = Console.WindowHeight;
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        Console.WindowWidth = Console.BufferWidth;
                    }
                    frame.FitSizesToWindow();
                }
            } catch (Exception)
            {
                Screen.Clear();
                exceptionCount++;
            }

            //Вывод графики

            frame.SetSpriteList(Game.SpriteList);
            frame.Draw();
            try
            {
                //frame.AddLine(3,2, Game.GetFPS(frame));

                //frame.AddLine(3, 3, Console.WindowWidth);
                //frame.AddLine(3, 4, Console.BufferWidth);

                //frame.AddLine(3, 6, Console.WindowHeight);
                //frame.AddLine(3, 7, Console.BufferHeight);

                //frame.AddLine(3, 9, exceptionCount);

                //frame.AddLine(TestPlayer.X, TestPlayer.Y, TestPlayer.Symbol);

                
            }
            catch (Exception)
            {
                Screen.Clear();
                exceptionCount++;
            }
        }

        private static string GetFPS(Frame frame)
        {
            var fps = new StringBuilder();

            switch (cycleCounter)
            {
                case 1:
                    //Console.Write($@"fps: {fpsTimer.ElapsedMilliseconds}ms/f  -   ");
                    fps.Append($@"fps: {fpsOffset}{elapsedTime}  -   ");
                    break;
                case 2:
                    //Console.Write($@"fps: {fpsTimer.ElapsedMilliseconds}ms/f  \   ");
                    fps.Append($@"fps: {fpsOffset}{elapsedTime}  \   ");
                    break;
                case 3:
                    //Console.Write($@"fps: {fpsTimer.ElapsedMilliseconds}ms/f  |   ");
                    fps.Append($@"fps: {fpsOffset}{elapsedTime}  |   ");
                    break;
                case 4:
                    //Console.Write($@"fps: {fpsTimer.ElapsedMilliseconds}ms/f  /   ");
                    fps.Append($@"fps: {fpsOffset}{elapsedTime}  /   ");
                    cycleCounter = 0;
                    break;
            }

            return fps.ToString();
        }

        private static void drawFrameBorders()
        {
            throw new NotImplementedException();
        }

        internal static void GetSecretEnding(Exception e)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WindowHeight = 20;
                Screen.EnableCursor();
                Console.SetCursorPosition(0, 0);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n\n The secret exit way has been applied! ^_^\n");
            Console.ResetColor();

            throw new Exception(e.Message);
        }
    }
    class Input
    {
        public static char GetInputKeyChar(bool showCharInConsole)
        {
            return Console.ReadKey(!showCharInConsole).KeyChar;
        }

        internal static bool GetDigit(ref int outputDigit)
        {
            return int.TryParse(Input.GetInputKeyChar(true).ToString(), out outputDigit);
        }

        public static string GetEmptyLine(int length)
        {
            var line = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                line.Append(' ');
            }
            return line.ToString();
        }

        internal static int Average(List<int> numbers)
        {
            int result = 0;
            foreach (var number in numbers)
            {
                result += number;
            }
            return result / numbers.Count;
        }

        internal static List<int> PutInts(int number, int count)
        {
            var result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                result.Add(number);
            }
            return result;
        }

        public static string PutChars(char @char, int count)
        {
            var result = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                result.Append(@char);
            }
            return result.ToString();
        }

        internal static ConsoleColor GetDefaultForegroundColor()
        {
            ConsoleColor currentForegroundColor = Console.ForegroundColor;
            ConsoleColor currentBackgroundColor = Console.BackgroundColor;

            Console.ResetColor();
            ConsoleColor foregroundColor = Console.ForegroundColor;

            Console.ForegroundColor = currentForegroundColor;
            Console.BackgroundColor = currentBackgroundColor;

            return foregroundColor;
        }

        internal static ConsoleColor GetDefaultBackgroundColor()
        {
            ConsoleColor currentForegroundColor = Console.ForegroundColor;
            ConsoleColor currentBackgroundColor = Console.BackgroundColor;

            Console.ResetColor();
            ConsoleColor backgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = currentForegroundColor;
            Console.BackgroundColor = currentBackgroundColor;

            return backgroundColor;
        }

        internal static string ReverseLine(string line)
        {
            var result = new StringBuilder();

            for (int i = 1; i <= line.Length; i++)
            {
                result.Append(line[line.Length - i]);
            }

            return result.ToString();
        }
    }
    class Screen
    {
        public enum TextAlign
        {
            Left = 0,
            Center = 1,
            Right = 2,
        }

        public enum ScreenAlign
        {
            TopLeft,
            TopCenter,
            TopRight,

            BottomLeft,
            BottomCenter,
            BottomRight,

            MiddleLeft,
            MiddleCenter,
            MiddleRight,
        }

        public static Dictionary<string, ConsoleColor> Colors;

        static string leftMargin = " ";

        public static void Init()
        {
            Colors = new Dictionary<string, ConsoleColor>();
            Colors.Add("BLACK",         ConsoleColor.Black);
            Colors.Add("BLUE",          ConsoleColor.Blue);
            Colors.Add("CYAN",          ConsoleColor.Cyan);
            Colors.Add("DARKBLUE",      ConsoleColor.DarkBlue);
            Colors.Add("DARKCYAN",      ConsoleColor.DarkCyan);
            Colors.Add("DARKGRAY",      ConsoleColor.DarkGray);
            Colors.Add("DARKGREEN",     ConsoleColor.DarkGreen);
            Colors.Add("DARKMAGENTA",   ConsoleColor.DarkMagenta);
            Colors.Add("DARKRED",       ConsoleColor.DarkRed);
            Colors.Add("DARKYELLOW",    ConsoleColor.DarkYellow);
            Colors.Add("GRAY",          ConsoleColor.Gray);
            Colors.Add("GREEN",         ConsoleColor.Green);
            Colors.Add("MAGENTA",       ConsoleColor.Magenta);
            Colors.Add("RED",           ConsoleColor.Red);
            Colors.Add("WHITE",         ConsoleColor.White);
            Colors.Add("YELLOW",        ConsoleColor.Yellow);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetWindowSize(70, 20);
                Console.SetBufferSize(70, 20);
                Console.SetWindowSize(70, 20);
            }

            Console.Title = "SpaceTail";
        }

        public static void SimpleTextLine(string output)
        {
            Console.WriteLine(leftMargin + output);
        }
        public static void SimpleText(string output)
        {
            Console.Write(leftMargin + output);
        }
        public static void DisableCursor()
        {
            Console.CursorVisible = false;
        }
        public static void EnableCursor()
        {
            Console.CursorVisible = true;
        }
        public static void SkipLine()
        {
            Console.WriteLine();
        }
        public static void SkipLines(int linesCount)
        {
            for (int i = 1; i <= linesCount; i++)
            {
                Console.WriteLine();
            }
        }

        internal static void Clear()
        {
            if (Console.WindowHeight > 0)
            {
                Console.Clear();
            }
            try
            {
                
            } catch (Exception e)
            {
                Game.GetSecretEnding(e);
            }
        }

        public static void ColoredOutput(string line)
        {
            //"Some [red,]text that [,grn]must be [blu,gry]colored"

            string rawLine = line;
            StringBuilder colorKeys = new StringBuilder();

            Dictionary<int, string> colorParts = new Dictionary<int, string>();

            for (int index = 0; index < rawLine.Length; index++)
            {
                if (rawLine[index] == '[')
                {
                    colorKeys.Clear();
                    int i = 0;
                    while (rawLine[index + i++ + 1] != ']')
                    {
                        colorKeys.Append(rawLine[index + i]);
                    }
                    colorParts.Add(index, colorKeys.ToString());

                    rawLine = rawLine.Remove(index, i + 1);
                    index--;
                }
            }

            StringBuilder linePart = new StringBuilder();
            for (int index = 0; index < rawLine.Length; index++)
            {
                if (colorParts.ContainsKey(index))
                {
                    Console.Write(linePart.ToString());
                    setColorFromParseValue(colorParts[index]);

                    linePart.Clear();
                    linePart.Append(rawLine[index]);
                }
                else
                {
                    linePart.Append(rawLine[index]);
                }

                if (index == rawLine.Length - 1)
                {
                    Console.Write(linePart.ToString());
                }
            }

            ResetColor();

            void setColorFromParseValue(string value)
            {
                string[] colors = value.Split(',');

                SetColors(colors[0].Trim(), colors[1].Trim());
            }

            void SetColors(string fgColor, string bgColor)
            {
                fgColor = fgColor.Trim().ToUpper();
                bgColor = bgColor.Trim().ToUpper();

                if (Colors.ContainsKey(fgColor))
                {
                    Console.ForegroundColor = Colors[fgColor];
                }
                else if (fgColor == "RESET")
                {
                    ConsoleColor lastBg = Console.BackgroundColor;
                    Console.ResetColor();
                    Console.BackgroundColor = lastBg;
                }

                if (Colors.ContainsKey(bgColor))
                {
                    Console.BackgroundColor = Colors[bgColor];
                }
                else if (bgColor == "RESET")
                {
                    ConsoleColor lastFg = Console.ForegroundColor;
                    Console.ResetColor();
                    Console.ForegroundColor = lastFg;
                }
            }

        }

        public static void ColoredOutput(string line, bool useMargin)
        {
            if (useMargin)
            {
                ColoredOutput(leftMargin + line);
            }
        }

        public static void ColoredOutput(string[] lines)
        {
            int[] cursorPos = { Console.CursorLeft, Console.CursorTop };
            foreach (var line in lines)
            {
                ColoredOutput(line);
                Console.SetCursorPosition(cursorPos[0], ++cursorPos[1]);
            }
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }

        internal static void DrawLine(int x, int y, object line)
        {
            if (x < Console.BufferWidth && y < Console.BufferHeight)
            {
                Console.SetCursorPosition(x, y);
                if (line.ToString().Length <= Console.BufferWidth - x)
                {
                    Console.Write(line);
                }
                else
                {
                    int lineLength = Console.BufferWidth - x;
                    if (lineLength < line.ToString().Length)
                    {
                        Console.Write(line.ToString().Substring(0, lineLength));
                    }
                }
            }
        }
    }
    class Program
    {
        public static string appVersion = "v0.1 ALPHA";

        static string[] devMenu_start =
        {
            "Welcome to [DarkBlue,]SpaceTail[Reset,]!",
            "Created by [DarkGray,]KostarSf",
            "Version " + appVersion,
            "",
            " - Menu -",
            "1. Start game",
            "2. About",
            "3. Quit",
        };

        static string[] devMenu_about =
        {
            "[DarkBlue,]SpaceTail[Reset,] " + appVersion,
            "Created by [DarkGray,]KostarSf",
            "",
            "[DarkGray,]A story about one pony",
            "[DarkGray,]that stuck alone in space.",
            "",
            " - Menu -",
            "1. Github author's page: github.com/KostarSf",
            "2. Github game's page:   kostarsf.github.io/SpaceTail",
            "3. Back",
        };

        public enum Menu
        {
            DevMenu,
            DevMenu_StartGame,
            DevMenu_About,
            DevMenu_Quit,
        }

        static void Main(string[] args)
        {
            Screen.Init();
            Screen.DisableCursor();
            Screen.Clear();
            DrawConsoleMenu(Menu.DevMenu);
        }

        public static void DrawConsoleMenu(Menu menuName)
        {
            int chosedMenu;

            switch (menuName)
            {
                case Menu.DevMenu:
                    Console.Clear();
                    drawTextArray(devMenu_start);
                    chosedMenu = getIntKeyInput();
                    switch (chosedMenu)
                    {
                        case 1:
                            DrawConsoleMenu(Menu.DevMenu_StartGame);
                            break;
                        case 2:
                            DrawConsoleMenu(Menu.DevMenu_About);
                            break;
                        case 3:
                            DrawConsoleMenu(Menu.DevMenu_Quit);
                            break;
                        default:
                            DrawConsoleMenu(Menu.DevMenu);
                            break;
                    }
                    break;
                case Menu.DevMenu_StartGame:
                    Game.StartGame();
                    break;
                case Menu.DevMenu_About:
                    Console.Clear();
                    drawTextArray(devMenu_about);
                    chosedMenu = getIntKeyInput();
                    switch (chosedMenu)
                    {
                        case 1:
                            DrawConsoleMenu(Menu.DevMenu_About);
                            //System.Diagnostics.Process.Start("https://github.com/KostarSf");
                            break;
                        case 2:
                            DrawConsoleMenu(Menu.DevMenu_About);
                            //System.Diagnostics.Process.Start("https://kostarsf.github.io/SpaceTail");
                            break;
                        case 3:
                            DrawConsoleMenu(Menu.DevMenu);
                            break;
                        default:
                            DrawConsoleMenu(Menu.DevMenu_About);
                            break;
                    }
                    break;
                case Menu.DevMenu_Quit:
                    Screen.EnableCursor();
                    Screen.SkipLines(2);
                    Screen.SimpleText("Farewell! (press any key) ");
                    Console.ReadKey(true);
                    Screen.SkipLine();
                    break;
                default:
                    DrawConsoleMenu(Menu.DevMenu);
                    break;
            }
        }

        private static int getIntKeyInput()
        {
            int inputNumber = 0;
            Screen.SkipLine();
            Screen.SimpleText("Your choise is.. ");
            int[] inputPos = { Console.CursorLeft, Console.CursorTop };
            while (Input.GetDigit(ref inputNumber) != true)
            {
                Console.SetCursorPosition(inputPos[0], inputPos[1]);
            }
            return inputNumber;
        }

        private static void drawTextArray(string[] textArray)
        {
            foreach (var textLine in textArray)
            {
                Screen.ColoredOutput(textLine + "\n", true);
            }
        }
    }
}
