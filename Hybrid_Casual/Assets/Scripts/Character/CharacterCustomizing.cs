using System;
using System.Collections.Generic;
using Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Character
{
    public class CharacterCustomizing : MonoBehaviour
    {
        [Header("Customizing Prefabs")]
        [SerializeField] private List<GameObject> bodyList;
        [SerializeField] private List<GameObject> eyesList;
        [SerializeField] private List<GameObject> mouthList;
        [SerializeField] private List<GameObject> headList;
        
        private Dictionary<CustomType, int> _selectedList = new Dictionary<CustomType, int>();
        public Action<CustomType,int> Select;

        void Awake()
        {
            Select += OnItemClick;
        }
        void Start()
        {
            if (GameManager.Instance != null || GameManager.Instance.CharacterOutput.Count > 0) 
            {
                _selectedList = new Dictionary<CustomType, int>(GameManager.Instance.CharacterOutput);
                foreach (var output in _selectedList)
                {
                    ChangeOutput(output.Key);
                }

            }else
            {
                foreach (CustomType type in Enum.GetValues(typeof(CustomType)))
                {
                    InitSelectedList(type,0);
                    Select.Invoke(type, _selectedList[type]);
                }
            }
        }

        void InitSelectedList(CustomType type,int value)
        {
            _selectedList.Add(type, value);
            _selectedList.Add(type, value);
            _selectedList.Add(type, value);
            _selectedList.Add(type, value);
        }

        void OnItemClick(CustomType type, int index)
        {
            _selectedList[type] = index;

            switch (type)
            {
                case CustomType.Body: ChangeOutput(CustomType.Body); break;
                case CustomType.Eyes: ChangeOutput(CustomType.Eyes); break;
                case CustomType.Mouth: ChangeOutput(CustomType.Mouth); break;
                case CustomType.Head: ChangeOutput(CustomType.Head); break;
            }
            
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
    
    public enum CustomType
    {
        Body,
        Eyes,
        Mouth,
        Head,
    }
}