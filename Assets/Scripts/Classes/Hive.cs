using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bee
{
    public class Hive : MonoBehaviour
    {
        public event Action OnValueChanged;
        public int HoneyCurrentAmount => _honeyCurrentAmount;
        public int BeesAmount => _bees.Count;

        [SerializeField] private Transform _lookAtPoint;
        [SerializeField] private Transform _destinationPoint;

        [SerializeField] private List<BeeBase> _bees;

        [SerializeField] private int _honeyCurrentAmount;
       
        private HiveSettings _hiveSettings;
        private BeeSettings _beeSettings;

        private float _tickTime;
        private float _tickCooldown;

        private void OnEnable()
        {
            HiveManager_Master.Instance.EventEventKillBee += KilBee;
            _bees = new List<BeeBase>();
            _bees.Clear();            
        }

        private void OnDisable()
        {
            HiveManager_Master.Instance.EventEventKillBee -= KilBee;
            _bees.Clear();
        }

        private void Start()
        {
            HiveManager_Master.Instance.CallEventAddHiveToRegister(this);
            _hiveSettings = Settings.GetReference<HiveSettings>();
            _beeSettings = Settings.GetReference<BeeSettings>();
            _tickTime = _hiveSettings.TickRate;

            for (int i = 0; i < _hiveSettings.BeeMaxAmountInHive; i++)
            {
                CreateBee();
            }
        }

        private void Update()
        {
            if (_tickCooldown <= 0f)
            {
                _tickCooldown = _tickTime;

                CreateBee();
                SendBeeCollectHoney();                
            }

            _tickCooldown -= Time.deltaTime;
        }

        public void GiveHoney(int honeyAmount)
        {
            _honeyCurrentAmount += honeyAmount;
            _honeyCurrentAmount = Mathf.Clamp(_honeyCurrentAmount, 0, _hiveSettings.HoneyMaxAmount);

            StatisticManager_Master.Instance.CallEventBeeHarvestHoney(honeyAmount);
            OnValueChanged?.Invoke();

            if (_honeyCurrentAmount >= _hiveSettings.HoneyMaxAmount)
            {
                ReturnAllBeeIntoHive();
            }
        }

        public int GetHoney()
        {
            int value = _honeyCurrentAmount;
            _honeyCurrentAmount = 0;
            OnValueChanged?.Invoke();
            return value;
        }

        public int GetActiveBeeAmount()
        {
            int beeAmount = 0;
            foreach (var bee in _bees)
            {
                if (bee.gameObject.activeSelf)
                {
                    beeAmount++;
                }
            }

            return beeAmount;
        }

        public void KilBee(BeeBase beeBase = null)
        {
            if (beeBase == null)
            {
                foreach (var bee in _bees)
                {
                    if (bee.GetType() == typeof(Dron))
                    {
                        _bees.Remove(bee);
                        StatisticManager_Master.Instance.CallEventBeeHasDied();
                        Destroy(bee.gameObject);
                        return;
                    }
                }

                BeeBase randomBee = _bees[UnityEngine.Random.Range(0, _bees.Count)];
                _bees.Remove(randomBee);
                Destroy(randomBee.gameObject);
                return;
            }

            if (_bees.Contains(beeBase))
            {
                StatisticManager_Master.Instance.CallEventBeeHasDied();
                _bees.Remove(beeBase);
                Destroy(beeBase.gameObject);
            }
        }

        public void SendBeeCollectHoney()
        {
            if (_bees.Count <= 0 || _bees == null || _honeyCurrentAmount >= _hiveSettings.HoneyMaxAmount)
            {
                return;
            }

            if (GetActiveBeeAmount() >= _hiveSettings.BeeMaxActiveAmount)
            {
                return;
            }

            BeeBase selectedBee = _bees[UnityEngine.Random.Range(0, _bees.Count)];

            if (selectedBee.GetType() != typeof(Bee))
            {
                return;
            }

            if (selectedBee.gameObject.activeSelf)
            {
                return;
            }

            selectedBee.SetState(BeeStateName.FindFlowerState);
            selectedBee.gameObject.SetActive(true);
            OnValueChanged?.Invoke();
        }

        private void CreateBee()
        {
            if (_bees.Count >= _hiveSettings.BeeMaxAmountInHive)
            {
                return;
            }

            int summ = _beeSettings.BeeCreateChance + _beeSettings.DroneCreateChance;

            float beeCreateChance = GetGeneralChance(_beeSettings.BeeCreateChance, summ);
            float droneCreateChance = GetGeneralChance(_beeSettings.DroneCreateChance, summ);            
            
            List<BeeWeight> weightTable = new List<BeeWeight>();

            weightTable.Add(new BeeWeight(bee: new Bee(), weight: Mathf.RoundToInt(beeCreateChance)));
            weightTable.Add(new BeeWeight(bee: new Dron(), weight: Mathf.RoundToInt(droneCreateChance)));

            weightTable.Sort((x, y) => -x.Weight.CompareTo(y.Weight));
                        
            int randomNumber = UnityEngine.Random.Range(0, 101);
             
            if (randomNumber <= weightTable[0].Weight)
            {
                InitializeBee(weightTable[0].Bee);
            }
            else
            {
                randomNumber -= weightTable[0].Weight;

                if (randomNumber <= weightTable[1].Weight)
                {
                    InitializeBee(weightTable[1].Bee);
                }                 
            }

            weightTable.Clear();          
        }

        private float GetGeneralChance(int chance, int summ)
        {
            return (chance * 100) / summ;
        }
         
        private void ReturnAllBeeIntoHive()
        {
            foreach (var bee in _bees)
            {
                if (bee.gameObject.activeSelf)
                {
                    bee.SetState(BeeStateName.ReturnToHiveState);
                }
            }             
        }

        private void InitializeBee(BeeBase beeBase)
        {
            if (beeBase.GetType() == typeof(Bee))
            {
                Bee newBee = Instantiate(_beeSettings.BeePrefab, transform);
                newBee.Initialize(this, _beeSettings.BeeMoveSpeed,
                                        _beeSettings.BeeHoneyCollected,
                                        _beeSettings.BeeHoneyMaxCapacity);

                StatisticManager_Master.Instance.CallEventBeeWasCreated();
                _bees.Add(newBee);
            }

            if (beeBase.GetType() == typeof(Dron))
            {
                Dron newDrone = Instantiate(_beeSettings.DronePrefab, transform);
                newDrone.Initialize(this, _beeSettings.DroneMoveSpeed);

                StatisticManager_Master.Instance.CallEventBeeWasCreated();
                _bees.Add(newDrone);
            } 
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                PlayerManager_Master.Instance.CallEventMoveToDestinationPoint(_destinationPoint.position, transform);
                UIManager_Master.Instance.CallEventShowHivePanel(this, () =>
                {
                    CameraManager_Master.Instance.CallEventSetCameraPosition(CameraPresetName.Hive, target: _lookAtPoint);
                });                
            }
        }
    } 
}