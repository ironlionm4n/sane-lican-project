using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class PlateMinigameManager : MonoBehaviour
{
    private Quickdraw input;
    private InputAction press;

    [SerializeField]
    GameObject platePrefab;

    [SerializeField]
    Transform plateLauncher;

    Transform plateSpawnPoint;

    private float delayBetweenShots = 0.4f;

    private float currentZAngle = 0f; //Current angle the shot put is set at

    private float currentHeight = 0f; //Current height value for the plate's arc

    private float currentSpeed = 0f; //Current speed value for how fast the plate moves

    private float[] heights = new float[3] { 0, 3, 5 };

    private float[] angles = new float[3] { 0, -22.5f, -45 };

    private float[] speeds = new float[3] { 0.5f, 1.3f, 2 };

    private float rotationDuration = 0.3f;

    private int numberOfShots = 3;

    private Queue<Plate> currentPlates = new Queue<Plate>();


    // Start is called before the first frame update
    void Start()
    {
        input = new Quickdraw();
        plateSpawnPoint = plateLauncher.transform.GetChild(0);
        StartCoroutine(Fire());

    }

    private void OnEnable()
    {
        press = input.Player.One_Tap;
        press.Enable();
    }

    private void OnDisable()
    {
        press.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (press.WasPerformedThisFrame())
        {
            Swing();
        }
    }

    private void Swing()
    {
        if(currentPlates.Count > 0)
        {
            Plate plate = currentPlates.Dequeue();
            plate.Hit();
        }
    }

    private IEnumerator Fire()
    {
        for(int i = 0; i < numberOfShots; i++)
        {
            GameObject newPlate = Instantiate(platePrefab, plateSpawnPoint.position, Quaternion.identity);
            currentPlates.Enqueue(newPlate.GetComponent<Plate>());
           
            newPlate.GetComponent<Plate>().StartProjectile(speeds[i], heights[i]);
            RotateLauncher(i);

            yield return new WaitForSeconds(delayBetweenShots);
        }

        RotateLauncher(0);
    }

    private void RotateLauncher(int angleIdentifier)
    {
        if(angleIdentifier == 0)
        {
            plateLauncher.DORotate(new Vector3(0, 0, angles[0]), rotationDuration);
        }
        else if(angleIdentifier == 1)
        {
            plateLauncher.DORotate(new Vector3(0, 0, angles[1]), rotationDuration);
        }
        else if (angleIdentifier == 2)
        {
            plateLauncher.DORotate(new Vector3(0, 0, angles[2]), rotationDuration);
        }
    }
}
