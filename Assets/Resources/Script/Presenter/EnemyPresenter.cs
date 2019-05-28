﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour {

    public UserModel status = new UserModel();
    public float speed = 4.0f;
    public bool isMove = false;
    public Vector3 targetPosition;
    
    [SerializeField]
    public EnemyView enemyView;

    void Update()
    {
        if (isMove)
        {
            Moving();
        }
    }
    
    public void Initialize()
    {
        enemyView.Initialize();
    }
        
    public int GetAction()
    {
        return Random.Range(1, 2);
    }

    //移動処理
    public void StartMove(float x, float z)
    {
        isMove = true;
        Rigidbody transform = enemyView.GetTransForm();
        Vector3 nowPosition = transform.position;

        targetPosition.x = x/200 + nowPosition.x;
        targetPosition.z = z/200 + nowPosition.z;
        
    }

    void Moving()
    {        
        Rigidbody transform = enemyView.GetTransForm();
        Vector3 nowPosition = transform.position;

        float step = 0.04f;//speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, 0, targetPosition.z), step);
        if (targetPosition.x == nowPosition.x && targetPosition.z == nowPosition.z)
        {
            transform.velocity = Vector3.zero;
            isMove = false;
            status.isAction = true;
        }
    }

    //position設定
    public void SetPosition(Vector3 position)
    {
        status.position = new Vector3(status.position.x + position.x, status.position.y + position.y, status.position.z + position.z);
    }
    /// <summary>
    /// <code>
    /// transform.eulerAnglesで向きを変更
    /// user.status.directionに向き保存
    /// </code>
    /// </summary>
    public void SetDirection(Vector3 position)
    {
        int nowDirection = TransFormUtil.GetChangeRotate(status.direction);
        int targetDirection = TransFormUtil.GetChangeRotate(position);

        float angle = Mathf.LerpAngle(nowDirection, targetDirection, Time.time);
        
        enemyView.SetAngles(new Vector3(0, angle, 0));

        status.direction = new Vector3(position.x, position.y, position.z);
    }
    //向き設定
    public void SetIsAction(bool isAction)
    {
        status.isAction = isAction;
    }

    void OnWillRenderObject()
    {
        //カメラに表示されている時のみ
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
        {
            // 処理
        }
    }
}
