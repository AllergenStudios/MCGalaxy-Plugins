using System;
using MCGalaxy;
using MCGalaxy.Events.ServerEvents;
using MCGalaxy.Events.PlayerEvents;


// REQUIRES THE VERY LATEST VERSION OF MCGALAXY!!!!!!!!!!!!!!!!!!!!!!!!!!!

// This version has been edited and changed by Goodly. Thank you so much!


namespace PluginToneIndicators
{
    public sealed class ToneIndicators : Plugin 
    {
        public override string name { get { return "tones"; } }

        public override string MCGalaxy_Version { get { return "1.9.4.9"; } }
        
        public override string creator { get { return "AllergenX"; } }
        
        public override void Load(bool startup) {
            OnChatEvent.Register(OnChat, Priority.High);
        }
        
        public override void Unload(bool shutdown) {
            OnChatEvent.Unregister(OnChat);
        }
        
        public static void OnChat(ChatScope scope, Player source, ref string msg, object arg, ref ChatMessageFilter filter, bool relay) {

            if (HandleTone("/j", "joking", ref msg)) { return; }
            if (HandleTone("/s", "sarcasm", ref msg)) { return; }
            if (HandleTone("/hj", "half-joking", ref msg)) { return; }
            if (HandleTone("/srs", "serious", ref msg)) { return; }
            if (HandleTone("/nsrs", "not-serious", ref msg)) { return; }
            if (HandleTone("/r", "romantic", ref msg)) { return; }
            if (HandleTone("/t", "teasing", ref msg)) { return; }

        }
            
        public static int FindTone(string tone, string msg) {
            int toneStart = msg.IndexOf(tone);
            if (toneStart == -1) { return -1; }
            
            bool leftSideClear = false;
            bool rightSideClear = false;
            
            if (toneStart == 0                        || msg[toneStart-1]           == ' ') { leftSideClear = true; } 
            if (toneStart + tone.Length == msg.Length || msg[toneStart+tone.Length] == ' ') { rightSideClear = true; }
            
            if (leftSideClear && rightSideClear) { return toneStart; }
            else { return -1; }
        }
        
        public static bool HandleTone(string tone, string prefix, ref string msg) {
        
            int toneStart = FindTone(tone, msg);
            if (toneStart == -1) { return false; }
            
            string startOfString = msg.Substring(0, toneStart);
            string endOfString = msg.Substring(toneStart + tone.Length);
            
            // Remove a space to prevent double spaces when removing a tone
            if (endOfString.Length > 0 && endOfString[0] == ' ') { endOfString = endOfString.Substring(1); }
            
            string cleanString = startOfString + endOfString;
            
            msg = ("&7[&a"+prefix+"&7] " + cleanString);
            return true;
        }
    }
}
