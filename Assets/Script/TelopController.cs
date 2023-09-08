using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TelopController : MonoBehaviour
{
    [SerializeField]
    private Text text; 

    [SerializeField]
    private string[] sentences; // �\�����镶�͂̔z��

    private int currentIndex = 0; // ���ݕ\�����̕��͂̃C���f�b�N�X

    /// <summary>
    /// Timeline����Ăяo�����\�b�h
    /// </summary>
    public void ChangeTelop()
    {
        // �z��͈͓̔��ł���΃e�L�X�g���X�V
        if (currentIndex < sentences.Length)
        {
            text.fontSize = 110;
            text.text = sentences[currentIndex];
            currentIndex++;       
        }
    }
}