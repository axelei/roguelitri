﻿using System;

namespace roguelitri.Model.Save;

[Serializable]
public struct Settings
{
    public float MusicVolume = 0.6f;
    public float SfxVolume = 1.0f;
    
    public bool AutoSave = true;

    public Settings()
    {
    }
}