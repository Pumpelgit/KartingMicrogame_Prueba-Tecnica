using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuickSettingSO", menuName = "Karting/New Settings")]
public class QuickSettingsSO : ScriptableObject
{
    public int NumberOfLaps = 3;
    public float MaxVelocity = 15f;
    public float Acceleration = 7f;
}
