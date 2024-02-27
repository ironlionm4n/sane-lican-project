using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Minigames.Arrow_Minigame
{
    public class ArrowWaveManager : MonoBehaviour
    {
        public static event Action OnArrowStart = delegate { };
        [SerializeField] private GameObject[] archerSpawnPoints;
        [SerializeField] private GameObject archerPrefab;
        private int numberOfWaves;
        private int numberOfArrows;
        private int numberOfArchers;
        private int currentWave = 1;
    
        private void Start()
        {
            AssignStageVariables();
            EnableArchers();
        }

        private void EnableArchers()
        {
            var tempList = archerSpawnPoints.ToList();
            for (int i = 0; i < numberOfArchers; i++)
            {
                var randomIndex = UnityEngine.Random.Range(0, tempList.Count);
                tempList[randomIndex].SetActive(true);
                tempList.RemoveAt(randomIndex);
            }

            StartCoroutine(WaitForStart());
        }

        private IEnumerator WaitForStart()
        {
            yield return new WaitForSeconds(2);
            OnArrowStart?.Invoke();
        }

        private void AssignStageVariables()
        {
            switch (GameManager.Instance.StageLevel)
            {
                case 1:
                case 2:
                    numberOfArchers = 1;
                    numberOfArrows = 2;
                    numberOfWaves = 3;
                    break;
                case 3:
                    numberOfArchers = 1;
                    numberOfArrows = 2;
                    numberOfWaves = 3;
                    break;
                case 4: 
                    numberOfWaves = 4;
                    numberOfArchers = 2;
                    numberOfArrows = 3;
                    break;
                case 5:
                case 6:
                    numberOfArchers = 2;
                    numberOfArrows = 4;
                    numberOfWaves = 4;
                    break;
                case 7:
                case 8:
                    numberOfArchers = 3;
                    numberOfArrows = 5;
                    numberOfWaves = 5;
                    break;
                case 9:
                    numberOfArchers = 3;
                    numberOfArrows = 6;
                    numberOfWaves = 5;
                    break;
                case 10:
                    numberOfArchers = 4;
                    numberOfArrows = 7;
                    numberOfWaves = 6;
                    break;
            }
        }
    }
}
