using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class MonsterAction : MonoBehaviour
    {
        [Header("Status")] 
        [SerializeField] private int maxHp = 3;
        private int _currentHp;

        [Header("Drop Item")] 
        [SerializeField] private GameObject dropItemPrefab;
        [SerializeField] private int dropCount = 1;

        [Header("UI")] 
        [SerializeField] private GameObject hpCanvas;
        [SerializeField] private Slider hpSlider;

        [Header("Effect")]
        [SerializeField] private GameObject bloodEffectPrefab;
        [SerializeField] private GameObject floorEffectPrefab;
        [SerializeField] private float knockbackDistance = 0.5f;
        [SerializeField] private float knockbackDuration = 0.1f;
        
        private Coroutine _knockbackCoroutine;
        
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

            ShowEffect(hitDirection);
            
            if (hitDirection != Vector3.zero)
            {
                if (_knockbackCoroutine != null) StopCoroutine(_knockbackCoroutine);
                _knockbackCoroutine = StartCoroutine(KnockbackRoutine(hitDirection));
            }

            if (_currentHp <= 0)
            {
                Die();
            }
        }

        void ShowEffect(Vector3 hitDirection)
        {
            
            if (bloodEffectPrefab != null)
            {
                Vector3 effectPos = transform.position + Vector3.up * 1f;

                Quaternion effectRot = Quaternion.LookRotation(hitDirection);

                GameObject effect = Instantiate(bloodEffectPrefab, effectPos, effectRot);
                Destroy(effect, 1f);
            }

            if (floorEffectPrefab != null)
            {
                RaycastHit hit;
                Vector3 rayStartPos = transform.position + Vector3.up * 0.5f;

                if (Physics.Raycast(rayStartPos, Vector3.down, out hit, 5f))
                {
                    Vector3 offset = hitDirection.normalized * 1.5f;
                    
                    Vector3 floorPos = hit.point + new Vector3(0, 0.02f, 0) + offset;

                    Quaternion splashRot = Quaternion.LookRotation(hitDirection) * Quaternion.Euler(90, 0, 0);
        
                    GameObject splash = Instantiate(floorEffectPrefab, floorPos, splashRot);
        
                    splash.transform.localScale = floorEffectPrefab.transform.localScale * Random.Range(1f, 2.5f);
        
                    Destroy(splash, 2f);
                }
            }
        }
        
        IEnumerator KnockbackRoutine(Vector3 direction)
        {
            float elapsedTime = 0f;
            Vector3 startPos = transform.position;
            
            direction.y = 0;
            Vector3 targetPos = startPos + (direction.normalized * knockbackDistance);

            while (elapsedTime < knockbackDuration)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / knockbackDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            transform.position = targetPos;
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