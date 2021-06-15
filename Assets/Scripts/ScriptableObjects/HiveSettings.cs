using UnityEngine;

[CreateAssetMenu(menuName = "HiveSettings", fileName = "HiveSettings")]
public class HiveSettings : ScriptableObject
{
    [Header("Цикл(Тик) обновления улья")]
    public float TickRate = 1f;
    [Header("Задержка перед периодическим событием смерти")]
    public float PeriodicDeathEventDelay = 15f;

    [Header("Параметры улья")]
    [Tooltip("Максимальное количество пчел внутри улья")]
    public int BeeMaxAmountInHive = 10;
    [Tooltip("Максимальное количество одновременно активных пчел в одном улье")]
    public int BeeMaxActiveAmount = 5; 
    public int HoneyMaxAmount = 100; 
}
