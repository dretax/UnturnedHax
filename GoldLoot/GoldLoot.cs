using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Timers;
using Rocket.Core.Plugins;
using SDG.Unturned;

namespace Equinox
{
    public class GoldLoot : RocketPlugin
    {
        private const float GoldChance = 0.45f;
        private const float GoldSpawn = 15f;
        private Timer _timer;

        protected override void Load()
        {
            SetStatic(typeof(ItemManager), "CHANCE_NORMAL", GoldChance);
            SetStatic(typeof(ItemManager), "RESPAWN_NORMAL", GoldSpawn);

            SetStatic(typeof(ItemManager), "RESPAWN_EASY", GoldChance);
            SetStatic(typeof(ItemManager), "CHANCE_EASY", GoldSpawn);

            SetStatic(typeof(ItemManager), "CHANCE_HARD", GoldChance);
            SetStatic(typeof(ItemManager), "RESPAWN_HARD", GoldSpawn);
            _timer = new Timer(50000);
            _timer.Elapsed += new ElapsedEventHandler(Called);
            _timer.Start();
        }

        private void Called(object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine(ItemManager.CHANCE_NORMAL + " - " + ItemManager.RESPAWN_NORMAL);
            if (ItemManager.CHANCE_NORMAL != GoldChance)
            {
                SetStatic(typeof (ItemManager), "CHANCE_NORMAL", GoldChance);
                Console.WriteLine("Re-set: " + ItemManager.CHANCE_NORMAL);
            }
            if (ItemManager.RESPAWN_NORMAL != GoldSpawn)
            {
                SetStatic(typeof (ItemManager), "RESPAWN_NORMAL", GoldSpawn);
                Console.WriteLine("Re-set: " + ItemManager.RESPAWN_NORMAL);
            }
            if (ItemManager.CHANCE_EASY != GoldChance)
            {
                SetStatic(typeof(ItemManager), "CHANCE_EASY", GoldChance);
                Console.WriteLine("Re-set: " + ItemManager.CHANCE_EASY);
            }
            if (ItemManager.RESPAWN_EASY != GoldSpawn)
            {
                SetStatic(typeof(ItemManager), "RESPAWN_EASY", GoldSpawn);
                Console.WriteLine("Re-set: " + ItemManager.RESPAWN_EASY);
            }
            if (ItemManager.CHANCE_HARD != GoldChance)
            {
                SetStatic(typeof(ItemManager), "CHANCE_HARD", GoldChance);
                Console.WriteLine("Re-set: " + ItemManager.CHANCE_HARD);
            }
            if (ItemManager.RESPAWN_HARD != GoldSpawn)
            {
                SetStatic(typeof(ItemManager), "RESPAWN_HARD", GoldSpawn);
                Console.WriteLine("Re-set: " + ItemManager.RESPAWN_HARD);
            }
        }

        protected override void Unload()
        {

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
