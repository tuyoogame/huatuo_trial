using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateByHotFix2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CreateByHotFix2");
        Huatuo.Perf.PerfTestFramework.Instance.CollectAllPerfTask(typeof(CreateByHotFix2).Assembly);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
