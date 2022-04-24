using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace DeadInmate
{
    [CalloutProperties("Dead Inmate", "GGGDunlix", "0.1.0")]
    public class DeadInmate  : Callout
    {
        Ped prisoner1;
        private Vector3[] coordinates = {
            new Vector3(1679.68f, 2512.979f, 45.56487f),
            new Vector3(1763.391f, 2536.424f, 45.56491f),
            new Vector3(1652.295f, 2605.754f, 45.56071f),

        };

        public DeadInmate()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(3).First();

            InitInfo(location);
            ShortName = "Dead Inmate";
            CalloutDescription = "A dead inmate was found at Bolingbroke Penitentiary. Respond in Code 2.";
            ResponseCode = 2;
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
            prisoner1.Kill();

        }
    }


}