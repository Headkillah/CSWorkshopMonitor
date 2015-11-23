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
    public class ShowWorkshopItemInfoCommand : ICommand
    {
        private WorkshopItem _workshopItem;

        public ShowWorkshopItemInfoCommand(WorkshopItem workshopItem)
        {
            _workshopItem = workshopItem;
        }

        public void Execute()
        {
            ModLogger.Debug("Executing ShowWorkshopItemInfo command for workshopid '{0}'", _workshopItem.WorkshopId);
            if (Steam.IsOverlayEnabled() && _workshopItem.WorkshopId > 0)
                Steam.ActivateGameOverlayToWorkshopItem(new PublishedFileId(_workshopItem.WorkshopId));
            else
                ConfirmPanel.ShowModal(UITexts.WorkshopItemInfoOpenInBrowserTitle, UITexts.WorkshopItemInfoOpenInBrowserMessage, AskInfoModalCallback);
        }

        private void AskInfoModalCallback(UIComponent component, int result)
        {
            if (result != 0)
            {
                string workshopUrl = string.Format("http://steamcommunity.com/sharedfiles/filedetails/?id={0}", _workshopItem.WorkshopId);
                Process.Start(workshopUrl);
            }
        }
    }
}
