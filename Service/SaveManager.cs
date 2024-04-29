using System.IO;
using System.Text.Json;
using roguelitri.Model.Save;
using roguelitri.Util;

namespace roguelitri.Service;

public static class SaveManager
{
    private const string SaveFolder = "save";
    private const string SettingsFileName = "roguelitri.settings.json";
    private const string QualifiedSaveFile = SaveFolder + "/" + SettingsFileName;
    
    private static JsonSerializerOptions _serializerOptions = new()
    {
        IncludeFields = true,
        WriteIndented = true
    };
    
    public static Settings LoadSettings()
    {
        Logger.Log("Loading settings");
        if (!File.Exists(SaveFolder + "/" + SettingsFileName))
        {
            Logger.Log("Settings file " + QualifiedSaveFile + " not found, using defaults");
            return new Settings();
        }

        string jsonSave = File.ReadAllText(QualifiedSaveFile);
        return JsonSerializer.Deserialize<Settings>(jsonSave, _serializerOptions);
        
    }

    public static void SaveSettings(Settings settings)
    {
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }
        Logger.Log("Saving settings");
        string jsonString = JsonSerializer.Serialize(settings, _serializerOptions);
        File.WriteAllText(QualifiedSaveFile, jsonString);
    }
}