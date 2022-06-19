namespace Huatuo.Perf.AOT
{
    [PerfClass(nameof(VoidNoParam), "AOT", "调用AOT函数")]
    public class VoidNoParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    obj.VoidNoParam();
                }
            }
        }
    }

    [PerfClass(nameof(VoidOneValueParam), "AOT", "调用AOT函数")]
    public class VoidOneValueParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    obj.VoidOneValueParam(i);
                }
            }
        }
    }

    [PerfClass(nameof(VoidTwoValueParam), "AOT", "调用AOT函数")]
    public class VoidTwoValueParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    obj.VoidTwoValueParam(i, i + 1);
                }
            }
        }
    }

    [PerfClass(nameof(VoidThreeValueParam), "AOT", "调用AOT函数")]
    public class VoidThreeValueParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    obj.VoidThreeValueParam(i, i + 1, i + 2);
                }
            }
        }
    }

    [PerfClass(nameof(ValueNoParam), "AOT", "调用AOT函数")]
    public class ValueNoParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.ValueNoParam();
                }
            }
        }
    }

    [PerfClass(nameof(ValueOneValueParam), "AOT", "调用AOT函数")]
    public class ValueOneValueParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.ValueOneValueParam(i);
                }
            }
        }
    }

    [PerfClass(nameof(ValueTwoValueParam), "AOT", "调用AOT函数")]
    public class ValueTwoValueParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.ValueTwoValueParam(i, i + 1);
                }
            }
        }
    }

    [PerfClass(nameof(ValueThreeValueParam), "AOT", "调用AOT函数")]
    public class ValueThreeValueParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.ValueThreeValueParam(i, i + 1, i + 2);
                }
            }
        }
    }

    [PerfClass(nameof(Vec3NoParam), "AOT", "调用AOT函数")]
    public class Vec3NoParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.Vec3NoParam();
                }
            }
        }
    }

    [PerfClass(nameof(Vec3OneParam), "AOT", "调用AOT函数")]
    public class Vec3OneParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.Vec3OneParam(new UnityEngine.Vector3(i, i + 1, i + 2));
                }
            }
        }
    }

    [PerfClass(nameof(Vec3TwoParam), "AOT", "调用AOT函数")]
    public class Vec3TwoParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.Vec3TwoParam(new UnityEngine.Vector3(i, i + 1, i + 2), new UnityEngine.Vector3(i, i + 1, i + 2));
                }
            }
        }
    }

    [PerfClass(nameof(ObjNoParam), "AOT", "调用AOT函数")]
    public class ObjNoParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.ObjNoParam();
                }
            }
        }
    }

    [PerfClass(nameof(ObjOneParam), "AOT", "调用AOT函数")]
    public class ObjOneParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.ObjOneParam(new object());
                }
            }
        }
    }

    [PerfClass(nameof(ObjTwoParam), "AOT", "调用AOT函数")]
    public class ObjTwoParam : IBenchmark
    {
        int number;
        int frame;
        AOTFunctionAssist obj;
        public void Clear() { }

        public void Prepare()
        {
            number = PerfLevel.callAOTFunctionCount;
            frame = 10;
            obj = new AOTFunctionAssist();
        }

        public void Run()
        {
            for (int i = 0; i < frame; i++)
            {
                for (var j = 0; j < number; j++)
                {
                    var res = obj.ObjTwoParam(new object(), new object());
                }
            }
        }
    }
}
