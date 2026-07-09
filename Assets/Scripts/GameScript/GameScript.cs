using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Note
{
    public List<GameScript> Notes;

}

[Serializable]
public class GameScript
{
    public int id;
    public string ContainsLine1;
    public string ContainsLine2;
    public string ContainsLine3;
}