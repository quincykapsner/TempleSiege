using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc1 : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    { 
        base.Start();
        health = 1f;
    }

}
