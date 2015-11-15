using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Configuration;

namespace WorkshopMonitor.UI
{
    public class UIModOptionsPanelBuilder
    {
        private UIHelper _uiHelper;
        private ConfigurationContainer _configuration;
        private UIScrollablePanel _rootPanel;
        private bool _wasVisible;

        public UIModOptionsPanelBuilder(UIHelper uiHelper, ConfigurationContainer configuration)
        {
            _uiHelper = uiHelper;
            _configuration = configuration;
            _rootPanel = uiHelper.self as UIScrollablePanel;
        }

        /// <summary>
        /// This method is invoked by CS internally
        /// </summary>
        public void CreateUI()
        {
            var modSettingsGroup = _uiHelper.AddGroup(UITexts.ModSettingsGroupLabel);
            modSettingsGroup.AddCheckbox(UITexts.ModSettingsDebugLoggingOption, _configuration.DebugLogging, b => _configuration.DebugLogging = b);

            _rootPanel.eventVisibilityChanged += rootPanel_eventVisibilityChanged;
        }

        private void rootPanel_eventVisibilityChanged(UIComponent component, bool value)
        {
            // Only save and apply the configuration if the rootpanel was visible but isn't anymore (meaning the user closed the window)
            if (_wasVisible && !value)
            {
                _configuration.SaveConfiguration();
                _configuration.ApplyConfiguration();
            }
            this._wasVisible = value;
        }
    }
}
