using Monster;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]private float moveSpeed = 5f;
        private Rigidbody _rigidbody;
        private Vector3 _moveInput;

        [Header("Attack")]
        [SerializeField]private float attackRadius = 2f;
        [SerializeField]private float attackDelay = 0.5f;
        [SerializeField]private int attackDamage = 1;
        [SerializeField]private LayerMask resourceLayer;
        
        private float _lastAttackTime;
        private readonly Collider[] _hitColliders = new Collider[10];
        
        void AutoAttack()
        {
            if (Time.time - _lastAttackTime >= attackDelay)
            {
                // 충돌 검사 후 GameObject의 참조를 반환
                // 메모리 할당 및 해제 과정이 없기 때문에 성능이 향상될 수 있음
                int hitCount = Physics.OverlapSphereNonAlloc(transform.position, attackRadius, _hitColliders, resourceLayer);
            
                if (hitCount > 0)
                {
                    MonsterAction resource = _hitColliders[0].GetComponent<MonsterAction>();
                
                    if (resource != null)
                    {
                        resource.TakeDamage(attackDamage);
                    
                        Vector3 lookTarget = new Vector3(_hitColliders[0].transform.position.x, transform.position.y, _hitColliders[0].transform.position.z);
                        transform.LookAt(lookTarget);
                    }
                
                    _lastAttackTime = Time.time;
                }
            }
        }
    }
}