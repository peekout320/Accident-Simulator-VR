using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TelopController : MonoBehaviour
{
    [SerializeField]
    private Text text; 

    [SerializeField]
    private string[] sentences; // 表示する文章の配列

    private int currentIndex = 0; // 現在表示中の文章のインデックス

    /// <summary>
    /// Timelineから呼び出すメソッド
    /// </summary>
    public void ChangeTelop()
    {
        // 配列の範囲内であればテキストを更新
        if (currentIndex < sentences.Length)
        {
            text.fontSize = 110;
            text.text = sentences[currentIndex];
            currentIndex++;       
        }
    }
}