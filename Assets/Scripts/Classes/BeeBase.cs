using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Bee
{
    public abstract class BeeBase : MonoBehaviour
    {
        [SerializeField] private string _currentStateName;        
        public Hive HomeHive;
        public float MoveSpeed;        

        private Dictionary<BeeStateName, IBeeState> StateRegister;

        private IBeeState _currentState;
        private bool _isInitialize;
         
        private void Awake()
        {            
            Initialize();
        }

        public void SetState(BeeStateName beeStateName)
        {
            //Debug.Log($"Set state: {beeStateName}");
            _currentState = StateRegister[beeStateName];
        }       
        
        private void Initialize()
        {
            if (_isInitialize)
            {
                return;
            }

            StateRegister = new Dictionary<BeeStateName, IBeeState>();

            StateRegister.Add(BeeStateName.WaitInHiveState, new WaitInHiveState());
            StateRegister.Add(BeeStateName.FindFlowerState, new FindFlowerState());
            StateRegister.Add(BeeStateName.ReturnToHiveState, new ReturnToHiveState());
            StateRegister.Add(BeeStateName.CollectHoneyState, new CollectHoneyState(Settings.GetReference<BeeSettings>().CollectHoneyTickRate));

            _currentState = StateRegister[BeeStateName.WaitInHiveState];
            transform.position += Vector3.up * 2;
            _isInitialize = true;            
        }
  
        private void Update()
        {           
            _currentState.Action(this);
            _currentStateName = _currentState.ToString(); 
        } 
    }
}