using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{
    public Slider _volumeSlider;
    public void Changevolume()
    {
        StaticClass._volume = _volumeSlider.value;
    }

    // Start is called before the first frame update
    public void Start()
    {
       _volumeSlider.value = StaticClass._volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
