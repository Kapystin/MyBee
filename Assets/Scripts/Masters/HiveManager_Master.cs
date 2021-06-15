using System;
using UnityEngine;

namespace Bee
{
    public class HiveManager_Master : MasterSingleton<HiveManager_Master>
    {        
        public event Action<Hive> EventAddHiveToRegister; 
        public event Action EventRandomBeeDied; 
        public event Action<BeeBase> EventEventKillBee; 
         
        public void CallEventAddHiveToRegister(Hive hive)
        {
            EventAddHiveToRegister?.Invoke(hive);
        }  

        public void CallEventRandomBeeDied()
        {
            EventRandomBeeDied?.Invoke();
        } 

        public void CallEventEventKillBee(BeeBase beeBase)
        {
            EventEventKillBee?.Invoke(beeBase);
        } 
    }
}