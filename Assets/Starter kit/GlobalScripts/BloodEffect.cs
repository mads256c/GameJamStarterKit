using UnityEngine;
using System.Collections;

public class BloodEffect : MonoBehaviour {

    public UnityEngine.UI.Image BloodImage;
    public Screenshake Shaker;

    public bool test;

    void Update()
    {
        if(test)
        {
            BloodAndShake();
            test = false;
        }
    }

    public void BloodAndShake()
    {
        StartCoroutine(StartBloodEffect());
    }

    IEnumerator StartBloodEffect()
    {
        BloodImage.enabled = true;
        BloodImage.gameObject.SetActive(true);
        //fade out
        for(float timer = 1; timer >= 0; timer -= Time.deltaTime*5)
        {
            BloodImage.color = new Color(BloodImage.color.r,BloodImage.color.g,BloodImage.color.b,timer);

            yield return null;
        }
        BloodImage.enabled = false;
        yield break;

    }
}
