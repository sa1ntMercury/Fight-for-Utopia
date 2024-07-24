using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score 
{
    private static byte _maxDefeats = 2;

    public static byte MaxDefeats => _maxDefeats; 


    public static byte MyPoints { get; private set; }
    public static byte EnemyPoints { get; private set; }
    
    public static void UpMyPoints()
    {
        MyPoints++;
    }
    public static void UpEnemyPoints()
    {
        EnemyPoints++;
    }
    public static void Nullify()
    {
        MyPoints = default;
        EnemyPoints = default;
    }
}
