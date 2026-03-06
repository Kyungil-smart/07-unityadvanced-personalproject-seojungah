using UnityEngine;
using UnityEngine.UI;
using Core;
using TMPro;

namespace UI.Menu
{
    public class SoundUI: MonoBehaviour
    {
        [Header("Slider")]
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;
        
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI bgmText;
        [SerializeField] private TextMeshProUGUI sfxText;
        void Start()
        {
            if (bgmSlider != null)
            {
                bgmSlider.value = 0.5f; 
                bgmSlider.onValueChanged.AddListener(ChangeBgm);
                UpdateBgmText(0.5f);
            }

            if (sfxSlider != null)
            {
                sfxSlider.value = 0.5f; 
                sfxSlider.onValueChanged.AddListener(ChangeSfx);
                UpdateSfxText(0.5f);
            }
        }
        
        private void ChangeBgm(float value)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.SetBgmVolume(value);
            }
        }

        private void ChangeSfx(float value)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.SetSfxVolume(value);
            }
        }
        
        private void UpdateBgmText(float value)
        {
            if (bgmText != null)
            {
                bgmText.text = $"{Mathf.RoundToInt(value * 100)}%";
            }
        }

        private void UpdateSfxText(float value)
        {
            if (sfxText != null)
            {
                sfxText.text = $"{Mathf.RoundToInt(value * 100)}%";
            }
        }
    }
}