using System;
using System.Net;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        enum Difficulty
        {
            peaceful=0, easy=1, normal=2, hard=3
        };

        const string PROPERTIES_FILE = @"#Minecraft server properties
{TIMESTAMP}
generator-settings={GENERATOR_SETTINGS}
op-permission-level={OP_PERMISSION_LEVEL}
resource-pack-hash={RESOURCE_PACK_HASH}
allow-nether={ALLOW_NETHER}
level-name={LEVEL_NAME}
enable-query={ENABLE_QUERY}
allow-flight={ALLOW_FLIGHT}
announce-player-achievements={ANNOUNCE_PLAYER_ACHIEVEMENTS}
server-port={SERVER_PORT}
max-world-size={MAX_WORLD_SIZE}
level-type={LEVEL_TYPE}
enable-rcon={ENABLE_RCON}
force-gamemode={FORCE_GAMEMODE}
level-seed={LEVEL_SEED}
server-ip={SERVER_IP}
network-compression-threshold={NETWORK_COMPRESSION_THRESHOLD}
max-build-height={MAX_BUILD_HEIGHT}
spawn-npcs={SPAWN_NPCS}
white-list={WHITE_LIST}
spawn-animals={SPAWN_ANIMALS}
snooper-enabled={SNOOPER_ENABLED}
hardcore={HARDCORE}
online-mode={ONLINE_MODE}
resource-pack={RESOURCE_PACK}
pvp={PVP}
difficulty={DIFFICULTY}
enable-command-block={ENABLE_COMMAND_BLOCK}
player-idle-timeout={PLAYER_IDLE_TIMEOUT}
gamemode={GAMEMODE}
max-players={MAX_PLAYERS}
max-tick-time={MAX_TICK_TIME}
spawn-monsters={SPAWN_MONSTERS}
view-distance={VIEW_DISTANCE}
generate-structures={GENERATE_STRUCTURES}
motd={MOTD}
";
        static bool generateFile(string file)
        {
            switch (file)
            {
                case "eula.txt":
                    using (StreamWriter sw = new StreamWriter(file))
                    {
                        sw.Write("eula=true");
                        sw.Close();
                    }
                    break;
                
                case "run.bat":
                    using (StreamWriter sw = new StreamWriter(file))
                    {
                        sw.Write("java -Xmx1024M -Xms1024M -jar minecraft_server.jar nogui\npause");
                        sw.Close();
                    }
                    break;
            }

            if (File.Exists(file))    return true;
            else                      return false;
        }

        static bool DownloadMinecraft(bool verbose)
        {
            if (verbose) { Console.WriteLine("Downloading minecraft_server*.jar from\nhttps://s3.amazonaws.com/Minecraft.Download/versions/1.8.7/"); }
            
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile("https://s3.amazonaws.com/Minecraft.Download/versions/1.8.7/minecraft_server.1.8.7.jar", "minecraft_server.jar");
            }

            if (File.Exists("minecraft_server.jar"))   return true;
            else                                                        return false;
        }

        static void Main(string[] args)
        {
            // FILES
            const string EULA = "eula.txt";
            const string SERVER_PROPERTIES = "server.properties";
            const string MINECRAFT_SERVER = "minecraft_server.jar";

            // Server properties
            string generatorSettings = "";
            string opPermissionLevel = "4";
            bool nether = true;
            string resourcePackHash = "";
            string lvlName = "world";
            bool enableQuery = false;
            bool allowFlight = false;
            bool announcePlayerAchievements = true;
            string serverPort = "25565";
            string maxWorldSize = "29999984";
            string lvlType = "DEFAULT";
            bool enableRcon = false;
            string lvlSeed = "";
            bool forceGamemode = false;
            string maxBuildHeight = "256";
            string serverIP = "";
            string networkCompressionThreshold = "256";
            bool spawnNPCs = true;
            bool whiteList = false;
            bool spawnAnimals = true;
            bool hardcore = false;
            bool snooperEnabled = true;
            bool onlineMode = true;
            string resourcePack = "";
            bool pvp = true;
            sbyte difficulty = (sbyte)Difficulty.peaceful;
            bool enableCommandBlock = false;
            string gamemode = "0";
            string playerIdleTimeout = "";
            string maxPlayers = "20";
            string maxTickTime = "60000";
            bool spawnMonsters = true;
            bool generateStructures = true;
            string viewDistance = "10";
            string motd = "";
            

            // attempt to download the minecraft server to the running directory
            if (!File.Exists(MINECRAFT_SERVER))
            {
                DownloadMinecraft(true);
            }

            if (!File.Exists("server.properties")) {
                using (StreamWriter sw = new StreamWriter(SERVER_PROPERTIES))
                {
                    string newpropertiesfile = PROPERTIES_FILE.Replace("{GENERATOR_SETTINGS}", generatorSettings)
                                                                .Replace("{OP_PERMISSION_LEVEL}", opPermissionLevel)
                                                                .Replace("{RESOURCE_PACK_HASH}", resourcePackHash)
                                                                .Replace("{ALLOW_NETHER}", nether.ToString().ToLower())
                                                                .Replace("{LEVEL_NAME}", lvlName)
                                                                .Replace("{ENABLE_QUERY}", enableQuery.ToString().ToLower())
                                                                .Replace("{ALLOW_FLIGHT}", allowFlight.ToString().ToLower())
                                                                .Replace("{ANNOUNCE_PLAYER_ACHIEVEMENTS}", announcePlayerAchievements.ToString().ToLower())
                                                                .Replace("{SERVER_PORT}", serverPort)
                                                                .Replace("{MAX_WORLD_SIZE}", maxWorldSize)
                                                                .Replace("{LEVEL_TYPE}", lvlType)
                                                                .Replace("{ENABLE_RCON}", enableRcon.ToString().ToLower())
                                                                .Replace("{FORCE_GAMEMODE}", forceGamemode.ToString().ToLower())
                                                                .Replace("{LEVEL_SEED}", lvlSeed)
                                                                .Replace("{SERVER_IP}", serverIP)
                                                                .Replace("{NETWORK_COMPRESSION_THRESHOLD}", networkCompressionThreshold)
                                                                .Replace("{MAX_BUILD_HEIGHT}", maxBuildHeight)
                                                                .Replace("{SPAWN_NPCS}", spawnNPCs.ToString().ToLower())
                                                                .Replace("{WHITE_LIST}", whiteList.ToString().ToLower())
                                                                .Replace("{SPAWN_ANIMALS}", spawnAnimals.ToString().ToLower())
                                                                .Replace("{SNOOPER_ENABLED}", snooperEnabled.ToString().ToLower())
                                                                .Replace("{HARDCORE}", hardcore.ToString().ToLower())
                                                                .Replace("{ONLINE_MODE}", onlineMode.ToString().ToLower())
                                                                .Replace("{RESOURCE_PACK}", resourcePack)
                                                                .Replace("{PVP}", pvp.ToString().ToLower())
                                                                .Replace("{DIFFICULTY}", difficulty.ToString())
                                                                .Replace("{ENABLE_COMMAND_BLOCK}", enableCommandBlock.ToString().ToLower())
                                                                .Replace("{PLAYER_IDLE_TIMEOUT}", playerIdleTimeout)
                                                                .Replace("{GAMEMODE}", gamemode)
                                                                .Replace("{MAX_PLAYERS}", maxPlayers)
                                                                .Replace("{MAX_TICK_TIME}", maxTickTime)
                                                                .Replace("{SPAWN_MONSTERS}", spawnMonsters.ToString().ToLower())
                                                                .Replace("{VIEW_DISTANCE}", viewDistance)
                                                                .Replace("{GENERATE_STRUCTURES}", generateStructures.ToString().ToLower())
                                                                .Replace("{MOTD}", motd);
                    sw.Write(newpropertiesfile);
                    sw.Close();
                }
            }

            if (!File.Exists(EULA)) {
                generateFile(EULA);
            }
        }
    }
}
