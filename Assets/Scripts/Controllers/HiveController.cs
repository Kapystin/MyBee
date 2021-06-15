using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee
{
    public class HiveController : MonoBehaviour
    {
        [SerializeField]
        private List<Hive> _hivesRegister;
        private HiveSettings _hiveSettings;

        private void Awake()
        {
            _hivesRegister = new List<Hive>();
        }

        private void Start()
        {
            _hiveSettings = Settings.GetReference<HiveSettings>();
            StartCoroutine(PeriodicDeathEvent(_hiveSettings.PeriodicDeathEventDelay));
        }

        private void OnEnable()
        {
            HiveManager_Master.Instance.EventAddHiveToRegister += AddHiveToRegister;
            HiveManager_Master.Instance.EventRandomBeeDied += RandomBeeDied;
        }

        private void OnDisable()
        {
            HiveManager_Master.Instance.EventAddHiveToRegister -= AddHiveToRegister;
            HiveManager_Master.Instance.EventRandomBeeDied -= RandomBeeDied;            
            StopAllCoroutines();
        }

        private void AddHiveToRegister(Hive hive)
        {
            if (_hivesRegister != null)
            {
                if (_hivesRegister.Contains(hive))
                {
                    return;
                }

                StatisticManager_Master.Instance.CallEventHiveRegister();
                _hivesRegister.Add(hive);
            }
        }

        private void Update()
        {           
            //For Debug
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RandomBeeDied();
            }
        }

        private void RandomBeeDied()
        {
            foreach (var hive in _hivesRegister)
            {
                hive.KilBee();
            }
        }

        private IEnumerator PeriodicDeathEvent(float delay)
        {
            yield return new WaitForSeconds(delay);
            RandomBeeDied();
        }
    }
}