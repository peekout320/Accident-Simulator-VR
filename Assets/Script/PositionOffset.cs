using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOffset : MonoBehaviour
{
    [SerializeField]
    private Transform[] offsetTran;

    [SerializeField]
    private Transform[] baseTran;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < offsetTran.Length; i++)
        {
            Vector3 deltaTran = offsetTran[i].position - baseTran[i].position;

            Debug.Log("����" + deltaTran);
            Debug.Log("�ʒu��������I�u�W�F�N�g" + i + offsetTran[i].position);
            Debug.Log("��ƂȂ�I�u�W�F�N�g" + i + baseTran[i].position);

            offsetTran[i].position = offsetTran[i].position - deltaTran;

            Debug.Log("�ʒu���������I�u�W�F�N�g" + i + offsetTran[i].position);
        }
    }
 }
