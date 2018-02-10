using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using Types = SDG.Unturned.Types;

namespace EquinoxHashBypasser
{
    public class EquinoxHashBypasser
    {
        public static void IShit(CSteamID steamID, byte[] packet, int offset, int size, int channel)
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

        }
    }
}
