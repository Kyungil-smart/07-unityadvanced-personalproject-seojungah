using UnityEngine;

namespace UI.Menu
{
    public class QuitButton : MonoBehaviour
    {
        /// <summary>
        /// 게임 종료
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}