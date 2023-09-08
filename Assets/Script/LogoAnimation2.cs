using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoAnimation2 : MonoBehaviour
{
    [SerializeField]
    private Renderer[] logoParts;
    [SerializeField]
    private Transform logoBase;
    [SerializeField]
    private Vector3 minPos, maxPos;
    [SerializeField]
    Fade fade;

    [SerializeField]
    private Material logoMaterial;
    [SerializeField]
    private Material woodMaterial;

    [SerializeField]
    private GameObject MirrorPlane;


    private Vector3[] startPartsPos;
    private Quaternion[] startPartsRot;

    private Sequence sequence;


    private void OnEnable()
    {
        InitialPosition();

        RandomPositions();

        Tween1();
    }

    private void Initialize()
    {
        logoBase.localScale = Vector3.one;

        for (int i = 0; i < logoParts.Length; i++)
        {
            logoParts[i].transform.position = startPartsPos[i];
            logoParts[i].transform.rotation = startPartsRot[i];

            logoParts[i].material.DOColor(Color.white, 0);
            logoParts[i].material = woodMaterial;

        }
        Debug.Log("再準備完了");

    }
    private void InitialPosition()
    {
        startPartsPos = new Vector3[logoParts.Length];
        startPartsRot = new Quaternion[logoParts.Length];
        for (int i = 0; i < logoParts.Length; i++)
        {
            startPartsPos[i] = logoParts[i].transform.position;
            startPartsRot[i] = logoParts[i].transform.rotation;
        }
        Debug.Log("初期位置記録");
    }
    private void RandomPositions()
    {
        foreach (Renderer parts in logoParts)
        {
            float randomX = Random.Range(minPos.x, maxPos.x);
            float randomY = Random.Range(minPos.y, maxPos.y);
            float randomZ = Random.Range(minPos.z, maxPos.z);
            parts.transform.position = new Vector3(randomX, randomY, randomZ);
        }
        Debug.Log("ランダム配置完了");
    }

    /// <summary>
    /// パーツが集まって一つのロゴ(オブジェクト)になるまで
    /// </summary>
    private void Tween1()
    {
        Debug.Log("tween1開始");
        for (int i = 0; i < logoParts.Length; i++)
        {
            float initTime = Random.Range(3, 7);

            sequence = DOTween.Sequence();
            sequence.Append(logoParts[i].transform.DOMove(startPartsPos[i], initTime))
                         .Join(logoParts[i].transform.DOLocalRotate(Vector3.one * 360, 6f, RotateMode.WorldAxisAdd))
                         .Join(logoParts[i].material.DOFade(1, 0.1f)); 
        }
        sequence.AppendInterval(0.5f)
             .AppendCallback(() =>
             {
                 Tween2();
                 MirrorPlane.SetActive(false);
             });
    }

    /// <summary>
    /// 背景のフェードインと共にロゴが回転
    /// </summary>
    private void Tween2()
    {
        Debug.Log("tween2開始");
        var sequence2 = DOTween.Sequence();

        sequence2.Append(logoBase.DOLocalRotate(Vector3.zero, 0.5f))
                     .AppendCallback(() =>
                     {
                         logoBase.DOPunchRotation(Vector3.up * 180, 1.5f);
                         fade.FadeOut(3); //136
                     })
                    .AppendCallback(() =>
                    {
                        foreach (Renderer parts in logoParts)
                        {
                            parts.material = logoMaterial;
                            parts.material.DOColor(Color.red, 1);
                        }
                    })
                    .AppendInterval(2)
                    .AppendCallback(() =>
                     {
                         Tween3();
                     });
        }

    /// <summary>
    /// ロゴがフェードアウトするまで
    /// </summary>
    private void Tween3()
    {
        Debug.Log("tween2開始");
        var sequence3 = DOTween.Sequence();

            sequence3.Append(logoBase.DOScale(Vector3.one * 0.1f, 0.5f))
                           .Append(logoBase.DOScale(Vector3.one * 5, 1).SetEase(Ease.InCirc))
                           .Join(logoBase.DOLocalRotate(Vector3.forward * 180, 1))
                            .AppendCallback(() =>
                             {
                                 foreach (Renderer parts in logoParts)
                                 {
                                     parts.material.DOFade(0, 1f);
                                     Debug.Log("tween2完了");
                                 }
                             })
                            .AppendInterval(3)
                            .AppendCallback(() =>
                            {
                                Initialize();
                            });
    }
}
