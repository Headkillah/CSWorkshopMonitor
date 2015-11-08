using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a class responsible for building the UI components for editing the WorkshopMonitor mod options
    /// </summary>
    public class UIModOptionsPanelBuilder
    {
        private UIHelper _uiHelper;
        private Configuration _configuration;
        private UIScrollablePanel _rootPanel;
        private bool _wasVisible;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIModOptionsPanelBuilder"/> class.
        /// </summary>
        /// <param name="helper">A reference to the CS helper class for creating configuration UI components</param>
        /// <param name="configuration">A reference to the configuration object holding the mod configuration options</param>
        public UIModOptionsPanelBuilder(UIHelper uiHelper, Configuration configuration)
        {
            _uiHelper = uiHelper;
            _configuration = configuration;
            _rootPanel = uiHelper.self as UIScrollablePanel;
        }

        public void CreateUI()
        {
            // Add a new group for the mod settings
            var modSettingsGroup = _uiHelper.AddGroup(UITexts.ModSettingsGroupLabel);
            modSettingsGroup.AddCheckbox(UITexts.ModSettingsDebugLoggingOption, _configuration.DebugLogging, b => _configuration.DebugLogging = b);

            // Attach to the visibilitychanged event, used to store the configuration changes when the options window is closed by the user.
            _rootPanel.eventVisibilityChanged += rootPanel_eventVisibilityChanged;
        }

        /// <summary>
        /// Handles the eventVisibilityChanged event of the rootpanel. Saves and applies the new configuration if the window was closed.
        /// </summary>
        /// <param name="component">The UI component of which the visibility changed</param>
        /// <param name="value">True if visibility changed, false otherwise</param>
        private void rootPanel_eventVisibilityChanged(UIComponent component, bool value)
        {
            if (_wasVisible && !value)
            {
                _configuration.SaveConfig();
                _configuration.ApplyConfig();
            }
            this._wasVisible = value;
        }
    }
}
