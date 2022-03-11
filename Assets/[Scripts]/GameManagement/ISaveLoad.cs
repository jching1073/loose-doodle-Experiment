// ISaveLoad.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

using System.IO;

/// <summary>
///   <para>An interface for any Unity objects that needs save/load to happen</para>
/// </summary>
public interface ISaveLoad
{
    void Save(TextWriter writer);

    void Load(TextReader reader);
}
