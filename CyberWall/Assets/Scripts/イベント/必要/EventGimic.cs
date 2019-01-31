using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEventGimic
{
    void OnPlayEvent();
    void OnPlayEffect(Collider other = null);
    void OnPlaySound();
}