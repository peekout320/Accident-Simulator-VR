using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform logoPrefab; 

    [SerializeField]
    private int numberOfParts = 12; // 生成するパーツの数
    [SerializeField]
    private float animationTime = 2f; // アニメーション時間
    [SerializeField]
    private float radius = 5f; // 生成するパーツの半径(距離)

    [SerializeField]
    private Fade fade;

    [SerializeField]
    private Light spotLight;
    [SerializeField]
    private GameObject MirrorPlane;

    [SerializeField]
    private Transform incName;
    [SerializeField]
    private Text[] inc, romaji;
    [SerializeField]
    private Image[] tsumikiImg;

    private List<Transform> generateParts = new List<Transform>();
    private Vector3 centerPosition;

    Sequence sequence2;

    void Start()
    {
        Initialize();

        Tween1();
    }

    // パーツの生成と配置
    private void Initialize()
    {
        centerPosition = transform.position;

        for (int i = 0; i < numberOfParts; i++)
        {
            float angle = (360f / numberOfParts) * i;
            Vector3 generatePos = centerPosition + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;

            Transform parts = Instantiate(logoPrefab, generatePos, Quaternion.identity);
            generateParts.Add(parts);
        }
    }

    private void Tween1()
    {
        if (generateParts != null)
        {
            Sequence sequence1 = DOTween.Sequence();

            foreach (Transform part in generateParts)
            {
                //複数のパーツを一点に移動させるアニメーション
                sequence1.Join(part.DOMove(centerPosition, animationTime)
                    .SetEase(Ease.OutBack));
                    //.SetEase(Ease.OutBounce));
            }

            sequence1.AppendCallback(() =>
            {
                Tween2();
                Tween3(inc, 70);
                Tween3(romaji, 40);

                Debug.Log("フェードアウト");
                fade.FadeOut(5); //

                MirrorPlane.SetActive(false);

            })
            .AppendInterval(1)
            .OnComplete(() =>
            {
                Tween4();
            });
            sequence2.OnComplete(()=> 
            {
                Tween5();  //TODO
            });
        }
    }

    /// <summary>
    /// Imageオブジェクトに関するアニメーション
    /// </summary>
    private void Tween2()
    {
        if (tsumikiImg != null)
        {
            foreach (Image img in tsumikiImg)
            {
                img.DOFade(1, 5f);  //"積木製作"の文字を表示
            }
        }
    }

    /// <summary>
    /// Textオブジェクトに関するアニメーション
    /// </summary>
    /// <param name="texts"></param>
    /// <param name="addition"></param>
    private void Tween3(Text[] texts, float addition)
    {
        Debug.Log("Tween3開始");
        float moveDistance = 0;

        if (texts != null)
        {
            foreach (Text txt in texts)
            {
                //"株式会社"と社名(ローマ字)を表示
                txt.rectTransform.DOLocalMoveX(moveDistance, 3f).SetEase(Ease.OutBack);
                txt.DOFade(1, 5).SetEase(Ease.Linear);

                moveDistance += addition;
            }
        }
    }

    /// <summary>
    /// Lightオブジェクトに関するアニメーション
    /// </summary>
    private void Tween4()
    {

        if (spotLight != null)
        {
            Debug.Log("Tween4開始");

            sequence2 = DOTween.Sequence();

            sequence2.Append(spotLight.transform.DOLocalMoveX(-10, 1f))
                           .Append(spotLight.transform.DOLocalMoveX(3, 5f));
        }
    }

    private void Tween5()
    {
        Debug.Log("Tween5開始");

        foreach (Image img in tsumikiImg)
        {
            img.transform.DOScale(1.5f, 1);
            img.DOFade(0, 1);
        }

        foreach (Text txt in inc)
        {
            txt.transform.DOScale(1.5f, 1);
            txt.DOFade(0, 1);
        }

        foreach (Text txt in romaji)
        {
            txt.transform.DOScale(1.5f, 1);
            txt.DOFade(0, 1);
        }
    }
}






