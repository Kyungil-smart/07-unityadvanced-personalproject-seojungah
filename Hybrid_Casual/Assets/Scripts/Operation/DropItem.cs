using UnityEngine;

namespace Operation
{
    
    public class DropItem : MonoBehaviour
    {
        private bool _isPickedUp = false;

        void OnTriggerEnter(Collider other)
        {
            if (_isPickedUp) return;

            if (other.CompareTag("Player"))
            {
                Debug.Log("Drop Item");
                StackManager stackManager = other.GetComponent<StackManager>();
            
                // 가방이 꽉 차있는지 확인
                if (stackManager != null && stackManager.CanStack())
                {
                    _isPickedUp = true;
                    stackManager.AddItem(transform);
                }
            }
        }
    }
}