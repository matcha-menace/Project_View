using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class GameManagerTests
{
    private GameManager gameManager;

    [SetUp]
    public void SetUp()
    {
        GameObject gameManagerObject = new GameObject("GameManager");
        gameManager = gameManagerObject.AddComponent<GameManager>();
    }

    [TearDown]
    public void TearDown()
    {
        if (gameManager != null && gameManager.gameObject != null)
        {
            Object.Destroy(gameManager.gameObject);
        }

    }

    [Test]
    public void GameManager_InitialState_IsMainMenu()
    {
        // Assert that the initial state is MainMenu
        Assert.AreEqual(GameState.MainMenu, gameManager.currentState);
    }

    [Test]
    public void GameManager_UpdateState_ChangesStateCorrectly()
    {
        // Change the state to Lv1
        gameManager.UpdateState(GameState.Lv1);
        Assert.AreEqual(GameState.Lv1, gameManager.currentState);

        // Change the state to Paused
        gameManager.UpdateState(GameState.paused);
        Assert.AreEqual(GameState.paused, gameManager.currentState);

        // Change the state back to MainMenu
        gameManager.UpdateState(GameState.MainMenu);
        Assert.AreEqual(GameState.MainMenu, gameManager.currentState);
    }

    [Test]
    public void GameManager_Singleton_InstanceIsSetCorrectly()
    {
        // Assert that the singleton instance is set correctly
        Assert.IsNotNull(GameManager.Instance);
    }

    [UnityTest]
    public IEnumerator GameManager_Singleton_OnlyOneInstanceExists()
    {
        GameObject secondGameManagerObject = new GameObject("SecondGameManager");
        GameManager secondGameManager = secondGameManagerObject.AddComponent<GameManager>();
        yield return null;

        Assert.IsTrue(secondGameManagerObject == null, "Duplicate singleton was not destroyed.");
        Assert.AreEqual(gameManager, GameManager.Instance);
    }
}