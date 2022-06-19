using UnityEngine;

public class PerfEntry : MonoBehaviour
{
    public void OnEntryClick()
    {        
        Debug.Log("Enter Performance Clicked");
        var res = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Assets/Scenes/Performance.unity");
        res.completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        Debug.Log("Enter Performance Scene");
        PerformanceTestUI.Instance.InitEntryUI();
    }
}
