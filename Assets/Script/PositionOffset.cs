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

            Debug.Log("差分" + deltaTran);
            Debug.Log("位置調整するオブジェクト" + i + offsetTran[i].position);
            Debug.Log("基準となるオブジェクト" + i + baseTran[i].position);

            offsetTran[i].position = offsetTran[i].position - deltaTran;

            Debug.Log("位置調整したオブジェクト" + i + offsetTran[i].position);
        }
    }
 }
