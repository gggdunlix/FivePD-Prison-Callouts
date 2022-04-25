using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace StolenPrisonBus
{
    [CalloutProperties("Stolen Prison Bus", "GGGDunlix", "0.1.0")]
    public class StolenPrisonBus : Callout
    {
        Ped prisoner1, prisoner2, prisoner3, prisoner4, prisondriver;
        Vehicle bus;
        private Vector3[] coordinates = {
            new Vector3(1881.034f, 2668.174f, 45.38115f),
new Vector3(1822.682f, 2746.321f, 45.63952f),
new Vector3(1726.151f, 2773.449f, 45.62531f),
new Vector3(1603.883f, 2733.926f, 45.62951f),
new Vector3(1540.545f, 2647.792f, 45.63612f),
new Vector3(1518.189f, 2543.714f, 45.64212f),
new Vector3(1569.182f, 2429.191f, 45.63353f),
new Vector3(1718.257f, 2388.296f, 45.63527f),
new Vector3(1813.734f, 2433.634f, 45.63579f),
new Vector3(1863.491f, 2573.945f, 45.35795f),
new Vector3(1797.612f, 2604.701f, 45.27082f),
new Vector3(1812.902f, 2669.367f, 45.25446f),
new Vector3(1807.022f, 2730.495f, 43.08173f),
new Vector3(1717.438f, 2756.997f, 43.07928f),
new Vector3(1615.499f, 2715.175f, 43.0826f),
new Vector3(1559.892f, 2631.133f, 43.10228f),
new Vector3(1541.897f, 2529.678f, 43.09956f),
new Vector3(1573.823f, 2452.696f, 43.10688f),
new Vector3(1705.928f, 2408.591f, 43.10583f),


        };

        public StolenPrisonBus()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(3).First();

            InitInfo(location);
            ShortName = "Stolen Prison Bus";
            CalloutDescription = "5 prisoners have stolen a Prison Bus from Bolingbroke Penitentiary. Respond in Code 3.";
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
            prisoner3 = await SpawnPed(peds[RandomUtils.Random.Next(peds.Length)], Location);
            prisoner4 = await SpawnPed(peds[RandomUtils.Random.Next(peds.Length)], Location);
            prisondriver = await SpawnPed(peds[RandomUtils.Random.Next(peds.Length)], Location);
            bus = await SpawnVehicle(VehicleHash.PBus, Location + 3);


            prisoner1.AlwaysKeepTask = true;
            prisoner1.BlockPermanentEvents = true;

            prisoner2.AlwaysKeepTask = true;
            prisoner2.BlockPermanentEvents = true;

            prisoner3.AlwaysKeepTask = true;
            prisoner3.BlockPermanentEvents = true;

            prisoner4.AlwaysKeepTask = true;
            prisoner4.BlockPermanentEvents = true;

            prisondriver.AlwaysKeepTask = true;
            prisondriver.BlockPermanentEvents = true;



            prisondriver.SetIntoVehicle(bus, VehicleSeat.Driver);
            prisoner1.SetIntoVehicle(bus, VehicleSeat.Any);
            prisoner2.SetIntoVehicle(bus, VehicleSeat.Any);
            prisoner3.SetIntoVehicle(bus, VehicleSeat.Any);
            prisoner4.SetIntoVehicle(bus, VehicleSeat.Any);

            prisondriver.Task.FleeFrom(player);

            Utilities.ExcludeVehicleFromTrafficStop(bus.NetworkId, true);
            



        }
    }


}