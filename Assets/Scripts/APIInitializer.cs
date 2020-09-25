using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

public class APIInitializer : MonoBehaviour
{
    private void Awake()
    {
        GameAnalytics.Initialize();
        FB.Init();
    }
}
