using UnityEngine;

namespace Monster
{
    public class MonsterAI : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float detectRange = 7f;
        
        private Transform _player;
        private Vector3 _randomWaypoint;
        private float _waypointTimer;

        void Start()
        {
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
            targetPos.y = transform.position.y; 

            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.LookAt(targetPos);
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