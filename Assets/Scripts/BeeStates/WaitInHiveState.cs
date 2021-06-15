namespace Bee
{
    public class WaitInHiveState : IBeeState
    {
        public void Action(BeeBase bee)
        {
            if (bee.gameObject.activeSelf)
            {
                bee.gameObject.SetActive(false);
            }
        }
    }
}
 
