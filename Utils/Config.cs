using UnityEngine;
using BepInEx.Configuration;

namespace SSMPEssentials.Utils
{

    internal static class Config
    {
        public const uint SSMPApiVersion = 1;
        public const string ModName = "SSMP Essentials";
        public const string Version = "0.1.3";

        static ConfigEntry<bool>? _testsEnabled;
        public static bool TestsEnabled => _testsEnabled?.Value ?? false;

        static ConfigEntry<KeyCode>? _spectateNext;
        public static KeyCode SpectateNext => _spectateNext?.Value ?? KeyCode.None;

        static ConfigEntry<KeyCode>? _spectatePrevious;
        public static KeyCode SpectatePrevious => _spectatePrevious?.Value ?? KeyCode.None;

        static ConfigEntry<KeyCode>? _exitSpectate;
        public static KeyCode ExitSpectate => _exitSpectate?.Value ?? KeyCode.None;

        static ConfigEntry<KeyCode>? _freecamToggle;
        public static KeyCode FreecamToggle => _freecamToggle?.Value ?? KeyCode.None;

        static ConfigEntry<KeyCode>? _quickTeleportKey;
        public static KeyCode QuickTeleportKey => _quickTeleportKey?.Value ?? KeyCode.None;

        static ConfigEntry<KeyCode>? _quickTeleportButton;
        public static KeyCode QuickTeleportButton => _quickTeleportButton?.Value ?? KeyCode.None;

        static ConfigEntry<float>? _quickTeleportHoldSeconds;
        public static float QuickTeleportHoldSeconds => Mathf.Max(0f, _quickTeleportHoldSeconds?.Value ?? 3f);

        static ConfigEntry<Color>? _defaultColor;

        public static Color DefaultColor => _defaultColor?.Value ?? Color.white;

        public static void Init(ConfigFile config)
        {
#if DEBUG
            _testsEnabled = config.Bind("Misc", "Run Tests", false);
#endif
            _spectatePrevious = config.Bind("Keybinds", "Spectate Previous Player", KeyCode.Alpha2);
            _spectateNext = config.Bind("Keybinds", "Spectate Next Player", KeyCode.Alpha3);
            _exitSpectate = config.Bind("Keybinds", "Exit Spectate Mode", KeyCode.Alpha4);
            _freecamToggle = config.Bind("Keybinds", "Toggle Freecam Mode", KeyCode.Alpha5);
            _quickTeleportKey = config.Bind("Keybinds", "Quick Teleport", KeyCode.Alpha6,
                "Teleports to the other player if exactly one is connected, otherwise lists players in chat.");
            _quickTeleportButton = config.Bind("Keybinds", "Quick Teleport (Controller)", KeyCode.JoystickButton6,
                "Controller button for Quick Teleport (hold). Default JoystickButton6 = View/Back on an Xbox pad. Set to None to disable.");
            _quickTeleportHoldSeconds = config.Bind("Keybinds", "Quick Teleport Hold Seconds", 3f,
                "How long the Quick Teleport key/button must be held before it activates. 0 = instant.");
            _defaultColor = config.Bind("Username Colors", "Default Color", Color.white);
        }
    }
}
