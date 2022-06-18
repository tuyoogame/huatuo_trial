using UnityEngine;

public class PerfEntry : MonoBehaviour
{
    public void OnEntryClick()
    {
        Huatuo.Perf.PerfTestFramework.Instance.CollectAllPerfTask(typeof(PerfEntry).Assembly);
        var res = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Assets/Scenes/Performance.unity");
        res.completed += OnSceneLoaded; ;
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        Debug.Log("Enter Performance Scene");
        PerformanceTestUI.Instance.InitEntryUI();
    }
}
