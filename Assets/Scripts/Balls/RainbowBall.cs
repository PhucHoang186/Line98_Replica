using UnityEngine;
public class RainbowBall : Ball
{
    public override void SetBallColor(Color _color, int _colorValue) // we dont want to set the color of rainbow ball
    {
        return;
    }
    public override void OnExplode()
    {
        var explode = Instantiate(explodeFX, transform.position, Quaternion.identity);
        Destroy(explode, 1f);
    }
}
