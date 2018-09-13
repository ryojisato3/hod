﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Use this for initialization
    public UserStatus status = new UserStatus();
    void Start () {
	}
   
	public float speed = 4.0f;
	public bool is_move = false;
	public Vector3 target_position;

	// Update is called once per frame
	void Update () {
        if (is_move && status.is_action == false){
            moving();
		}
	}

    //攻撃処理
    public void attack(GameMap m,EnemyControll e)
    {
        Vector3 pos = status.position + status.direction;
        if (pos.x < 0 || pos.z < 0){
            return;
        }
        Debug.Log(pos);
        if (m.map[(int)pos.x, (int)pos.z].chara_type != 1){
            Debug.Log("c_type:" + m.map[(int)pos.x, (int)pos.z].chara_type);
            Debug.Log("c_id:" + m.map[(int)pos.x, (int)pos.z].chara_id);
            for (int x = 0; x < e.enemy_list.Count; x++) {
                Enemy com = e.enemy_list[x].GetComponent<Enemy>();
                Debug.Log("attack:e_id=" + com.status.id);
                if (com.status.id == m.map[(int)pos.x, (int)pos.z].chara_id){
                    Debug.Log("delete:id="+com.status.id);
                    com.status.hp = 0;
                    e.delete(x);
                    break;
                }
            }
            //GameObject obj = GameObject.FindWithTag();
            //Destroy(obj);
            //GameObject obj = GameObjecta.Find("Player");
            //find
        }
        status.is_action = true;
    }

    //移動処理
	public void move (float x,float z) {
        is_move = true;
		Rigidbody transform = GetComponent<Rigidbody>();
		Vector3 now_position = transform.position;
		 
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		//Force	その質量を使用して、rigidbodyへの継続的な力を追加します。
		//Acceleration	その質量を無視して、rigidbodyへの継続的な加速を追加します。
		//Impulse	その質量を使用して、rigidbodyに瞬時に速度変化を追加します。
		//VelocityChange	その質量を無視して、rigidbodyに瞬時に速度変化を追加します。
		rigidbody.AddForce(x, 0, z, ForceMode.Acceleration);
        target_position.x = now_position.x;
        target_position.z = now_position.z;
	}

    void moving(){
        Rigidbody transform = GetComponent<Rigidbody>();
        Vector3 now_position = transform.position;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (target_position.x != now_position.x)
        {
            if (target_position.x + 1 < now_position.x)
            {
                rigidbody.velocity = Vector3.zero;
                is_move = false;
                status.is_action = true;
            }
            else if (target_position.x - 1 > now_position.x)
            {
                rigidbody.velocity = Vector3.zero;
                is_move = false;
                status.is_action = true;
            }
        }
        else if (target_position.z != now_position.z)
        {
            if (target_position.z + 1 < now_position.z)
            {
                rigidbody.velocity = Vector3.zero;
                is_move = false;
                status.is_action = true;
            }
            else if (target_position.z - 1 > now_position.z)
            {
                rigidbody.velocity = Vector3.zero;
                is_move = false;
                status.is_action = true;
            }
        }

    }


    //ダメージ計算
    void set_hp(int value)
    {
        Hud hud = (GameObject.Find("Hud")).GetComponent<Hud>();
        hud.update_health(value);
    }
    //mp計算
    void set_mp(int value)
    {
        Hud hud = (GameObject.Find("Hud")).GetComponent<Hud>();
        hud.update_magic(value);
    }
    //ep計算
    void set_ep(int value)
    {
        Hud hud = (GameObject.Find("Hud")).GetComponent<Hud>();
        hud.update_energy(value);
    }

    //position設定
    public Vector3 set_position(Vector3 position)
    {
        status.position = new Vector3(status.position.x + position.x, status.position.y + position.y, status.position.z + position.z);
        return status.position;
    }

    //向き設定
    public void set_direction(Vector3 position)
    {
        int n_direction = get_rotate(status.direction);
        Debug.Log(n_direction);
        int s_direction = get_rotate(position);
        Debug.Log(s_direction);

        int nd = 360 - n_direction;
        int sd = 360 - s_direction;
        if(n_direction > s_direction){
            int rd = n_direction - s_direction;
            if(rd > 180){
                this.transform.Rotate(0.0f, -360 + rd, 0.0f);
            }else{
                this.transform.Rotate(0.0f, rd, 0.0f);
            }
        }else if (s_direction > n_direction)
        {
            int rd = s_direction - n_direction;
            if (rd > 180)
            {
                this.transform.Rotate(0.0f, rd, 0.0f);
            }
            else
            {
                this.transform.Rotate(0.0f, -360 + rd, 0.0f);
            }

        }


        status.direction = new Vector3(position.x, position.y, position.z);
    }

    int get_rotate(Vector3 d){
        
        int rotate = 0;
        if (d.x == -1)
        {
            rotate += 180;
            if (d.z == 1)
            {
                rotate += 135;
            }
            else if (d.z == 0)
            {
                rotate += 90;
            }
            else if (d.z == -1)
            {
                rotate += 45;
            }
        }
        else if (d.x == 0)
        {
            if (d.z == -1)
            {
                rotate += 180;
            }
        }
        else if (d.x == 1)
        {
            if (d.z == 1)
            {
                rotate += 45;
            }
            else if (d.z == 0)
            {
                rotate += 90;
            }
            else if (d.z == -1)
            {
                rotate += 135;
            }
        }
        return rotate;
    }

}
