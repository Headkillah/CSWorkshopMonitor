using ColossalFramework.Steamworks;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public class ShowWorkshopAssetInfoCommand : ICommand
    {
        /// <summary>
        /// The _workshop asset row data
        /// </summary>
        private IUIWorkshopAssetRowData _workshopAssetRowData;

        public ShowWorkshopAssetInfoCommand(IUIWorkshopAssetRowData workshopAssetRowData)
        {
            _workshopAssetRowData = workshopAssetRowData;
        }

        public void Execute()
        {
            ModLogger.Debug("Executing ShowWorkshopAssetInfo command for workshopid '{0}'", _workshopAssetRowData.WorkshopId);
            if (Steam.IsOverlayEnabled() && _workshopAssetRowData.WorkshopId > 0)
                Steam.ActivateGameOverlayToWorkshopItem(new PublishedFileId(_workshopAssetRowData.WorkshopId));
            else
                ConfirmPanel.ShowModal(UITexts.WorkshopAssetInfoOpenInBrowserTitle, UITexts.WorkshopAssetInfoOpenInBrowserMessage, AskInfoModalCallback);
        }

        private void AskInfoModalCallback(UIComponent component, int result)
        {
            if (result != 0)
            {
                string workshopUrl = string.Format("http://steamcommunity.com/sharedfiles/filedetails/?id={0}", _workshopAssetRowData.WorkshopId);
                Process.Start(workshopUrl);
            }
        }
    }
}
