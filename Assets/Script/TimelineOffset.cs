using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Timeline�����[�v����۔������������邽�߂̃��\�b�h
/// </summary>
public class TimelineOffset : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;

    [SerializeField]
    private double loopStartTime = 0.0; // ���[�v����N�_�ƂȂ鎞��

    [SerializeField]
    private float InitialTime = 25.0f; // ���[�v���ɍĐ����J�n���鎞��

    private bool isLooping = false;

    [SerializeField]
    private Camera cutOutCamera;



    private void Start()
    {
        if (playableDirector == null)
        {
            Debug.LogError("PlayableDirector���A�^�b�`����Ă��܂���B");
            return;
        }
    }

    private void Update()
    {
        if (!isLooping)
        {
            if (playableDirector.time >= loopStartTime)
            {
                // ���[�v�̊J�n���Ԃɓ��B�����烋�[�v���J�n
                playableDirector.time = InitialTime;
                playableDirector.Play();
                isLooping = true;
            }
        }
    }

    /// <summary>
    /// ���[�v�Đ�����MainCamera���A�N�e�B�u�ɂ��āA���ՃJ�����ɐ؂�ւ���
    /// </summary>
    public void CutOutCamera()
    {
        cutOutCamera.gameObject.SetActive(false);

    }
}

