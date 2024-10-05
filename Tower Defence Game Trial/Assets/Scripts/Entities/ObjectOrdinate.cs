using UnityEngine;

[CreateAssetMenu(fileName = "Ordinate", menuName = "Ordinate/CreateOrdinate", order = 1)]
public class ObjectOrdinate : ScriptableObject
{
    public OrdinateStruct[] Ordinate;
}

[System.Serializable]
public struct OrdinateStruct
{
    public Vector2[] Checkpoint;
}