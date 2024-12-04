using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Slider timeGage;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }
    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;
        timeGage.value = asyncLoad.progress / 1.0f;
        while (!asyncLoad.isDone)
        {
            yield return null;
            if (asyncLoad.progress < 0.7f)
            {
                timeGage.value = asyncLoad.progress / 1.0f;
            }
            else
            {
                timeGage.value += 0.01f;
                if (timeGage.value >= 1.0f)
                {
                    asyncLoad.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}
