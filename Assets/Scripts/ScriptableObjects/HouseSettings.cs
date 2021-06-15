using UnityEngine;

[CreateAssetMenu(menuName = "HouseSettings", fileName = "HouseSettings")]
public class HouseSettings : ScriptableObject
{
    [Header("Параметры дома")]
    [Tooltip("Максимальное количество меда хранящееся в доме")]
    public int HoneyMaxAmountCapacity = 1000;
    public int HoneyPrice = 2;
}
