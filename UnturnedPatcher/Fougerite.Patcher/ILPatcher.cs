using System.Collections.Generic;
using System.Linq;

namespace Equinox.Patcher
{
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using System;

    public class ILPatcher
    {
        private AssemblyDefinition rustAssembly = null;
        private AssemblyDefinition rustAssembly2 = null;
        private AssemblyDefinition EquinoxUnturned = null;

        public ILPatcher()
        {
            try
            {
                rustAssembly = AssemblyDefinition.ReadAssembly("Assembly-CSharp.dll");
                //rustAssembly2 = AssemblyDefinition.ReadAssembly("Assembly-CSharp-firstpass.dll");
                EquinoxUnturned = AssemblyDefinition.ReadAssembly("EquinoxUnturned.dll");
                // rustFirstPassAssembly = AssemblyDefinition.ReadAssembly("Assembly-CSharp-firstpass.dll");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        public bool FirstPass()
        {
            TypeDefinition type = rustAssembly.MainModule.GetType("SDG.Unturned.ItemManager");
            type.GetField("RESPAWN_EASY").SetPublic(true);
            type.GetField("RESPAWN_HARD").SetPublic(true);
            type.GetField("RESPAWN_NORMAL").SetPublic(true);
            type.GetField("RESPAWN_PRO").SetPublic(true);
            type.GetField("CHANCE_EASY").SetPublic(true);
            type.GetField("CHANCE_HARD").SetPublic(true);
            type.GetField("CHANCE_NORMAL").SetPublic(true);
            type.GetField("CHANCE_PRO").SetPublic(true);
            /*TypeDefinition TempSteamworksMatchmaking = rustAssembly.MainModule.GetType("SDG.Provider.TempSteamworksMatchmaking");
            TempSteamworksMatchmaking.GetField("serverListRequest").SetPublic(true);
            TempSteamworksMatchmaking.GetField("currentList").SetPublic(true);
            TempSteamworksMatchmaking.GetField("serverInfoComparer").SetPublic(true);
            MethodDefinition method = EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned")
                .GetMethod("kurwaanyad");
            MethodDefinition md = TempSteamworksMatchmaking.GetMethod("onServerListResponded");
            ILProcessor iLProcessor = md.Body.GetILProcessor();
            iLProcessor.Body.Instructions.Clear();
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0));
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_1));
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_2));
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Callvirt, rustAssembly.MainModule.Import(method)));
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));*/
            /* MethodDefinition method = EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned")
                 .GetMethod("kurwaanyad");
             TypeDefinition SteamworksServerInfo = rustAssembly.MainModule.GetType("SDG.SteamworksProvider.Services.Multiplayer.SteamworksServerInfo");
             SteamworksServerInfo.GetProperty("players").SetMethod.SetPublic(true);
             MethodDefinition md2 = SteamworksServerInfo.GetConstructors().ToArray()[0];
             ILProcessor iLProcessor = md2.Body.GetILProcessor();
             int Position = md2.Body.Instructions.Count - 1;
             iLProcessor.InsertBefore(md2.Body.Instructions[Position],
                 Instruction.Create(OpCodes.Callvirt, this.rustAssembly.MainModule.Import(method)));
             iLProcessor.InsertBefore(md2.Body.Instructions[Position], Instruction.Create(OpCodes.Ldarg_0));




             TypeDefinition SteamServerInfo = rustAssembly.MainModule.GetType("SDG.Unturned.SteamServerInfo");
             MethodDefinition method2 = EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned")
                 .GetMethod("receiveServer");
             MethodDefinition md3 = SteamServerInfo.GetConstructors().ToArray()[0];
             SteamServerInfo.GetField("_players").SetPublic(true);
             ILProcessor iLProcessor2 = md3.Body.GetILProcessor();
             int Position2 = md3.Body.Instructions.Count - 1;
             iLProcessor2.InsertBefore(md3.Body.Instructions[Position2],
                 Instruction.Create(OpCodes.Callvirt, this.rustAssembly.MainModule.Import(method2)));
             iLProcessor2.InsertBefore(md3.Body.Instructions[Position2], Instruction.Create(OpCodes.Ldarg_1));
             iLProcessor2.InsertBefore(md3.Body.Instructions[Position2], Instruction.Create(OpCodes.Ldarg_0));*/


            //MethodDefinition method = EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned")
              //  .GetMethod("IShit");
            TypeDefinition Provider = rustAssembly.MainModule.GetType("SDG.Unturned.Provider");
            Provider.GetMethod("isUpdate").SetPublic(true);
            Provider.GetMethod("verifyTicket").SetPublic(true);
            Provider.GetField("_bytesReceived").SetPublic(true);
            Provider.GetField("_packetsReceived").SetPublic(true);
            Provider.GetField("_serverPasswordHash").SetPublic(true);
            TypeDefinition PlayerLife = rustAssembly.MainModule.GetType("SDG.Unturned.PlayerLife");
            PlayerLife.GetField("_temperature").SetPublic(true);
            /*MethodDefinition md = Provider.GetMethod("receiveServer");
            ILProcessor iLProcessor = md.Body.GetILProcessor();
            iLProcessor.Body.Instructions.Clear();
            foreach (var param in md.Parameters)
            {
                iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg, param));
            }*/
            /*iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0));
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_1));
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_2));
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_3));
            iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg, md.Parameters[4]));*/
            //iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Callvirt, rustAssembly.MainModule.Import(method)));
            //iLProcessor.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));

            /*MethodDefinition kurwaanyad2 = EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned")
                 .GetMethod("kurwaanyad2");

            MethodDefinition kurwaanyad3 = EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned")
                 .GetMethod("kurwaanyad3");

            ILProcessor iLProcessor = md.Body.GetILProcessor();
            int Position = 0;
            iLProcessor.InsertBefore(md.Body.Instructions[Position],
                Instruction.Create(OpCodes.Callvirt, this.rustAssembly.MainModule.Import(kurwaanyad2)));
            iLProcessor.InsertBefore(md.Body.Instructions[Position], Instruction.Create(OpCodes.Ldarg_2));
            iLProcessor.InsertBefore(md.Body.Instructions[Position], Instruction.Create(OpCodes.Ldarg_1));

            rustAssembly.Write("Assembly-CSharp.dll");

            Provider = rustAssembly.MainModule.GetType("SDG.Unturned.Provider");
            md = Provider.GetMethod("receiveServer");
            ILProcessor iLProcessor2 = md.Body.GetILProcessor();
            int Position2 = md.Body.Instructions.Count - 8;
            iLProcessor2.InsertBefore(md.Body.Instructions[Position2],
                Instruction.Create(OpCodes.Callvirt, this.rustAssembly.MainModule.Import(kurwaanyad3)));
            iLProcessor2.InsertBefore(md.Body.Instructions[Position2], Instruction.Create(OpCodes.Ldarg_2));
            iLProcessor2.InsertBefore(md.Body.Instructions[Position2], Instruction.Create(OpCodes.Ldarg_1));*/

            rustAssembly.Write("Assembly-CSharp.dll");

            /*TypeDefinition gameserveritem_t = rustAssembly2.MainModule.GetType("Steamworks.gameserveritem_t");
            MethodDefinition md4 = gameserveritem_t.GetConstructors().ToArray()[0];
            MethodDefinition method3 = EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned")
                .GetMethod("kurwaanyad3");
            Console.WriteLine(md4.Body.Instructions.Count);
            Console.ReadKey();
            ILProcessor iLProcessor3 = md4.Body.GetILProcessor();
            int Position3 = md4.Body.Instructions.Count - 1;
            iLProcessor3.InsertBefore(md4.Body.Instructions[Position3],
                Instruction.Create(OpCodes.Callvirt, this.rustAssembly2.MainModule.Import(method3)));
            iLProcessor3.InsertBefore(md4.Body.Instructions[Position3], Instruction.Create(OpCodes.Ldarg_0));
            rustAssembly2.Write("Assembly-CSharp-firstpass.dll");*/

            /*TypeDefinition gameserveritem_t = rustAssembly2.MainModule.GetType("Steamworks.gameserveritem_t");
            MethodDefinition md4 = gameserveritem_t.GetConstructors().ToArray()[0];
            
            Console.WriteLine(md4.HasBody);
            Console.WriteLine(md4 + " " + md4.Name);
            Console.ReadKey();
            MethodDefinition method3 = EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned")
                .GetMethod("kurwaanyad3");
            //ILProcessor iLProcessor3 = md4.Body.GetILProcessor();
            //int Position3 = md4.Body.Instructions.Count - 1;
            md4.Body.Instructions.Clear();
            md4.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0));
            md4.Body.Instructions.Add(Instruction.Create(OpCodes.Callvirt, this.rustAssembly2.MainModule.Import(method3)));
            md4.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
            try
            {
                rustAssembly2.Write("Assembly-CSharp-firstpass.dll");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }*/
            //type.Properties.Add(new PropertyDefinition("asd", PropertyAttributes.None, new TypeReference("","", new ModuleDefinition(), )));

            /*TypeDefinition type3 = rustAssembly.MainModule.GetType("SDG.SteamworksProvider.Services.Matchmaking.SteamworksServerInfoRequestHandle");
            type3.GetMethod("cleanupQuery").SetPublic(true);
            MethodDefinition fos = type3.GetMethod("onServerResponded");*/

            /*int i = fos.Body.Instructions.Count - 1;
            ILProcessor iLProcessor = fos.Body.GetILProcessor();
            iLProcessor.InsertBefore(fos.Body.Instructions[i], Instruction.Create(OpCodes.Call, rustAssembly.MainModule.Import(EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned").GetMethod("Respond"))));
            iLProcessor.InsertBefore(fos.Body.Instructions[i], Instruction.Create(OpCodes.Ldarg_1));
            iLProcessor.InsertBefore(fos.Body.Instructions[i], Instruction.Create(OpCodes.Ldarg_0));*/
            //TypeDefinition type3 = rustAssembly.MainModule.GetType("SDG.SteamworksProvider.Services.Matchmaking.SteamworksMatchmakingService");
            //MethodDefinition fos = type3.GetMethod("requestServerInfo");
            /*int i = fos.Body.Instructions.Count - 1;
            ILProcessor iLProcessor = fos.Body.GetILProcessor();
            iLProcessor.InsertBefore(fos.Body.Instructions[i], Instruction.Create(OpCodes.Call, rustAssembly.MainModule.Import(EquinoxUnturned.MainModule.GetType("EquinoxUnturned.EquinoxUnturned").GetMethod("Respond"))));
            iLProcessor.InsertBefore(fos.Body.Instructions[i], Instruction.Create(OpCodes.Ldarg_1));
            iLProcessor.InsertBefore(fos.Body.Instructions[i], Instruction.Create(OpCodes.Ldarg_0));*/
            return true;
        }
    }
}