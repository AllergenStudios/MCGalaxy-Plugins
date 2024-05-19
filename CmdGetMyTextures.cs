using MCGalaxy;

namespace CommandGetTheTextures
{
    public sealed class CmdGetTheTextures : Command2 
    {
        public override string name { get { return "TextureLink"; } }
        public override string shortcut { get { return "tl"; } }
        public override string type { get { return CommandTypes.Other; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        
        public override void Use(Player p, string message, CommandData data) {
            if (p.level.Config.Terrain != "") {
                p.Message("&aYour current &elevel's local terrain.png &ais: &7" + p.level.Config.Terrain);
                // p.Message("&8-*-*-*-*-*-*-*-*-*-*-*-*-");
            }
            
            if (Server.Config.DefaultTerrain != "") {
                p.Message("&aThe entire &eserver's global terrain.png &ais: &7" + Server.Config.DefaultTerrain);
            }

            if (p.level.Config.TexturePack != "") {
                p.Message("&aYour current &elevel's local texture pack &ais: &7" + p.level.Config.TexturePack);
                // p.Message("&8-*-*-*-*-*-*-*-*-*-*-*-*-");
            }
            
            if (Server.Config.DefaultTexture != "") {
                p.Message("&aThe entire &eserver's global texture pack &ais: &7" + Server.Config.DefaultTexture);
            }
        }
        
        public override void Help(Player p) {
            p.Message("&T/texturelink");
            p.Message("&HGets the link to the server's default texture pack.");
        }
    }
}
