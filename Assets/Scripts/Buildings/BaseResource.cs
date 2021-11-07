using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResource : BaseTile
{
    public Resource.CurrencyType type;
    public int amount = 0;
    public bool isFull = false;
}
