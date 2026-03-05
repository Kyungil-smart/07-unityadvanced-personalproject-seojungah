using UnityEngine;

namespace Operation
{
    public class JuiceOutputZone : MonoBehaviour
    {
        [SerializeField] private JuiceMachineController juiceMachine;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ItemStack playerStack = other.GetComponent<ItemStack>();
                
                if (playerStack != null && playerStack.CanStack() && juiceMachine.JuiceList.Count > 0)
                {
                    GameObject juice = juiceMachine.JuiceList.Dequeue();
                    playerStack.AddItem(juice);
                }
            }
        }
    }
}