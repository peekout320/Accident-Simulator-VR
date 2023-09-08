using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    ReflectionProbe probe;

    void Start()
    {
        this.probe = GetComponent<ReflectionProbe>();
    }

    void Update()
    {
        //y����-1�������ċt���ɔz�u����
        this.probe.transform.position = new Vector3(Camera.main.transform.position.x,
                                                    Camera.main.transform.position.y * -1,
                                                    Camera.main.transform.position.z);
        probe.RenderProbe();
    }
}
