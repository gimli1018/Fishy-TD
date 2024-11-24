using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    public void destroySelf()
    {
        Destroy(gameObject);
    }
}
