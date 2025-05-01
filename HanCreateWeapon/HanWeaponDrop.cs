using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Serialization;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Admin;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.Json;
using System.Drawing;
using CounterStrikeSharp.API.Core.Capabilities;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class DropWeapon
{
    public static void DropWeaponSlot1(CCSPlayerController client)
    {
        if(client?.IsValid == true && client.PlayerPawn?.IsValid == true && !client.IsBot && !client.IsHLTV && client.PawnIsAlive && client.PlayerPawn.Value!.WeaponServices != null)
        {
            foreach(var weapon in client.PlayerPawn.Value.WeaponServices.MyWeapons)
            {
                if(weapon.IsValid && weapon.Value != null)
                {
                    CCSWeaponBase ccSWeaponBase = weapon.Value.As<CCSWeaponBase>();
                    if(ccSWeaponBase != null && ccSWeaponBase.IsValid) 
                    {
                        CCSWeaponBaseVData? weaponData = ccSWeaponBase.VData;
                        if(weaponData == null ||(weaponData.GearSlot != gear_slot_t.GEAR_SLOT_PISTOL && weaponData.GearSlot == gear_slot_t.GEAR_SLOT_RIFLE))
                        {
                            client.PlayerPawn.Value.WeaponServices.ActiveWeapon.Raw = weapon.Raw;
                            client.DropActiveWeapon();
                            Server.NextFrame(() =>
                            {
                                if (ccSWeaponBase != null && ccSWeaponBase.IsValid)
                                {
                                    ccSWeaponBase.AcceptInput("Kill");
                                }
                            });
                        }

                    }
                }
            }
        }

    }

    public static void DropWeaponSlot2(CCSPlayerController client)
    {
        if(client?.IsValid == true && client.PlayerPawn?.IsValid == true && !client.IsBot && !client.IsHLTV && client.PawnIsAlive && client.PlayerPawn.Value!.WeaponServices != null)
        {
            foreach(var weapon in client.PlayerPawn.Value.WeaponServices.MyWeapons)
            {
                if(weapon.IsValid && weapon.Value != null)
                {
                    CCSWeaponBase ccSWeaponBase = weapon.Value.As<CCSWeaponBase>();
                    if(ccSWeaponBase != null && ccSWeaponBase.IsValid) 
                    {
                        CCSWeaponBaseVData? weaponData = ccSWeaponBase.VData;
                        if(weaponData == null ||(weaponData.GearSlot == gear_slot_t.GEAR_SLOT_PISTOL && weaponData.GearSlot != gear_slot_t.GEAR_SLOT_RIFLE))
                        {
                            client.PlayerPawn.Value.WeaponServices.ActiveWeapon.Raw = weapon.Raw;
                            client.DropActiveWeapon();
                            Server.NextFrame(() =>
                            {
                                if (ccSWeaponBase != null && ccSWeaponBase.IsValid)
                                {
                                    ccSWeaponBase.AcceptInput("Kill");
                                }
                            });
                        }

                    }
                }
            }
        }

    }

    public static void DropWeaponSlot3(CCSPlayerController client)
    {
        if(client?.IsValid == true && client.PlayerPawn?.IsValid == true && !client.IsBot && !client.IsHLTV && client.PawnIsAlive && client.PlayerPawn.Value!.WeaponServices != null)
        {
            foreach(var weapon in client.PlayerPawn.Value.WeaponServices.MyWeapons)
            {
                if(weapon.IsValid && weapon.Value != null)
                {
                    CCSWeaponBase ccSWeaponBase = weapon.Value.As<CCSWeaponBase>();
                    if(ccSWeaponBase != null && ccSWeaponBase.IsValid) 
                    {
                        CCSWeaponBaseVData? weaponData = ccSWeaponBase.VData;
                        if(weaponData == null ||(weaponData.GearSlot != gear_slot_t.GEAR_SLOT_PISTOL && weaponData.GearSlot != gear_slot_t.GEAR_SLOT_RIFLE && weaponData.GearSlot == gear_slot_t.GEAR_SLOT_KNIFE))
                        {
                            client.PlayerPawn.Value.WeaponServices.ActiveWeapon.Raw = weapon.Raw;
                            client.DropActiveWeapon();
                            Server.NextFrame(() =>
                            {
                                if (ccSWeaponBase != null && ccSWeaponBase.IsValid)
                                {
                                    ccSWeaponBase.AcceptInput("Kill");
                                }
                            });
                        }

                    }
                }
            }
        }

    }

    private static readonly Dictionary<int, (string Name, string Class)> ItemDefinitionIndexes = new()
    {
        { 1, ("Desert Eagle", "weapon_deagle") },
        { 2, ("Dual Berettas", "weapon_elite") },
        { 3, ("Five-SeveN", "weapon_fiveseven") },
        { 4, ("Glock-18", "weapon_glock") },
        { 7, ("AK-47", "weapon_ak47") },
        { 8, ("AUG", "weapon_aug") },
        { 9, ("AWP", "weapon_awp") },
        { 10, ("FAMAS", "weapon_famas") },
        { 11, ("G3SG1", "weapon_g3sg1") },
        { 13, ("Galil AR", "weapon_galilar") },
        { 14, ("M249", "weapon_m249") },
        { 16, ("M4A4", "weapon_m4a1") },
        { 17, ("MAC-10", "weapon_mac10") },
        { 19, ("P90", "weapon_p90") },
        { 23, ("MP5-SD", "weapon_mp5sd") },
        { 24, ("UMP-45", "weapon_ump45") },
        { 25, ("XM1014", "weapon_xm1014") },
        { 26, ("PP-Bizon", "weapon_bizon") },
        { 27, ("MAG-7", "weapon_mag7") },
        { 28, ("Negev", "weapon_negev") },
        { 29, ("Sawed-Off", "weapon_sawedoff") },
        { 30, ("Tec-9", "weapon_tec9") },
        { 31, ("Zeus x27", "weapon_taser") },
        { 32, ("P2000", "weapon_hkp2000") },
        { 33, ("MP7", "weapon_mp7") },
        { 34, ("MP9", "weapon_mp9") },
        { 35, ("Nova", "weapon_nova") },
        { 36, ("P250", "weapon_p250") },
        { 37, ("Riot Shield", "weapon_shield") },
        { 38, ("SCAR-20", "weapon_scar20") },
        { 39, ("SG 553", "weapon_sg556") },
        { 40, ("SSG 08", "weapon_ssg08") },
        { 41, ("Knife", "") },
        { 42, ("Knife", "weapon_knife") },
        { 43, ("Flashbang", "weapon_flashbang") },
        { 44, ("High Explosive Grenade", "weapon_hegrenade") },
        { 45, ("Smoke Grenade", "weapon_smokegrenade") },
        { 46, ("Molotov", "weapon_molotov") },
        { 47, ("Decoy Grenade", "weapon_decoy") },
        { 48, ("Incendiary Grenade", "weapon_incgrenade") },
        { 49, ("C4 Explosive", "weapon_c4") },
        { 57, ("Medi-Shot", "weapon_healthshot") },
        { 59, ("Knife", "weapon_knife_t") },
        { 60, ("M4A1-S", "weapon_m4a1_silencer") },
        //{ 60, ("M4A1-S", "weapon_m4a1_silencer_off") },
        { 61, ("USP-S", "weapon_usp_silencer") },
        //{ 61, ("USP-S", "weapon_usp_silencer_off") },
        { 63, ("CZ75-Auto", "weapon_cz75a") },
        { 64, ("R8 Revolver", "weapon_revolver") },
        { 68, ("Tactical Awareness Grenade", "weapon_tagrenade") },
        { 69, ("Bare Hand", "weapon_fists") },
        { 70, ("Breach Charge", "weapon_breachcharge") },
        { 72, ("Tablet", "weapon_tablet") },
        { 74, ("Knife", "") },
        { 75, ("Axe", "weapon_axe") },
        { 76, ("Hammer", "weapon_hammer") },
        { 78, ("Wrench", "weapon_spanner") },
        { 80, ("Spectral Shiv", "") },
        { 81, ("Fire Bomb", "") },
        { 82, ("Diversion Device", "") },
        { 83, ("Frag Grenade", "") },
        { 84, ("Snowball", "weapon_snowball") },
        { 85, ("Bump Mine", "weapon_bumpmine") },
        { 500, ("Bayonet", "weapon_bayonet") },
        { 503, ("Classic Knife", "weapon_knife_css") },
        { 505, ("Flip Knife", "weapon_knife_flip") },
        { 506, ("Gut Knife", "weapon_knife_gut") },
        { 507, ("Karambit", "weapon_knife_karambit") },
        { 508, ("M9 Bayonet", "weapon_knife_m9_bayonet") },
        { 509, ("Huntsman Knife", "weapon_knife_tactical") },
        { 512, ("Falchion Knife", "weapon_knife_falchion") },
        { 514, ("Bowie Knife", "weapon_knife_survival_bowie") },
        { 515, ("Butterfly Knife", "weapon_knife_butterfly") },
        { 516, ("Shadow Daggers", "weapon_knife_push") },
        { 517, ("Paracord Knife", "weapon_knife_cord") },
        { 518, ("Survival Knife", "weapon_knife_canis") },
        { 519, ("Ursus Knife", "weapon_knife_ursus") },
        { 520, ("Navaja Knife", "weapon_knife_gypsy_jackknife") },
        { 521, ("Nomad Knife", "weapon_knife_outdoor") },
        { 522, ("Stiletto Knife", "weapon_knife_stiletto") },
        { 523, ("Talon Knife", "weapon_knife_widowmaker") },
        { 525, ("Skeleton Knife", "weapon_knife_skeleton") },
        { 526, ("Kukri Knife", "weapon_knife_kukri") },
        { 5028, ("Default T Gloves", "t_gloves") },
        { 5029, ("Default CT Gloves", "ct_gloves") },
        { 5030, ("Sport Gloves", "sporty_gloves") },
        { 5031, ("Driver Gloves", "slick_gloves") },
        { 5032, ("Hand Wraps", "leather_handwraps") },
        { 5033, ("Moto Gloves", "motorcycle_gloves") },
        { 5034, ("Specialist Gloves", "specialist_gloves") },
        { 5035, ("Hydra Gloves", "studded_hydra_gloves") },
        { 5027, ("Bloodhound Gloves", "studded_bloodhound_gloves") },
        { 4725, ("Broken Fang Gloves", "studded_brokenfang_gloves") },
    };

}