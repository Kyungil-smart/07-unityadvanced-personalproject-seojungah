using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Field;
using TMPro;

namespace Operation
{
    public class JuiceTable : MonoBehaviour
    {
        [SerializeField] private List<Transform> displayPoints;
        [SerializeField] private TextMeshProUGUI countText;
        
        private Dictionary<int, GameObject> _placedJuices = new Dictionary<int, GameObject>();
        private Coroutine _putJuiceRoutine;
        
        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ItemStack playerStack = other.GetComponent<ItemStack>();
                
                // 가방에 아이템이 있고 && 테이블에 아직 빈자리가 있다면
                if (playerStack != null && playerStack.StackedItems.Count > 0 && _placedJuices.Count < displayPoints.Count)
                {
                    _putJuiceRoutine = StartCoroutine(PutJuiceRoutine(playerStack));
                }
            }
        }

        IEnumerator PutJuiceRoutine(ItemStack stack)
        {
            while (true)
            {
                if (stack.StackedItems.Count > 0 && _placedJuices.Count < displayPoints.Count)
                {
                    for (int i = 0; i < displayPoints.Count; i++)
                    {
                        if (!_placedJuices.ContainsKey(i))
                        {
                            GameObject juice = stack.StackedItems.Pop();
                            stack.RemoveItem();

                            juice.transform.SetParent(displayPoints[i]);
                            juice.transform.localPosition = Vector3.zero;
                            juice.transform.localRotation = Quaternion.identity;
                            countText.text = stack.StackedItems.Count.ToString();
                            _placedJuices.Add(i, juice);
                            break; 
                        }
                    }
                    
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    yield return null;
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
        
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_putJuiceRoutine != null)
                {
                    StopCoroutine(_putJuiceRoutine);
                    _putJuiceRoutine = null;
                }
            }
        }
    }
}