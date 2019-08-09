namespace SpaceTail.Game
{
    internal class MenuText : MenuTitle
    {
        public MenuText(string text) : base(text)
        {
            SetDecoration("");
            SetDecorationMargin(0);

            SetWordWrap(true);
        }
    }
}