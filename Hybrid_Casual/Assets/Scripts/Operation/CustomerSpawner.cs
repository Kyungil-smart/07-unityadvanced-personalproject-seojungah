using UnityEngine;
using System.Collections;
using Core;

namespace Operation
{
    public class CustomerSpawner : MonoBehaviour
    {
        public JuiceTable targetTable;
        public float moveSpeed = 3f;
        public int juicePrice = 100;
        
        private bool _hasJuice = false;
        private bool _isWaiting = false;
        
        void Update()
        {
            if (!_isWaiting)
            {
                // 손님이 테이블로 이동
                transform.position = Vector3.MoveTowards(transform.position, targetTable.transform.position, moveSpeed * Time.deltaTime);
                
                if (Vector3.Distance(transform.position, targetTable.transform.position) < 1.2f)
                {
                    _isWaiting = true;
                    StartCoroutine(CheckTableRoutine());
                }
            }
        }
        
        IEnumerator CheckTableRoutine()
        {
            while (!_hasJuice)
            {
                GameObject juice = targetTable.TakeJuice();
                
                if (juice != null)
                {
                    // 주스 획득
                    juice.transform.SetParent(this.transform);
                    juice.transform.localPosition = new Vector3(0, 1f, 0.5f); // 손에 든 위치
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
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}