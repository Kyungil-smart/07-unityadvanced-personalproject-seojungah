using System.Collections.Generic;
using UnityEngine;

namespace Operation
{
    public class CustomerSpawner : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private int maxCustomers = 5;
        [SerializeField] private float spawnTime = 5f;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private JuiceTable targetTable;

        private List<GameObject> _activeCustomers = new List<GameObject>();
        private float _currentTime = 0f;

        void Update()
        {
            // 리스트에서 파괴된 손님 제거
            _activeCustomers.RemoveAll(c => c == null);

            if (_activeCustomers.Count < maxCustomers)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= spawnTime)
                {
                    SpawnCustomer();
                    _currentTime = 0f;
                }
            }
        }

        private void SpawnCustomer()
        {
            if (spawnPoint == null || customerPrefab == null || targetTable == null) return;

            // 손님 생성
            GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // 손님에게 테이블 정보 전달
            CustomerController customer = newCustomer.GetComponent<CustomerController>();
            if (customer != null)
            {
                customer.targetTable = targetTable;
            }

            _activeCustomers.Add(newCustomer);
        }
    }
}