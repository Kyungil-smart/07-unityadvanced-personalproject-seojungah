using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Operation
{
    public class JuiceMachineController : MonoBehaviour
    {
        [Header("Prefabs")] [SerializeField] private Transform inputPoint;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject juicePrefab;

        public float makeTime = 10.0f;
        public float currentTimer = 0f;
        public bool isWorking = false;
        public readonly Queue<GameObject> MakeList = new Queue<GameObject>();
        public readonly Queue<GameObject> JuiceList = new Queue<GameObject>();
        public float maxCount = 20;

        public void AddToMachine(GameObject item)
        {
            if (MakeList.Count >= maxCount) return;

            // 물리 충돌 제거
            Rigidbody itemRb = item.GetComponent<Rigidbody>();
            Collider itemCol = item.GetComponent<Collider>();
            if (itemRb != null) Destroy(itemRb);
            if (itemCol != null) Destroy(itemCol);

            // 입구 위치로 이동
            item.transform.SetParent(inputPoint);
            item.transform.localPosition = Vector3.zero;

            // 큐에 추가
            MakeList.Enqueue(item);

            if (!isWorking)
            {
                StartCoroutine(MakeJuiceRoutine());
            }
        }

        private IEnumerator MakeJuiceRoutine()
        {
            isWorking = true;

            while (MakeList.Count > 0)
            {
                // 큐에서 가장 오래된 아이템 꺼내기
                GameObject currentItem = MakeList.Dequeue();

                if (currentItem == null) continue;

                // 제작 준비
                // --- 제작 시작 ---
               currentTimer = 0f;
                while (currentTimer < makeTime)
                {
                    currentTimer += Time.deltaTime;
                    yield return null; // 다음 프레임까지 대기 (Update에서 슬라이더 갱신됨)
                }
                // --- 제작 완료 ---
                Destroy(currentItem);

                // 주스 생성
                GameObject juice = Instantiate(juicePrefab, spawnPoint.position, Quaternion.identity);

                // 완성된 큐에 추가
                JuiceList.Enqueue(juice);
            }

            isWorking = false;
            currentTimer = 0f;
        }
    }
}