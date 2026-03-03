using UnityEngine;

namespace UI.Field
{
    public class Billboard: MonoBehaviour
    {
        private Camera _mainCamera;

        void Start()
        {
            _mainCamera = Camera.main;
        }

        // UI가 카메라를 바라보는 스크립트
        void LateUpdate()
        {
            if (_mainCamera == null) return;

            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                _mainCamera.transform.rotation * Vector3.up);
        }
    }
}