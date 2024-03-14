using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuelManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;

    private float timerStartTime = 10;

    private float timerValue = 0;

    private TargetSpawning targetSpawning;

    private Vector3 clickPosition;

    public LayerMask normalHitMask;
    public LayerMask critHitMask;

    private bool precisionDone;

    private int normalHits = 0;
    private int critHits = 0;
    private int misses = 0;

    private void Awake()
    {
        targetSpawning = GetComponent<TargetSpawning>();
    }


    // Start is called before the first frame update
    void Start()
    {
        targetSpawning.Spawn();

        timerText.text = timerStartTime.ToString();
        timerValue = timerStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerValue > 0)
        {
            timerValue -= Time.deltaTime;

            timerText.text = timerValue.ToString("F2");

            if(timerValue <= 0)
            {
                timerText.text = "0";

                targetSpawning.DisableSpawning();

                precisionDone = true;

                Debug.Log("Normal Hits: " + normalHits + "\n" + "Critical Hits: " + critHits + "\n" + "Misses: " + misses);
            }
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(precisionDone)
            {
                return;
            }


            Vector2 mousePos = new Vector2();

            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;

            clickPosition = Camera.main.ScreenToWorldPoint(mousePos);
            clickPosition.z = 0;


            Debug.DrawRay(clickPosition, -transform.up, Color.red, 10.0f);

            RaycastHit2D normalHit = Physics2D.Raycast(clickPosition, -transform.up, 0.005f, normalHitMask);
            RaycastHit2D critHit = Physics2D.Raycast(clickPosition, -transform.up, 0.005f, critHitMask);

            if(critHit.collider != null)
            {
                //Debug.Log("Crit Hit");
                critHits++;

            }else if(normalHit.collider != null)
            {
                //Debug.Log("Normal Hit");
                normalHits++;
            }
            else
            {
                //Debug.Log("Miss");
                misses++;
            }

            targetSpawning.Spawn();
        }
    }



}
