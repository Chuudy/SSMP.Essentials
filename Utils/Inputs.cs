using InControl;
using UnityEngine;
using SSMPEssentials.Client.Modules;

namespace SSMPEssentials.Utils
{
    internal static class Inputs
    {

        static float MovementAmount(PlayerAction action, PlayerAction inverseAction)
        {
            var deadZone = 0.1f;
            if (action.Value > deadZone)
            {
                return action.Value;
            }
            else if (inverseAction > deadZone)
            {
                return 0 - inverseAction.Value;
            }
            else
            {
                return 0;
            }
        }

        static void FreecamMovement()
        {
            var distance = 6f;
            var controller = InputHandler.Instance.inputActions;
            
            if (controller.Dash.IsPressed) distance = 2f;

            Spectate.FreecamMovementVector.x = MovementAmount(controller.Right, controller.Left) / distance;
            Spectate.FreecamMovementVector.y = MovementAmount(controller.Up, controller.Down) / distance;
        }
        static readonly TapCounter dpadDownTaps = new(3);
        static readonly TapCounter dpadUpTaps = new(3);

        static void DpadShortcuts()
        {
            // Only during gameplay, so D-pad navigation in menus/inventory doesn't count.
            var hero = HeroController.instance;
            if (hero == null || !hero.acceptingInput)
            {
                dpadDownTaps.Reset();
                dpadUpTaps.Reset();
                return;
            }

            var device = InputManager.ActiveDevice;

            if (dpadDownTaps.Register(device.DPadDown.WasPressed) && Config.QuickTeleportDpad)
            {
                QuickTeleport.Trigger();
            }

            // Stay silent when nothing is pending, since Up is also look/interact.
            if (dpadUpTaps.Register(device.DPadUp.WasPressed) && Config.AcceptTeleportDpad && TeleportRequests.HasPending)
            {
                TeleportRequests.RespondToRequest(true);
            }
        }

        public static void Update()
        {
            var chatOpen = Client.Client.api?.UiManager.ChatBox.IsOpen ?? false;

            // Hold-to-activate; typing in chat or releasing the key resets the timer.
            QuickTeleport.UpdateHold(!chatOpen && Input.GetKey(Config.QuickTeleportKey));

            if (chatOpen) return;

            if (Input.GetKeyDown(Config.AcceptTeleportKey)) TeleportRequests.RespondToRequest(true);
            DpadShortcuts();

            if (Input.GetKeyDown(Config.SpectateNext)) Spectate.FocusOnPlayer(Spectate.MoveDir.Next);
            else if (Input.GetKeyDown(Config.SpectatePrevious)) Spectate.FocusOnPlayer(Spectate.MoveDir.Prev);
            else if (Input.GetKeyDown(Config.ExitSpectate)) Spectate.ReturnToSelf();
            else if (Input.GetKeyDown(Config.FreecamToggle))
            {
                if (Spectate.freecam) Spectate.ReturnToSelf();
                else Spectate.Freecam();
            }

            if (Spectate.freecam)
            {
                FreecamMovement();
            }
        }
    }
}
