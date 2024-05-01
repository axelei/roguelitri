using System.IO;
using System.Text.Json;
using roguelitri.Model;
using roguelitri.Util;

namespace roguelitri.Service;

public class LevelManager
{
    private static JsonSerializerOptions _serializerOptions = new()
    {
        IncludeFields = true,
        WriteIndented = true
    };
    
    public static Level LoadLevel(string level)
    {
        Logger.Log("Loading level: " + level);
        string jsonSave = File.ReadAllText(level);
        Level levelObject = JsonSerializer.Deserialize<Level>(jsonSave, _serializerOptions);
        Logger.Log("Level loaded: " + levelObject.Name);
        return levelObject;
    }
}