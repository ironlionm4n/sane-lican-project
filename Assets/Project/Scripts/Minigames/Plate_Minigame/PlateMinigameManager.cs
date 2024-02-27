using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Linq;

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

    private float[] speedsOriginals = new float[3] { 0.5f, 1.3f, 2 };

    private Pattern[] wave1 = new Pattern[3];

    private Pattern straight = new Pattern(0, 0, 0.5f);
    private Pattern midLob = new Pattern(3, -22.5f, 1.3f);
    private Pattern highLob = new Pattern(5, -45, 2);

    private float rotationDuration = 0.3f;

    private int numberOfShots = 3;

    private List<Plate> currentPlates = new List<Plate>();

    [SerializeField]
    private Transform straightEndTarget;

    [SerializeField]
    private Transform lobEndTarget;

    [SerializeField]
    private Transform highLobTarget;

    // Start is called before the first frame update
    void Awake()
    {
        input = new Quickdraw();
        plateSpawnPoint = plateLauncher.transform.GetChild(0);

        wave1 = new Pattern[] { straight, highLob, straight };

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

    public void Swing()
    {
        if(currentPlates.Count > 0)
        {
            currentPlates.RemoveAll(x => x == null);
           
            Plate plate = currentPlates.OrderByDescending(x => x.percentTraveled).FirstOrDefault(); 

            if (plate != null)
            {
                plate.Hit();
            }
        }
    }

    private IEnumerator Fire()
    {

        yield return new WaitForSeconds(2);

        foreach(var pat in wave1)
        {
            GameObject newPlate = Instantiate(platePrefab, plateSpawnPoint.position, Quaternion.identity);
            currentPlates.Add(newPlate.GetComponent<Plate>());

            Transform endTarget = null;

            if (pat.angle == 0)
            {
                endTarget = straightEndTarget;
            }
            else if (pat.angle == -22.5f)
            {
                endTarget = lobEndTarget;
            }
            else
            {
                endTarget = highLobTarget;
            }

            newPlate.GetComponent<Plate>().StartProjectile(pat.speed, pat.height, endTarget);
            RotateLauncher(pat.angle);

            yield return new WaitForSeconds(delayBetweenShots);
        }

        RotateLauncher(0);
    }

    private void RotateLauncher(float angleIdentifier)
    {
        plateLauncher.DORotate(new Vector3(0, 0, angleIdentifier), rotationDuration);
    }
}

struct Pattern
{
    public float speed;
    public float height;
    public float angle;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="height"></param>
    /// <param name="angle"></param>
    /// <param name="speed"></param>
    public Pattern(float height, float angle, float speed) 
    {
        this.speed = speed;
        this.height = height;
        this.angle = angle;
    }


}
