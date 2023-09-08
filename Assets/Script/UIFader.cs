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
        newColor = fadeImg.color; // ���݂̐F�����擾
    }



    /// <summary>
    /// �t�F�[�h�C����
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
    /// ���̔������ɉ�ʂ̐F��Ԃ����߂�p
    /// </summary>
    public void FadeInScreen2()
    {
        newColor.r = 1f; // �Ԑ�����1.0�ɐݒ�i1.0���ő�l�ŐԐF��\���܂��j

        fadeImg.color = newColor;

        fadeImg.UpdateMaskTexture(fadeIn2Texture);

        fade.FadeIn(fadeTime);
    }

    /// <summary>
    /// ���[�v�Đ����p
    /// </summary>
    public void FadeInScreen3()
    {
        Color newColor = Color.black; �@// �F�����ɖ߂�

        fadeImg.color = newColor;

        fadeImg.UpdateMaskTexture(fadeInTexture);

        fade.FadeIn(fadeTime);

    }
}
