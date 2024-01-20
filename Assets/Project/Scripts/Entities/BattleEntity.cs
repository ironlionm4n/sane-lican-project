using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleEntity", menuName = "Quickdraw/Entities/BattleEntity")]
public class BattleEntity : ScriptableObject
{
    [SerializeField] float firstPrepMessageDelay;
    public float FirstPrepMessageDelay => firstPrepMessageDelay;

    [SerializeField]
    AnimatorController animatorController;
    public AnimatorController AnimatorController => animatorController;
    
    [SerializeField] float secondPrepMessageDelay;
    public float SecondPrepMessageDelay => secondPrepMessageDelay;
    
    [SerializeField] float attackWindow;
    public float AttackWindow => attackWindow;
}
