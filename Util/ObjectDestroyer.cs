﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    
    public void DestroyMe(){
        Destroy(this.gameObject);
    }
}
