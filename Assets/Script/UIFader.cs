using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour
{
    [SerializeField]
    Fade fade;

    [SerializeField]
    FadeImage fadeImg;

    [SerializeField]
    private float fadeTime = 2.0f;
    [SerializeField]
    private float fadeTime2 = 4.0f;


    [SerializeField]
    private Texture fadeInTexture;
    [SerializeField]
    private Texture fadeOutTexture;
    [SerializeField]
    private Texture fadeIn2Texture;

    private Color newColor;

    [SerializeField]
    private GameObject canvas;

    private void Start()
    {
        newColor = fadeImg.color; // 現在の色情報を取得
    }



    /// <summary>
    /// フェードイン時
    /// </summary>
    /// <param name="texture"></param>
    public void FadeInScreen()
    {
        fadeImg.UpdateMaskTexture(fadeInTexture);

        fade.FadeIn(fadeTime);
    }

    private IEnumerator FadeInScreenCoroutine()
    {
        fadeImg.UpdateMaskTexture(fadeInTexture);

        fade.FadeIn(fadeTime);

        while (true)
        {
            canvas.transform.rotation *= Quaternion.Euler(0, 0, 0.3f);

            yield return null;
        }
    }

    public void FadeInScreen4()
    {
        StartCoroutine(FadeInScreenCoroutine());
    }


    /// <summary>
    /// 事故発生時に画面の色を赤く染める用
    /// </summary>
    public void FadeInScreen2()
    {
        newColor.r = 1f; // 赤成分を1.0に設定（1.0が最大値で赤色を表します）

        fadeImg.color = newColor;

        fadeImg.UpdateMaskTexture(fadeIn2Texture);

        fade.FadeIn(fadeTime);
    }

    /// <summary>
    /// ループ再生時用
    /// </summary>
    public void FadeInScreen3()
    {
        Color newColor = Color.black; 　// 色を黒に戻す

        fadeImg.color = newColor;

        fadeImg.UpdateMaskTexture(fadeInTexture);

        fade.FadeIn(fadeTime);

    }
}
