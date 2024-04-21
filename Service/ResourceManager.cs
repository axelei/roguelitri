using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace roguelitri.Service;

public static class ResourceManager
{
    public static class Fonts
    {
        public static string IbmVgaFont;
        public static string Arcade;
    }

    public static class Music
    {
        public static Song TestSong;
    }

    public static void LoadContent(ContentManager content)
    {
        // Fonts
        Fonts.Arcade = "fonts/arcadepix.fnt";
        Fonts.IbmVgaFont = "fonts/ibmVga.fnt";
        
        // Music
       Music.TestSong = content.Load<Song>("music/test");
    }
}