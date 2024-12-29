using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : TargetObject
{
    void Start()
    {

    }

    void OnCollect()
    {
        active = false;

        CoinDisplay.OnCoinPickup();

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active) return;

        if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
            OnCollect();
    }
}
