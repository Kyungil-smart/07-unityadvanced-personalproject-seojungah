using System.Collections.Generic;
using UnityEngine;

namespace Operation
{
    public class StackManager : MonoBehaviour
    {
        [Header("Stack Settings")] public Transform stackPoint;
        public float itemHeight = 0.5f;
        public int maxStack = 10;

        private readonly List<Transform> _stackedItems = new List<Transform>();

        /// <summary>
        /// 아이템을 넣을 수 있는지 확인하는 함수
        /// </summary>
        public bool CanStack()
        {
            return _stackedItems.Count < maxStack;
        }

        /// <summary>
        /// 아이템을 가방에 넣는 함수
        /// </summary>
        public void AddItem(Transform item)
        {
            if (!CanStack()) return;

            _stackedItems.Add(item);

            // 물리 충돌 제거
            Destroy(item.GetComponent<Rigidbody>());
            Destroy(item.GetComponent<Collider>());

            // transform를 따라감
            item.SetParent(stackPoint);

            // 쌓일 위치 계산 (기존 아이템 개수 * 아이템 높이)
            Vector3 targetPos = new Vector3(0, (_stackedItems.Count - 1) * itemHeight, 0);

            item.localPosition = targetPos;
            item.localRotation = Quaternion.identity;
        }
    }
}