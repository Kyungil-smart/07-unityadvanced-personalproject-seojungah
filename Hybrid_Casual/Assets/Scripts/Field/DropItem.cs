using Core;
using UnityEngine;

namespace Field
{
    
    public class DropItem : MonoBehaviour
    {
        private bool _isPickedUp = false;

        void OnTriggerEnter(Collider other)
        {
            if (_isPickedUp) return;

            if (other.CompareTag("Player"))
            {
                ItemStack itemStack = other.GetComponent<ItemStack>();
            
                // 가방이 꽉 차있는지 확인
                if (itemStack != null && itemStack.CanStack())
                {
                    _isPickedUp = true;
                    itemStack.AddItem(gameObject);
                    SoundManager.Instance.PlaySfx(SfxType.Item);
                }
            }
        }
    }
}