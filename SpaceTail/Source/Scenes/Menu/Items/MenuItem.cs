namespace SpaceTail
{
    class MenuItem
    {
        string itemText;
        MenuScene itemLink;

        bool isSelected = false;
        bool isSkipable = false;
        bool isActive = true;

        bool isColored = false;

        public MenuItem()
        {
            itemText = "";
            isSkipable = true;
        }

        public MenuItem(string text)
        {
            itemText = text;
            isSkipable = true;
        }

        public MenuItem(string itemText, MenuScene itemLink)
        {
            this.itemText = itemText;
            this.itemLink = itemLink;
        }

        public MenuItem(string itemText, MenuScene itemLink, bool selected)
        {
            this.itemText = itemText;
            this.itemLink = itemLink;
            isSelected = selected;
        }

        public MenuItem(string itemText, MenuScene itemLink, bool selected, bool active)
        {
            this.itemText = itemText;
            this.itemLink = itemLink;
            isSelected = selected;
            isActive = active;
        }

        public string GetMenuItemText() => itemText;

        public bool IsSelected()
        {
            return isSelected;
        }

        public bool IsSkipable()
        {
            return isSkipable;
        }

        public bool IsActive()
        {
            return isActive;
        }

        internal bool IsColored()
        {
            return isColored;
        }

        public void SetSelected(bool state)
        {
            isSelected = state;
        }

        public void SetActive(bool state)
        {
            isActive = state;
        }

        internal void SetColored(bool state)
        {
            isColored = state;
        }

        internal void OpenLink()
        {
            if (itemLink == null)
            {
                return;
            }
            else
            {
                itemLink.Show();
            }
        }

        
    }
}
