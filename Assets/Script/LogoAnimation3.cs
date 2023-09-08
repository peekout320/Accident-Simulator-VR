using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class LogoAnimation3 : MonoBehaviour
{
    [SerializeField]
    Fade fade;

    [SerializeField]
    private Vector3 minPos, maxPos;

    [SerializeField]
    private Material logoMaterial;
    [SerializeField]
    private Material woodMaterial;

    [SerializeField]
    private Transform logoBase1;
    [SerializeField]
    private Transform logoBase2;

    [SerializeField]
    private Renderer[] logoParts;
    [SerializeField]
    private Image[] tsumikiImgs;
    [SerializeField]
    private Transform[] axisTrans;
    [SerializeField]
    private Text[] inc,romaji;

    [SerializeField]
    private float seqence2PositionOffset;
    [SerializeField]
    private float seqence2ScaleOffset;


    private Vector3[] startPartsPos;
    private Quaternion[] startPartsRot;

    private Sequence sequence;



    private void OnEnable()
    {
        InitialPosition();

        RandomPositions();

        StartCoroutine(Tween1());
    }

    private void Initialize()
    {
        logoBase1.localScale = Vector3.one;

        for (int i = 0; i < logoParts.Length; i++)
        {
            logoParts[i].transform.position = startPartsPos[i];
            logoParts[i].transform.rotation = startPartsRot[i];

            logoParts[i].material.DOColor(Color.white, 0);
            logoParts[i].material = woodMaterial;

        }
        Debug.Log("�ď�������");

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
        Debug.Log("�����ʒu�L�^");
    }
    private void RandomPositions()
    {
        foreach (Renderer parts in logoParts)
        {
            parts.transform.position = new Vector3(parts.transform.position.x, parts.transform.position.y + 50, parts.transform.position.z);
        }
        Debug.Log("�����_���z�u����");
    }

    private IEnumerator Tween1()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("tween1�J�n");
        sequence = DOTween.Sequence();

        //�o���o���ɔz�u����parts�����̈ʒu�֌��ɖ߂��A�j���[�V����
        for (int i = 0; i < logoParts.Length; i++)
        {
            float initTime = Random.Range(0.01f, 0.1f);
            sequence.Append(logoParts[i].transform.DOMove(startPartsPos[i], initTime).SetEase(Ease.OutBounce, 2))
                         .Join(logoParts[i].transform.DOLocalRotate(Vector3.up * 360, 0.1f, RotateMode.WorldAxisAdd))
                         .Join(logoParts[i].material.DOFade(1, 0.1f));
        }
        sequence.AppendInterval(0.5f)
                     .AppendCallback(() =>
                        {
                            Tween2();
                        });
    }
    private void Tween2()
    {
        Debug.Log("tween2�J�n");
        var sequence2 = DOTween.Sequence();

        sequence2.Append(logoBase1.transform.DOLocalMoveX(seqence2PositionOffset, 1f).SetEase(Ease.OutBack)) //���S����x��ʉE�[�Ɋ񂹂T�C�Y�𒲐�����
                        .Join(logoBase1.transform.DOScale(Vector3.one*seqence2ScaleOffset,0.5f))
                        .AppendInterval(0.5f)
                        .AppendCallback(async () =>
                       {
                           var sequence3 = DOTween.Sequence();
                           var tasks = new List<Task>();

                           for (int i =0; i < axisTrans.Length; i++)
                           {
                               tsumikiImgs[i].transform.DOLocalMoveX(-50, 0.1f);�@�@�@�@�@�@          

                               await Task.Delay(300);
                               axisTrans[i].transform.DORotate(new Vector3(0, 0, 90), 0.3f)             //���S����ʍ������փT�C�R���̂悤�ɉ�]�����Ȃ���ړ�������
                               .SetEase(Ease.OutQuad);

                               sequence3.Append(tsumikiImgs[i].transform.DOLocalMoveX(0, 0.3f))
                                              .Join(tsumikiImgs[i].DOFade(1, 3f));�@�@�@�@�@�@�@�@           //���S�̉�]�ɍ��킹��"�ϖؐ���"�̕������t�F�[�h�����Ȃ���\��

                               tasks.Add(sequence3.AsyncWaitForCompletion());
                           }
                           await Task.WhenAll(tasks);

                           DOVirtual.DelayedCall(1, () =>
                            {
                                Debug.Log("�t�F�[�h�A�E�g");
                                fade.FadeOut(3); //225

                                logoBase2.transform.DORotate(Vector3.zero, 1f);                              //���S�𐳊m�Ȋp�x�ƐF�ɖ߂�

                                foreach(Renderer parts in logoParts)
                                {
                                    parts.material.DOColor(Color.red,1);
                                }

                                Tween3(inc,80);
                                Tween3(romaji,50);
                            });
                       });

    }
    private void Tween3(Text[] texts,float addition)
    {
        Debug.Log("Tween3�J�n");
        float moveDistance = 0;

        foreach (Text text in texts)
        {
            text.rectTransform.DOLocalMoveX(moveDistance, 3f).SetEase(Ease.OutBack);
            text.DOFade(1, 5).SetEase(Ease.Linear);

            moveDistance += addition;
        }
    }
}