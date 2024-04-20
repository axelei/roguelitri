using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.BitmapFonts;

namespace roguelitri.Service;

public static class Resources
{
    public static class Fonts
    {
        public static BitmapFont IbmVgaFont;
        public static BitmapFont Arcade;
    }

    public static class Music
    {
        public static Song TestSong;
    }

    public static void LoadContent(ContentManager content)
    {
        // Fonts
        Fonts.Arcade = content.Load<BitmapFont>("fonts/arcadepix");
        Fonts.IbmVgaFont = content.Load<BitmapFont>("fonts/ibmVga");
        
        // Music
       Music.TestSong = content.Load<Song>("music/test");
    }
}