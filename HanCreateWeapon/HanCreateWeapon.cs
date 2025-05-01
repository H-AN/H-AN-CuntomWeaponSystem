using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Utils;
using System;
using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Commands;
using System.Data.SqlTypes;
using CounterStrikeSharp.API.Modules.Admin;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static CounterStrikeSharp.API.Core.Listeners;
using CounterStrikeSharp.API.Modules.UserMessages;


namespace HanCreateWeapon;

[MinimumApiVersion(80)]

public class HanCreateWeaponPlugin : BasePlugin
{

    public override string ModuleName => "[华仔]自定义武器系统";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "By : 华仔H-AN";
    public override string ModuleDescription => "自定义武器系统";

    private HanCreateWeaponConfig? WeaponConfig;

    private HanCreateWeaponConfig _weaponsConfig = new();

    GiveItem2 giveItem = new GiveItem2();


    public override void Load(bool hotReload)
    {
        WeaponConfig = HanCreateWeaponConfig.Load();
        
        VirtualFunctions.GiveNamedItemFunc.Hook(OverrideGiveNamedItemPost, HookMode.Post);
        RegisterEventHandler<EventWeaponFire>(OnWeaponFire);
        RegisterListener<Listeners.OnTick>(OnTick);

        foreach (var weapons in WeaponConfig.WeaponList)
        {
            AddCommand(weapons.Command, "", (client, info) => 
            {
                if (client == null || !client.IsValid || !client.PawnIsAlive || client.TeamNum != 3)
                    return;

                var playerPawn = client.PlayerPawn.Value;
                if(playerPawn == null || !playerPawn.IsValid )
                    return;
                
                if(weapons.Slot == "slot1")
                {
                    DropWeapon.DropWeaponSlot1(client);
                    //client.GiveNamedItem(weapons.ClassName);
                    giveItem.PlayerGiveNamedItem(client, weapons.ClassName);
                    client.ExecuteClientCommand("slot3;");
                    if (client.IsValid && playerPawn != null && playerPawn.IsValid && client.TeamNum == 3 && client.PawnIsAlive)
                    { 
                        if(client.PlayerPawn?.IsValid == true && !client.IsBot && !client.IsHLTV && client.PawnIsAlive && client.PlayerPawn.Value!.WeaponServices != null)
                        { 
                            foreach(var weapon in client.PlayerPawn.Value.WeaponServices.MyWeapons)
                            {
                                if(weapon.IsValid && weapon.Value != null)
                                {
                                    CCSWeaponBase ccSWeaponBase = weapon.Value.As<CCSWeaponBase>();
                                    if(ccSWeaponBase != null && ccSWeaponBase.IsValid) 
                                    {
                                        CCSWeaponBaseVData? weaponData = ccSWeaponBase.VData;
                                        if(weaponData == null ||(weaponData.GearSlot != gear_slot_t.GEAR_SLOT_PISTOL && weaponData.GearSlot == gear_slot_t.GEAR_SLOT_RIFLE && weaponData.GearSlot != gear_slot_t.GEAR_SLOT_KNIFE))
                                        {
                                            ccSWeaponBase.AttributeManager.Item.CustomName = weapons.CustomName; //WeaponNames[player.Slot]
                                            ccSWeaponBase.AttributeManager.Item.CustomNameOverride = weapons.CustomName; //WeaponNames[player.Slot]
                                            ccSWeaponBase.AttributeManager.Item.ItemDefinitionIndex = (ushort)weapons.ItemDefinitionIndex; //武器使用的数据
                                            ccSWeaponBase.Globalname = weapons.CustomName;
                                            if(ccSWeaponBase.VData != null && ccSWeaponBase.AttributeManager.Item.CustomName == weapons.CustomName)
                                            {
                                                ccSWeaponBase.VData.MaxClip1 = weapons.MaxClip;
                                                ccSWeaponBase.Clip1 = weapons.MaxClip;
                                                Utilities.SetStateChanged(ccSWeaponBase, "CBasePlayerWeapon", "m_iClip1");
                                            }
                                            
                                        }					
                                    }
                                }

                            }
                        }
                        
                    }
                }
                else if(weapons.Slot == "slot2")
                {
                    DropWeapon.DropWeaponSlot2(client);
                    client.ExecuteClientCommand("slot3;");
                    //client.GiveNamedItem(weapons.ClassName);
                    giveItem.PlayerGiveNamedItem(client, weapons.ClassName);
                    if (client.IsValid && playerPawn != null && playerPawn.IsValid && client.TeamNum == 3 && client.PawnIsAlive)
                    { 
                        if(client.PlayerPawn?.IsValid == true && !client.IsBot && !client.IsHLTV && client.PawnIsAlive && client.PlayerPawn.Value!.WeaponServices != null)
                        { 
                            foreach(var weapon in client.PlayerPawn.Value.WeaponServices.MyWeapons)
                            {
                                if(weapon.IsValid && weapon.Value != null)
                                {
                                    CCSWeaponBase ccSWeaponBase = weapon.Value.As<CCSWeaponBase>();
                                    if(ccSWeaponBase != null && ccSWeaponBase.IsValid) 
                                    {
                                        CCSWeaponBaseVData? weaponData = ccSWeaponBase.VData;
                                        if(weaponData == null ||(weaponData.GearSlot == gear_slot_t.GEAR_SLOT_PISTOL && weaponData.GearSlot != gear_slot_t.GEAR_SLOT_RIFLE && weaponData.GearSlot != gear_slot_t.GEAR_SLOT_KNIFE))
                                        {
                                            ccSWeaponBase.AttributeManager.Item.CustomName = weapons.CustomName; //WeaponNames[player.Slot]
                                            ccSWeaponBase.AttributeManager.Item.CustomNameOverride = weapons.CustomName; //WeaponNames[player.Slot]
                                            ccSWeaponBase.AttributeManager.Item.ItemDefinitionIndex = (ushort)weapons.ItemDefinitionIndex; //武器使用的数据
                                            ccSWeaponBase.Globalname = weapons.CustomName;
                                            if(ccSWeaponBase.VData != null && ccSWeaponBase.AttributeManager.Item.CustomName == weapons.CustomName)
                                            {
                                                ccSWeaponBase.VData.MaxClip1 = weapons.MaxClip;
                                                ccSWeaponBase.Clip1 = weapons.MaxClip;
                                                Utilities.SetStateChanged(ccSWeaponBase, "CBasePlayerWeapon", "m_iClip1");
                                            }
                                            
                                        }							
                                    }
                                }

                            }
                        }
                        
                    }
                }
                else if(weapons.Slot == "slot3")
                {
                    DropWeapon.DropWeaponSlot3(client);
                    client.ExecuteClientCommand("slot1;");
                    //client.GiveNamedItem(weapons.ClassName);
                    giveItem.PlayerGiveNamedItem(client, weapons.ClassName);
                    if (client.IsValid && playerPawn != null && playerPawn.IsValid && client.TeamNum == 3 && client.PawnIsAlive)
                    { 
                        if(client.PlayerPawn?.IsValid == true && !client.IsBot && !client.IsHLTV && client.PawnIsAlive && client.PlayerPawn.Value!.WeaponServices != null)
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
                                            ccSWeaponBase.AttributeManager.Item.CustomName = weapons.CustomName; //WeaponNames[player.Slot]
                                            ccSWeaponBase.AttributeManager.Item.CustomNameOverride = weapons.CustomName; //WeaponNames[player.Slot]
                                            ccSWeaponBase.AttributeManager.Item.ItemDefinitionIndex = (ushort)weapons.ItemDefinitionIndex; //武器使用的数据
                                            ccSWeaponBase.Globalname = weapons.CustomName;
                                            if(ccSWeaponBase.VData != null && ccSWeaponBase.AttributeManager.Item.CustomName == weapons.CustomName)
                                            {
                                                ccSWeaponBase.VData.MaxClip1 = weapons.MaxClip;
                                                ccSWeaponBase.Clip1 = weapons.MaxClip;
                                                Utilities.SetStateChanged(ccSWeaponBase, "CBasePlayerWeapon", "m_iClip1");
                                            }
                                            
                                        }							
                                    }
                                }

                            }
                        }
                        
                    }
                }

                
                

                
            });
        }

        RegisterListener<Listeners.OnServerPrecacheResources>(manifest => 
        {
            List<HanCreateWeaponConfig.WeaponData> WeaponList = WeaponConfig.WeaponConfig();
            foreach (var models in WeaponList)
            {
                if (!string.IsNullOrEmpty(models.VModel))
                {
                    manifest.AddResource(models.VModel);
                }
                if (!string.IsNullOrEmpty(models.WModel))
                {
                    manifest.AddResource(models.WModel);
                }
                if (!string.IsNullOrEmpty(models.SoundEvent))
                {
                    manifest.AddResource(models.SoundEvent);
                }

            }

        });


    }

    public List<string> IgnoredItems = [
        "weapon_decoy",
        "weapon_flashbang",
        "weapon_smokegrenade",
        "weapon_hegrenade",
        "weapon_molotov",
        "weapon_incgrenade",
        "weapon_healthshot",
        "weapon_tagrenade",
        "weapon_breachcharge",
        "weapon_diversion",
        "weapon_firebomb",
        "weapon_frag",
        "weapon_snowball",
        "weapon_tablet",
        "weapon_bumpmine",
        "weapon_shield",
        "weapon_c4",
        "weapon_knife"
    ];

    private HookResult OverrideGiveNamedItemPost(DynamicHook h)
    {
        string weaponClass = h.GetParam<string>(1);
        
        if (string.IsNullOrEmpty(weaponClass) || IgnoredItems.Contains(weaponClass))
            return HookResult.Continue;

        CCSPlayerController? player = GetPlayerFromItemServices(h.GetParam<CCSPlayer_ItemServices>(0));
        CBasePlayerWeapon item = h.GetReturn<CBasePlayerWeapon>();
        
        if (player == null || !player.IsValid || !item.IsValid || player.IsBot || player.TeamNum == 2)
            return HookResult.Continue;

        string globalname = item.Globalname;
        string customname = item.AttributeManager.Item.CustomName;

        // 查找匹配的武器配置
        var weaponConfig = _weaponsConfig.WeaponList.FirstOrDefault(w => w.CustomName == customname);
        if (weaponConfig != null && item.VData != null)
        {
            item.VData.MaxClip1 = weaponConfig.MaxClip;
            item.Clip1 = weaponConfig.MaxClip;
            Utilities.SetStateChanged(item, "CBasePlayerWeapon", "m_iClip1");
        }

        return HookResult.Continue;
    }
    



    public static CCSPlayerController? GetPlayerFromItemServices(CCSPlayer_ItemServices itemServices)
    {
        if (itemServices?.Pawn?.Value is CBasePlayerPawn pawn && pawn.IsValid && pawn.Controller?.IsValid == true && pawn.Controller.Value != null)
        {
            var player = new CCSPlayerController(pawn.Controller.Value.Handle);
            if (player.IsValid && !player.IsBot && !player.IsHLTV && player.Connected == PlayerConnectedState.PlayerConnected)
            {
                return player;
            }
        }

        return null;
    }

    private HookResult OnWeaponFire(EventWeaponFire @event, GameEventInfo info)
    {
        var client = @event.Userid;
        if (client == null) return HookResult.Continue; 
        {
            if(client?.IsValid == true && client.PlayerPawn?.IsValid == true && !client.IsBot && !client.IsHLTV && client.PawnIsAlive && client.PlayerPawn.Value!.WeaponServices != null)
            {
                WeaponConfig = HanCreateWeaponConfig.Load();
                foreach (var weapons in WeaponConfig.WeaponList)
                {
                    foreach(var weapon in client.PlayerPawn.Value.WeaponServices.MyWeapons)
                    {
                        if(weapon.IsValid && weapon.Value != null)
                        {
                            CCSWeaponBase ccSWeaponBase = weapon.Value.As<CCSWeaponBase>();
                            if(ccSWeaponBase != null && ccSWeaponBase.IsValid) 
                            {
                                CCSWeaponBaseVData? weaponData = ccSWeaponBase.VData;

                                if(weapons.Slot == "slot1")
                                {
                                    if(weaponData == null ||(weaponData.GearSlot != gear_slot_t.GEAR_SLOT_PISTOL && weaponData.GearSlot == gear_slot_t.GEAR_SLOT_RIFLE))
                                    {
                                        string globalname = ccSWeaponBase.Globalname;
                                        if (!string.IsNullOrEmpty(globalname))
                                        {
                                            if(!string.IsNullOrEmpty(ccSWeaponBase.AttributeManager.Item.CustomName))
                                            { 
                                                if(ccSWeaponBase.AttributeManager.Item.CustomName == weapons.CustomName )
                                                {
                                                    Server.NextWorldUpdate(() =>
                                                    {
                                                        var weapon = client.PlayerPawn.Value!.WeaponServices!.ActiveWeapon.Value;
                                                        if (weapon != null && weapon == ccSWeaponBase)
                                                        {
                                                            var time = (weapon.NextPrimaryAttackTick - Server.TickCount) / weapons.Rate;
                                                            weapon.NextPrimaryAttackTick = Convert.ToInt32(time + Server.TickCount);
                                                        }
                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                                else if(weapons.Slot == "slot2")
                                {
                                    if(weaponData == null ||(weaponData.GearSlot == gear_slot_t.GEAR_SLOT_PISTOL && weaponData.GearSlot != gear_slot_t.GEAR_SLOT_RIFLE))
                                    {
                                        string globalname = ccSWeaponBase.Globalname;
                                        if (!string.IsNullOrEmpty(globalname))
                                        {
                                            if(!string.IsNullOrEmpty(ccSWeaponBase.AttributeManager.Item.CustomName))
                                            { 
                                                if(ccSWeaponBase.AttributeManager.Item.CustomName == weapons.CustomName )
                                                {
                                                    Server.NextWorldUpdate(() =>
                                                    {
                                                        var weapon = client.PlayerPawn.Value!.WeaponServices!.ActiveWeapon.Value;
                                                        if (weapon != null && weapon == ccSWeaponBase)
                                                        {
                                                            var time = (weapon.NextPrimaryAttackTick - Server.TickCount) / weapons.Rate;
                                                            weapon.NextPrimaryAttackTick = Convert.ToInt32(time + Server.TickCount);
                                                        }
                                                    });
                                                }
                                            }
                                        }
                                    }

                                }

                            }
                        }
                    }
                }
            }
        }
        return HookResult.Continue;
    }

    private void OnTick()
    {
        var UseHuman = Utilities.GetPlayers().Where(player => player.TeamNum == 3 && player.PlayerPawn.Value?.LifeState == (byte)LifeState_t.LIFE_ALIVE);
        foreach (var player in UseHuman)
        {
            if (player == null || !player.IsValid || !player.PawnIsAlive || player.TeamNum != 3)
                return;

            var playerPawn = player.PlayerPawn.Value;

            if (playerPawn == null || !playerPawn.IsValid)
                return;

            WeaponConfig = HanCreateWeaponConfig.Load();
            foreach (var weapons in WeaponConfig.WeaponList)
            {
                var weapon = player?.PlayerPawn?.Value?.WeaponServices?.ActiveWeapon.Value;
                if (weapon == null || !weapon.IsValid)
                    return;
                    
                string globalname = weapon.Globalname;
                if (!string.IsNullOrEmpty(globalname))
                {
                    if(weapon != null && weapon.IsValid && playerPawn != null && playerPawn.IsValid && !string.IsNullOrEmpty(weapon.AttributeManager.Item.CustomName))
                    {
                        if(weapons.Recoil == false)
                        {
                            playerPawn.AimPunchAngle.X = 0;
                            playerPawn.AimPunchAngle.Y = 0;
                            playerPawn.AimPunchAngle.Z = 0;
                            playerPawn.AimPunchAngleVel.X = 0;
                            playerPawn.AimPunchAngleVel.Y = 0;
                            playerPawn.AimPunchAngleVel.Z = 0;
                            playerPawn.AimPunchTickBase = -1;
                            playerPawn.AimPunchTickFraction = 0;    
                        }
                    }

                }

            }
              
        }
    }

 

    private void OnTick2()
    {
        var UseHuman = Utilities.GetPlayers().Where(player => player.TeamNum == 3 && player.PlayerPawn.Value?.LifeState == (byte)LifeState_t.LIFE_ALIVE);
        foreach (var player in UseHuman)
        {
             if (player == null || !player.IsValid || !player.PawnIsAlive || player.TeamNum != 3)
                return;

            var playerPawn = player.PlayerPawn.Value;

            if (playerPawn == null || !playerPawn.IsValid)
                return;

            var weapon = player?.PlayerPawn?.Value?.WeaponServices?.ActiveWeapon.Value;
            if(weapon == null || !weapon.IsValid)
                return;

            WeaponConfig = HanCreateWeaponConfig.Load();
            foreach (var weapons in WeaponConfig.WeaponList)
            {
                string globalname = weapon.Globalname;
                if (!string.IsNullOrEmpty(globalname))
                {
                    if(weapon != null && weapon.IsValid && playerPawn != null && playerPawn.IsValid)
                    {
                        string weaponName = weapon.AttributeManager.Item.CustomName;
                        if (!string.IsNullOrEmpty(globalname))
                        {
                            var weaponConfig = _weaponsConfig.WeaponList.FirstOrDefault(w => w.CustomName == weaponName);
                            if (weaponConfig != null && weaponConfig.Recoil == false)
                            {
                                playerPawn.AimPunchAngle.X = 0;
                                playerPawn.AimPunchAngle.Y = 0;
                                playerPawn.AimPunchAngle.Z = 0;
                                playerPawn.AimPunchAngleVel.X = 0;
                                playerPawn.AimPunchAngleVel.Y = 0;
                                playerPawn.AimPunchAngleVel.Z = 0;
                                playerPawn.AimPunchTickBase = -1;
                                playerPawn.AimPunchTickFraction = 0; 
                            }
                        }

                    }
                }

            }

            
        }
    }

    public HookResult OnPlayerDeathIcon(EventPlayerDeath @event, GameEventInfo info) 
    {
        // 1. 检查攻击者是否有效
        var attacker = @event.Attacker;
        if (attacker == null || !attacker.IsValid || attacker.Connected != PlayerConnectedState.PlayerConnected)
            return HookResult.Continue;

        // 2. 获取当前武器
        var activeWeapon = attacker.PlayerPawn.Value?.WeaponServices?.ActiveWeapon.Value;
        if (activeWeapon == null || !activeWeapon.IsValid)
            return HookResult.Continue;

        // 3. 获取武器自定义名称
        string customName = activeWeapon.AttributeManager.Item.CustomName;
        if (string.IsNullOrEmpty(customName))
            return HookResult.Continue;

        // 4. 查找匹配的武器配置
        var weaponConfig = _weaponsConfig.WeaponList.FirstOrDefault(w => w.CustomName == customName);
        if (weaponConfig == null)
            return HookResult.Continue;

        // 5. 根据配置设置死亡图标
        if (!string.IsNullOrEmpty(weaponConfig.DeathIcon))
        {
            @event.Weapon = weaponConfig.DeathIcon;
        }
        else
        {
            // 默认使用原版武器图标
            @event.Weapon = weaponConfig.ClassName;
        }

        return HookResult.Continue;
    }


    


}
