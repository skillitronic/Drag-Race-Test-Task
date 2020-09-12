using UnityEngine.SceneManagement;

public class SceneController
{
    private static SceneController _current;
    public static SceneController Current
    {
        get
        {
            if (_current == null)
            {
                _current = new SceneController();
            }

            return _current;
        }

    }

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
