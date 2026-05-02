using UnityEngine;

namespace MalumMenu;

public class HostOnlyTab : ITab
{
    public string name => "Host-Only";

    public void Draw()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.Width(MenuUI.windowWidth * 0.425f));

        DrawGeneral();

        GUILayout.Space(15);

        DrawMurder();

        GUILayout.Space(15);

        DrawGameState();

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        DrawMeetings();

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private static bool _isEditingMaxPlayers = false;

    private void DrawGeneral()
    {
        // --- CUSTOM MAX PLAYERS OVERRIDE ---
        GUILayout.Label("Lobby Settings", GUIStylePreset.TabSubtitle);
        
        GUILayout.BeginHorizontal();
        GUILayout.Label(" Max Players: ", GUILayout.Width(100f));

        string currentMax = MalumMenu.customMaxPlayers.Value;

        if (_isEditingMaxPlayers)
        {
            if (GUILayout.Button(currentMax + "_", GUI.skin.box, GUILayout.Width(100f)))
            {
                _isEditingMaxPlayers = false;
            }

            Event e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter || e.keyCode == KeyCode.Escape)
                {
                    _isEditingMaxPlayers = false;
                }
                else if (e.keyCode == KeyCode.Backspace && currentMax.Length > 0)
                {
                    MalumMenu.customMaxPlayers.Value = currentMax.Substring(0, currentMax.Length - 1);
                }
                else if (char.IsDigit(e.character) && currentMax.Length < 3) // Max 999 players!
                {
                    MalumMenu.customMaxPlayers.Value += e.character;
                }
            }
        }
        else
        {
            string displayStr = string.IsNullOrEmpty(currentMax) ? "15" : currentMax;
            if (GUILayout.Button(displayStr, GUI.skin.box, GUILayout.Width(100f)))
            {
                _isEditingMaxPlayers = true;
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        // --- ORIGINAL TOGGLES ---
        GUILayout.Label("General", GUIStylePreset.TabSubtitle);

        CheatToggles.killVanished = GUILayout.Toggle(CheatToggles.killVanished, " Kill While Vanished");

        CheatToggles.killAnyone = GUILayout.Toggle(CheatToggles.killAnyone, " Kill Anyone");

        CheatToggles.noKillCd = GUILayout.Toggle(CheatToggles.noKillCd, " No Kill Cooldown");

        CheatToggles.showProtectMenu = GUILayout.Toggle(CheatToggles.showProtectMenu, " Show Protect Menu");
    }

    private void DrawMurder()
    {
        GUILayout.Label("Murder", GUIStylePreset.TabSubtitle);

        CheatToggles.killPlayer = GUILayout.Toggle(CheatToggles.killPlayer, " Kill Player");

        CheatToggles.telekillPlayer = GUILayout.Toggle(CheatToggles.telekillPlayer, " Telekill Player");

        CheatToggles.killAllCrew = GUILayout.Toggle(CheatToggles.killAllCrew, " Kill All Crewmates");

        CheatToggles.killAllImps = GUILayout.Toggle(CheatToggles.killAllImps, " Kill All Impostors");

        CheatToggles.killAll = GUILayout.Toggle(CheatToggles.killAll, " Kill Everyone");
    }

    private void DrawGameState()
    {
        GUILayout.Label("Game State", GUIStylePreset.TabSubtitle);

        CheatToggles.forceStartGame = GUILayout.Toggle(CheatToggles.forceStartGame, " Force Start Game");

        CheatToggles.noGameEnd = GUILayout.Toggle(CheatToggles.noGameEnd, " No Game End");
    }

    private void DrawMeetings()
    {
        GUILayout.Label("Meetings", GUIStylePreset.TabSubtitle);

        CheatToggles.skipMeeting = GUILayout.Toggle(CheatToggles.skipMeeting, " Skip Meeting");

        CheatToggles.voteImmune = GUILayout.Toggle(CheatToggles.voteImmune, " Vote Immune");

        CheatToggles.ejectPlayer = GUILayout.Toggle(CheatToggles.ejectPlayer, " Eject Player");
    }
}
