namespace SpaceTail
{
    class MenuItem
    {
        string itemType = "MenuItem";

        string itemText;
        MenuScene itemLink;

        bool isSelected = false;
        bool isSkipable = false;
        bool isActive = true;
        bool isClickable = true;

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

        public string GetItemText()
        {
            return itemText;
        }

        public void SetItemText(string text)
        {
            itemText = text;
        }

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

        public bool IsClickable()
        {
            return isClickable;
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

        public void SetClickable(bool state)
        {
            isClickable = state;
        }

        internal void SetColored(bool state)
        {
            isColored = state;
        }

        public string GetItemType()
        {
            return itemType;
        }

        internal void setItemType(string type)
        {
            itemType = type;
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
