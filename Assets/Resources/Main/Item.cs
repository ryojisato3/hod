using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public float speed = 4.0f;
    public bool is_move = false;
    public Vector3 target_position;
    public ItemModel status = new ItemModel();

    private ItemModelJson item_master;
    public void generate()
    {
        TextAsset item_master_json = Resources.Load<TextAsset>("Main/Master/Item");
        item_master = JsonUtility.FromJson<ItemModelJson>(item_master_json.ToString());
        Debug.Log(item_master.item[0].name);
    }

    public void re_random()
    {
        status = item_master.item[Random.Range(0, item_master.item.Length-1)];
    }
    
    //使用
    public void use()
    {

    }

    //移動
	public void move (float x,float z) {
        is_move = true;
		Rigidbody transform = this.GetComponent<Rigidbody>();
		Vector3 now_position = transform.position;

        target_position.x = x/200 + now_position.x;
        target_position.z = z/200 + now_position.z;

		//Force	その質量を使用して、rigidbodyへの継続的な力を追加します。
		//Acceleration	その質量を無視して、rigidbodyへの継続的な加速を追加します。
		//Impulse	その質量を使用して、rigidbodyに瞬時に速度変化を追加します。
		//VelocityChange	その質量を無視して、rigidbodyに瞬時に速度変化を追加します。
        transform.AddForce(x, 0, z, ForceMode.Acceleration);
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
            }
            else if (target_position.x - 1 > now_position.x)
            {
                rigidbody.velocity = Vector3.zero;
                is_move = false;
            }
        }
        else if (target_position.z != now_position.z)
        {
            if (target_position.z + 1 < now_position.z)
            {
                rigidbody.velocity = Vector3.zero;
                is_move = false;
            }
            else if (target_position.z - 1 > now_position.z)
            {
                rigidbody.velocity = Vector3.zero;
                is_move = false;
            }
        }
    }

    //position設定
    public Vector3 set_position(Vector3 position)
    {
        status.position = new Vector3(status.position.x + position.x, status.position.y + position.y, status.position.z + position.z);
        return status.position;
    }

}
