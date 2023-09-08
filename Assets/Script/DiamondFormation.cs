using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiamondFormation : MonoBehaviour
{
    public Transform objectPrefab; // �C���X�^���X������I�u�W�F�N�g��Prefab
    public int numberOfObjects = 100; // �I�u�W�F�N�g�̐�
    public float duration = 0.5f; // �ړ��̃f�����[�V�����i�b�j
    public float waitTime = 2f;
    public float radius = 4f;
    private List<Renderer> objects = new List<Renderer>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private Vector3 initialPosition;

    void Start()
    {
        // �����ʒu���L�����A�A�j���[�V�������J�n
        initialPosition = objectPrefab.transform.position;
        AnimateObjects();
    }

    // �I�u�W�F�N�g�̏����ʒu���L��
    void RecordInitialPositions()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // ���̏�Ƀ����_���ɔz�u
            float theta = Random.Range(0f, Mathf.PI * 2); // �Ɗp�i0����2�΂̊ԂŃ����_���j
            float phi = Random.Range(0f, Mathf.PI); // �ӊp�i0����΂̊ԂŃ����_���j
            float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = radius * Mathf.Cos(phi);

            Vector3 position = new Vector3(x, y, z);
            initialPositions.Add(position);
        }
    }

    // �I�u�W�F�N�g���A�j���[�V����������
    void AnimateObjects()
    {
        var sequence = DOTween.Sequence();

        for (int i = 0; i < numberOfObjects; i++)
        {
            Transform obj = Instantiate(objectPrefab, new Vector3(100,0,0), Quaternion.identity);

            objects[i].material.DOFade(0, 0);
            // ���S�Ɉړ�����A�j���[�V����
            sequence.Append(obj.transform.DOMove(initialPosition, duration))// �I�u�W�F�N�g���Ƃɒx��
            .Join(objects[i].material.DOFade(1, 2f));
                //.OnComplete(() =>
                //{
                //    // �����ʒu�ɖ߂��A�j���[�V����
                //    objTransform.DOMove(Vector3.zero, duration);
                //        //.SetDelay(waitTime)
                //        //.SetLoops(-1); // �����ɌJ��Ԃ�
                //});
        }
    }
}