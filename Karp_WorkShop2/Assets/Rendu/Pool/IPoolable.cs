using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public void PoolInit();
    public void PoolReset();
}
