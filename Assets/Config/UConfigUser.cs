using TMPro;
using UnityEngine;
using DarkMagic;

/// <summary>
/// Optional project override for UConfig.
/// Copy this file into Assets/Config (or anywhere in Assets) and edit values here.
/// U will auto-apply these at startup if this class exists.
/// </summary>
public static class UConfigUser
{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    public static bool TRACE = false;
#else
    public static bool TRACE = false;
#endif

    public static TMP_FontAsset Font = null;

    public static Color PanelColor = new Color(0f, 0f, 0f, 0.72f);
        // public static int BorderSize = 6;
        // public static Color BorderColor = Color.white;
        // public static bool PopFadeIn = true;
        // public static bool PopFadeOut = true;
        // public static float PopFadeDuration = 0.12f;

    public static Color BorderColor = new Color(1f, 1f, 1f, 0.18f);
    public static Color TextColor = Color.white;

    // Style preset (pick one)
    public static UStylePreset StylePreset = UStylePreset.JRPG;

    // Font sizes
    // public static int BodyFontSize = 26;
    // public static int TitleFontSize = 32;
    // public static int ChoiceFontSize = 28;
    // public static int DescFontSize = 22;
    // public static int PageFontSize = 20;

    // Highlight styles
    // public static Color SelectedTextColor = Color.white;
    public static Color SelectedColor = new Color(1f, 1f, 1f, 0.12f);

    public static float BannerHeightPct = 0.07f;
    public static float DialogueHeightPct = 0.25f;
    public static float ModalWidthPct = 0.85f;
    public static float ModalHeightPct = 0.85f;

    public static int DialogueMaxCharsPerPage = 220;
        // public static int DialoguePaddingX = 40;
        // public static int DialoguePaddingY = 24;
        // public static float DialogueLineSpacing = 6f;


    public static int BannerMaxChars = 90;

    public static int HistoryMax = 50;

    // CanvasScaler reference resolution
    // public static Vector2 ReferenceResolution = new Vector2(1920, 1080);

    // Choice layout spacing
    // public static float ChoiceSpacingPx = 16f;
    // public static float ChoiceLabelSpacingPx = 12f;

    public static float DisplayMarginPx = 10f;

    public static KeyCode[] ConfirmKeys = { KeyCode.Return, KeyCode.Space };
    public static KeyCode[] CancelKeys = { KeyCode.Escape, KeyCode.Backspace, KeyCode.Mouse1 };

    public static string ConfirmButtonName = "Submit";
    public static string CancelButtonName = "Cancel";

    public static KeyCode[] UpKeys = { KeyCode.UpArrow, KeyCode.W };
    public static KeyCode[] DownKeys = { KeyCode.DownArrow, KeyCode.S };
    public static KeyCode[] LeftKeys = { KeyCode.LeftArrow, KeyCode.A };
    public static KeyCode[] RightKeys = { KeyCode.RightArrow, KeyCode.D };
}