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
    [SerializeField] private BattleEntity battleEntity;
    [SerializeField] private GameObject attackText;
    [SerializeField] private InputReader inputReader;
    
    private TextMeshProUGUI attackTextMesh;
    private CountDownTimer attackWindowTimer;
    private bool didPlayerWin;
    private bool didPlayerAttackEarly;
    private bool canPlayerAttack;

    private void OnEnable()
    {
        attackWindowTimer = new CountDownTimer(battleEntity.AttackWindow);
        attackWindowTimer.OnTimerStart += HandleBattleTimerStart;
        attackWindowTimer.OnTimerComplete += HandleBattleTimerComplete;
        attackWindowTimer.OnTimerStop += HandleBattleTimerStopped;
        inputReader.FirePressed += HandleFirePressed;
        attackTextMesh = attackText.GetComponent<TextMeshProUGUI>();  
    }

    private void OnDisable()
    {
        inputReader.FirePressed -= HandleFirePressed;
        attackWindowTimer.OnTimerStart -= HandleBattleTimerStart;
        attackWindowTimer.OnTimerComplete -= HandleBattleTimerComplete;
        attackWindowTimer.OnTimerStop -= HandleBattleTimerStopped;
    }

    IEnumerator Start()
    {
         // TODO where / how to handle enabling player input - inputReader.EnablePlayerActions();
        HandleActiveBattleText("Preparation Message 1");
        yield return new WaitForSeconds(battleEntity.FirstPrepMessageDelay);
        inputReader.EnablePlayerActions();
        canPlayerAttack = true;
        // Testing some code to randomly choose a second message or just start the battle
        if (Utilities.CoinFlip())
        {
            HandleActiveBattleText("Preparation Message 2");
   
            yield return new WaitForSeconds(battleEntity.SecondPrepMessageDelay);    
            if(!didPlayerAttackEarly)
                attackWindowTimer.Start();
        }
        else
        {
            // Really no concept of attacking early if no second prep message is shown with this current setup - maybe
            attackWindowTimer.Start();
        }
    }

    private void Update()
    {
        if (attackWindowTimer.IsRunning)
        {
            attackWindowTimer.Tick(Time.deltaTime);
        }     
    }
    
    /// <summary>
    /// Enables player input and sets the text to "Attack!"
    /// </summary>
    private void HandleBattleTimerStart()
    {
        HandleActiveBattleText("Attack!");
    }

    /// <summary>
    /// Activates the attack text and sets the text to the provided value
    /// </summary>
    /// <param name="textValue"></param>
    private void HandleActiveBattleText(string textValue)
    {
        attackText.SetActive(true);
        attackTextMesh.SetText(textValue);
    }

    /// <summary>
    /// Player pressed the fire button, check if the player attacked in the window or early
    /// </summary>
    private void HandleFirePressed()
    {
        if(!canPlayerAttack)
            return;
        
        if(attackWindowTimer.IsRunning)
        {
            HandlePlayerAttackedInWindow();
        }
        else
        {
            HandlePlayerInputEarly();
        }
    }

    // Player attacked in the window, the player should win
    private void HandlePlayerAttackedInWindow()
    {
        didPlayerWin = true;
        attackWindowTimer.Stop();
    }

    /// <summary>
    /// Player attacked early, the player should lose, the attack window timer never started
    /// </summary>
    private void HandlePlayerInputEarly()
    {
        // TODO handle when the player can attack early for a defeat
        Debug.Log("Player Attacked Early, Player should lose");
        inputReader.DisablePlayerActions();
        didPlayerWin = false;
        didPlayerAttackEarly = true;
        HandleActiveBattleText("You Lose!");
    }
    
    /// <summary>
    /// Battle time completed, the player did not attack in time and lost
    /// </summary>
    private void HandleBattleTimerComplete()
    {
        // Lose
        Debug.Log("Battle Timer Finished, Player should lose");
        inputReader.DisablePlayerActions();
        HandleActiveBattleText("You Lose!");
        didPlayerWin = false;
    }
    
    /// <summary>
    /// Battle timer stopped, the player attacked in time and won
    /// </summary>
    private void HandleBattleTimerStopped()
    {
        // Win
        Debug.Log("Battle Timer Stopped, Player should win");
        HandleActiveBattleText("You Win!");
        didPlayerWin = true;
        canPlayerAttack = false;
    }

    /// <summary>
    /// Reload the battle scene
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
}
