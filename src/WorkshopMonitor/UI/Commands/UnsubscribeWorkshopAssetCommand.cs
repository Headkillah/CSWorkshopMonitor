using ColossalFramework.Steamworks;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public class UnsubscribeWorkshopAssetCommand : ICommand
    {
        private UIMainWindow _mainWindow;
        private IUIWorkshopAssetRowData _workshopAssetRowData;

        public UnsubscribeWorkshopAssetCommand(UIMainWindow mainWindow, IUIWorkshopAssetRowData workshopAssetRowData)
        {
            _mainWindow = mainWindow;
            _workshopAssetRowData = workshopAssetRowData;
        }

        public void Execute()
        {
            ConfirmPanel.ShowModal("CONTENT_CONFIRM_WORKSHOPDELETE", AskUnsubscribeModalCallback);
        }

        private void AskUnsubscribeModalCallback(UIComponent component, int result)
        {
            if (result == 1)
            {
                if (Steam.workshop.Unsubscribe(new PublishedFileId(_workshopAssetRowData.WorkshopId)))
                {
                    ModLogger.Debug("Successfully unsubscribed '{0}'", _workshopAssetRowData.ReadableName);
                    WorkshopAssetMonitor.Instance.Remove(_workshopAssetRowData.WorkshopAssetId);
                    _mainWindow.RemoveRow(_workshopAssetRowData);
                }
                else
                    ModLogger.Debug("Failed to unsbuscribe '{0}'", _workshopAssetRowData.ReadableName);
            }
        }
    }
}
