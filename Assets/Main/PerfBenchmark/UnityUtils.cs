using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Huatuo.Perf
{
    public class UnityUtils
    {
        public static List<GameObject> BuildObjects(string prefix1, string prefix2, int number)
        {
            List<GameObject> objList = new List<GameObject>();
            for (int i = 0; i < number; i++)
            {
                objList.Add(new GameObject($"{prefix1}{prefix2}{i}"));
            }
            return objList;
        }

        public static void ReleaseObjects(List<GameObject> objList)
        {
            foreach (GameObject obj in objList)
            {
                GameObject.DestroyImmediate(obj);
            }
            GC.Collect();
        }

        public static Mesh BuildMesh()
        {
            var cube = new Mesh();
            cube.name = "hotfix-cube";
            cube.vertices = new Vector3[]
            {
                new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),

                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
            };
            cube.triangles = new int[]
            {
                0,1,2,
                0,2,3,

                0,3,7,
                0,7,4,

                0,5,1,
                0,4,5,

                6,2,1,
                6,1,5,

                6,5,4,
                6,4,7,

                6,7,3,
                6,3,2
            };
            return cube;
        }

        public static Material LoadMaterial()
        {
            return Resources.Load<Material>("Assets/Material/PerfMaterial.mat");
        }
    }
}
