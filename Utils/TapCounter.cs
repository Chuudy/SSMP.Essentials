using UnityEngine;

namespace SSMPEssentials.Utils
{
    /// <summary>
    /// Detects N quick presses in a row. Each tap must follow the previous one within
    /// <see cref="Config.TripleTapWindow"/> seconds, otherwise counting starts over.
    /// </summary>
    internal sealed class TapCounter(int requiredTaps)
    {
        int _count;
        float _lastTapTime;

        /// <summary>Call every frame. Returns true on the frame the final tap lands.</summary>
        public bool Register(bool pressedThisFrame)
        {
            if (!pressedThisFrame) return false;

            var now = Time.unscaledTime;
            if (_count > 0 && now - _lastTapTime > Config.TripleTapWindow) _count = 0;

            _count++;
            _lastTapTime = now;

            if (_count < requiredTaps) return false;

            _count = 0;
            return true;
        }

        public void Reset() => _count = 0;
    }
}
