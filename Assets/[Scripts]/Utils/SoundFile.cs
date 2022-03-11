// SoundFile.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

using UnityEngine;

/// <summary>
///   <para>A structure to hold settings for audio clips</para>
/// </summary>
[System.Serializable]
public class SoundFile
{
    /// <summary>
    ///   <para>The audio clip</para>
    /// </summary>
    public AudioClip file;

    /// <summary>
    ///   <para>The volume the audio clip should be played at</para>
    /// </summary>
    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    /// <summary>
    ///   <para>Play the file once with the given audio source</para>
    /// </summary>
    /// <param name="source">AudioSource to use</param>
    public void PlayWithSource(AudioSource source)
    {
        source.PlayOneShot(file, volume);
    }
}
