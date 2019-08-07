namespace SpaceTail
{
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
        {   
            // Вертикальные
            foreach (var line in frame.FrameLines)
            {
                line.PutLine(Input.PutChars(marginChar, marginSize + 1) + leftBorder, 0);
                line.PutReverseLine(Input.PutChars(marginChar, marginSize + 1) + rightBorder, 0, false);
            }

            // Горизонтальные
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

                    // Углы
                    if (useCorners)
                    {
                        frame.PutReverseColumn(corners[2].ToString(), marginSize, marginSize + 1);
                        frame.PutReverseColumn(corners[3].ToString(), marginSize, frame.Width - marginSize - 1);
                    }
                }
            }

            return frame;
        }

        public void AddMargins(int marginSize)
        {
            this.marginSize = marginSize;
        }
    }
}
