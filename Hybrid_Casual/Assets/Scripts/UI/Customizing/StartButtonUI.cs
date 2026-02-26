using Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartButtonUI : MonoBehaviour
    {

        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnSelect);
        }
        
        private void OnSelect()
        {
            
        }
    }
}