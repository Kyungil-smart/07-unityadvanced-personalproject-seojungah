using Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CustomizingButtonUI : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private CustomType type;
        [SerializeField] private CharacterCustomizing  characterCustomizing;

        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnSelect);
        }
        
        private void OnSelect()
        {
            characterCustomizing.Select.Invoke(type, index);
        }
    }
}