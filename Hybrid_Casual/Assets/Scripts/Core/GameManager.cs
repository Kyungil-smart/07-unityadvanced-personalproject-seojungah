using System;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Dictionary<CustomType, int> CharacterOutput = new Dictionary<CustomType, int>();
        public int currentMoney = 0;
        public Action<int> OnMoneyChanged;
        
        void Awake()
        {
            if (null == Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void AddMoney(int amount)
        {
            currentMoney += amount;
            OnMoneyChanged?.Invoke(currentMoney);
        }
    }
}