using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace your_namespace_here
{
    public class RemoveRigidbodies : MonoBehaviour
    {
        [Button("Remove")]
        private void Remove()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
            Joint[] joints = GetComponentsInChildren<Joint>();

            for(int j = 0; j < joints.Length; j++)
                DestroyImmediate(joints[j]);

            for (int i = 0; i < colliders.Length; i++)
                DestroyImmediate(colliders[i]);

            for(int i = 0; i < rigidbodies.Length; i++)
                DestroyImmediate(rigidbodies[i]);
        }
    }
}
