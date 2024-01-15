using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.UI
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private int battleSceneIndex;
        
        /// <summary>
        /// Play Button onClick event handler. Loads the battle scene asynchronously, activating the scene once ready.
        /// </summary>
        public void HandlePlayButton()
        {
            Debug.Log("Play button pressed");
            // load scene asynchronously, activating the scene once ready
            SceneManager.LoadSceneAsync(battleSceneIndex, LoadSceneMode.Single).completed += _ =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(battleSceneIndex));
            };
        }
    }
}
