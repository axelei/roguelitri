using MonoSound.Streaming;

namespace roguelitri.Service;

public static class MusicManager
{
    private static StreamPackage _playing;
    
    public static void Play(StreamPackage music)
    {
        _playing?.Stop();
        _playing = music;
        _playing.Play();
        _playing.IsLooping = true;
        _playing.Metrics.Volume = Game1.Settings.MusicVolume;
    }
    
    public static void Stop()
    {
        _playing?.Stop();
    }
    
    public static void Pause()
    {
        _playing?.Pause();
    }
    
    public static void Resume()
    {
        _playing?.Resume();
    }
}