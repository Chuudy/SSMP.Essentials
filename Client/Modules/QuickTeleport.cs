using System;
using System.Linq;
using UnityEngine;
using SSMPEssentials.Utils;

namespace SSMPEssentials.Client.Modules
{
    /// <summary>
    /// One-button teleport: if exactly one other player is connected, sends a teleport
    /// request to them (same as /tp). Otherwise lists the connected players in chat.
    /// </summary>
    internal static class QuickTeleport
    {
        static float _heldFor;
        static bool _firedThisHold;

        /// <summary>
        /// Call every frame. Fires <see cref="Trigger"/> once the key has been held for
        /// <see cref="Config.QuickTeleportHoldSeconds"/>; releasing the key re-arms it.
        /// </summary>
        public static void UpdateHold(bool held)
        {
            if (!held)
            {
                _heldFor = 0f;
                _firedThisHold = false;
                return;
            }

            if (_firedThisHold) return;

            // Unscaled so slow-motion/pause effects don't change the hold time.
            _heldFor += Time.unscaledDeltaTime;
            if (_heldFor >= Config.QuickTeleportHoldSeconds)
            {
                _firedThisHold = true;
                Trigger();
            }
        }

        public static void Trigger()
        {
            var api = Client.api;
            if (api == null || !api.NetClient.IsConnected) return;

            if (!Client.ServerSettings.TeleportsEnabled)
            {
                Client.LocalChat("Teleports are currently disabled.");
                return;
            }

            var localName = api.ClientManager.Username;
            var others = api.ClientManager.Players
                .Where(p => !string.Equals(p.Username, localName, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(p => p.Username, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            switch (others.Count)
            {
                case 0:
                    Client.LocalChat("No other players are connected.");
                    return;

                case 1:
                    var target = others[0];
                    Client.LocalChat($"Teleport request sent to {Common.ColoredUsername(target)}.");
                    PacketSender.SendTeleportRequest(target.Id);
                    return;

                default:
                    Client.LocalChat($"{others.Count} other players connected:");
                    for (var i = 0; i < others.Count; i++)
                    {
                        Client.LocalChat($"  {i + 1}. {Common.ColoredUsername(others[i])}");
                    }
                    Client.LocalChat("Use /tp <username> to pick one.");
                    return;
            }
        }
    }
}
