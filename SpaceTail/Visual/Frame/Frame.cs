using System;
using System.Collections.Generic;

namespace SpaceTail
{
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
                // Долго обновляется

                frameLines.Clear();
                for (int line = 0; line < Height; line++)
                {
                    frameLines.Add(new CharLine(Width));
                }

                // Проблемы с рамками фрейма

                //frameLines.Capacity = Height;
                //if (FrameLines.Count < Height)
                //{
                //    while (frameLines.Count < Height)
                //    {
                //        frameLines.Add(new CharLine(Width));
                //    }
                //}
                //else if (FrameLines.Count > Height)
                //{
                //    frameLines.RemoveRange(Height, frameLines.Count - Height);
                //}

                //foreach (var line in frameLines)
                //{
                //    line.SetLength(Width);
                //}

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

            foreach (var line in FrameLines)
            {
                line.Draw(FrameLines.IndexOf(line));
            }
        }

        public void AddLine(int stardIndex, int lineIndex, object obj)
        {   
            frameLines[lineIndex].PutLine(obj.ToString(), stardIndex);
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
            // Перерисовка тех CharPixel, что были использованы TextSrite
            // Могут возникать артефакты

            if (SpriteList != null)
            {
                SetSpriteList(ClearSpriteList(SpriteList));
            }

            // Перерисовка каждого CharPixel
            // Очень долго на винде

            //foreach (var line in FrameLines)
            //{
            //    line.Clear();
            //}

            // Пересоздание CharLine
            // Всё ещё долго на винде

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
}
