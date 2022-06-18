﻿using System.Collections.Generic;
using UnityEngine;

namespace Huatuo.Perf.AOT
{
    [PerfClass(nameof(UnitySetPosition), "AOT", "和Unity交互")]
    class UnitySetPosition : IBenchmark
    {
        List<GameObject> objList;
        int frame;

        public void Clear()
        {
            UnityUtils.ReleaseObjects(objList);
        }

        public void Prepare()
        {
            objList = UnityUtils.BuildObjects("AOT", nameof(UnitySetPosition), PerfLevel.unityGameObjectCount);
            this.frame = 10;
        }

        public void Run()
        {
            for (int frameIndex = 0; frameIndex < frame; ++frameIndex)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    var obj = objList[i];
                    obj.transform.position = new Vector3(i, i, i);
                }
            }
        }
    }

    [PerfClass(nameof(UnityRotate), "AOT", "和Unity交互")]
    public class UnityRotate : IBenchmark
    {
        List<GameObject> objList;
        int frame;
        public void Clear()
        {
            UnityUtils.ReleaseObjects(objList);
        }

        public void Prepare()
        {
            objList = UnityUtils.BuildObjects("AOT", nameof(UnityRotate), PerfLevel.unityGameObjectCount);
            this.frame = 10;
        }

        public void Run()
        {
            for (int frameIndex = 0; frameIndex < frame; ++frameIndex)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    var obj = objList[i];
                    obj.transform.Rotate(Vector3.up, 60 * i);
                }
            }
        }
    }

    [PerfClass(nameof(UnityNewObject), "AOT", "和Unity交互")]
    public class UnityNewObject : IBenchmark
    {
        List<GameObject> objList;
        int frame;
        int number;

        public void Clear()
        {
            UnityUtils.ReleaseObjects(objList);
        }

        public void Prepare()
        {
            this.number = PerfLevel.unityGameObjectCount;
            this.frame = 10;
            objList = new List<GameObject>();
        }

        public void Run()
        {
            for (int frameIndex = 0; frameIndex < frame; ++frameIndex)
            {
                for (int i = 0; i < number; i++)
                {
                    objList.Add(new GameObject($"AOT{nameof(UnityNewObject)}{frameIndex * number + i}"));
                }
            }
        }
    }

    [PerfClass(nameof(UnitySetMesh), "AOT", "和Unity交互")]
    public class UnitySetMesh : IBenchmark
    {
        List<GameObject> objList;
        int frame;
        public void Clear()
        {
            UnityUtils.ReleaseObjects(objList);
        }

        public void Prepare()
        {
            objList = UnityUtils.BuildObjects("AOT", nameof(UnitySetMesh), PerfLevel.unityGameObjectCount);
            this.frame = 10;
        }

        public void Run()
        {
            for (int frameIndex = 0; frameIndex < frame; ++frameIndex)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    var obj = objList[i];
                    var meshFilter = obj.AddComponent<MeshFilter>();
                    meshFilter.mesh = UnityUtils.BuildMesh();

                    var meshRender = obj.AddComponent<MeshRenderer>();
                    meshRender.material = UnityUtils.LoadMaterial();

                    GameObject.DestroyImmediate(meshFilter);
                    GameObject.DestroyImmediate(meshRender);
                }
            }
        }
    }

    [PerfClass(nameof(UnityGetComponent), "AOT", "和Unity交互")]
    public class UnityGetComponent : IBenchmark
    {
        List<GameObject> objList;
        int frame;

        public void Clear()
        {
            UnityUtils.ReleaseObjects(objList);
        }

        public void Prepare()
        {
            objList = UnityUtils.BuildObjects("AOT", nameof(UnityGetComponent), PerfLevel.unityGameObjectCount);
            this.frame = 10;
            for (int i = 0; i < objList.Count; i++)
            {
                var obj = objList[i];
                var meshFilter = obj.AddComponent<MeshFilter>();
                meshFilter.mesh = UnityUtils.BuildMesh();

                var meshRender = obj.AddComponent<MeshRenderer>();
                meshRender.material = UnityUtils.LoadMaterial();
            }
        }

        public void Run()
        {
            for (int frameIndex = 0; frameIndex < frame; ++frameIndex)
            {

                for (int i = 0; i < objList.Count; i++)
                {
                    var comp = objList[i].GetComponent<MeshFilter>();
                    var comp2 = objList[i].GetComponent<MeshRenderer>();
                    var comp3 = objList[i].GetComponent<Collider>();
                }
            }
        }
    }

}