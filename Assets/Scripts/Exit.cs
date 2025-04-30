using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Text/Exit")]
public class Exit : ScriptableObject
{
   public enum Direction { one, two, three} //edited to be one teo and three
   
   public Direction direction;
    [TextArea]
    public string description;
    public Room room;

    public bool isLocked;
    public bool isHidden;
}
