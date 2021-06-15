using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSettings", fileName = "PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Параметры игрока")]
    [Tooltip("Максимальное количество меда у игрока")]
    public int HoneyMaxAmountCapacity = 100;    
}
