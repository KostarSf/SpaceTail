namespace SpaceTail.Game
{
    internal class MenuOption : MenuButton
    {
        public MenuOption(GameManager.Options option) : this("", option)
        {
        }

        public MenuOption(string buttonText, GameManager.Options option) : base(buttonText)
        {
            SetEdges("<", ">");
        }
    }
}
