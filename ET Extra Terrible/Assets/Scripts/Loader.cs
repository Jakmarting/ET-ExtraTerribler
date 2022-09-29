using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private class LoadingMonoBehaviour : MonoBehaviour { }
    public enum Scene
    {
        MotherShip,
        TitleCard,
        FileSelect,
        BonusBackRooms,
        Area51,
        Pandemic,
        BlackPentagon,
        LoadingScene,
    }
    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;
    public static void Load(Scene scene)
    {
        // Set the loader callback action to load the target scene
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            LoadSceneAsync(scene);
        };

        // Load the loading scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString());

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0f,0f,player.transform.position.z);
}

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
    
        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        // Triggered after the first update which lets the screen refresh
        // Execute the loader callback which will load the target scene
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
