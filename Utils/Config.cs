using UnityEngine;
using BepInEx.Configuration;

namespace SSMPEssentials.Utils
{

    internal static class Config
    {
        public const uint SSMPApiVersion = 1;
        public const string ModName = "SSMP Essentials";
        public const string Version = "0.1.4";

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

        static ConfigEntry<float>? _quickTeleportHoldSeconds;
        public static float QuickTeleportHoldSeconds => Mathf.Max(0f, _quickTeleportHoldSeconds?.Value ?? 3f);

        static ConfigEntry<bool>? _quickTeleportDpad;
        public static bool QuickTeleportDpad => _quickTeleportDpad?.Value ?? true;

        static ConfigEntry<KeyCode>? _acceptTeleportKey;
        public static KeyCode AcceptTeleportKey => _acceptTeleportKey?.Value ?? KeyCode.None;

        static ConfigEntry<bool>? _acceptTeleportDpad;
        public static bool AcceptTeleportDpad => _acceptTeleportDpad?.Value ?? true;

        static ConfigEntry<float>? _tripleTapWindow;
        public static float TripleTapWindow => Mathf.Max(0.05f, _tripleTapWindow?.Value ?? 0.4f);

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
            _quickTeleportHoldSeconds = config.Bind("Keybinds", "Quick Teleport Hold Seconds", 3f,
                "How long the Quick Teleport key must be held before it activates. 0 = instant.");
            _quickTeleportDpad = config.Bind("Keybinds", "Quick Teleport D-pad Triple Tap", true,
                "Controller: quickly tap D-pad Down three times to Quick Teleport.");
            _acceptTeleportKey = config.Bind("Keybinds", "Accept Teleport", KeyCode.Alpha7,
                "Accepts the newest teleport request (same as /tpa).");
            _acceptTeleportDpad = config.Bind("Keybinds", "Accept Teleport D-pad Triple Tap", true,
                "Controller: quickly tap D-pad Up three times to accept the newest teleport request.");
            _tripleTapWindow = config.Bind("Keybinds", "Triple Tap Window", 0.4f,
                "Maximum seconds between D-pad taps for them to count as one triple tap.");
            _defaultColor = config.Bind("Username Colors", "Default Color", Color.white);
        }
    }
}
