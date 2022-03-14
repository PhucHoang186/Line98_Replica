using UnityEngine;
using System.Linq;
[System.Serializable]
public class Node : MonoBehaviour
{
    //Hover 
    [SerializeField] GameObject hightlightSprite;
    [SerializeField] public GameObject selectedballSprite;
    //manager
    public Ball occupiedBall; // the ball on this node
    [HideInInspector] public bool isSelected;
    public int visited = -1;
    private void Start()
    {
    }
    private void OnMouseEnter()
    {
        hightlightSprite.SetActive(true);
    }
    private void OnMouseExit()
    {
        hightlightSprite.SetActive(false);
    }
    private void OnMouseDown()
    {

        if (GamePlayManager.instance.currentState == GameState.SelectBall)
        {
            if (occupiedBall != null)
            {

                GamePlayManager.instance.SelectBall(this);
                return;
            }
        }
        if (GamePlayManager.instance.currentState == GameState.SelectNodeToPlaceBall)
        {
            if (occupiedBall == null)
            {

                GamePlayManager.instance.SelectNode(this);

            }
            else
            {
                GamePlayManager.instance.SwitchState(GameState.SelectBall);
                GamePlayManager.instance.SelectBall(this);

            }
        }
    }


}

