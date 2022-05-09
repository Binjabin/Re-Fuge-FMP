using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour
{
    float disappearTimer;
    [SerializeField] float disappearTimerMax;
    private Color color;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        disappearTimer += Time.deltaTime;
        color.a = Mathf.Lerp(disappearTimerMax, 0f, disappearTimer / disappearTimerMax);
        if (disappearTimer >= disappearTimerMax)
        {
            Destroy(gameObject);
        }
        sr.color = color;
    }

    public void SetColor(Color setColor)
    {
        this.color = setColor;
    }

    public void SetDisappearTimer(float disappearTimerMax)
    {
        this.disappearTimerMax = disappearTimerMax;
        disappearTimer = 0f;
    }
}
