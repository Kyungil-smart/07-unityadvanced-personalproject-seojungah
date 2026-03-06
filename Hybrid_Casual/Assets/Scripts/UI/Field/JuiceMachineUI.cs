using Operation;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Field
{
    public class JuiceMachineUI : MonoBehaviour
    {
        [SerializeField] private JuiceMachineController juiceMachineController;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI makingCountText;
        [SerializeField] private TextMeshProUGUI juiceCountText;
        [SerializeField] private Slider makeTimeSlider;


        void Update()
        {
            if (juiceMachineController == null) return;

            if (makingCountText != null)
            {
                makingCountText.text = $"{juiceMachineController.MakeList.Count}/{juiceMachineController.maxCount}";
            }
            
            if (juiceCountText != null)
            {
                juiceCountText.text = $"{juiceMachineController.JuiceList.Count}";
            }

            if (makeTimeSlider != null)
            {
                if (juiceMachineController.isWorking)
                {
                    makeTimeSlider.value = juiceMachineController.currentTimer / juiceMachineController.makeTime;
                }
                else
                {
                    makeTimeSlider.value = 0;
                }
            }
        }
    }
}