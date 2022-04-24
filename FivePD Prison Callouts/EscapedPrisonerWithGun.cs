using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace EscapedPrisonerWGun
{
    [CalloutProperties("Escaped Prisoner with Weapon", "GGGDunlix", "0.1.0")]
    public class EscapedPrisonerWGun : Callout
    {
        Ped prisoner, guard;
        private Vector3[] coordinates = {
           new Vector3(1854.335f, 2609.614f, 45.67205f),
new Vector3(1771.777f, 2773.05f, 45.92725f),
new Vector3(1595.454f, 2720.772f, 45.92336f),
new Vector3(1525.555f, 2561.914f, 45.81637f),
new Vector3(1591.362f, 2423.942f, 45.81396f),
new Vector3(1790.836f, 2425.504f, 45.84047f),
new Vector3(1825.602f, 2558.276f, 45.6768f),
new Vector3(1856.67f, 2806.813f, 42.38688f),
new Vector3(1861.189f, 2700.246f, 45.61473f),
new Vector3(1727.573f, 2775.676f, 45.58622f),
new Vector3(1603.823f, 2736.533f, 45.58676f),
new Vector3(1543.549f, 2647.04f, 45.56726f),
new Vector3(1521.223f, 2543.637f, 45.58117f),
new Vector3(1571.533f, 2428.887f, 45.57724f),
new Vector3(1709.142f, 2386.96f, 45.58681f),
new Vector3(1788.169f, 2415.925f, 45.57029f),
new Vector3(1830.74f, 2497.407f, 45.33282f),
new Vector3(1828.589f, 2572.767f, 45.33857f),
new Vector3(1823.977f, 2607.621f, 45.24598f),
new Vector3(1753.991f, 2596.405f, 45.23054f),
new Vector3(1756.308f, 2622.403f, 45.23023f),
new Vector3(1692.093f, 2596.03f, 45.22894f),
new Vector3(1693.394f, 2610.214f, 45.22561f),
new Vector3(1656.006f, 2662.199f, 45.21648f),
new Vector3(1623.891f, 2625.154f, 45.23031f),
new Vector3(1712.668f, 2593.606f, 59.54821f),
new Vector3(1742.612f, 2579.077f, 58.80925f),
new Vector3(1699.568f, 2464.798f, 54.82708f),
new Vector3(1557.066f, 2545.482f, 45.23617f),
new Vector3(1609.911f, 2659.665f, 45.2281f),
new Vector3(1673.594f, 2705.125f, 45.23011f),


        };

        public EscapedPrisonerWGun()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(3).First();

            InitInfo(location);
            ShortName = "Escaped Prisoner with Weapon";
            CalloutDescription = "A prisoner escaped from the San Andreas State Penitentiary, and has a guard's gun.";
            ResponseCode = 3;
            StartDistance = 60f;
        }

        public async override Task OnAccept()
        {
            
            InitBlip(50);
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
            guard = await SpawnPed(PedHash.Prisguard01SMM, Location);
            guard.Kill();
            Blip blip = guard.AttachBlip();
            blip.Color = BlipColor.Blue;

            prisoner = await SpawnPed(peds[RandomUtils.Random.Next(peds.Length)], Location);
            prisoner.Weapons.Give(WeaponHash.PistolMk2, 9999, true, true);
            prisoner.AlwaysKeepTask = true;
            prisoner.BlockPermanentEvents = true;

            prisoner.AttachBlip();
            //thanks to ins1de from GTAForums.com :)
            //uint PRISONERS = Convert.ToUInt32(World.AddRelationshipGroup("PRISONERS"));
            //uint PLAYERS = Convert.ToUInt32(World.AddRelationshipGroup("PLAYERS"));
            //
            //////       Game.PlayerPed.RelationshipGroup = PLAYERS;
            //      player.RelationshipGroup = PLAYERS;
            //      prisoner.RelationshipGroup = PRISONERS;



            //     API.SetRelationshipBetweenGroups(255, PRISONERS, PLAYERS);
            //     API.SetRelationshipBetweenGroups(255, PLAYERS, PRISONERS);

            //    API.SetRelationshipBetweenGroups(255, PRISONERS, PLAYERS);
            //    API.SetRelationshipBetweenGroups(255, PLAYERS, PRISONERS);

            //   API.RegisterHatedTargetsAroundPed(prisoner.NetworkId, 200f);
            //   API.TaskCombatHatedTargetsAroundPed(prisoner.NetworkId, 50f, 0);

            // API.SetPlayerAngry(prisoner.NetworkId, true);
            //  API.TaskCombatPed(prisoner.NetworkId, player.NetworkId, 0, 16);
            prisoner.Armor = 500;
            prisoner.FiringPattern = FiringPattern.BurstInCover;
            prisoner.Task.ShootAt(player);

        }
    }


}