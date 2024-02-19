using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Quickdraw;

namespace Project.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Quickdraw/Input/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public UnityAction FirePressed = delegate {  };
        private Quickdraw quickDraw;
        
        private void OnEnable()
        {
            if (quickDraw == null)
            {
                quickDraw = new Quickdraw();
                quickDraw.Player.SetCallbacks(this);
            }
        }

        public void EnablePlayerActions()
        {
            quickDraw.Enable();
        }
        
        public void DisablePlayerActions()
        {
            quickDraw.Disable();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                FirePressed.Invoke();
            }
        }

        public void OnOne_Tap(InputAction.CallbackContext context)
        {

        }
        
    }
}
