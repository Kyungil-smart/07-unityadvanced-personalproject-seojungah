using UnityEngine;

namespace UI.Menu
{
    public class MenuPopup:MonoBehaviour
    {
        [SerializeField] private GameObject menuPopup;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menuPopup != null)
                {
                    bool isActive = menuPopup.activeSelf;
                    menuPopup.SetActive(!isActive);
                }
            }
        }
        

        public void ClosePopup()
        {
            menuPopup.SetActive(false);
        }
    }
}