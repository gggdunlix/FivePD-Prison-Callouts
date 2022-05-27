using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace PrisonFight
{
    [CalloutProperties("Prison Fight", "GGGDunlix", "0.1.0")]
    public class PrisonFight : Callout
    {
        Ped prisoner1, prisoner2;
        private Vector3[] coordinates = {
            new Vector3(1679.68f, 2512.979f, 45.56487f),
            new Vector3(1763.391f, 2536.424f, 45.56491f),
            new Vector3(1652.295f, 2605.754f, 45.56071f),

        };

        public PrisonFight()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(3).First();

            InitInfo(location);
            ShortName = "Prison Fight";
            CalloutDescription = "2 prisoners are fighting at the Bolingbroke Penitentiary. Respond in Code 3.";
            ResponseCode = 3;
            StartDistance = 60f;
        }

        public async override Task OnAccept()
        {
            
            InitBlip(30);
            UpdateData();
            
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            var peds = new[]
            {
                PedHash.Prisoner01,
                PedHash.Prisoner01SMY,
                PedHash.PrisMuscl01SMY
            };
            prisoner1 = await SpawnPed(peds[RandomUtils.Random.Next(peds.Length)], Location);
            prisoner2 = await SpawnPed(peds[RandomUtils.Random.Next(peds.Length)], Location);
            prisoner1.AlwaysKeepTask = true;
            prisoner1.BlockPermanentEvents = true;

            prisoner2.AlwaysKeepTask = true;
            prisoner2.BlockPermanentEvents = true;

            prisoner1.Armor = 1000;
            prisoner2.Armor = 1000;

            prisoner1.Task.FightAgainst(prisoner2);
            prisoner2.Task.FightAgainst(prisoner1);

        }
    }


}