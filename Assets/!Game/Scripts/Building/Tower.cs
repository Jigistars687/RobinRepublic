using UnityEngine;

public class Tower : Building
{
    [SerializeField] private float _scaleMultiplier = 1.2f;
    [SerializeField] private float _duration = 0.2f;


    public void Pulse()
    {
        LeanTween.scale(gameObject, transform.localScale * _scaleMultiplier, _duration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setLoopPingPong(1);
    }

    public override void Place()
    {
        base.Place();
        Pulse();
    }
}

