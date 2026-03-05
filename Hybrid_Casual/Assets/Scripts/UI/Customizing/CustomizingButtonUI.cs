using Character;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Customizing
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
            SoundManager.Instance.PlaySfx(SfxType.Click);
        }
    }
}