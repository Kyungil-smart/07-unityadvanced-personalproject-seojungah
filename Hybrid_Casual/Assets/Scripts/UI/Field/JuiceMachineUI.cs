using Operation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Field
{
    public class JuiceMachineUI : MonoBehaviour
    {
        [SerializeField] private JuiceMachineController juiceMachineController;

        [Header("UI")] [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Slider makeTimeSlider;


        void Update()
        {
            if (makeTimeSlider == null) return;

            if (juiceMachineController.isWorking)
            {
                UpdateUI();
            }
            else
            {
                ResetUI();
            }
        }

        private void UpdateUI()
        {
            if (makeTimeSlider != null)
            {
                makeTimeSlider.value = juiceMachineController.currentTimer / juiceMachineController.makeTime;
            }

            if (countText != null)
            {
                countText.text = $"{juiceMachineController.MakeList.Count}/{juiceMachineController.maxCount}";
            }
        }
        
        private void ResetUI()
        {
            if (makeTimeSlider != null)
            {
                makeTimeSlider.value = 0;
            }
        }
    }
}