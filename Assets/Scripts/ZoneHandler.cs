using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZoneHandler : MonoBehaviour
{
    enum ZoneTypes
    {
        DeadZone,
        FinishZone
    }

    [SerializeField] ZoneTypes zoneType;
    private void OnTriggerEnter(Collider other)
    {
        switch (zoneType)
        {
            case ZoneTypes.DeadZone:
                GameController.Instance.PlayerLost?.Invoke();
                break;
            case ZoneTypes.FinishZone:
                GameController.Instance.PlayerWin?.Invoke();
                break;
        }
    }
}
