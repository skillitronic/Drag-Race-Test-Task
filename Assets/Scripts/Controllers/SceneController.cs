using UnityEngine.SceneManagement;

public class SceneController
{

    public static void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public static void AddSceneByName(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public static void UnloadSceneByName(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }
}
