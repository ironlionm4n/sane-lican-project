using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BambooMingameController : MonoBehaviour
{
    private Slider strengthBar;

    [SerializeField]
    private Transform handle;

    [SerializeField]
    private Image critZoneImage;

    private Quickdraw input;
    private InputAction stop;

    private bool increasing = true; //Controls if the bar moves up (increasing values) or down (decreasing values)

    private float barSpeed = 250f; //How fast the bar moves up and down

    private float[] critZone = new float[2]; //The value spread on the slider that is considered a crit (should be ordered LOWEST, HIGHEST value)

    private float[] winZone = new float[2]; //The value spread on the slider that is considered a win (should be ordered LOWEST, HIGHEST value)

    private bool stopped = false; //If the player has tapped the screen to stop the bar or not

    private int critZoneSpread = 10; //The spread of how many values the crit zone is

    private int winZoneSpread = 30; //The spread of how many values the win zone is

    [SerializeField]
    private float winZoneMidVal; //The value that is the center of the win zone. All sizing and the crit zone values are based on this value

    private float critImageToSpreadProportion = 6.85f; //Used for sizing the crit zone based on crit spread. Get value from PrintCritImage logging method


    //Win vairables
    [SerializeField]
    private bool criticalWin = false;

    [SerializeField]
    private bool normalWin = false;

    [SerializeField]
    private float selectedValue = -1f; //The value of the slider after the player stops the bar

    private void Awake()
    {
        strengthBar = transform.GetChild(0).GetComponent<Slider>(); //First child gameobject should be the slider
        input = new Quickdraw();
        strengthBar.value = 0;


        //Logging code
        //PrintCritImageToZoneSizeProportion();

    }

    private void OnEnable()
    {
        stop = input.Player.Fire;
        stop.Enable();
    }

    private void OnDisable()
    {
        stop.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (stop.ReadValue<float>() > 0)
        {
            stopped = true;
        }

        if (!stopped)
        {
            if (increasing)
            {
                strengthBar.value += barSpeed * Time.deltaTime; //Increase the slider value based on the bar speed 

                if(strengthBar.value == strengthBar.maxValue) //If the slider value reaches the max, switch to decreasing
                {
                    increasing = false;
                }
            }
            else
            {
                strengthBar.value -= barSpeed * Time.deltaTime; //Decrease the slider value based on the bar speed

                if( strengthBar.value == strengthBar.minValue ) //If the slider value reaches the min, switch to decreasing
                { 
                    increasing = true; 
                }
            }
        }
        else
        {
            selectedValue = strengthBar.value;
           CheckForWin();
        }
    }

    private void CheckForWin()
    {
        if(selectedValue <= critZone[1] && selectedValue >= critZone[0])
        {
            criticalWin = true;
            return;
        }

        if(selectedValue <= winZone[1] && selectedValue >= winZone[0])
        {
            normalWin = true;
            return;
        }
    }

    /// <summary>
    /// Chooses a random starting point for the win and crit zones.
    /// </summary>
    private void ChooseRandomZonePoint()
    {
        //Keeps the win zone always within the confines of the bar
        int maxPosValue = 100 - winZoneSpread/2; 
        int minPosValue = 0 + winZoneSpread / 2;

        winZoneMidVal = Random.Range( minPosValue, maxPosValue );

        //Sets all the values in the crit and win zone arrays
        critZone[0] = winZoneMidVal - critZoneSpread/2;
        critZone[1] = winZoneMidVal + critZoneSpread/2;

        winZone[0] = winZoneMidVal - winZoneSpread / 2;
        winZone[1] = winZoneMidVal + winZoneSpread / 2;

        MoveZones();
    }

    private void MoveZones()
    {

        strengthBar.value = winZoneMidVal;

        critZoneImage.transform.parent.SetParent(handle.transform);
       
        critZoneImage.transform.parent.localPosition = Vector3.zero;

        critZoneImage.transform.parent.SetParent(handle.parent);

        handle.SetAsLastSibling(); //Makes it so the handle appears above the zones

        ResizeCrit();

    }

    private void ResizeCrit()
    {
        //Temporary Testing
        critZoneSpread = Random.Range(2, 10);

        critZoneImage.rectTransform.sizeDelta = new Vector2(100, critZoneSpread * critImageToSpreadProportion);
    }

    public void ResetGame()
    {
        stopped = false;
        criticalWin = false;
        normalWin = false;

        ChooseRandomZonePoint();

        strengthBar.value = 0;
    }

    private void PrintCritImageToZoneSizeProportion()
    {
        Debug.Log(critZoneImage.rectTransform.rect.height/critZoneSpread);
    }
}
