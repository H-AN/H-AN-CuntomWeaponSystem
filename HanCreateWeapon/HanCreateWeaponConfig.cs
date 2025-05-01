using CounterStrikeSharp.API.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using System.IO;




public class HanCreateWeaponConfig
{
    public class WeaponData
    {
        [JsonPropertyName("customname")]
        public required string CustomName { get; set; }

        [JsonPropertyName("command")]
        public required string Command  { get; set; }

        [JsonPropertyName("classname")]
        public required string ClassName  { get; set; }

        [JsonPropertyName("itemDefinitionIndex")] //武器数据
        public int ItemDefinitionIndex  { get; set; } 

        [JsonPropertyName("maxclip")] //弹匣
        public int MaxClip   { get; set; } 

        [JsonPropertyName("rate")] //射速
        public float Rate   { get; set; } 

        [JsonPropertyName("recoil")] //后坐力
        public bool Recoil   { get; set; } 

        [JsonPropertyName("deathicon")]
        public required string DeathIcon  { get; set; }

        [JsonPropertyName("slot")]
        public required string Slot  { get; set; }
        
        [JsonPropertyName("vmodel")]
        public required string VModel  { get; set; }

        [JsonPropertyName("wmodel")]
        public required string WModel  { get; set; }

        [JsonPropertyName("soundevent")]
        public required string SoundEvent  { get; set; }

    }

    [JsonPropertyName("WeaponList")]
    public List<WeaponData> WeaponList { get; set; } = new List<WeaponData>();

    public static string ConfigPath = Path.Combine(Application.RootDirectory, "configs", "HanCreateWeapon", "CreateWeaponData.json");

    public static HanCreateWeaponConfig Load()
    {
        if (File.Exists(ConfigPath))
        {
            try
            {
                string json = File.ReadAllText(ConfigPath);
                // 尝试反序列化配置
                var config = JsonSerializer.Deserialize<HanCreateWeaponConfig>(json);
                if (config != null)
                {
                    return config; // 成功反序列化，返回配置
                }
                else
                {
                    Console.WriteLine("读取配置文件失败，使用默认配置。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取配置文件时发生错误: {ex.Message}，使用默认配置。");
            }
        }

        // 如果配置文件不存在或读取失败，则创建默认配置并保存
        var defaultConfig = new HanCreateWeaponConfig
        {
            
            WeaponList = new List<WeaponData>
            {
                new WeaponData
                {
                    CustomName = "testawp",
                    Command = "css_createawp",
                    ClassName = "weapon_awp",
                    ItemDefinitionIndex = 9,
                    MaxClip = 100,
                    Rate = 2.0f,
                    Recoil = false,
                    DeathIcon = "weapon_ak47",
                    Slot = "slot1",
                    VModel = "VModel",
                    WModel = "WModel",
                    SoundEvent = "SoundEvent/"
                },
                new WeaponData
                {
                    CustomName = "testak",
                    Command = "css_createak47",
                    ClassName = "weapon_ak47",
                    ItemDefinitionIndex = 7,
                    MaxClip = 20,
                    Rate = 0.1f,
                    Recoil = false,
                    DeathIcon = "weapon_p90",
                    Slot = "slot1",
                    VModel = "VModel",
                    WModel = "WModel",
                    SoundEvent = "SoundEvent/"
                }
            }
            
        };
        Save(defaultConfig); // 保存默认配置
        return defaultConfig;
    }

    public static void Save(HanCreateWeaponConfig config)
    {
        try
        {
            string directoryPath = Path.GetDirectoryName(ConfigPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"[WeaponData] 文件夹 {directoryPath} 不存在，已创建.");
            }

            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
            Console.WriteLine("[WeaponData] 配置文件已保存。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WeaponData] 无法写入配置文件: {ex.Message}");
            Console.WriteLine($"详细错误：{ex.StackTrace}");
        }
    }

    public List<HanCreateWeaponConfig.WeaponData> WeaponConfig()
    {
        // 从配置文件加载并返回皮肤列表
        var config = HanCreateWeaponConfig.Load();
        return config.WeaponList;
    }


    

}