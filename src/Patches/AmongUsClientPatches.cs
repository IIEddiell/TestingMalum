using HarmonyLib;

namespace MalumMenu;

[HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.Update))]
public static class AmongUsClient_Update
{
    public static void Postfix()
    {
        MalumSpoof.SpoofLevel();

        // GuestMode cheats are commented out as they are broken in latest updates

        // Code to treat temp accounts the same as full accounts, including access to friend codes
        // if (!EOSManager.Instance.loginFlowFinished || !MalumMenu.guestMode.Value) return;
        // DataManager.Player.Account.LoginStatus = EOSManager.AccountLoginStatus.LoggedIn;

        // if (!string.IsNullOrWhiteSpace(EOSManager.Instance.FriendCode)) return;
        // var friendCode = MalumSpoof.spoofFriendCode();
        // var editUsername = EOSManager.Instance.editAccountUsername;
        // editUsername.UsernameText.SetText(friendCode);
        // editUsername.SaveUsername();
        // EOSManager.Instance.FriendCode = friendCode;
    }
}

[HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameJoined))]
public static class AmongUsClient_OnGameJoined
{
    // Postfix patch of AmongUsClient.OnGameJoined to store the last joined game ID string
    public static string lastGameIdString = "";

    public static void Postfix(string gameIdString)
    {
        lastGameIdString = gameIdString;
    }
}


public static class InnerNetClient_HostGame_Patch
{
    // This intercepts the HostGame packet right before it leaves your PC
    public static void Prefix(AmongUs.GameOptions.IGameOptions settings)
    {
        if (int.TryParse(MalumMenu.customMaxPlayers.Value, out int max))
        {
            // Override the game's UI slider with our custom value
            settings.MaxPlayers = max;
            MalumMenu.Log.LogInfo($"[LOBBY OVERRIDE] Hosting game with {max} Max Players!");
        }
    }
}[HarmonyPatch(typeof(GameData), nameof(GameData.GetAvailableId))]
public static class GameData_GetAvailableId_Patch
{
    // The Hidden Color Crash Boss:
    // Vanilla Among Us only has 18 colors. If player 19 joins, GetAvailableId returns -1 and the game crashes.
    // This patch says: "If we are out of colors, just start handing out random duplicate colors."
    public static bool Prefix(ref sbyte __result)
    {
        if (GameData.Instance && GameData.Instance.AllPlayers.Count >= 18)
        {
            __result = (sbyte)UnityEngine.Random.Range(0, 18); // Assign a random color ID from 0 to 17
            return false; // Skip the original method so it doesn't crash
        }
        return true; // Run normally if under 18 players
    }
}