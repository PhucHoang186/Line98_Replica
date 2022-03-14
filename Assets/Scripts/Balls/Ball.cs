using DG.Tweening;
using UnityEngine;
public enum BallType 
{
    Normal, Rainbow, Ghost
}

public class Ball : MonoBehaviour
{
    [SerializeField] float spawnTime = 0.2f;
    //ball animation
    [HideInInspector]public Animator ballAni;
    private void Awake()
    {
        ballAni = GetComponent<Animator>();
    }
    public GameObject explodeFX;
    // public Node occupiedNode;// the node contain this ball
    public int colorValue;
    public BallType type = BallType.Normal;
    public virtual void SetBallColor(Color _color,int _colorValue)
    {
        GetComponentInChildren<SpriteRenderer>().color = _color;
        colorValue= _colorValue;
    }
    public void OnSpawnAnimation()
    {
        transform.localScale = Vector2.zero;
        transform.DOScale(new Vector2(0.8f, 0.8f), spawnTime);

    }
    public virtual void OnExplode()
    {
        var explode =Instantiate(explodeFX,transform.position, Quaternion.identity);
        var main = explode.GetComponentInChildren<ParticleSystem>().main;
        main.startColor = GetComponentInChildren<SpriteRenderer>().color;
        Destroy(explode, 1f);
    }
}
