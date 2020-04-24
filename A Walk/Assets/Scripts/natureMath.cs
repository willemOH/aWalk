using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class natureMath
{
    public float[] fibonacciSeq(int length)
    {
        float[] fibSeq = new float[length];
        fibSeq[0] = 0;
        fibSeq[1] = 1;
        for(int i = 2; i<length; i++)
        {
            fibSeq[i] = fibSeq[i - 1] + fibSeq[i - 2];
        }
        return fibSeq;
    }
}

