using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Huatuo.Perf
{
    using PerfTaskResult = Dictionary<string, List<PerfTaskResultItem>>;
    public delegate PerfTaskResult PerfTrigger(string category);
    public delegate void PerfTaskFunc();

    public static class PerfLevel
    {
        public static int unityGameObjectCount = 5000;
        public static int callAOTFunctionCount = 1000000;


        public static uint fibonacciNum = 46;

        public static uint mandelbrotW = 1920;
        public static uint mandelbrotH = 1080;
        public static uint mandelbrotIterations = 8;

        public static uint nbodyAdvancements = 100000000;

        public static uint sieveOfEratosthenesIterations = 1000000;

        public static uint pixarRaytracerW = 120;
        public static uint pixarRaytracerH = 90;
        public static uint pixarRaytracerSamples = 60;

        public static uint firefliesFlockingBoids = 1000;
        public static uint firefliesFlockingLifeTime = 1000;

        public static uint polynomialsIterations = 10000000;

        public static uint particleKinematicsQuantity = 1000;
        public static uint particleKinematicsIterations = 10000000;

        public static uint arcfourIterations = 10000000;

        public static uint seahashIterations = 100000;

        public static uint radixIterations = 1000000;
    }

    public class PerfTestFramework
    {
        private static PerfTestFramework s_Instance;
        public static PerfTestFramework Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new PerfTestFramework();
                }
                return s_Instance;
            }
        }
        public event PerfTrigger ePerfTrigger;
        private Dictionary<string, List<PerfTaskCase>> m_TaskPool;

        public PerfTestFramework()
        {
            m_TaskPool = new Dictionary<string, List<PerfTaskCase>>();
            ePerfTrigger += OnPerfTriggered;
        }

        /// <summary>
        /// 触发Perf测试任务的执行<br/>
        /// category: 为空表示全部执行<br/>
        /// name: category非空且name非空表示执行指定任务，若category非空且name为空表示执行特定category的任务
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        public PerfTaskResult TriggerPerfTask(string category = "")
        {
            return ePerfTrigger(category);
        }

        private PerfTaskResult OnPerfTriggered(string category)
        {
            if (string.IsNullOrEmpty(category)) // 说明全部执行
            {
                return RunAllTask();
            }
            else // 说明执行指定category
            {

            }
            return null;
        }

        public void AddPerfTask(PerfTaskCase task)
        {
            if (!m_TaskPool.TryGetValue(task.Category, out var taskList))
            {
                taskList = new List<PerfTaskCase>();
                m_TaskPool.Add(task.Category, taskList);
            }
            taskList.Add(task);
        }

        public void CollectAllPerfTask(System.Reflection.Assembly asm)
        {
            var perfTestTypes = asm.GetTypes().ToList().FindAll(t => t.GetInterfaces().Contains(typeof(IBenchmark))
                && t.CustomAttributes.Any(item => item.AttributeType == typeof(PerfClassAttribute)));
            foreach (var perfTestType in perfTestTypes)
            {
                var methods = perfTestType.GetMethods().ToList();
                var perfClassAttr = (PerfClassAttribute)perfTestType.GetCustomAttributes(typeof(PerfClassAttribute), false).FirstOrDefault();
                var taskName = perfClassAttr.TaskName;
                var taskType = perfClassAttr.TaskType;
                var category = perfClassAttr.Category;
                var prepareMethod = methods.Find(item => item.Name == "Prepare" && item.GetParameters().Count() == 0);
                var taskMetahod = methods.Find(item => item.Name == "Run" && item.GetParameters().Count() == 0);
                var cleanMetahod = methods.Find(item => item.Name == "Clear" && item.GetParameters().Count() == 0);

                var obj = Activator.CreateInstance(perfTestType);
                var prepareDelegate = Delegate.CreateDelegate(typeof(PerfTaskFunc), obj, prepareMethod);
                var taskDelegate = Delegate.CreateDelegate(typeof(PerfTaskFunc), obj, taskMetahod);
                var cleanDelegate = Delegate.CreateDelegate(typeof(PerfTaskFunc), obj, cleanMetahod);
                AddPerfTask(new PerfTaskCase()
                {
                    TaskName = taskName,
                    TaskType = taskType,
                    Category = category,
                    TaskPrepare = (PerfTaskFunc)prepareDelegate,
                    TaskFunc = (PerfTaskFunc)taskDelegate,
                    TaskCleanup = (PerfTaskFunc)cleanDelegate
                });
            }

        }

        public PerfTaskResult RunAllTask()
        {
            var result = new PerfTaskResult();

            foreach (var item in m_TaskPool)
            {
                var category = item.Key;
                var perfTaskList = item.Value;
                var taskResultList = new List<PerfTaskResultItem>();
                foreach (var task in perfTaskList)
                {
                    UnityEngine.Debug.Log($"Perf Task: {task.TaskType}.{task.TaskName} Start......");
                    task.TaskPrepare();
                    var timeRecord = Stopwatch.StartNew();
                    task.TaskFunc();
                    timeRecord.Stop();
                    task.TaskCleanup();
                    taskResultList.Add(new PerfTaskResultItem()
                    {
                        Category = category,
                        TaskName = task.TaskName,
                        TaskType = task.TaskType,
                        PerfData = new Dictionary<PerfType, double>() { { PerfType.emTimeCost, timeRecord.ElapsedMilliseconds }, }
                    });
                    UnityEngine.Debug.Log($"Perf Task: {task.TaskType}.{task.TaskName} End====");
                }
                result.Add(category, taskResultList);
            }
            return result;
        }
    }

    public class DumpPerfResult
    {
        public static void Dump(PerfTaskResult result, PerfType perfType = PerfType.emTimeCost)
        {
            string savePath = UnityEngine.Application.persistentDataPath + "/huatuo_perf_result.tab";
            if (File.Exists(savePath))
            {
                File.Copy(savePath, savePath + ".bak", true);
            }
            using (var fs = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (var item in result)
                    {
                        var category = item.Key;
                        var perfTaskResultList = item.Value;
                        sw.WriteLine();
                        sw.WriteLine();
                        sw.WriteLine(category);
                        sw.WriteLine();

                        // 做一点数据的整理
                        // TaskName, TaskType, TaskResultDataItem
                        Dictionary<string, Dictionary<string, PerfTaskResultItem>> cache = new Dictionary<string, Dictionary<string, PerfTaskResultItem>>();
                        HashSet<string> taskTypes = new HashSet<string>();
                        foreach (var perfTaskResult in perfTaskResultList)
                        {
                            if (!cache.TryGetValue(perfTaskResult.TaskName, out var taskTypeItem))
                            {
                                taskTypeItem = new Dictionary<string, PerfTaskResultItem>();
                                cache.Add(perfTaskResult.TaskName, taskTypeItem);
                            }
                            taskTypeItem.Add(perfTaskResult.TaskType, perfTaskResult);
                            taskTypes.Add(perfTaskResult.TaskType);
                        }

                        // 写入表格头部
                        sw.Write("case/type\t");
                        sw.Write(string.Join("\t", taskTypes));
                        sw.WriteLine();

                        // 写入表格主体
                        foreach (var taskNameItem in cache)
                        {
                            var taskName = taskNameItem.Key;
                            var taskTypeDatas = taskNameItem.Value;

                            sw.Write(taskName);
                            sw.Write("\t");
                            foreach (var taskType in taskTypes) // 这里要注意和表头保序
                            {
                                if (!taskTypeDatas.TryGetValue(taskType, out var taskTypeRes))
                                {
                                    sw.Write("-\t"); // 没数据就留空
                                    continue;
                                }
                                sw.Write(taskTypeRes.PerfData[perfType]);
                                sw.Write("\t");
                            }
                            sw.Flush();
                            fs.Seek(-1, SeekOrigin.Current); // 移动一下指针把最后一个 "\t" 去掉
                            sw.WriteLine();
                        }
                        // 写入表格数据部分
                    }
                }
            }
        }
    }
}
