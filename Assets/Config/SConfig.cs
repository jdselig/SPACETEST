using DarkMagic;
using UnityEngine;

/// <summary>
/// SConfig: one obvious place to configure the V-style state machine.
/// Import this sample via Package Manager → V → Samples → Import "VStateConfig".
/// </summary>
public static class SConfig
{
    public const bool TRACE = true; // logs transitions
    public const bool WARNINGS = true; // logs common beginner mistakes

    private static bool AllowedNow =>
#if UNITY_EDITOR
        true;
#else
        Debug.isDebugBuild;
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        VStateMachine.Trace = TRACE && AllowedNow;
        VStateMachine.Warnings = WARNINGS && AllowedNow;

        if (VStateMachine.Trace)
            Debug.Log("<color=#7CFFB2><b>[S]</b> Trace enabled (SConfig)</color>");
    }
}

/// <summary>
/// Define your state groups + states here.
/// Recommended pattern:
/// - group classes: Player, Enemy, UI, Game, etc.
/// - states: small empty structs nested in the group
/// </summary>
public static partial class S
{
    public static class Player
    {
        public readonly struct Idle { }

        public readonly struct Running { }

        public readonly struct Jumping { }

        public readonly struct Falling { }

        public readonly struct Stunned { }
    }

    public static class Enemy
    {
        public readonly struct Idle { }

        public readonly struct Patrol { }

        public readonly struct Chasing { }

        public readonly struct Attacking { }
    }

    public static class Boss
    {
        public readonly struct Idle { }

        public readonly struct Waiting { }

        public readonly struct Patrol { }

        public readonly struct Chasing { }

        public readonly struct Attacking { }

        public readonly struct Enraged { }
    }
}
