using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Bee
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _playerCamera;
        [SerializeField] private NavMeshAgent navAgent;
        private bool _isMoving;
        private Transform _currentLookAtPoint;

        private void Start()
        {
            _playerCamera = CameraManager_Master.Instance.MainCamera;
            PlayerManager_Master.Instance.CallEventUpdateAnimationState(PlayerAnimationState.Idle);
        }

        private void OnEnable()
        {
            PlayerManager_Master.Instance.EventMoveToDestinationPoint += MoveToDestinationPoint;
        }

        private void OnDisable()
        {
            PlayerManager_Master.Instance.EventMoveToDestinationPoint -= MoveToDestinationPoint;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.transform.CompareTag("InteractiveObject"))
                    {
                        return;
                    }

                    MoveToDestinationPoint(raycastHit.point);
                }
            }

            CheckDestinationReached();
        }
         
        private void MoveToDestinationPoint(Vector3 destinationPoint, Transform lookAtPoint = null)
        {
            _isMoving = true;
            navAgent.SetDestination(destinationPoint);
            PlayerManager_Master.Instance.CallEventUpdateAnimationState(PlayerAnimationState.Walk); 
            if (lookAtPoint != null)
            {
                _currentLookAtPoint = lookAtPoint;
            }
        }

        private void CheckDestinationReached()
        {
            if (_isMoving)
            {
                if (navAgent.pathPending == false)
                {
                    if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                    {                        
                        _isMoving = false;
                        

                        if (_currentLookAtPoint == null)
                        {
                            PlayerManager_Master.Instance.CallEventUpdateAnimationState(PlayerAnimationState.Idle);
                        }
                        else
                        {
                            transform.LookAt(_currentLookAtPoint.position);
                            PlayerManager_Master.Instance.CallEventUpdateAnimationState(PlayerAnimationState.Idle);
                            _currentLookAtPoint = null;
                        }                        
                    }
                }
            }    
        }
    }
}