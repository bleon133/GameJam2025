using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VolumeIniciation : MonoBehaviour
{
    private void Awake()
        {
        VolumeManager.Instance.ApplyGlobalVolume();
    }
}
