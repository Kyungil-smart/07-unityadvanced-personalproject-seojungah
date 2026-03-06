using UnityEngine;
using TMPro;
using Core;

namespace UI.Field
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        void Start()
        {
            GameManager.Instance.OnMoneyChanged += UpdateMoneyText;
            UpdateMoneyText(GameManager.Instance.currentMoney);
        }

        void UpdateMoneyText(int amount)
        {
            // 천단위 콤마 표시
            moneyText.text = $"{amount:N0}";
        }
    }
}