using System;
using MCGalaxy;
using MCGalaxy.Events.ServerEvents;
using MCGalaxy.Events.PlayerEvents;


// REQUIRES THE LATEST VERSION OF MCGALAXY!!!!!!!!!!!!!!!!!!!!!!!!!!!


namespace PluginToneIndicators
{
    public sealed class ToneIndicators : Plugin 
    {
        public override string name { get { return "ToneIndicators"; } }
        public override string MCGalaxy_Version { get { return "0.1"; } }
        public override string creator { get { return "AllergenX"; } }
        
        public override void Load(bool startup) {
            OnChatEvent.Register(OnChat, Priority.High);
        }
        
        public override void Unload(bool shutdown) {
            OnChatEvent.Unregister(OnChat);
        }

        public static void OnChat(ChatScope scope, Player source, ref string msg, object arg, ref ChatMessageFilter filter, bool relay) {
            if (msg.CaselessContains("/j"))
            {
                const string removeString = "/j";
                int index = msg.IndexOf(removeString);
                int length = removeString.Length;
                string startOfString = msg.Substring(0, index);
                string endOfString = msg.Substring(index + length);
                string cleanString = startOfString + endOfString;
                msg = ("&7[&ajoking&7] " + cleanString);
            }
            else if (msg.CaselessContains("/s"))
            {
                const string removeString = "/s";
                int index = msg.IndexOf(removeString);
                int length = removeString.Length;
                string startOfString = msg.Substring(0, index);
                string endOfString = msg.Substring(index + length);
                string cleanString = startOfString + endOfString;
                msg = ("&7[&asarcasm&7] " + cleanString);
            }
            else if (msg.CaselessContains("/hj"))
            {
                const string removeString = "/hj";
                int index = msg.IndexOf(removeString);
                int length = removeString.Length;
                string startOfString = msg.Substring(0, index);
                string endOfString = msg.Substring(index + length);
                string cleanString = startOfString + endOfString;
                msg = ("&7[&ahalf-joking&7] " + cleanString);
            }
            else if (msg.CaselessContains("/srs"))
            {
                const string removeString = "/srs";
                int index = msg.IndexOf(removeString);
                int length = removeString.Length;
                string startOfString = msg.Substring(0, index);
                string endOfString = msg.Substring(index + length);
                string cleanString = startOfString + endOfString;
                msg = ("&7[&aserious&7] " + cleanString);
            }
            else if (msg.CaselessContains("/srs"))
            {
                const string removeString = "/nsrs";
                int index = msg.IndexOf(removeString);
                int length = removeString.Length;
                string startOfString = msg.Substring(0, index);
                string endOfString = msg.Substring(index + length);
                string cleanString = startOfString + endOfString;
                msg = ("&7[&anot serious&7] " + cleanString);
            }
            else if (msg.CaselessContains("/r"))
            {
                const string removeString = "/r";
                int index = msg.IndexOf(removeString);
                int length = removeString.Length;
                string startOfString = msg.Substring(0, index);
                string endOfString = msg.Substring(index + length);
                string cleanString = startOfString + endOfString;
                msg = ("&7[&aromantic&7] " + cleanString);
            }
            else if (msg.CaselessContains("/t"))
            {
                const string removeString = "/t";
                int index = msg.IndexOf(removeString);
                int length = removeString.Length;
                string startOfString = msg.Substring(0, index);
                string endOfString = msg.Substring(index + length);
                string cleanString = startOfString + endOfString;
                msg = ("&7[&ateasing&7] " + cleanString);
            }
        }
    }
}
