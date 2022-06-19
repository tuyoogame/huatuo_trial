namespace Huatuo.Perf.Huatuo
{
    [PerfClass(nameof(VoidNoParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(VoidOneValueParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(VoidTwoValueParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(VoidThreeValueParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(ValueNoParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(ValueOneValueParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(ValueTwoValueParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(ValueThreeValueParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(Vec3NoParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(Vec3OneParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(Vec3TwoParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(ObjNoParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(ObjOneParam), "Huatuo", "调用AOT函数")]
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

    [PerfClass(nameof(ObjTwoParam), "Huatuo", "调用AOT函数")]
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
