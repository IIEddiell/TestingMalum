using UnityEngine;

namespace MalumMenu;

public class PassiveTab : ITab
{
    public string name => "Passive";

    public void Draw()
    {
        GUILayout.BeginVertical(GUILayout.Width(MenuUI.windowWidth * 0.425f));

        DrawGeneral();

        GUILayout.EndVertical();
    }

    private void DrawGeneral()
    {
        // --- NEW SPOOFING SECTION ---
        GUILayout.Label("Spoofing", GUIStylePreset.TabSubtitle);
        
        GUILayout.BeginHorizontal();
        GUILayout.Label(" Fake Level: ", GUILayout.Width(100f));
        MalumMenu.spoofLevel.Value = GUILayout.TextField(MalumMenu.spoofLevel.Value, 6, GUILayout.Width(100f));
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
