using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawning : MonoBehaviour
{

    [SerializeField]
    private GameObject targetPrefab;

    [SerializeField]
    private float targetOffset;

    [SerializeField]
    private Transform targetSpawnArea;

    private Transform spawnAreaTop;
    private Transform spawnAreaBottom;
    private Transform spawnAreaLeft;
    private Transform spawnAreaRight;

    GameObject lastSpawnedTarget;

    private bool canSpawnTargets;

    private void Awake()
    {
        spawnAreaRight = targetSpawnArea.GetChild(0);
        spawnAreaLeft = targetSpawnArea.GetChild(1);
        spawnAreaTop = targetSpawnArea.GetChild(2);
        spawnAreaBottom = targetSpawnArea.GetChild(3);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        if(!canSpawnTargets)
        {
            return;
        }

        Vector2 spawnPosition = CreateRandomSpawnLocation();

        if (lastSpawnedTarget != null)
        {

            //Make sure the targets are offset by a certain value
            while (Vector2.Distance(spawnPosition, lastSpawnedTarget.transform.position) < targetOffset)
            {
                spawnPosition = CreateRandomSpawnLocation();
            }
        }

        Destroy(lastSpawnedTarget);
        lastSpawnedTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 CreateRandomSpawnLocation()
    {
        float randomX = Random.Range(spawnAreaLeft.position.x, spawnAreaRight.position.x);
        float randomY = Random.Range(spawnAreaBottom.position.y, spawnAreaTop.position.y);

        return new Vector2(randomX, randomY);
    }

    public void DisableSpawning()
    {
        canSpawnTargets = false;
        Destroy(lastSpawnedTarget);
    }
}
