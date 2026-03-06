using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Character
{
    public enum CustomType
    {
        Body,
        Eyes,
        Mouth,
        Head,
    }
    public class CharacterCustomizing : MonoBehaviour
    {
        [Header("Customizing Prefabs")]
        [SerializeField] private List<GameObject> bodyList;
        [SerializeField] private List<GameObject> eyesList;
        [SerializeField] private List<GameObject> mouthList;
        [SerializeField] private List<GameObject> headList;

        private Dictionary<CustomType, int> _selectedList = new Dictionary<CustomType, int>();
        public Action<CustomType, int> Select;

        void Awake()
        {
            Select += OnItemClick;
            
            foreach (CustomType type in Enum.GetValues(typeof(CustomType)))
            {
                _selectedList[type] = 0;
            }
        }

        void Start()
        {
            if (GameManager.Instance != null && GameManager.Instance.CharacterOutput.Count > 0)
            {
                _selectedList = new Dictionary<CustomType, int>(GameManager.Instance.CharacterOutput);
                foreach (var output in _selectedList)
                {
                    ChangeOutput(output.Key);
                }
            }
            foreach (CustomType type in Enum.GetValues(typeof(CustomType)))
            {
                Select.Invoke(type, _selectedList[type]);
            }
        }

        void OnItemClick(CustomType type, int index)
        {
            _selectedList[type] = index;
            ChangeOutput(type);
            GameManager.Instance.CharacterOutput = _selectedList;
        }

        void ChangeOutput(CustomType type)
        {
            List<GameObject> list = GetOutputList(type);
            if (_selectedList.TryGetValue(type, out int value))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (value == i)
                    {
                        list[i].SetActive(true);
                    }
                    else
                    {
                        list[i].SetActive(false);
                    }
                }
            }
        }

        List<GameObject> GetOutputList(CustomType type)
        {
            switch (type)
            {
                case CustomType.Body: return bodyList;
                case CustomType.Eyes: return eyesList;
                case CustomType.Mouth: return mouthList;
                case CustomType.Head: return headList;
                default: return null;
            }
        }
    }


}