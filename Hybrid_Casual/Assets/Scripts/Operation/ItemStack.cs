using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Operation
{
    public class ItemStack : MonoBehaviour
    {
        [Header("Stack")]
        [SerializeField] private Transform stackPoint;
        [SerializeField] private GameObject backStand;
        [SerializeField] private float itemHeight = 0.5f;
        [SerializeField] private int maxStack = 10;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI stackCountText;
        
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
            Destroy(item.GetComponent<Rigidbody>());
            Destroy(item.GetComponent<Collider>());

            // transform를 따라감
            item.transform.SetParent(stackPoint);

            // 쌓일 위치 계산 (기존 아이템 개수 * 아이템 높이)
            Vector3 targetPos = new Vector3(0, (StackedItems.Count - 1) * itemHeight, 0);

            item.transform.localPosition = targetPos;
            item.transform.localRotation = Quaternion.identity;
            UpdateStackUI();
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