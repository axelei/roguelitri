using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace roguelitri.Service;

public static class MusicManager
{
    private static Song _playing;
    public static bool IsPlaying { get; private set; }
    public static bool IsPaused { get; private set; }
    
    public static void Play(Song song)
    {
        if (_playing != null)
        {
            MediaPlayer.Stop();
        }

        _playing = song;
        MediaPlayer.Play(song);
        IsPaused = false;
        IsPlaying = true;
    }

    public static void Pause()
    {
        MediaPlayer.Pause();
        IsPaused = true;
        IsPlaying = false;
    }

    public static void Resume()
    {
        MediaPlayer.Resume();
        IsPaused = false;
        IsPlaying = false;
    }

    public static void Stop()
    {
        if (_playing != null && IsPlaying)
        {
            MediaPlayer.Stop();
        }

        IsPlaying = false;
    }
}