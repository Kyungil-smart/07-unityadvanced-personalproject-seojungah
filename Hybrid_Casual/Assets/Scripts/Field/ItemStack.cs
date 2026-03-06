using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Field
{
    public class ItemStack : MonoBehaviour
    {
        [Header("Stack")] [SerializeField] private Transform stackPoint;
        [SerializeField] private GameObject backStand;
        [SerializeField] private float itemHeight = 0.8f;
        [SerializeField] private int maxStack = 10;

        [Header("UI")] [SerializeField] private TextMeshProUGUI stackCountText;

        public readonly Stack<GameObject> StackedItems = new Stack<GameObject>();

        void Start()
        {
            UpdateStackUI();
        }

        void Update()
        {
            if (StackedItems.Count > 0)
            {
                backStand.SetActive(true);
            }
            else
            {
                backStand.SetActive(false);
            }
        }

        /// <summary>
        /// 아이템을 넣을 수 있는지 확인하는 함수
        /// </summary>
        public bool CanStack()
        {
            return StackedItems.Count < maxStack;
        }

        /// <summary>
        /// 아이템을 가방에 넣는 함수
        /// </summary>
        public void AddItem(GameObject item)
        {
            if (!CanStack()) return;

            StackedItems.Push(item);

            // 물리 충돌 제거
            Rigidbody rb = item.GetComponent<Rigidbody>();
            Destroy(rb);

            Collider[] cols = item.GetComponents<Collider>();
            foreach (Collider col in cols)
            {
                Destroy(col);
            }

            item.transform.localRotation = Quaternion.identity;

            // transform를 따라감
            item.transform.SetParent(stackPoint);

            // 쌓일 위치 계산 (기존 아이템 개수 * 아이템 높이)
            float targetY = (StackedItems.Count - 1) * itemHeight;
            Vector3 targetPos = new Vector3(0, targetY, 0);

            StartCoroutine(AnimationRoutine(item, targetPos));

            UpdateStackUI();
        }

        IEnumerator AnimationRoutine(GameObject item, Vector3 targetPos)
        {
            if (item == null) yield break;

            float duration = 0.2f;
            float elapsed = 0f;
            Vector3 startPos = item.transform.localPosition;

            while (elapsed < duration)
            {
                // 현재 위치에서 가방으로 이동
                item.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            if (item != null)
            {
                item.transform.localPosition = targetPos;
            }
        }

        public void RemoveItem()
        {
            UpdateStackUI();
        }

        void UpdateStackUI()
        {
            if (stackCountText != null)
            {
                stackCountText.text = $"{StackedItems.Count}/{maxStack}";
            }
        }
    }
}