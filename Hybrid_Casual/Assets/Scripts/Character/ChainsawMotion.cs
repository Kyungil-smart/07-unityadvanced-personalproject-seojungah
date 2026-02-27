using UnityEngine;

namespace Character
{
    public class ChainsawMotion :MonoBehaviour
    {
        [Header("State")]
        public GameObject weaponModel;
        public bool isEquipped = false;
        
        [Header("Vibration")]
        public bool isVibrating = true;
        public float vibrationAmount = 0.05f;
        
        private Vector3 _originalLocalPos;
        
        void Start()
        {
            if (weaponModel != null)
            {
                _originalLocalPos = weaponModel.transform.localPosition;
                weaponModel.SetActive(isEquipped);
            }
        }
        
        void Update()
        {
            if (isEquipped && isVibrating && weaponModel != null)
            {
                Vector3 randomShake = Random.insideUnitSphere * vibrationAmount;
                weaponModel.transform.localPosition = _originalLocalPos + randomShake;
            }
            else if (weaponModel != null)
            {
                // 진동이 꺼지거나 해제되면 원래 위치로
                weaponModel.transform.localPosition = _originalLocalPos;
            }
        }
        
        public void SetEquip(bool equip)
        {
            isEquipped = equip;
            if (weaponModel != null)
            {
                weaponModel.SetActive(isEquipped);
            }
        }
    }
}