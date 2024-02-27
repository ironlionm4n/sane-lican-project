using UnityEngine;

namespace Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;
        [SerializeField] private int stageLevel = 1;
        public int StageLevel => stageLevel;
    
        public enum MinigameType
        {
            Arrow,
            Bamboo,
            Battle
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
