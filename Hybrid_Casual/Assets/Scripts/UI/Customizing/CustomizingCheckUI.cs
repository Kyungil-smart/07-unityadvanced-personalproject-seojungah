using System;
using Character;
using UnityEngine;

namespace UI.Customizing
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

        private void Start()
        {
            gameObject.SetActive(false);
        }

        void ShowCheckIcon(CustomType checkType, int checkIndex)
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