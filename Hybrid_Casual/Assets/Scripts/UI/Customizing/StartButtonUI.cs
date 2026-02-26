using Character;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Customizing
{
    public class StartButtonUI : MonoBehaviour
    {

        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnSelect);
        }

        void OnSelect()
        {
            SceneManager.LoadScene("GameScene");
        }

    }
}