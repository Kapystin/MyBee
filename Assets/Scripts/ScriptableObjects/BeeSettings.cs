using UnityEngine;

namespace Bee
{
    [CreateAssetMenu(menuName = "BeeSettins", fileName = "BeeSettings")]
    public class BeeSettings : ScriptableObject
    {
        [Header("Параметры обычной пчелы")]
        public Bee BeePrefab;
        public float BeeMoveSpeed = 5f;        
        public int BeeHoneyMaxCapacity = 3;          
        [Tooltip("Цикл(Тик) обновления метода сбора меда")]
        public float CollectHoneyTickRate = 3f;
        [Tooltip("Количество собираемого меда за тик")]
        public int BeeHoneyCollected = 1;     
        [Range(1,100)]
        public int BeeCreateChance = 76;
        
        [Header("Параметры обычной пчелы")]
        public Dron DronePrefab;
        public float DroneMoveSpeed = 1;        
        [Range(1,100)]
        public int DroneCreateChance = 19;     

        //public float BeeLifeTime;
        //public int BeeHoneyMaxCapacity;          
        //[Tooltip("Количество собираемого меда за тик")]
        //public int BeeHoneyCollected;        

    }
}