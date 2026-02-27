using System;
using System.Collections.Generic;
using Core;
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
        
        private readonly Dictionary<CustomType, int> _selectedList = new Dictionary<CustomType, int>();
        public Action<CustomType,int> Select;

        void Awake()
        {
            Select += OnItemClick;
        }
        void Start()
        {
            _selectedList.Add(CustomType.Body, 0);
            _selectedList.Add(CustomType.Eyes, 0);
            _selectedList.Add(CustomType.Mouth, 0);
            _selectedList.Add(CustomType.Head, 0);
            Select.Invoke(CustomType.Body, _selectedList[CustomType.Body]);
            Select.Invoke(CustomType.Eyes, _selectedList[CustomType.Eyes]);
            Select.Invoke(CustomType.Mouth, _selectedList[CustomType.Mouth]);
            Select.Invoke(CustomType.Head, _selectedList[CustomType.Head]);
        }

        void OnItemClick(CustomType type, int index)
        {
            _selectedList[type] = index;

            switch (type)
            {
                case CustomType.Body: ChangeOutput(bodyList,CustomType.Body); break;
                case CustomType.Eyes: ChangeOutput(eyesList,CustomType.Eyes); break;
                case CustomType.Mouth: ChangeOutput(mouthList,CustomType.Mouth); break;
                case CustomType.Head: ChangeOutput(headList,CustomType.Head); break;
            }
            
            GameManager.Instance.CharacterOutput = _selectedList;
        }

        void ChangeOutput(List<GameObject> list,CustomType type )
        {
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
    }

    public enum CustomType
    {
        Body,
        Eyes,
        Mouth,
        Head,
    }
}