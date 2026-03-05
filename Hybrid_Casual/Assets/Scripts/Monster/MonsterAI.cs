using UnityEngine;

namespace Monster
{
    public class MonsterAI : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float detectRange = 7f;
        
        private Transform _player;
        private Vector3 _randomWaypoint;
        private float _waypointTimer;
        private Rigidbody _rigidbody;

        void Start()
        {
            _rigidbody =  GetComponent<Rigidbody>();
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _player = playerObj.transform;
            }
            
            SetNewWaypoint();
        }

        void Update()
        {
            if (_player == null) return;
            float distance = Vector3.Distance(transform.position, _player.position);

            if (distance <= detectRange)
            {
                MoveToTarget(_player.position);
            }
            else
            {
                _waypointTimer += Time.deltaTime;
                if (_waypointTimer >= 3f)
                {
                    SetNewWaypoint();
                    _waypointTimer = 0f;
                }
                
                MoveToTarget(_randomWaypoint);
            }
        }

        void MoveToTarget(Vector3 targetPos)
        {
            // 거리 체크
            float distance = Vector3.Distance(transform.position, targetPos);
            if (distance > 0.1f)
            {
                // 방향 계산
                Vector3 direction = (targetPos - transform.position).normalized;
                // 수평 이동만 계산
                direction.y = 0; 

                float currentYVelocity = _rigidbody.linearVelocity.y;
                _rigidbody.linearVelocity = new Vector3(direction.x * moveSpeed, currentYVelocity, direction.z * moveSpeed);
                
                // 회전 처리
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
                }
            }
            else
            {
                // 목표 도착 시 정지
                _rigidbody.linearVelocity = new Vector3(0, _rigidbody.linearVelocity.y, 0);
            }
        }

        void SetNewWaypoint()
        {
            float randomX = Random.Range(-5f, 5f);
            float randomZ = Random.Range(-5f, 5f);
            _randomWaypoint = transform.position + new Vector3(randomX, 0, randomZ);
        }
    }
}