using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class LineMove : MonoBehaviour
{
    public Action OnSubmit;
    public Queue<GameObject> curPlats;
    public Queue<GameObject> usedPlats;
    public Dictionary<GameObject, string> answers;
    public Transform prefab;
    public int stopNums = 3;
    public float speed = 1;
    public float waitTime = 1;
    public float interactionDistance = 0.1f;


    private bool isPaused;
    private PoolManager<Transform> platManager;

    public Transform startPos;
    public Transform endPos;
    public Transform playerPos;
    Random random;

    private bool islow;

    private void Awake()
    {
        InitData();
    }

    private void InitData()
    {
        curPlats = new Queue<GameObject>();
        usedPlats = new Queue<GameObject>();

        answers = new Dictionary<GameObject, string>();
        random = new Random();

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

        if ((curPlats.Count > 0 || usedPlats.Count > 0))
        {
            
            if (curPlats.Count > 0 && curPlats.Peek().transform.position.x < playerPos.position.x)
            {
                foreach (var item in curPlats)
                {
                    float _x = item.transform.position.x + (speed * Time.deltaTime);
                    item.transform.position = new Vector3(_x, item.transform.position.y, item.transform.position.z);
                    Debug.Log("移动了对象1");
                }
            }
            else if (curPlats.Count > 0 && curPlats.Peek().transform.position.x >= playerPos.position.x)
            {
                isPaused = true;
            }


            if (usedPlats.Count > 0)
            {
                int outCount = 0;
                foreach (var item in usedPlats)
                {
                    float _x = item.transform.position.x + (speed * Time.deltaTime);
                    item.transform.position = new Vector3(_x, item.transform.position.y, item.transform.position.z);
                    if (Vector3.Distance(item.transform.position, endPos.position) <= 0.1f)
                    {
                        platManager.Despawn(item.transform);
                        outCount++;
                    }

                    Debug.Log("移动了对象");
                }

                for (int i = 0; i < outCount; i++)
                {
                    usedPlats.Dequeue();
                }
            }
        }
        else if (curPlats.Count > 0 && curPlats.Peek().transform.position.x >= playerPos.position.x)
        {
            isPaused = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && curPlats.Count > 0 &&
            GetDistance(curPlats.Peek().transform, transform) <= interactionDistance)
        {
            OnSubmit?.Invoke();

            isPaused = false;
            var obj = curPlats.Dequeue();
            usedPlats.Enqueue(obj);

            answers.Remove(obj);
            Debug.Log("启动运行");
//                platManager.Despawn(obj.transform);
        }
    }

    IEnumerator SpawPlat()
    {
        while (curPlats.Count < stopNums && !isPaused)
        {
            yield return new WaitForSeconds(waitTime);
            if (isPaused)
            {
                continue;
            }

            var plat = platManager.Spawn();
            string answer = "红蓝绿";

            plat.position = startPos.position;

            curPlats.Enqueue(plat.gameObject);
            answers.Add(plat.gameObject, answer);
            Debug.Log($"CreateGameObj {curPlats.Count}");
        }

        islow = false;
    }

    private float GetDistance(Transform start, Transform end)
    {
        float result = 0;
        return result = Mathf.Abs(end.position.x - start.position.x);
    }

    public string GetAnswer()
    {
        return answers[curPlats.Peek()];
    }
}