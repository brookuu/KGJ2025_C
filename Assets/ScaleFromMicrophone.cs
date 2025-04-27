using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    public Vector3 minScale;
    public Vector3 maxScale;
    public AudioLoudnessDetection detector;
    
    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    public GameObject image;
    public GameObject superBulletPrefab;
    public Transform bulletArea;

    public float cooldownTime = 20f;      // 冷卻時間 (秒)

    private bool isCoolingDown = false;
    private float cooldownTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        if (loudness < threshold)
            loudness = 0;

        image.transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);

        if (isCoolingDown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCoolingDown = false;
            }
        }
        if (image.transform.localScale == maxScale)
        {
            TryFireSuperBullet();
        }
    }


    public void TryFireSuperBullet()
    {
        if (isCoolingDown)
        {
            Debug.Log("大招冷卻中，不能發射！");
            return;
        }

        FireSuperBullet();
        isCoolingDown = true;
        cooldownTimer = cooldownTime;
    }

    private void FireSuperBullet()
    {
        GameObject obj = Instantiate(superBulletPrefab, bulletArea);
        obj.transform.localPosition = new Vector3(0f, -150f, 0f);
        Debug.Log("超級子彈發射！");
    }
}
