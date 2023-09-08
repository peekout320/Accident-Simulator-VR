using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchKinematic : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigid;

    public void OffKinematic()
    {
        rigid.isKinematic = false;
    }
}
