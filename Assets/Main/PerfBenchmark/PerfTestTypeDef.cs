using System;
using System.Collections.Generic;

namespace Huatuo.Perf
{
    public class PerfTaskKey
    {
        public string Category; // 表示当前是什么测试项目，比如测试逻辑，测试和Unity交互
        public string TaskName; // 测试case，比如是 斐波那契还是GO旋转
        public string TaskType; // 测试类型，比如是原生，还是huatuo

        public class PerfTaskKeyComparer : IEqualityComparer<PerfTaskKey>
        {
            public bool Equals(PerfTaskKey x, PerfTaskKey y)
            {
                return x.Category.CompareTo(y.Category) == 0
                    && x.TaskName.CompareTo(y.TaskName) == 0
                    && x.TaskType.CompareTo(y.TaskType) == 0;
            }

            public int GetHashCode(PerfTaskKey obj)
            {
                return (obj.Category + obj.TaskName + obj.TaskType).GetHashCode();
            }
        }
    }

    public class PerfTaskCase : PerfTaskKey
    {
        public PerfTaskFunc TaskPrepare; // 测试开始前的准备逻辑，不计入时间
        public PerfTaskFunc TaskFunc; // 测试执行逻辑，计时，计算其他性能数据
        public PerfTaskFunc TaskCleanup; // 测试结束时的清理工作，不计时
    }

    public enum PerfType
    {
        emTimeCost,
    }

    public class PerfTaskResultItem : PerfTaskKey
    {
        public Dictionary<PerfType, double> PerfData;
    }

    public interface IBenchmark
    {
        void Prepare();
        void Run();
        void Clear();
    }

    public class PerfClassAttribute : Attribute
    {
        public string Category;
        public string TaskName;
        public string TaskType;
        public PerfClassAttribute(string taskName, string taskType, string category)
        {
            this.Category = category;
            this.TaskName = taskName;
            this.TaskType = taskType;
        }
    }
}
