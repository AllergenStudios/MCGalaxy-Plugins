using System;
using MCGalaxy;
using MCGalaxy.Events.ServerEvents;
using MCGalaxy.Events.PlayerEvents;


// REQUIRES THE LATEST DEVELOPMENT BUILD OF MCGALAXY!!!!!!!!!!!!!!!!!!!!!!!!!!!

// tone indicators code fixed and edited by goodly thank u!!!


namespace PluginFancyChat
{
    public sealed class FancyChat : Plugin 
    {
        public override string name { get { return "FancyChat"; } }

        public override string MCGalaxy_Version { get { return "1.9.4.9"; } }

        public override string creator { get { return "AllergenX"; } }

        public override void Load(bool startup) {
            OnChatEvent.Register(OnChat, Priority.Low);
        }

        public override void Unload(bool shutdown) {
            OnChatEvent.Unregister(OnChat);
        }
        
        public static string CleanChatMessage(string originalString) {
            string targetString = ": ";
            int startIndex = originalString.IndexOf(targetString) + targetString.Length;
            string result = originalString.Substring(startIndex);
            return result;
        }
        
        public static string GetChatMessageBadge(LevelPermission rank) {
            if (rank == LevelPermission.Owner) { return "&c|"; }
            else if (rank == LevelPermission.Guest) { return "&7|"; }
            else if (rank == LevelPermission.Builder) { return "&2|"; }
            else if (rank == LevelPermission.AdvBuilder) { return "&9|"; }
            else if (rank == LevelPermission.Operator) { return "&b|"; }
            else if (rank == LevelPermission.Admin) { return "&e|"; }
            
            return "&0|";
        }

        public static void OnChat(ChatScope scope, Player p, ref string msg, object arg, ref ChatMessageFilter filter, bool relay) {
            string sentMessage = msg;
            string displayName = p.DisplayName;
            string mapName = p.Level.MapName.ToUpper();
            string cleanedMessage = CleanChatMessage(sentMessage);
            
            msg = "&8[&a" + mapName + "&8] " + GetChatMessageBadge(p.Rank) + " &3" + displayName + ": " + cleanedMessage;
        }
    }
}
