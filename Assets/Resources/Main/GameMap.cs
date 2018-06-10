﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour {

	public int max_map_x = 100;
    public int max_map_y = 100;
	public int[,] map;

	// Use this for initialization
	void Start () {
        map = new int[max_map_x,max_map_y];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void create() {
        List<int> map_x = generate_map ();
        List<List<int>> map_y = new List<List<int>>();
        int[,] map_list = new int[max_map_x, max_map_y];
        int count_x = 0;
        for (int x = 0; x < map_x.Count; x++)
        {
            List<int> list_y = generate_map ();
            map_y.Add(list_y);
            int count_y = 0;
            /*
            for (int y = 0; y < map_x[x]; y++)
            {
                GameObject original = GameObject.Find("Tile");
                GameObject copied = Object.Instantiate(original) as GameObject;
                copied.transform.Translate(count_x+y, 0, 0);
            }
            */
            for (int y = 0; y < list_y.Count; y++){
                int[,] floor = generate_floor(map_x[x],list_y[y],count_x,count_y);
                count_y += list_y[y];
            }
            count_x += map_x[x];
            //map_y[x];
            //map_list[x,0] = 1;
        }
	}

    void generate_wall (int[,] data){
        for (int x = 0; x < data.Length; x++) {
            for (int y = 0; y < data.Length; y++) {
                if(data[x+1,y] == 1){
                    
                }
            }
        }

    }

    int[,] generate_floor(int x,int y,int seq_x,int seq_y)
    {
        //maxだと部屋同士でくっつくので-1
        int[,] result = new int[x, y];
        int floor_x = Random.Range(5, x-2);
        int floor_y = Random.Range(5, y-2);
        int start_x = Random.Range(1, x-floor_x-1);
        int start_y = Random.Range(1, y-floor_y-1);
        for (int l = 0; l < x; l++)
        {
            for (int m = 0; m < y; m++)
            {
                if (l >= start_x && l <= start_x + floor_x && m >= start_y && m <= start_y + floor_y)
                {
                    GameObject original = GameObject.Find("Tile");
                    GameObject copied = Object.Instantiate(original) as GameObject;
                    copied.transform.Translate(seq_x + l, 0, seq_y + m);
                    //Debug.Log((seq_x + l) + " : " + (seq_y + m));
                    result[l, m] = 1;
                }else{
                    result[l, m] = 0;                    
                }
            }
        }
        return result;
    }
    List<int> generate_map(){
        return split_map(new List<int>{max_map_x});
	}
    List<int> split_map(List<int> data){
		Random.Range (0, 2);
		//show_list_log (data);
		for (int x = 0; x < data.Count; x++) {
			int is_split = Random.Range (0, 10);
			if (data [x] < 20) {
				continue;
			}
			if (is_split <= 8) {
				int ins = data [x] / 2;
				data [x] = data [x] / 2;
				data.Insert (x, ins);
				x--;
				continue;
			} 
		}
		//show_list_log (data);
        return data;
	}

	void show_list_log(List<int> data){
		string log = "";
		for (int x = 0; x < data.Count; x++) {
			log += data[x].ToString ()+",";
		}
		Debug.Log (log);
	}
}