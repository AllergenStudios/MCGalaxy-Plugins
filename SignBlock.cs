using System;
using System.Collections.Generic;
using System.IO;
using MCGalaxy;
using MCGalaxy.Events.ServerEvents;
using MCGalaxy.Events.PlayerEvents;
using MCGalaxy.Blocks.Extended;
using System.Reflection;

// You'll have to edit some code to configure this onew
// Dont worry, I laid it out easily. just scroll down past the name, version, and creator stuff.

namespace PluginSignBlock
{
    public sealed class SignBlock : Plugin 
    {
        public override string name { get { return "SignBlock"; } }

        public override string MCGalaxy_Version { get { return "1.9.4.9"; } }

        public override string creator { get { return "AllergenX"; } }
        
        // CONFIGURATION IS DONE HERE
        
        // Leave it at 0 if you dont want to use
        // Make sure you have set them as message blocks using blockproperties
        public static int signBlockID1 = 0;
        public static int signBlockID2 = 0;
        public static int signBlockID3 = 0;
        public static int signBlockID4 = 0;
        public static int signBlockID5 = 0;

        public override void Load(bool startup) {
            OnChatEvent.Register(OnChat, Priority.High);
            OnBlockChangedEvent.Register(OnBlockChanged, Priority.High);
        }

        public override void Unload(bool shutdown) {
            OnChatEvent.Unregister(OnChat);
            OnBlockChangedEvent.Unregister(OnBlockChanged);
        }

        public static string CleanChatMessage(string originalString) {
            string targetString = ": ";
            int startIndex = originalString.IndexOf(targetString) + targetString.Length;
            string result = originalString.Substring(startIndex);
            return result;
        }
        
        public static bool gettingSignText = false;
        public static string gotSignText = "";
        
        public static ushort pubX = 0;
        public static ushort pubY = 0;
        public static ushort pubZ = 0;
        
        public static void SubmitText(Player p, string text) {
            gettingSignText = false;
            p.Message("&aSign created. &7You can delete it by typing &a'/delete' &7to toggle delete mode, and then by punching it.");
            MessageBlock.Set(p.Level.MapName, pubX, pubY, pubZ, "&7This sign says... &e" + text);
        }
    
        public static void OnChat(ChatScope scope, Player p, ref string msg, object arg, ref ChatMessageFilter filter, bool relay) {
            string cleaned = CleanChatMessage(msg);
            if (gettingSignText == true) {
                gotSignText = CleanChatMessage(msg);
                msg = "";
                SubmitText(p, gotSignText);
            }
        }
        
        public static void OnBlockChanged(Player p, ushort x, ushort y, ushort z, ChangeResult result) {
            int blockID = p.Level.GetBlock(x, y, z);
            pubX = x; pubY = y; pubZ = z;
            if (result == ChangeResult.Modified) {
                // trigger warning, this next piece of code is a mess, oh well.
                if (signBlockID1 != 0 && blockID == signBlockID1 || signBlockID2 != 0 && blockID == signBlockID2 || signBlockID3 != 0 && blockID == signBlockID3 || signBlockID4 != 0 && blockID == signBlockID4 || signBlockID5 != 0 && blockID == signBlockID5) {
                    p.Message("&7Enter the text for your sign in chat.");
                    gettingSignText = true;
                }
            }
        }
    }
}    
