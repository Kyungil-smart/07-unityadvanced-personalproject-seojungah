using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Operation
{
    public class JuiceMachineController : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Transform inputPoint;
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

            // 큐에 추가
            MakeList.Enqueue(item);
            
            StartCoroutine(AnimationRoutine(item));

            if (!isWorking)
            {
                StartCoroutine(MakeJuiceRoutine());
            }
        }

        IEnumerator AnimationRoutine(GameObject item)
        {
            if (item == null) yield break; 
            
            float duration = 0.2f;
            float currentTime = 0f;
            Vector3 startPos = item.transform.localPosition;
            
            while (currentTime < duration)
            {
                // 현재 위치에서 입구으로 이동
                item.transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, currentTime / duration);
                currentTime += Time.deltaTime;
                yield return null;
            }
            
            if (item != null)
            {
                item.transform.localPosition = Vector3.zero;
            }
        }

        IEnumerator MakeJuiceRoutine()
        {
            isWorking = true;

            while (MakeList.Count > 0)
            {
                // 큐에서 가장 오래된 아이템 꺼내기
                GameObject currentItem = MakeList.Dequeue();

                if (currentItem == null) continue;

                // 제작 시작
               currentTimer = 0f;
                while (currentTimer < makeTime)
                {
                    currentTimer += Time.deltaTime;
                    yield return null; // 다음 프레임까지 대기
                }
                // 제작 완료
                Destroy(currentItem);

                // 주스 생성
                GameObject juice = Instantiate(juicePrefab, spawnPoint.position, spawnPoint.rotation);
                juice.transform.SetParent(spawnPoint);
                
                // 완성된 큐에 추가
                JuiceList.Enqueue(juice);
                
                //정렬 로직
                ArrangeJuices();
            }
            
            isWorking = false;
            currentTimer = 0f;
        }
        
        public void ArrangeJuices()
        {
            int i = 0;
    
            foreach (GameObject juice in JuiceList)
            {
                if (juice == null) continue;

                int row = i / 5;
                int col = i % 5;

                Vector3 targetPos = new Vector3(col * 0.5f, 0, row * -0.5f);

                juice.transform.localPosition = targetPos;
                juice.transform.localRotation = Quaternion.identity;

                i++;
            }
        }
    }
}