using UnityEngine;

namespace UI.Menu
{
    public class MenuPopup:MonoBehaviour
    {

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenPopup();
            }
        }
        
        void OpenPopup()
        {
            gameObject.SetActive(true);
        }
        
        public void ClosePopup()
        {
            gameObject.SetActive(false);
        }
    }
}