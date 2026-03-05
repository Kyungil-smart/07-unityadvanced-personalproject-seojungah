using UnityEngine;
using Character;
using UnityEngine.Serialization;

namespace Operation
{
    public class JuiceInputZone: MonoBehaviour
    {
        [SerializeField] private JuiceMachineController juiceMachineController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Juice input");
                ItemStack stackManager = other.GetComponent<ItemStack>();
                foreach (GameObject item in stackManager.StackedItems)
                {
                    juiceMachineController.AddToMachine(item);
                }
            }
        }
    }
}