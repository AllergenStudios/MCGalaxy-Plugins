using System;
using MCGalaxy;
using MCGalaxy.Events.ServerEvents;
using MCGalaxy.Events.PlayerEvents;


// REQUIRES THE LATEST DEVELOPMENT BUILD OF MCGALAXY!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Will not work with tone indicators or other chat plugins probably.


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

        public static void OnChat(ChatScope scope, Player p, ref string msg, object arg, ref ChatMessageFilter filter, bool relay) {
            string sentMessage = msg;
            string displayName = p.DisplayName;
            string mapName = p.Level.MapName.ToUpper();
            string cleanedMessage = CleanChatMessage(sentMessage);
            msg = "&8[&a" + mapName + "&8] &3" + displayName + ": &7" + cleanedMessage;
        }
    }
}
