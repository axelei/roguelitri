using System;
using System.Text.Json.Serialization;

namespace roguelitri.Model;

public class Level
{
    public string Name;
    public string Description;
    public int Seed;
    
    [JsonIgnore]
    public Random Random;
}