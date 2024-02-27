using UnityEngine;

namespace Project.Scripts.Minigames.Arrow_Minigame
{
    public class ArrowHandler : MonoBehaviour
    {
        [Range(0,100), SerializeField]
        private int arrowSpeed;

        [SerializeField] private GameObject arrowTarget;

        private bool canStart;

        private void OnEnable()
        {
            ArrowWaveManager.OnArrowStart += HandleArrowStart;
        }
    
        private void OnDisable()
        {
            ArrowWaveManager.OnArrowStart -= HandleArrowStart;
        }

        private void HandleArrowStart()
        {
            Debug.Log("Arrow Start");
            canStart = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(!canStart)
                return;
            
            transform.Translate(-Vector3.right * (arrowSpeed * Time.deltaTime));
            
            
        }
    }
}
