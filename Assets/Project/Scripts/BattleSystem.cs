using System;
using System.Collections;
using System.Collections.Generic;
using Project.Input;
using Project.Scripts.Utilities;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private float startDelay;
    [SerializeField] private float attackWindow;
    [SerializeField] private GameObject attackText;
    [SerializeField] private InputReader inputReader;
    
    private TextMeshProUGUI attackTextMesh;
    private CountDownTimer battleCountdownTimer;
    
    private void OnEnable()
    {
        battleCountdownTimer = new CountDownTimer(attackWindow);
        battleCountdownTimer.OnTimerStart += HandleBattleTimerStart;
        battleCountdownTimer.OnTimerComplete += HandleBattleTimerComplete;
        battleCountdownTimer.OnTimerStop += HandleBattleTimerStopped;
        inputReader.FirePressed += HandleFirePressed;
        attackTextMesh = attackText.GetComponent<TextMeshProUGUI>();  
    }

    private void OnDisable()
    {
        inputReader.FirePressed -= HandleFirePressed;
        battleCountdownTimer.OnTimerStart -= HandleBattleTimerStart;
        battleCountdownTimer.OnTimerComplete -= HandleBattleTimerComplete;
        battleCountdownTimer.OnTimerStop -= HandleBattleTimerStopped;
    }

    IEnumerator Start()
    {
        HandleActiveBattleText("Breathe...");
        yield return new WaitForSeconds(startDelay);
        battleCountdownTimer.Start();
    }

    private void Update()
    {
        if (battleCountdownTimer.IsRunning)
        {
            battleCountdownTimer.Tick(Time.deltaTime);
        }     
    }
    
    private void HandleBattleTimerStart()
    {
        inputReader.EnablePlayerActions();
        HandleActiveBattleText("Attack!");
    }

    private void HandleActiveBattleText(string textValue)
    {
        attackText.SetActive(true);
        attackTextMesh.SetText(textValue);
    }

    private void HandleFirePressed()
    {
        battleCountdownTimer.Stop();
    }
    
    private void HandleBattleTimerComplete()
    {
        // Lose
        Debug.Log("Battle Timer Finished, Player should lose");
        inputReader.DisablePlayerActions();
        HandleActiveBattleText("You Lose!");
    }
    
    private void HandleBattleTimerStopped()
    {
        // Won
        Debug.Log("Battle Timer Stopped, Player Attacked In Time And Won!");
        inputReader.DisablePlayerActions();
        HandleActiveBattleText("You Win!");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
}
