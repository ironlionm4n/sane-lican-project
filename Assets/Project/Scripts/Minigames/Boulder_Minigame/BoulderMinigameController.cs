using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BoulderMinigameController : MonoBehaviour
{
    private Slider strengthBar;

    private Quickdraw input;
    private InputAction press;

    [SerializeField]
    private float pressAmount = 5f; //How much the bar goes up when the user presses the screen

    [SerializeField]
    private float decreaseAmount = 10f; //How much the bar decreaes naturally

    private void Awake()
    {
        strengthBar = transform.GetChild(0).GetComponent<Slider>(); //First child gameobject should be the slider
        input = new Quickdraw();
        strengthBar.value = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(press.WasPerformedThisFrame())
        {
            strengthBar.value += pressAmount;
        }

        strengthBar.value -= decreaseAmount;

    }

    private void LateUpdate()
    {

    }
}
