// IItemPickUp.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

/// <summary>
///   <para>Interface to be implemented by any scripts that handle custom logic for being picked up by the player</para>
///   <para>Implementing this is not required, but it helps to prevent errors.</para>
/// </summary>
public interface IItemPickUp
{
    /// <summary>
    ///   <para>Any logics to be performed when picked up by the player</para>
    /// </summary>
    /// <param name="player">The player manager</param>
    void OnItemPickUp(PlayerManager player);
}
