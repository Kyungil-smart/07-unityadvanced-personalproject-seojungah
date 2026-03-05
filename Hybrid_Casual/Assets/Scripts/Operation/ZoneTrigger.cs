using Character;
using Core;
using UnityEngine;

namespace Operation
{
    public class ZoneTrigger : MonoBehaviour
    {
        [Header("State")]
        public bool isCombatZone = false;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ChainsawMotion chainsaw = other.GetComponentInChildren<ChainsawMotion>(true);
                if (chainsaw != null)
                {
                    chainsaw.SetEquip(isCombatZone);
                }
                SoundManager.Instance.MuteSfx();
            }
        }
        
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ChainsawMotion chainsaw = other.GetComponentInChildren<ChainsawMotion>(true);
                if (chainsaw != null)
                {
                    chainsaw.SetEquip(!isCombatZone);
                }
                SoundManager.Instance.MuteSfx();
            }
        }
        
        void OnDrawGizmos()
        {
            BoxCollider box = GetComponent<BoxCollider>();
            if (box != null)
            {
                Gizmos.matrix = transform.localToWorldMatrix;

                Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
                Gizmos.DrawCube(box.center, box.size);

                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(box.center, box.size);
            }
        }
    }
}