using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Blinky : MonoBehaviour
{
    public Light2D myLight;
    public bool blinked = false;
    public Color overdrive_color = Color.white;
    private Color classic_color;
    private int insane_counter = 0;
    public int insane_counter_max = 20;
    public float overdrive_intensity = 4;
    private float default_intensity;
    private float temp_intensity = 1;
    public bool dimming = false;
    public AudioSource lightsnd;
    public BlockLighter blocklighter;
    float ZMove = 0f;
    float ZyMove = 0f;
    Vector3 lightpos;
    // Start is called before the first frame update
    void Start()
    {
        classic_color = myLight.color;
        default_intensity = myLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        ZMove = Input.GetAxisRaw("Z") * 4;
        ZyMove = Input.GetAxisRaw("Zy") * 4 * (true ? -1 : 1);
        lightpos.x = ZMove;
        lightpos.y = ZyMove;
        transform.localPosition = lightpos;
        if (Input.GetButtonDown("Blink") && !blinked)
        {
            blinked = true;
            myLight.intensity = overdrive_intensity;
            myLight.color = overdrive_color;
            lightsnd.transform.position = transform.position;
            blocklighter.LightThem();
            lightsnd.Play();
        }
        if (blinked && !dimming)
        {
            if (insane_counter >= insane_counter_max)
            {
                insane_counter = 0;
                temp_intensity = overdrive_intensity;
                dimming = true;
                blinked = false;
            }
            else
            {
                insane_counter += 1;
            }
        }
        if (dimming == true)
        {
            if (temp_intensity > default_intensity)
            {
                myLight.intensity = temp_intensity;
                temp_intensity -= 1f / insane_counter_max;

            }
            if (temp_intensity < default_intensity)
            {
                temp_intensity = default_intensity;
            }
            else if (temp_intensity == default_intensity)
            {
                myLight.color = classic_color;
                myLight.intensity = default_intensity;
                lightsnd.Stop();
                dimming = false;
            }
        }
        if (temp_intensity > default_intensity)
        {
            myLight.color = overdrive_color;
        }
        if (temp_intensity == default_intensity)
        {
            myLight.color = classic_color;
        }
    }
}
