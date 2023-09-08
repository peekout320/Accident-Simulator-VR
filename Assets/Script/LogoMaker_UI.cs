using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoMaker_UI : MonoBehaviour
{
    public Image[] images;
    public Transform _base;
    public Vector3 minPosition;
    public Vector3 maxPosition;

    private Vector2[] startImagesTran;

    private void Start()
    {
        InitialPosition();

        RandomizeImagePositions();

        StartCoroutine(InitializeImages());
    }
    private void InitialPosition()
    {
        startImagesTran = new Vector2[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            startImagesTran[i] = images[i].transform.position;
        }
        Debug.Log("初期位置記録");
    }

    private void RandomizeImagePositions()
    {
        foreach (Image image in images)
        {
            float randomX = Random.Range(minPosition.x, maxPosition.x);
            float randomY = Random.Range(minPosition.y, maxPosition.y);
            image.transform.position = new Vector2(randomX, randomY);
        }
        Debug.Log("ランダム配置完了");
    }

    private IEnumerator InitializeImages()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("UItween開始");

        for (int i = 0; i < images.Length; i++)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(images[i].transform.DOMove(startImagesTran[i], 5))
                         .Join(images[i].transform.DOLocalRotate(Vector2.one * 360, 7f, RotateMode.LocalAxisAdd))
                         .AppendCallback(() =>
                         {
                             _base.transform.DOLocalRotate(Vector3.forward*45, 2);
                             Debug.Log("UItween完了");
                         });
        }
    }
}
