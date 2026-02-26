using UnityEngine;

namespace Monster
{
    public class MonsterAction : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField]private int maxHp = 10;
        private int _currentHp;

        [Header("Drop Item")]
        [SerializeField]private GameObject dropItemPrefab;
        [SerializeField]private int dropCount = 1;
        
        void Start()
        {
            _currentHp = maxHp;
        }
        
        /// <summary>
        /// 몬스터 데미지 피해
        /// </summary>
        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
        
            if (_currentHp <= 0)
            {
                Die();
            }
        }
        
        void Die()
        {
            if (dropItemPrefab != null)
            {
                for (int i = 0; i < dropCount; i++)
                {
                    Vector3 spawnPos = transform.position + Vector3.up * 1f;
                    GameObject drop = Instantiate(dropItemPrefab, spawnPos, Quaternion.identity);
                
                    Rigidbody rb = drop.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
                        rb.AddForce(randomDir * 4f, ForceMode.Impulse);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}