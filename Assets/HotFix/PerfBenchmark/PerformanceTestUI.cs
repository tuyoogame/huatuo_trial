using Huatuo.Perf;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceTestUI
{
    GameObject m_CanvasGo;
    Button m_BtnRunAll;
    Button m_BtnSaveReport;

    Dictionary<string, List<PerfTaskResultItem>> m_PerfResult;
    private static PerformanceTestUI s_Instance;
    public static PerformanceTestUI Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new PerformanceTestUI();
            }
            return s_Instance;
        }
    }

    public void OnAwake()
    {
    }

    public void OnDispose()
    {
    }

    public void OnReset()
    {
    }

    public void OnStart()
    {
    }

    public void InitEntryUI()
    {
        m_CanvasGo = GameObject.Find("PerformanceCanvas");

        var btnRunAllGo = GameObject.Find("PerformanceCanvas/Btn_RunAll");
        m_BtnRunAll = btnRunAllGo.GetComponent<Button>();
        m_BtnRunAll.onClick.AddListener(OnClickRunAll);

        var btnSaveReportGo = GameObject.Find("PerformanceCanvas/Btn_SaveReport");
        m_BtnSaveReport = btnSaveReportGo.GetComponent<Button>();
        m_BtnSaveReport.onClick.AddListener(OnClickSaveReporter);
    }

    private void OnClickRunAll()
    {
        m_BtnRunAll.gameObject.SetActive(false);
        m_BtnSaveReport.gameObject.SetActive(false);
        m_PerfResult = PerfTestFramework.Instance.TriggerPerfTask();
        m_BtnRunAll.gameObject.SetActive(true);
        m_BtnSaveReport.gameObject.SetActive(true);
        DumpPerfResult.Dump(m_PerfResult);
    }

    private void OnClickSaveReporter()
    {
        DumpPerfResult.Dump(m_PerfResult);
    }
}
