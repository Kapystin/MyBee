using System;

namespace Bee
{
    public class LevelManager_Master : MasterSingleton<LevelManager_Master>
    {        
        public event Action EventStarLevel;

        public void CallEventStarLevel()
        {
            EventStarLevel?.Invoke();
        } 
    }
}