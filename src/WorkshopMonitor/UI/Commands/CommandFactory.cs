using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public class CommandFactory
    {
        private static readonly CommandFactory _instance = new CommandFactory();
        private UIMainWindow _mainWindow;

        private CommandFactory()
        { }

        public static CommandFactory Instance
        {
            get { return _instance; }
        }

        public void SetMainWindow(UIMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public ICommand CreateShowWorkshopItemInfoCommand(WorkshopItem workshopItem)
        {
            return new ShowWorkshopItemInfoCommand(workshopItem);
        }

        public ICommand CreateUnsubscribeWorkshopItemCommand(WorkshopItem workshopItem)
        {
            return new UnsubscribeWorkshopItemCommand(_mainWindow, workshopItem);
        }
    }
}
