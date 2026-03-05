using System.Collections.Generic;
using UnityEngine;
using Operation;

namespace Operation
{
    public class JuiceTable : MonoBehaviour
    {
        [Header("Display Settings")]
        [SerializeField] private List<Transform> displayPoints;
        
        private Dictionary<int, GameObject> _placedJuices = new Dictionary<int, GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ItemStack playerStack = other.GetComponent<ItemStack>();
                if (playerStack != null && playerStack.StackedItems.Count > 0)
                {
                    TryPlaceJuice(playerStack);
                }
            }
        }

        private void TryPlaceJuice(ItemStack stack)
        {
            for (int i = 0; i < displayPoints.Count; i++)
            {
                if (!_placedJuices.ContainsKey(i))
                {
                    GameObject juice = stack.StackedItems.Pop();
                    stack.RemoveItem();

                    // 주스를 테이블 위치로 이동
                    juice.transform.SetParent(displayPoints[i]);
                    juice.transform.localPosition = Vector3.zero;
                    juice.transform.localRotation = Quaternion.identity;

                    _placedJuices.Add(i, juice);
                    break; 
                }
            }
        }

        public GameObject TakeJuice()
        {
            foreach (var index in _placedJuices.Keys)
            {
                GameObject juice = _placedJuices[index];
                _placedJuices.Remove(index);
                return juice;
            }
            return null;
        }
    }
}