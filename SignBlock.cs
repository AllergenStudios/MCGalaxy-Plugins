using System;
using System.Collections.Generic;
using System.IO;
using MCGalaxy;
using MCGalaxy.Events.ServerEvents;
using MCGalaxy.Events.PlayerEvents;
using MCGalaxy.Blocks.Extended;
using System.Reflection;

// You'll have to edit some code to configure this onew
// Dont worry, I laid it out easily. just scroll down until you get to the Load method
// You will need the latest dev build of mcgalaxy

namespace PluginSignBlock
{
    public sealed class SignBlock : Plugin 
    {
        public override string name { get { return "SignBlock"; } }

        public override string MCGalaxy_Version { get { return "1.9.4.9"; } }

        public override string creator { get { return "AllergenX"; } }
        
        public static List<int> blockIDs = new List<int>();

        public override void Load(bool startup) {
            // CONFIGURATION IS DONE HERE
            // You know what to do to add more block IDs
            // Remember that they are all message blocks, and remember to add each rotation of each block to
            // this list and the block properties.
            
            // This example list just adds every ore
            blockIDs.Add(14);
            blockIDs.Add(15);
            blockIDs.Add(16);

            
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
            if (!text.Contains("/")) {
                p.Message("&aSign created. &7You can delete it by typing &a'/delete' &7to toggle delete mode, and then by punching it.");
                MessageBlock.Set(p.Level.MapName, pubX, pubY, pubZ, "&7This sign says... &e" + text);
            } else {
                p.Message("&cYou cannot put commands inside of signs.");
            }
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
                if (blockIDs.Contains(blockID)) {
                    p.Message("&7Enter the text for your sign in chat.");
                    gettingSignText = true;
                }
            }
        }
    }
}    
