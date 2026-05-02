using UnityEngine;

namespace MalumMenu;

public class PassiveTab : ITab
{
    public string name => "Passive";

    // Tracks whether we are currently typing in the box
    private static bool _isEditingLevel = false;

    public void Draw()
    {
        GUILayout.BeginVertical(GUILayout.Width(MenuUI.windowWidth * 0.425f));

        DrawGeneral();

        GUILayout.EndVertical();
    }

    private void DrawGeneral()
    {
        // --- NEW SPOOFING SECTION (BYPASSING STRIPPED TEXTFIELD) ---
        GUILayout.Label("Spoofing", GUIStylePreset.TabSubtitle);
        
        GUILayout.BeginHorizontal();
        GUILayout.Label(" Fake Level: ", GUILayout.Width(100f));

        string currentLevel = MalumMenu.spoofLevel.Value;

        if (_isEditingLevel)
        {
            // Draw a box that looks like an active text field with a cursor (_)
            if (GUILayout.Button(currentLevel + "_", GUI.skin.box, GUILayout.Width(100f)))
            {
                _isEditingLevel = false; // Click again to stop editing
            }

            // Capture raw keyboard events to build our own text field
            Event e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter || e.keyCode == KeyCode.Escape)
                {
                    _isEditingLevel = false;
                }
                else if (e.keyCode == KeyCode.Backspace && currentLevel.Length > 0)
                {
                    MalumMenu.spoofLevel.Value = currentLevel.Substring(0, currentLevel.Length - 1);
                }
                else if (char.IsDigit(e.character) && currentLevel.Length < 6)
                {
                    MalumMenu.spoofLevel.Value += e.character;
                }
            }
        }
        else
        {
            // Standard box to start editing
            string displayStr = string.IsNullOrEmpty(currentLevel) ? "Click to set" : currentLevel;
            if (GUILayout.Button(displayStr, GUI.skin.box, GUILayout.Width(100f)))
            {
                _isEditingLevel = true;
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        // --- ORIGINAL TOGGLES ---
        GUILayout.Label("General", GUIStylePreset.TabSubtitle);

        CheatToggles.freeCosmetics = GUILayout.Toggle(CheatToggles.freeCosmetics, " Free Cosmetics");

        CheatToggles.avoidPenalties = GUILayout.Toggle(CheatToggles.avoidPenalties, " Avoid Penalties");

        CheatToggles.unlockFeatures = GUILayout.Toggle(CheatToggles.unlockFeatures, " Unlock Extra Features");

        CheatToggles.copyLobbyCodeOnDisconnect = GUILayout.Toggle(CheatToggles.copyLobbyCodeOnDisconnect, " Copy Lobby Code on Disconnect");

        CheatToggles.spoofAprilFoolsDate = GUILayout.Toggle(CheatToggles.spoofAprilFoolsDate, " Spoof Date to April 1st");
    }
}