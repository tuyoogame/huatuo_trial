using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Huatuo.Perf
{
    public class AOTFunctionAssist
    {
        public void VoidNoParam() {}

        public void VoidOneValueParam(int a) {}

        public void VoidTwoValueParam(int a, int b) { }

        public void VoidThreeValueParam(int a, int b, int c) { }

        public void VoidMultiValueParam(int a, int b, int c, int d, float e, float f, float g, float h) { }

        public int ValueNoParam() { return 0; }

        public int ValueOneValueParam(int a) { return 0; }

        public int ValueTwoValueParam(int a, int b) { return 0; }

        public int ValueThreeValueParam(int a, int b, int c) { return 0; }

        public int ValueMultiValueParam(int a, int b, int c, int d, float e, float f, float g, float h) { return 0; }

        public Vector3 Vec3NoParam() { return new Vector3(1, 1, 1); }

        public Vector3 Vec3OneParam(Vector3 vec1) { return new Vector3(1, 1, 1); }

        public Vector3 Vec3TwoParam(Vector3 vec1, Vector3 vec2) { return new Vector3(1, 1, 1); }

        public object ObjNoParam() { return new object(); }
        public object ObjOneParam(object obj1) { return new object(); }
        public object ObjTwoParam(object obj1, object obj2) { return new object(); }
    }
}
