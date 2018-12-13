using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTrunk : MonoBehaviour
{
    struct PseudoTransform
    {
        public Vector3 initPostition;
        public Quaternion initRotation;

        public PseudoTransform(Transform transform)
        {
            initPostition = transform.position;
            initRotation = transform.rotation;
        }
    }

    public static BrokenTrunk Instance;

    PseudoTransform[] childTransforms;
    int childCount;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        childCount = transform.childCount;
        childTransforms = new PseudoTransform[childCount];

        for (int i = 0; i < childCount; i++)
            childTransforms[i] = new PseudoTransform(transform.GetChild(i));
    }

    public void ResetBrokenTrunk()
    {
        for (int i = 0; i < childCount; i++)
            transform.GetChild(i).SetPositionAndRotation(childTransforms[i].initPostition, childTransforms[i].initRotation);
    }
}
