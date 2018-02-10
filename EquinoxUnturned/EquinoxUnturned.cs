using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Timers;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Effects;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Provider;
using SDG.Provider.Services.Multiplayer;
using SDG.SteamworksProvider.Services.Matchmaking;
using SDG.SteamworksProvider.Services.Multiplayer;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using Types = SDG.Unturned.Types;

namespace EquinoxUnturned
{
    public class EquinoxUnturned : RocketPlugin
    {
        private Timer _timer;
        private Timer _timer2;
        private Timer _flyhack;
        private static Timer timednigger = null;
        private const float GoldChance = 0.45f;
        private const float GoldSpawn = 15f;
        public static Dictionary<string, int> Storage = new Dictionary<string, int>();
        public static System.IO.StreamWriter file;
        public static gameserveritem_t globaldata = null;
        //private ItemManager im;

        protected override void Load()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "CheatDetection.log"))) { File.Create(Path.Combine(ModuleFolder, "CheatDetection.log")).Dispose(); }
            //im = new ItemManager();
            SetStatic(typeof(ItemManager), "CHANCE_NORMAL", GoldChance);
            SetStatic(typeof(ItemManager), "RESPAWN_NORMAL", GoldSpawn);
            _timer = new Timer(50000); 
            _timer.Elapsed += new ElapsedEventHandler(Called);
            _timer.Start();
            _timer2 = new Timer(600000);
            _timer2.Elapsed += new ElapsedEventHandler(Called2);
            _timer2.Start();
            //_flyhack = new Timer(5000);
            //_flyhack.Elapsed += new ElapsedEventHandler(Called3);
            //_flyhack.Start();
            Provider.MaxPlayers = 100;
            Console.WriteLine("Feka has launched");
            U.Events.OnPlayerDisconnected += new UnturnedEvents.PlayerDisconnected(this.Events_OnPlayerDisconnected);
            U.Events.OnPlayerConnected += new UnturnedEvents.PlayerConnected(this.Events_OnPlayerConnected);
            UnturnedPlayerEvents.OnPlayerDeath += new UnturnedPlayerEvents.PlayerDeath(this.UnturnedPlayerEvents_OnPlayerDeath);
        }

        protected override void Unload()
        {
            U.Events.OnPlayerDisconnected -= new UnturnedEvents.PlayerDisconnected(this.Events_OnPlayerDisconnected);
            U.Events.OnPlayerConnected -= new UnturnedEvents.PlayerConnected(this.Events_OnPlayerConnected);
            UnturnedPlayerEvents.OnPlayerDeath -= new UnturnedPlayerEvents.PlayerDeath(this.UnturnedPlayerEvents_OnPlayerDeath);
        }

        public void Respond(object obj, object obj2)
        {
            
        }

        public string ModuleFolder
        {
            get
            {
                return
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) + "\\Rocket\\Plugins\\";
            }
        }

        public void UnturnedPlayerEvents_OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
                if (cause.ToString() == "ZOMBIE")
                {
                    UnturnedChat.Say(player.CharacterName + " got killed by a zombie! ", Color.red);
                }
                else if (cause.ToString() == "GUN")
                {
                    UnturnedChat.Say(UnturnedPlayer.FromCSteamID(murderer).CharacterName + " shot " + player.CharacterName + "!", Color.red);
                }
                else if (cause.ToString() == "MELEE")
                {
                    UnturnedChat.Say(UnturnedPlayer.FromCSteamID(murderer).CharacterName + " meleed " + player.CharacterName + " to death!", Color.red);
                }
                else if (cause.ToString() == "PUNCH")
                {
                    UnturnedChat.Say(UnturnedPlayer.FromCSteamID(murderer).CharacterName + " punched " + player.CharacterName + " to death!", Color.red);
                }
                else if (cause.ToString() == "ROADKILL")
                {
                    UnturnedChat.Say(UnturnedPlayer.FromCSteamID(murderer).CharacterName + " ran over " + player.CharacterName + "!", Color.red);
                }
                else if (cause.ToString() == "VEHICLE")
                {
                    UnturnedChat.Say(player.CharacterName + " died in a vehicle explosion!", Color.red);
                }
                else if (cause.ToString() == "FOOD")
                {
                    UnturnedChat.Say(player.CharacterName + " starved to death!", Color.red);
                }
                else if (cause.ToString() == "WATER")
                {
                    UnturnedChat.Say(player.CharacterName + " has dehydrated to death!", Color.red);
                }
                else if (cause.ToString() == "INFECTION")
                {
                    UnturnedChat.Say(player.CharacterName + " got infected!", Color.red);
                }
                else if (cause.ToString() == "BLEEDING")
                {
                    UnturnedChat.Say(player.CharacterName + " bled out!", Color.red);
                }
        }

        public static void kurwaanyad(SteamworksServerInfo asd)//(SteamworksServerInfo ts, HServerListRequest request, int index)
        {
            Console.WriteLine("Nigger");
            Console.WriteLine("Nigger2 " + asd.players + "/" + asd.capacity);
            //asd.players = 23;
            Console.WriteLine("Nigger2 " + asd.players + "/" + asd.capacity);
            //if (request == ts.serverListRequest)
            //{
            /* Console.WriteLine("Nigger");
                SteamServerInfo item = new SteamServerInfo(SteamMatchmakingServers.GetServerDetails(request, index));
                try
                {
                    PropertyInfo prop = item.GetType()
                        .GetProperty("players", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(item, 23, null);
                    Console.WriteLine("a1");
                }
                Console.WriteLine("a2");
            }
                catch (Exception ex)
                {
                    Console.WriteLine("Kurva neniked " + ex.Message);
                }
                if (item.maxPlayers >= CommandMaxPlayers.MIN_NUMBER)
                {
                    if (ts.currentList == ESteamServerList.INTERNET)
                    {
                        if (item.maxPlayers > (CommandMaxPlayers.MAX_NUMBER / 2))
                        {
                            return;
                        }
                    }
                    else if (item.maxPlayers > CommandMaxPlayers.MAX_NUMBER)
                    {
                        return;
                    }
                    int num = ts.serverList.BinarySearch(item, ts.serverInfoComparer);
                    if (num < 0)
                    {
                        num = ~num;
                    }
                    ts.serverList.Insert(num, item);
                    if (ts.onMasterServerAdded != null)
                    {
                        ts.onMasterServerAdded(num, item);
                    }
                }*/
            //}
        }

        /*public static void kurwaanyad2(SteamServerInfo info, gameserveritem_t data)
        {
            Console.WriteLine("DIKK");
            data.m_nPlayers = 23;
            info._players = 23;
            Console.WriteLine("DIKK " + info._players);
        }*/

        public static void kurwaanyad2(byte[] packet, int offset)
        {
            ESteamPacket packet2 = (ESteamPacket)packet[offset];
            if (Provider.isUpdate(packet2))
            {
                return;
            }
            if (packet2 == ESteamPacket.CONNECT)
            {
                Console.WriteLine("- " + Provider.MaxPlayers);
                Provider.MaxPlayers = 100;
                Console.WriteLine("-2 " + Provider.MaxPlayers);
            }
            
        }

        public static void kurwaanyad3(byte[] packet, int offset)
        {
            ESteamPacket packet2 = (ESteamPacket)packet[offset];
            if (Provider.isUpdate(packet2))
            {
                return;
            }
            if (packet2 == ESteamPacket.CONNECT)
            {
                Console.WriteLine("-3 " + Provider.MaxPlayers);
                Provider.MaxPlayers = 100;
                Console.WriteLine("-4 " + Provider.MaxPlayers);
            }
        }

        /*public static void IShit(CSteamID steamID, byte[] packet, int offset, int size, int channel)
        {
            Provider._bytesReceived += (uint)size;
            Provider._packetsReceived++;
            if (Dedicator.isDedicated)
            {
                ESteamPacket packet2 = (ESteamPacket)packet[offset];
                if (Provider.isUpdate(packet2))
                {
                    if (steamID == Provider.server)
                    {
                        for (int i = 0; i < Provider.receivers.Count; i++)
                        {
                            if (Provider.receivers[i].id == channel)
                            {
                                Provider.receivers[i].receive(steamID, packet, offset, size);
                                return;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < Provider.clients.Count; j++)
                        {
                            if (Provider.clients[j].playerID.steamID == steamID)
                            {
                                for (int k = 0; k < Provider.receivers.Count; k++)
                                {
                                    if (Provider.receivers[k].id == channel)
                                    {
                                        Provider.receivers[k].receive(steamID, packet, offset, size);
                                        return;
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
                else
                {
                    SteamPending pending;
                    switch (packet2)
                    {
                        case ESteamPacket.WORKSHOP:
                            {
                                ulong num8;
                                List<ulong> list = new List<ulong>();
                                string[] strArray = ReadWrite.getFolders("/Bundles/Workshop/Content");
                                for (int m = 0; m < strArray.Length; m++)
                                {
                                    ulong num5;
                                    if (ulong.TryParse(ReadWrite.folderName(strArray[m]), out num5))
                                    {
                                        list.Add(num5);
                                    }
                                }
                                string[] strArray2 = ReadWrite.getFolders(ServerSavedata.directory + "/" + Provider.serverID + "/Workshop/Content");
                                for (int n = 0; n < strArray2.Length; n++)
                                {
                                    ulong num7;
                                    if (ulong.TryParse(ReadWrite.folderName(strArray2[n]), out num7))
                                    {
                                        list.Add(num7);
                                    }
                                }
                                if (ulong.TryParse(new DirectoryInfo(Level.info.path).Parent.Name, out num8))
                                {
                                    list.Add(num8);
                                }
                                byte[] array = new byte[2 + (list.Count * 8)];
                                array[0] = 1;
                                array[1] = (byte)list.Count;
                                for (byte num9 = 0; num9 < list.Count; num9 = (byte)(num9 + 1))
                                {
                                    BitConverter.GetBytes(list[num9]).CopyTo(array, (int)(2 + (num9 * 8)));
                                }
                                Provider.send(steamID, ESteamPacket.WORKSHOP, array, array.Length, 0);
                                return;
                            }
                        case ESteamPacket.TICK:
                            {
                                int num10;
                                object[] objects = new object[] { (byte)14, Provider.net };
                                byte[] buffer2 = SteamPacker.getBytes(0, out num10, objects);
                                Provider.send(steamID, ESteamPacket.TIME, buffer2, num10, 0);
                                return;
                            }
                        case ESteamPacket.TIME:
                            for (int num11 = 0; num11 < Provider.clients.Count; num11++)
                            {
                                if (Provider.clients[num11].playerID.steamID == steamID)
                                {
                                    if (Provider.clients[num11].lastPing > 0f)
                                    {
                                        Provider.clients[num11].lastNet = Time.realtimeSinceStartup;
                                        Provider.clients[num11].lag(Time.realtimeSinceStartup - Provider.clients[num11].lastPing);
                                        Provider.clients[num11].lastPing = -1f;
                                    }
                                    return;
                                }
                            }
                            return;

                        case ESteamPacket.CONNECT:
                            {
                                long num14;
                                double num15;
                                long num16;
                                double num17;
                                SteamBlacklistID tid;
                                for (int num12 = 0; num12 < Provider.pending.Count; num12++)
                                {
                                    if (Provider.pending[num12].playerID.steamID == steamID)
                                    {
                                        Provider.Reject(steamID, ESteamRejection.ALREADY_PENDING);
                                        return;
                                    }
                                }
                                for (int num13 = 0; num13 < Provider.clients.Count; num13++)
                                {
                                    if (Provider.clients[num13].playerID.steamID == steamID)
                                    {
                                        Provider.Reject(steamID, ESteamRejection.ALREADY_CONNECTED);
                                        return;
                                    }
                                }
                                Type[] types = new Type[] {
                        Types.BYTE_TYPE, Types.BYTE_TYPE, Types.STRING_TYPE, Types.STRING_TYPE, Types.BYTE_ARRAY_TYPE, Types.BYTE_ARRAY_TYPE, Types.BYTE_ARRAY_TYPE, Types.BYTE_TYPE, Types.STRING_TYPE, Types.BOOLEAN_TYPE, Types.SINGLE_TYPE, Types.STRING_TYPE, Types.STEAM_ID_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE, Types.BYTE_TYPE,
                        Types.COLOR_TYPE, Types.COLOR_TYPE, Types.BOOLEAN_TYPE, Types.UINT64_TYPE, Types.UINT64_TYPE, Types.UINT64_TYPE, Types.UINT64_TYPE, Types.UINT64_TYPE, Types.UINT64_TYPE, Types.UINT64_TYPE, Types.UINT64_ARRAY_TYPE, Types.BYTE_TYPE
                     };
                                object[] objArray = SteamPacker.getObjects(steamID, offset, 0, packet, types);
                                SteamPlayerID newPlayerID = new SteamPlayerID(steamID, (byte)objArray[1], (string)objArray[2], (string)objArray[3], (string)objArray[11], (CSteamID)objArray[12]);
                                if (((string)objArray[8]) != Provider.Version)
                                {
                                    Provider.Reject(steamID, ESteamRejection.WRONG_VERSION);
                                    return;
                                }
                                if (newPlayerID.SteamName.Length < 2)
                                {
                                    Provider.Reject(steamID, ESteamRejection.NAME_PLAYER_SHORT);
                                    return;
                                }
                                if (newPlayerID.CharacterName.Length < 2)
                                {
                                    Provider.Reject(steamID, ESteamRejection.NAME_CHARACTER_SHORT);
                                    return;
                                }
                                if (newPlayerID.SteamName.Length > 0x20)
                                {
                                    Provider.Reject(steamID, ESteamRejection.NAME_PLAYER_LONG);
                                    return;
                                }
                                if (newPlayerID.CharacterName.Length > 0x20)
                                {
                                    Provider.Reject(steamID, ESteamRejection.NAME_CHARACTER_LONG);
                                    return;
                                }
                                if (long.TryParse(newPlayerID.SteamName, out num14) || double.TryParse(newPlayerID.SteamName, out num15))
                                {
                                    Provider.Reject(steamID, ESteamRejection.NAME_PLAYER_NUMBER);
                                    return;
                                }
                                if (long.TryParse(newPlayerID.CharacterName, out num16) || double.TryParse(newPlayerID.CharacterName, out num17))
                                {
                                    Provider.Reject(steamID, ESteamRejection.NAME_CHARACTER_NUMBER);
                                    return;
                                }
                                if (Provider.filterName)
                                {
                                    if (!NameTool.isValid(newPlayerID.SteamName))
                                    {
                                        Provider.Reject(steamID, ESteamRejection.NAME_PLAYER_INVALID);
                                        return;
                                    }
                                    if (!NameTool.isValid(newPlayerID.CharacterName))
                                    {
                                        Provider.Reject(steamID, ESteamRejection.NAME_CHARACTER_INVALID);
                                        return;
                                    }
                                }
                                if (SteamBlacklist.checkBanned(steamID, out tid))
                                {
                                    int num18;
                                    object[] objArray2 = new object[] { (byte)9, tid.reason, tid.getTime() };
                                    byte[] buffer3 = SteamPacker.getBytes(0, out num18, objArray2);
                                    Provider.send(steamID, ESteamPacket.BANNED, buffer3, num18, 0);
                                    return;
                                }
                                if (!SteamWhitelist.checkWhitelisted(steamID))
                                {
                                    Provider.Reject(steamID, ESteamRejection.WHITELISTED);
                                    return;
                                }
                                if ((Provider.clients.Count + 1) > Provider.maxPlayers)
                                {
                                    Provider.Reject(steamID, ESteamRejection.SERVER_FULL);
                                    return;
                                }
                                byte[] buffer4 = (byte[])objArray[4];
                                if (buffer4.Length != 20)
                                {
                                    Provider.Reject(steamID, ESteamRejection.WRONG_HASH);
                                    return;
                                }
                                byte[] buffer5 = (byte[])objArray[5];
                                if (buffer5.Length != 20)
                                {
                                    Provider.Reject(steamID, ESteamRejection.WRONG_HASH);
                                    return;
                                }
                                byte[] h = (byte[])objArray[6];
                                if (h.Length != 20)
                                {
                                    Provider.Reject(steamID, ESteamRejection.WRONG_HASH);
                                    return;
                                }
                                if ((Provider.serverPassword == string.Empty) || Hash.verifyHash(buffer4, Provider._serverPasswordHash))
                                {
                                    if (Hash.verifyHash(buffer5, Level.hash))
                                    {
                                        if (ReadWrite.appIn(h, (byte)objArray[7]))
                                        {
                                            if (((float)objArray[10]) < Provider.timeout)
                                            {
                                                Provider.pending.Add(new SteamPending(newPlayerID, (bool)objArray[9], (byte)objArray[13], (byte)objArray[14], (byte)objArray[15], (Color)objArray[0x10], (Color)objArray[0x11], (bool)objArray[0x12], (ulong)objArray[0x13], (ulong)objArray[20], (ulong)objArray[0x15], (ulong)objArray[0x16], (ulong)objArray[0x17], (ulong)objArray[0x18], (ulong)objArray[0x19], (ulong[])objArray[0x1a], (EPlayerSpeciality)((byte)objArray[0x1b])));
                                                byte[] buffer1 = new byte[] { 3 };
                                                Provider.send(steamID, ESteamPacket.VERIFY, buffer1, 1, 0);
                                                return;
                                            }
                                            Provider.Reject(steamID, ESteamRejection.PING);
                                            return;
                                        }
                                        Provider.Reject(steamID, ESteamRejection.WRONG_HASH);
                                        return;
                                    }
                                    Provider.Reject(steamID, ESteamRejection.WRONG_HASH);
                                    return;
                                }
                                Provider.Reject(steamID, ESteamRejection.WRONG_PASSWORD);
                                return;
                            }
                        default:
                            if (packet2 != ESteamPacket.AUTHENTICATE)
                            {
                                Debug.LogError("Failed to handle message: " + packet2);
                                return;
                            }
                            pending = null;
                            for (int num19 = 0; num19 < Provider.pending.Count; num19++)
                            {
                                if (Provider.pending[num19].playerID.steamID == steamID)
                                {
                                    pending = Provider.pending[num19];
                                    break;
                                }
                            }
                            break;
                    }
                    if (pending == null)
                    {
                        Provider.Reject(steamID, ESteamRejection.NOT_PENDING);
                    }
                    else if ((Provider.clients.Count + 1) > Provider.maxPlayers)
                    {
                        Provider.Reject(steamID, ESteamRejection.SERVER_FULL);
                    }
                    else
                    {
                        ushort count = BitConverter.ToUInt16(packet, 1);
                        byte[] dst = new byte[count];
                        Buffer.BlockCopy(packet, 3, dst, 0, count);
                        ushort num21 = BitConverter.ToUInt16(packet, 3 + count);
                        byte[] buffer8 = new byte[num21];
                        Buffer.BlockCopy(packet, 5 + count, buffer8, 0, num21);
                        if (!Provider.verifyTicket(steamID, dst))
                        {
                            Provider.Reject(steamID, ESteamRejection.AUTH_VERIFICATION);
                        }
                        else if (num21 > 0)
                        {
                            if (!SteamGameServerInventory.DeserializeResult(out pending.inventoryResult, buffer8, num21, false))
                            {
                                Provider.Reject(steamID, ESteamRejection.AUTH_ECON);
                            }
                        }
                        else
                        {
                            pending.shirtItem = 0;
                            pending.pantsItem = 0;
                            pending.hatItem = 0;
                            pending.backpackItem = 0;
                            pending.vestItem = 0;
                            pending.maskItem = 0;
                            pending.glassesItem = 0;
                            pending.skinItems = new int[0];
                            pending.packageShirt = 0L;
                            pending.packagePants = 0L;
                            pending.packageHat = 0L;
                            pending.packageBackpack = 0L;
                            pending.packageVest = 0L;
                            pending.packageMask = 0L;
                            pending.packageGlasses = 0L;
                            pending.packageSkins = new ulong[0];
                            pending.inventoryResult = SteamInventoryResult_t.Invalid;
                            pending.inventoryDetails = new SteamItemDetails_t[0];
                            pending.hasProof = true;
                        }
                    }
                }
            }

        }*/



        /*private static void Called4(object sender, ElapsedEventArgs e)
        {
            timednigger.Dispose();
            timednigger = null;
            Console.WriteLine("DIKK " + globaldata.m_nPlayers);
        }

        public static void kurwaanyad3(gameserveritem_t data)
        {
            Console.WriteLine("DIKK2- ");
            Console.WriteLine("DIKK2- " + data.m_nPlayers);
            data.m_nPlayers = 23;
            Console.WriteLine("DIKK23- " + data.m_nPlayers);
            globaldata = data;
            if (timednigger != null)
            {
                return;
            }
            timednigger = new Timer(1000);
            timednigger.Elapsed += new ElapsedEventHandler(Called4);
            timednigger.Start();
        }*/

        public void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            foreach (SteamPlayer player2 in Provider.Players)
            {
                UnturnedChat.Say(player2.SteamPlayerID.CSteamID, player.DisplayName + " joined the server!", Color.yellow);
            }
            UnturnedChat.Say(player.CSteamID, "Welcome! You may use /kit to grab the default kit.", Color.cyan);
        }

        public void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            foreach (SteamPlayer player2 in Provider.Players)
            {
                UnturnedChat.Say(player2.SteamPlayerID.CSteamID, player.DisplayName + " left the server!", Color.yellow);
            }
        }

        private void Called3(object sender, ElapsedEventArgs e)
        {
            foreach (SteamPlayer player2 in Provider.Players)
            {
                UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromSteamPlayer(player2);
                if (unturnedPlayer == null)
                {
                    continue;
                }
                try
                {
                    if (unturnedPlayer.Position == Vector3.zero || unturnedPlayer.Dead)
                    {
                        continue;
                    }
                    var shits = Physics.OverlapSphere(unturnedPlayer.Position, 4.5f);
                    List<string> data = shits.Select(x => x.name.ToLower()).ToList();
                    UnturnedChat.Say(player2.SteamPlayerID.CSteamID, string.Join(", ", data.ToArray()), Color.yellow);

                    float dist = GetGround(unturnedPlayer.Position);
                    if (dist == 9999999f)
                    {
                        continue;
                    }
                    if (dist >= 11f)
                    {
                        if (!data.Contains("ground"))
                        {
                            Broadcast(unturnedPlayer.SteamName + "|" + unturnedPlayer.CSteamID + " is probably fly hacking!");
                            file = new System.IO.StreamWriter(Path.Combine(ModuleFolder, "CheatDetection.log"), true);
                            file.WriteLine(DateTime.Now + unturnedPlayer.SteamName + "|" + unturnedPlayer.CSteamID + " is probably fly hacking! Pos: " + unturnedPlayer.Position + " Dist: " + dist);
                            file.Close();
                            unturnedPlayer.Kick("FLY HACKING BITCH");
                        }
                    }
                }
                catch
                {
                    
                }
            }
        }

        public void Broadcast(string msg)
        {
            foreach (SteamPlayer player2 in Provider.Players)
            {
                UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromSteamPlayer(player2);
                if (unturnedPlayer == null)
                {
                    continue;
                }
                UnturnedChat.Say(player2.SteamPlayerID.CSteamID, msg, Color.red);
            }
        }

        public float GetGround(Vector3 target)
        {
            Vector3 above = new Vector3(target.x, 2000f, target.z);
            RaycastHit[] raycast = Physics.RaycastAll(above, Vector3.down, 2001f);
            float f = 9999999f;
            foreach (var x in raycast)
            {
                if (x.collider.name.ToLower().Contains("nav") || x.collider.name.ToLower().Contains("block"))
                {
                    var n = Vector3.Distance(target, x.transform.position);
                    //Console.WriteLine("nav: " + x.transform.position + " dist: " + n);
                    if (f > n)
                    {
                        f = Vector3.Distance(target, x.transform.position);
                    }
                }
            }
            return f;
        }

        private void Called2(object sender, ElapsedEventArgs e)
        {
            foreach (SteamPlayer player2 in Provider.Players)
            {
                UnturnedChat.Say(player2.SteamPlayerID.CSteamID, "Thank you for playing on our server!", Color.cyan);
                UnturnedChat.Say(player2.SteamPlayerID.CSteamID, "Please visit our website for more! WWW.EQUINOXGAMERS.COM ", Color.cyan);
                UnturnedChat.Say(player2.SteamPlayerID.CSteamID, "Our server has gold loot, enjoy!", Color.cyan);
            }
        }

        private void Called(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer launched");
            //Console.WriteLine(ItemManager.CHANCE_NORMAL + " - " + ItemManager.RESPAWN_NORMAL);
            if (ItemManager.CHANCE_NORMAL != GoldChance)
            {
                SetStatic(typeof(ItemManager), "CHANCE_NORMAL", GoldChance);
                Console.WriteLine("Re-set: " + ItemManager.CHANCE_NORMAL);
            }
            if (ItemManager.RESPAWN_NORMAL != GoldSpawn)
            {
                SetStatic(typeof(ItemManager), "RESPAWN_NORMAL", GoldSpawn);
                Console.WriteLine("Re-set: " + ItemManager.RESPAWN_NORMAL);
            }

            foreach (SteamPlayer plr in Provider.Players)
            {
                if (plr == null)
                {
                    continue;
                }
                UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromSteamPlayer(plr);
                if (unturnedPlayer == null)
                {
                    continue;
                }
                try
                {
                    if (plr.IsAdmin)
                    {
                        plr.Color = Color.white;
                    }
                    //plr.IsPro = true;
                    //SteamGameServer.BUpdateUserData(unturnedPlayer.CSteamID, unturnedPlayer.SteamName, 1);
                    //Console.WriteLine(unturnedPlayer.DisplayName);
                }
                catch (Exception)
                {
                    //Console.WriteLine("Error occured with player m:1 " + unturnedPlayer.SteamName + " ");
                }
                int ll = 0;
                try
                {
                    Items[] list = unturnedPlayer.Inventory.Items;
                    if (list == null)
                    {
                        continue;
                    }
                    ll = list.Length;
                    if (list.Length == 0)
                    {
                        continue;
                    }
                    foreach (Items item in list)
                    {
                        if (item == null)
                        {
                            continue;
                        }
                        try
                        {
                            for (int i = 0; i < item.getItemCount(); i++)
                            {
                                try
                                {
                                    item.updateQuality((byte) i, (byte) 100);
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                        catch
                        {
                            
                        }
                    }
                    unturnedPlayer.Player.Equipment.sendUpdateQuality();
                    //unturnedPlayer.Player.Equipment.sendUpdateState();
                }
                catch (Exception)
                {
                    //Console.WriteLine("Error occured with player m:2 " + unturnedPlayer.SteamName + " - " + ll);
                }
            }
                /*Console.WriteLine("12");
                LightingManager.isFullMoon = false;
                //var zombies = UnityEngine.Object.FindObjectsOfType<Zombie>();
                ZombieRegion zr = new ZombieRegion();
                foreach (Zombie z in zombies)
                {
                    Transform eyes = (Transform) GetInstanceField(z.GetType(), z, "eyes");
                    eyes.gameObject.SetActive(true);
                    z.updateStates();
                }
                foreach (Zombie z in zr.Zombies)
                {
                    Transform eyes = (Transform) GetInstanceField(z.GetType(), z, "eyes");
                    eyes.gameObject.SetActive(true);
                    z.updateStates();
                }
                Console.WriteLine("13");
                LightingManager.isFullMoon = false;*/
            Console.WriteLine("Timer Finished");
        }

        public void SetStatic(Type type, string field, object val)
        {
            FieldInfo info = type.GetField(field, BindingFlags.Public | BindingFlags.Static);
            if (info != null)
            {
                info.SetValue(null, Convert.ChangeType(val, info.FieldType));
            }
        }

        public void FixedUpdate()
        {
        }
    }
}
