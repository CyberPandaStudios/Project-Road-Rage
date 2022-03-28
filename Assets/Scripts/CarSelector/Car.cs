using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Scriptable Objects/Car")]
public class Car : ScriptableObject
{
    [Header("Description")]
    public string carName; 

    [Header("Stats")]
    public float speed; 
    public float control; 
    public float damage; 

    [Header("3D Model")]
    public GameObject carModel; 
}
