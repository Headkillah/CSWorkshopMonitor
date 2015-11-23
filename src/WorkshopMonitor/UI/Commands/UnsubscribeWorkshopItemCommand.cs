using ColossalFramework.Steamworks;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public class UnsubscribeWorkshopItemCommand : ICommand
    {
        private UIMainWindow _mainWindow;
        private WorkshopItem _workshopItem;

        public UnsubscribeWorkshopItemCommand(UIMainWindow mainWindow, WorkshopItem workshopItem)
        {
            if (mainWindow == null)
                ModLogger.Debug("Argh");
            _mainWindow = mainWindow;
            _workshopItem = workshopItem;
        }

        public void Execute()
        {
            ConfirmPanel.ShowModal("CONTENT_CONFIRM_WORKSHOPDELETE", AskUnsubscribeModalCallback);
        }

        private void AskUnsubscribeModalCallback(UIComponent component, int result)
        {
            if (result == 1)
            {
                if (Steam.workshop.Unsubscribe(new PublishedFileId(_workshopItem.WorkshopId)))
                {
                    ModLogger.Debug("Successfully unsubscribed '{0}'", _workshopItem.ReadableName);
                    WorkshopItemMonitor.Instance.Remove(_workshopItem);
                    _mainWindow.RemoveRow(_workshopItem);
                }
                else
                    ModLogger.Debug("Failed to unsbuscribe '{0}'", _workshopItem.ReadableName);
            }
        }
    }
}
