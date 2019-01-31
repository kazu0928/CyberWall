//プレイヤーの衝突イベント処理
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gimic")
        {
            IEventGimic gimic = other.GetComponent<IEventGimic>();
            gimic.OnPlayEffect(other);
            gimic.OnPlayEvent();
            gimic.OnPlaySound();
        }
    }
}
