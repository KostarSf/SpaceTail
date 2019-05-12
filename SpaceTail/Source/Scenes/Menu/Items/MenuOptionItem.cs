using System;

namespace SpaceTail
{
    internal class MenuOptionItem : MenuItem
    {
        string optionText;
        string optionValue;

        Config.Option option;

        public MenuOptionItem(string text, Config.Option option) : base(text, null)
        {
            this.option = option;
            defineItemType();
            SetClickable(false);
            setOptionText();
        }

        private void defineItemType()
        {
            switch (option)
            {
                case Config.Option.WindowWidth:
                    setItemType("OptionItem");
                    break;
                case Config.Option.WindowHeight:
                    setItemType("OptionItem");
                    break;
                case Config.Option.AudioMaster:
                    setItemType("OptionItem");
                    break;
                case Config.Option.AudioSounds:
                    setItemType("OptionItem");
                    break;
                case Config.Option.AudioMusic:
                    setItemType("OptionItem");
                    break;
                case Config.Option.ResetScores:
                    setItemType("OptionButtonItem");
                    break;
                case Config.Option.ResetOptions:
                    setItemType("OptionButtonItem");
                    break;
            }
        }

        private void setOptionText()
        {
            optionText = GetItemText();
            optionValue = getOptionValue();
            SetItemText(optionText + optionValue);
        }

        private void updateOptionText()
        {
            optionValue = getOptionValue();
            SetItemText(optionText + optionValue);
        }

        private string getOptionValue()
        {
            switch (option)
            {
                case Config.Option.WindowWidth:
                    return Config.WindowWidth.ToString();
                case Config.Option.WindowHeight:
                    return Config.WindowHeight.ToString();
                case Config.Option.AudioMaster:
                    return (Config.AudioMaster * 10).ToString();
                case Config.Option.AudioSounds:
                    return (Config.AudioSounds * 10).ToString();
                case Config.Option.AudioMusic:
                    return (Config.AudioMusic * 10).ToString();
            }

            return "";
        }

        public void ChangeOptionValue(int amount)
        {
            switch (option)
            {
                case Config.Option.WindowWidth:
                    Config.ResizeGameWindowWidth(amount);
                    break;
                case Config.Option.WindowHeight:
                    Config.ResizeGameWindowHeight(amount);
                    break;
                case Config.Option.AudioMaster:
                    break;
                case Config.Option.AudioSounds:
                    break;
                case Config.Option.AudioMusic:
                    break;
            }

            updateOptionText();
        }
    }
}