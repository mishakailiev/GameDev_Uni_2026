using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTakeDamage
{
    [Test]
    public void PlayerTakesDamage()
    {
        GameObject player = new GameObject();
        PlayerHealth health = player.AddComponent<PlayerHealth>();

        health.health = 3;
        health.Initialize();

        health.TakeDamage(1);

        Assert.AreEqual(2, health.GetCurrentHealth());
    }
}