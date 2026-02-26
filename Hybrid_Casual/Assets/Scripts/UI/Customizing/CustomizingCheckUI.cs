using Character;
using UnityEngine;

namespace UI
{
    public class CustomizingCheckUI : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private CustomType type;
        [SerializeField] private CharacterCustomizing characterCustomizing;
        void Awake()
        {
            characterCustomizing.Select += ShowCheckIcon;
        }
        
        private void ShowCheckIcon(CustomType checkType, int checkIndex)
        {
            if (checkType != type) return;
            if (checkIndex == index)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}