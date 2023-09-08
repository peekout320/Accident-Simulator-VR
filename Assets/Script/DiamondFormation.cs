using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiamondFormation : MonoBehaviour
{
    public Transform objectPrefab; // インスタンス化するオブジェクトのPrefab
    public int numberOfObjects = 100; // オブジェクトの数
    public float duration = 0.5f; // 移動のデュレーション（秒）
    public float waitTime = 2f;
    public float radius = 4f;
    private List<Renderer> objects = new List<Renderer>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private Vector3 initialPosition;

    void Start()
    {
        // 初期位置を記憶し、アニメーションを開始
        initialPosition = objectPrefab.transform.position;
        AnimateObjects();
    }

    // オブジェクトの初期位置を記憶
    void RecordInitialPositions()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // 球体上にランダムに配置
            float theta = Random.Range(0f, Mathf.PI * 2); // θ角（0から2πの間でランダム）
            float phi = Random.Range(0f, Mathf.PI); // φ角（0からπの間でランダム）
            float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = radius * Mathf.Cos(phi);

            Vector3 position = new Vector3(x, y, z);
            initialPositions.Add(position);
        }
    }

    // オブジェクトをアニメーションさせる
    void AnimateObjects()
    {
        var sequence = DOTween.Sequence();

        for (int i = 0; i < numberOfObjects; i++)
        {
            Transform obj = Instantiate(objectPrefab, new Vector3(100,0,0), Quaternion.identity);

            objects[i].material.DOFade(0, 0);
            // 中心に移動するアニメーション
            sequence.Append(obj.transform.DOMove(initialPosition, duration))// オブジェクトごとに遅延
            .Join(objects[i].material.DOFade(1, 2f));
                //.OnComplete(() =>
                //{
                //    // 初期位置に戻すアニメーション
                //    objTransform.DOMove(Vector3.zero, duration);
                //        //.SetDelay(waitTime)
                //        //.SetLoops(-1); // 無限に繰り返す
                //});
        }
    }
}