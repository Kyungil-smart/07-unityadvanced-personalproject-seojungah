using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class MonsterAction : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField]private int maxHp = 3;
        private int _currentHp;

        [Header("Drop Item")]
        [SerializeField]private GameObject dropItemPrefab;
        [SerializeField]private int dropCount = 1;
        
        [Header("UI")]
        [SerializeField] private GameObject hpCanvas;
        [SerializeField] private Slider hpSlider;
        
        [Header("Effect")]
        [SerializeField] private GameObject bloodEffectPrefab;
        
        void Start()
        {
            _currentHp = maxHp;
            
            if (hpSlider != null)
            {
                hpSlider.maxValue = maxHp;
                hpSlider.value = _currentHp;
            }
            
            if (hpCanvas != null)
            {
                hpCanvas.SetActive(false);
            }
        }
        
        /// <summary>
        /// 몬스터 데미지 피해
        /// </summary>
        public void TakeDamage(int damage, Vector3 hitDirection)
        {
            _currentHp -= damage;
        
            if (hpCanvas != null && !hpCanvas.activeSelf)
            {
                hpCanvas.SetActive(true);
            }

            if (hpSlider != null)
            {
                hpSlider.value = _currentHp;
            }
            
            if (bloodEffectPrefab != null)
            {
                Vector3 effectPos = transform.position + Vector3.up * 1f;
                
                Quaternion effectRot = Quaternion.LookRotation(hitDirection);
                
                GameObject effect = Instantiate(bloodEffectPrefab, effectPos, effectRot);
                Destroy(effect, 1f); 
            }
        
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