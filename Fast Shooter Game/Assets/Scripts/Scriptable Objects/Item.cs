using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    new public string name;
    public string description;
    public string type;
    public Sprite icon;
    public Vector2 iconSize;

    public virtual void Use()
	{
        Debug.Log($"'{name}' was used");
	}
}

