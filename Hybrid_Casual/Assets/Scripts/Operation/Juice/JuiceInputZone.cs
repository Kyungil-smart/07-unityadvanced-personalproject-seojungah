using UnityEngine;
using Field;
using System.Collections;

namespace Operation
{
    public class JuiceInputZone: MonoBehaviour
    {
        [SerializeField] private JuiceMachineController juiceMachineController;
        private bool _isPutting = false;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && !_isPutting)
            {
                ItemStack itemStack = other.GetComponent<ItemStack>();
                
                if (itemStack != null && itemStack.StackedItems.Count > 0)
                {
                    if (juiceMachineController.MakeList.Count < juiceMachineController.maxCount)
                    {
                        StartCoroutine(PutInRoutine(itemStack));
                    }
                }
            }
        }
        private IEnumerator PutInRoutine(ItemStack itemStack)
        {
            _isPutting = true;

            GameObject item = itemStack.StackedItems.Pop();
            itemStack.RemoveItem();

            juiceMachineController.AddToMachine(item);

            yield return new WaitForSeconds(0.2f);

            _isPutting = false;
        }
    }
}