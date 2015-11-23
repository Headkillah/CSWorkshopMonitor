using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorkshopMonitor.UI
{
    public class UICustomButton : UIButton
    {
        public ICommand _command;

        public UICustomButton()
        {
            eventClick += UICustomButton_eventClick;
        }

        private void UICustomButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            ModLogger.Debug("Triggering command '{0}' from clickhandler", _command.GetType().Name);
            if (_command != null)
                _command.Execute();
        }

        public void SetCommand(ICommand command)
        {
            ModLogger.Debug("Setting command '{0}' to '{1}' button", command.GetType().Name, this.name);
            _command = command;
        }

        public void ClearCommand()
        {
            ModLogger.Debug("Clearing command '{0}' from '{1}' button", _command.GetType().Name, this.name);
            _command = null;
        }

        public override void OnDestroy()
        {
            eventClick -= UICustomButton_eventClick;
        }
    }
}
