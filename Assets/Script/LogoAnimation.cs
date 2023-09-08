using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform logoPrefab; 

    [SerializeField]
    private int numberOfParts = 12; // ��������p�[�c�̐�
    [SerializeField]
    private float animationTime = 2f; // �A�j���[�V��������
    [SerializeField]
    private float radius = 5f; // ��������p�[�c�̔��a(����)

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

    // �p�[�c�̐����Ɣz�u
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
                //�����̃p�[�c����_�Ɉړ�������A�j���[�V����
                sequence1.Join(part.DOMove(centerPosition, animationTime)
                    .SetEase(Ease.OutBack));
                    //.SetEase(Ease.OutBounce));
            }

            sequence1.AppendCallback(() =>
            {
                Tween2();
                Tween3(inc, 70);
                Tween3(romaji, 40);

                Debug.Log("�t�F�[�h�A�E�g");
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
    /// Image�I�u�W�F�N�g�Ɋւ���A�j���[�V����
    /// </summary>
    private void Tween2()
    {
        if (tsumikiImg != null)
        {
            foreach (Image img in tsumikiImg)
            {
                img.DOFade(1, 5f);  //"�ϖؐ���"�̕�����\��
            }
        }
    }

    /// <summary>
    /// Text�I�u�W�F�N�g�Ɋւ���A�j���[�V����
    /// </summary>
    /// <param name="texts"></param>
    /// <param name="addition"></param>
    private void Tween3(Text[] texts, float addition)
    {
        Debug.Log("Tween3�J�n");
        float moveDistance = 0;

        if (texts != null)
        {
            foreach (Text txt in texts)
            {
                //"�������"�ƎЖ�(���[�}��)��\��
                txt.rectTransform.DOLocalMoveX(moveDistance, 3f).SetEase(Ease.OutBack);
                txt.DOFade(1, 5).SetEase(Ease.Linear);

                moveDistance += addition;
            }
        }
    }

    /// <summary>
    /// Light�I�u�W�F�N�g�Ɋւ���A�j���[�V����
    /// </summary>
    private void Tween4()
    {

        if (spotLight != null)
        {
            Debug.Log("Tween4�J�n");

            sequence2 = DOTween.Sequence();

            sequence2.Append(spotLight.transform.DOLocalMoveX(-10, 1f))
                           .Append(spotLight.transform.DOLocalMoveX(3, 5f));
        }
    }

    private void Tween5()
    {
        Debug.Log("Tween5�J�n");

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






