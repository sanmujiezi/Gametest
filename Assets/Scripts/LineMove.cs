using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class LineMove : MonoBehaviour
{
    public Queue<GameObject> curPlats;
    public Transform prefab;
    public int stopNums = 3;
    public float speed = 1;
    public float waitTime = 3;

    private PoolManager<Transform> platManager;

    public Transform startPos;
    public Transform endPos;
    public Transform playerPos;
    Random random ;
    
    private bool islow;

    private void Awake()
    {
        InitData();
    }

    private void InitData()
    {
        random = new Random();
        curPlats = new Queue<GameObject>();
        platManager = new PoolManager<Transform>(prefab);
    }


    // Update is called once per frame
    void Update()
    {
        if (islow == false && curPlats.Count < stopNums)
        {
            islow = true;
            StartCoroutine(SpawPlat());
        }

        if (curPlats.Count > 0 && curPlats.Peek().transform.position.x < playerPos.position.x)
        {
            foreach (var item in curPlats)
            {
                float _x = item.transform.position.x + (speed * Time.deltaTime);
                item.transform.position = new Vector3(_x, item.transform.position.y, item.transform.position.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curPlats.Count > 0)
            {
                var obj = curPlats.Dequeue();
                platManager.Despawn(obj.transform);
            }
        }
    }

    IEnumerator SpawPlat()
    {
        while (curPlats.Count < stopNums)W
        {
            waitTime = (float)random.NextDouble();
            yield return new WaitForSeconds(waitTime);
            var plat = platManager.Spawn();

            plat.position = startPos.position;

            curPlats.Enqueue(plat.gameObject);
            Debug.Log($"CreateGameObj {curPlats.Count}");
        }

        islow = false;
    }
}