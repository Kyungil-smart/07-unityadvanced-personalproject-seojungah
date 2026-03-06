using UnityEngine;
using System.Collections;
using Core;
using UnityEngine.AI;

namespace Operation
{
    public class CustomerController : MonoBehaviour
    {
        public JuiceTable targetTable;
        public float moveSpeed = 3f;
        public int juicePrice = 100;
        
        private NavMeshAgent _agent;
        private Vector3 _spawnPoint;
        private bool _hasJuice = false;
        private bool _isWaiting = false;
        
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _spawnPoint = transform.position;
            
            if (targetTable != null)
            {
                _agent.SetDestination(targetTable.transform.position);
            }
        }
        
        void Update()
        {
            if (!_isWaiting && !_hasJuice)
            {
                if (!_agent.pathPending && _agent.remainingDistance <= 1.5f)
                {
                    _isWaiting = true;
                    // 이동 정지
                    _agent.isStopped = true; 
                    StartCoroutine(BuyJuiceRoutine());
                }
            }
        }
        
        IEnumerator BuyJuiceRoutine()
        {
            while (!_hasJuice)
            {
                GameObject juice = targetTable.TakeJuice();
                
                if (juice != null)
                {
                    // 주스 획득
                    juice.transform.SetParent(this.transform);
                    juice.SetActive(false);
                    _hasJuice = true;

                    // 돈 지불
                    GameManager.Instance.AddMoney(juicePrice);
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(LeaveRoutine());
                }
                
                yield return new WaitForSeconds(1f);
            }
        }
        
        IEnumerator LeaveRoutine()
        {
            _isWaiting = false;
            
            //다시 돌아가고 제거
            _agent.isStopped = false;
            _agent.SetDestination(_spawnPoint);
            while (_agent.pathPending || _agent.remainingDistance > 0.5f)
            {
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}