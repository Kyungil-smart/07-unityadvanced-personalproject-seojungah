using System.Collections;
using UnityEngine;
using Field;

namespace Operation
{
    public class JuiceOutputZone : MonoBehaviour
    {
        [SerializeField] private JuiceMachineController juiceMachine;
        private bool _isTaking = false;
        
        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && !_isTaking)
            {
                ItemStack playerStack = other.GetComponent<ItemStack>();
                
                if (playerStack != null && playerStack.CanStack() && juiceMachine.JuiceList.Count > 0)
                {
                    StartCoroutine(TakeJuiceRoutine(playerStack));
                }
            }
        }
        
        IEnumerator TakeJuiceRoutine(ItemStack playerStack)
        {
            _isTaking = true;

            // 가방에 넣기
            GameObject juice = juiceMachine.JuiceList.Dequeue();
            playerStack.AddItem(juice);
            
            // 재정렬
            juiceMachine.ArrangeJuices();

            yield return new WaitForSeconds(0.2f);

            _isTaking = false;
        }
    }
}