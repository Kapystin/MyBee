using System.Collections.Generic;
using UnityEngine;

namespace Bee
{
    public class FlowersController : MonoBehaviour
    {
        [SerializeField] private List<Flower> _flowers;
        [SerializeField] private Transform _flowersHolder;
        [SerializeField] private List<SquareZone> _spawnZone;
        
        private float _tickRate = 10f;

        private FlowersSettings _flowersSettings;
        private float _tickCooldown;

        private void OnEnable()
        {
            FlowersManager_Master.Instance.EventGetRandomFlower += GetRandomFlower;
            FlowersManager_Master.Instance.EventRemoveFlower += RemoveFlower;
        }

        private void OnDisable()
        {
            FlowersManager_Master.Instance.EventGetRandomFlower -= GetRandomFlower;
            FlowersManager_Master.Instance.EventRemoveFlower -= RemoveFlower;
        }

        private void Start()
        {
            _flowersSettings = Settings.GetReference<FlowersSettings>();
            int flowersStartCount = _flowersSettings.MediumAmountFlowers / _spawnZone.Count;            
            CreateFlower(flowersStartCount);
            _tickRate = _flowersSettings.TickRate;
            _tickCooldown = _tickRate;
        }

        private void Update()
        {
            if (_tickCooldown <= 0f)
            {
                _tickCooldown = _tickRate;                
                CreateFlower(10);
            }

            _tickCooldown -= Time.deltaTime;
        }

        private Flower GetRandomFlower()
        {
            return _flowers[Random.Range(0, _flowers.Count)];
        }

        private void CreateFlower(int amount = 1)
        {               
            foreach (var zone in _spawnZone)
            {
                for (int i = 0; i < amount; i++)
                {
                    if (_flowers.Count >= _flowersSettings.MediumAmountFlowers)
                    {
                        return;
                    }

                    bool flag = false;

                    float xPosition = Mathf.RoundToInt(Random.Range(zone.xPosition.x, zone.xPosition.y));
                    float zPosition = Mathf.RoundToInt(Random.Range(zone.zPosition.x, zone.zPosition.y));

                    Vector3 newPosition = new Vector3(xPosition, 0f, zPosition);                   

                    foreach (var flower in _flowers)
                    {
                        if (flower.transform.position == newPosition)
                        {                            
                            flag = true;
                            break;
                        }
                    }

                    if (flag)
                    {
                        continue;
                    }

                    Flower newFlower = Instantiate(_flowersSettings.GetRandomFlowerPrefab(), _flowersHolder);
                    newFlower.transform.position = newPosition;
                    newFlower.MaxBeeAttachAmount = _flowersSettings.MaxBeeAttachAmount;
                    newFlower.SetHoneyCapacity(_flowersSettings.HoneyCapacityAmount);
                    _flowers.Add(newFlower);
                }
            }            
        } 
        
        private void RemoveFlower(Flower flower)
        {
            if (_flowers.Contains(flower))
            {
                _flowers.Remove(flower);
            }
        }
    }
}