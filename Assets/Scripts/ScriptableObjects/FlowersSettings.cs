using System.Collections.Generic;
using UnityEngine;

namespace Bee
{
    [CreateAssetMenu(menuName = "FlowersSettings", fileName = "FlowersSettings")]
    public class FlowersSettings : ScriptableObject
    {
        [Header("Цикл(Тик) обновления цветов")]
        public float TickRate = 10f;

        [Header("Параметры цветов")]      
        [Tooltip("Среднее количество цветов которое будет присутствовать на карте")]
        public int MediumAmountFlowers = 320;
        [Tooltip("Максимальное количество пчел которые могут собирать мед у цветка")]
        public int MaxBeeAttachAmount = 3;        
        [Tooltip("Количество меда в цветке")]
        public int HoneyCapacityAmount = 1;        
        [Tooltip("Количество восстанавливаемого меда")]
        public int HoneyRecoveredAmount = 2;

        public List<Flower> FlowersPrefab;        


        public Flower GetRandomFlowerPrefab()
        {
            return FlowersPrefab[Random.Range(0, FlowersPrefab.Count)];
        }
    }
}