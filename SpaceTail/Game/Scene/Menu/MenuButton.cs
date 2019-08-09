using System;
using SpaceTail.Game.Scenes;

namespace SpaceTail.Game
{
    internal class MenuButton : MenuItem
    {
        private string _text;

        private GameManager.Menu _linkedMenu;
        private bool _isLinked;

        private bool _isActive;
        private bool _isSelected;
        private bool _isDisabled;

        private string _leftEdge;
        private string _rightEdge;

        private int _buttonWidth;

        internal delegate void ButtonAction();
        private ButtonAction _action;
        private bool _canAct;

        public string Text { get => _text; }

        internal GameManager.Menu LinkedMenu { get => _linkedMenu; }
        public bool IsLinked { get => _isLinked; }

        public bool IsActive { get => _isActive; }
        public bool IsSelected { get => _isSelected; }
        public bool IsDisabled { get => _isDisabled; }

        public string LeftEdge { get => _leftEdge; }
        public string RightEdge { get => _rightEdge; }

        public int ButtonWidth { get => _buttonWidth; }
        public bool CanAct { get => _canAct;}

        public MenuButton(string buttonText)
        {
            _text = buttonText;

            _isLinked = false;

            SetActive(true);
            Selected(false);
            Enabled();

            SetEdges("*");

            SetWidth(28);

            SetActioning(false);
        }

        private MenuButton SetWidth(int width)
        {
            _buttonWidth = width;

            return this;
        }

        internal MenuButton LinkTo(GameManager.Menu linkedMenu)
        {
            _linkedMenu = linkedMenu;
            _isLinked = true;

            return this;
        }

        internal MenuButton ResetLink()
        {
            _isLinked = false;

            return this;
        }

        internal MenuButton SetActive(bool state)
        {
            _isActive = state;

            return this;
        }

        internal MenuButton Selected()
        {
            Selected(true);
            return this;
        }

        internal MenuButton Selected(bool state)
        {
            _isSelected = state;
            return this;
        }

        internal MenuButton Disabled()
        {
            _isDisabled = true;

            return this;
        }

        internal MenuButton Enabled()
        {
            _isDisabled = false;

            return this;
        }

        internal MenuButton SetEdges(string edgesPattern)
        {
            SetEdges(edgesPattern, edgesPattern);

            return this;
        }

        internal MenuButton SetEdges(string leftEdge, string rightEdge)
        {
            _leftEdge = leftEdge;
            _rightEdge = rightEdge;

            return this;
        }

        internal MenuButton SetAction(ButtonAction action)
        {
            _action = action;
            SetActioning(true);

            return this;
        }

        internal MenuButton DoAction()
        {
            _action();

            return this;
        }

        internal MenuButton SetActioning(bool state)
        {
            _canAct = state;

            return this;
        }
    }
}
