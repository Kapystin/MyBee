using System;

namespace Bee
{
    public class UIManager_Master : MasterSingleton<UIManager_Master>
    {        
        public event Action<Action> EventShowMenuPanel;
        public event Action<Action> EventHideMenuPanel;

        public event Action<House, Action> EventShowHousePanel;
        public event Action<Action> EventHideHousePanel;

        public event Action<Hive, Action> EventShowHivePanel;
        public event Action<Action> EventHideHivePanel;
        
        public event Action<Action> EventShowStatisticPanel;
        public event Action<Action> EventHideStatisticPanel;
        

        public void CallEventShowMenuPanel(Action callBack = null)
        {
            EventShowMenuPanel?.Invoke(callBack);
        }

        public void CallEventHideMenuPanel(Action callBack = null)
        {
            EventHideMenuPanel?.Invoke(callBack);
        }
 
        public void CallEventShowHousePanel(House house, Action callBack = null)
        {
            EventShowHousePanel?.Invoke(house, callBack);
        }

        public void CallEventHideHousePanel(Action callBack = null)
        {
            EventHideHousePanel?.Invoke(callBack);
        }

        public void CallEventShowHivePanel(Hive hive, Action callBack = null)
        {
            EventShowHivePanel?.Invoke(hive, callBack);
        }

        public void CallEventHideHivePanel(Action callBack = null)
        {
            EventHideHivePanel?.Invoke(callBack);
        }

        public void CallEventShowStatisticPanel(Action callBack = null)
        {
            EventShowStatisticPanel?.Invoke(callBack);
        }

        public void CallEventHideStatisticPanel(Action callBack = null)
        {
            EventHideStatisticPanel?.Invoke(callBack);
        }
    }
}