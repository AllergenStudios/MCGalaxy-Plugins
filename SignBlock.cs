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
        
        public static string gotSignText = "";
        
        public static List<string> selectingSigns = new List<string>();
        
        public static void SubmitText(Player p, string text, string infos) {
            if (!text.Contains("/")) {
                string[] info_args = infos.Split(' ');
                p.Message("&aSign created. &7You can delete it by typing &a'/delete' &7to toggle delete mode, and then by punching it.");
                MessageBlock.Set(p.Level.MapName, Convert.ToUInt16(info_args[1]), Convert.ToUInt16(info_args[2]), Convert.ToUInt16(info_args[3]), "&7This sign says... &e" + text);
                int index = -1;
                foreach (string info in selectingSigns) {
                    index++;
                    if (info.Split(' ')[0] == p.DisplayName) {
                        selectingSigns.Remove(selectingSigns[index]);
                    }
                }
            } else {
                p.Message("&cYou cannot put commands inside of signs.");
                int index = -1;
                foreach (string info in selectingSigns) {
                    index++;
                    if (info.Split(' ')[0] == p.DisplayName) {
                        selectingSigns.Remove(selectingSigns[index]);
                    }
                }
            }
        }
    
        public static void OnChat(ChatScope scope, Player p, ref string msg, object arg, ref ChatMessageFilter filter, bool relay) {
            string cleaned = CleanChatMessage(msg);
            
            foreach (string info in selectingSigns) {
                if (info.Split(' ')[0] == p.DisplayName) {
                    gotSignText = cleaned;
                    msg = "";
                    SubmitText(p, gotSignText, info);
                }
            }
        }
        
        public static void OnBlockChanged(Player p, ushort x, ushort y, ushort z, ChangeResult result) {
            int blockID = p.Level.GetBlock(x, y, z); 
            if (result == ChangeResult.Modified) {
                if (blockIDs.Contains(blockID)) {
                    p.Message("&7Enter the text for your sign in chat.");
                    selectingSigns.Add(p.DisplayName + " " + Convert.ToString(x) + " " + Convert.ToString(y) + " " + Convert.ToString(z));
                }
            }
        }
    }
}    
