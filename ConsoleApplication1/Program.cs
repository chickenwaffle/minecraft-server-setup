using System;
using System.Net;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        enum Difficulty
        {
            peaceful, easy, normal, hard
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

        static bool WritePropertiesFile()
        {
            using (StreamWriter sw = new StreamWriter("minecraft_server/newserver.properties"))
            {
                string newpropertiesfile = PROPERTIES_FILE.Replace("{LEVEL_NAME}", "YOU SUCK!");
                sw.Write(newpropertiesfile);
                sw.Close();
            }

            if (File.Exists("minecraft_server/newserver.properties"))   return true;
            else                                                        return false;
        }

        static bool DownloadMinecraft(bool verbose)
        {
            if (verbose) { Console.WriteLine("Downloading minecraft_server*.jar from\nhttps://s3.amazonaws.com/Minecraft.Download/versions/1.8.7/"); }
            
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile("https://s3.amazonaws.com/Minecraft.Download/versions/1.8.7/minecraft_server.1.8.7.jar", "minecraft_server/minecraft_server.jar");
            }

            if (File.Exists("minecraft_server/minecraft_server.jar"))   return true;
            else                                                        return false;
        }

        static void Main(string[] args)
        {
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
            string playerIdleTimeout;
            string maxPlayers = "20";
            string maxTickTime = "60000";
            bool spawnMonsters = true;
            bool generateStructures = true;
            string viewDistance = "10";
            string motd = "You suck.";
            

            // attempt to download the minecraft server to the running directory
            if (!Directory.Exists("minecraft_server"))
            {
                Directory.CreateDirectory("minecraft_server");
            }

            if (!DownloadMinecraft(true))
            {
                Console.WriteLine("Download failed.");
                System.Environment.Exit(1);
            }

            if (!WritePropertiesFile())
            {
                Console.WriteLine("ERROR: Could not write server.properties file. Check to see if you have permission to write to this folder.");
                System.Environment.Exit(1);
            }

            using (StreamWriter sw = new StreamWriter("minecraft_server/eula.txt"))
            {
                sw.Write("eula=true");
                sw.Close();
            }


        }
    }
}
