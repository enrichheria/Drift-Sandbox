using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Test
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable() => 
            _button.onClick.AddListener(ReloadScene);

        private void OnDisable() => 
            _button.onClick.RemoveListener(ReloadScene);

        private void ReloadScene()
        {
            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }
}
