using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager instance;
    [SerializeField] Ball ballPrefab;
    [SerializeField] Ball rainbowBallPrefab;
    [SerializeField] Ball ghostBallPrefab;

    [SerializeField] int ballNumber;
    [HideInInspector]public Queue<Ball> ballQueue;
    [HideInInspector] public Queue<Ball> rainbowBallQueue;
    [HideInInspector] public Queue<Ball> ghostBallQueue;

    [SerializeField] Color[] ballColors;
    void SetBallColor(Ball _currentBall)
    {
        int randomColor = Random.Range(0, ballColors.Length);
        _currentBall.SetBallColor(ballColors[randomColor],randomColor);
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        ballQueue = new Queue<Ball>();
        rainbowBallQueue= new Queue<Ball>();
        ghostBallQueue = new Queue<Ball>();
        for (int i = 0; i < ballNumber; i++)
        {
            SpawnballPrefab(ballPrefab,ballQueue);
            SpawnballPrefab(rainbowBallPrefab, rainbowBallQueue);
            SpawnballPrefab(ghostBallPrefab, ghostBallQueue);

        }
    }

    private void SpawnballPrefab(Ball _ballToSpawn, Queue<Ball> _ballQueue)
    {
        var newBall = Instantiate(_ballToSpawn, Vector3.zero, Quaternion.identity);
        SetBallColor(newBall);
        newBall.gameObject.SetActive(false);
        _ballQueue.Enqueue(newBall);
        newBall.transform.parent = transform;
    }

    public void DeActiveBall(Ball _ballToDeactivate)
    {
        if (_ballToDeactivate.type == BallType.Normal)
        {
            ballQueue.Enqueue(_ballToDeactivate);
        }
        if (_ballToDeactivate.type == BallType.Rainbow)
        {
            rainbowBallQueue.Enqueue(_ballToDeactivate);
        }
    }
}
