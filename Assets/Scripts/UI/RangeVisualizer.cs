using UnityEngine;
using Systems;

public class RangeVisualizer : MonoBehaviour
{
    [SerializeField] private BunkerGun bunkerGun;

    private void Reset()
    {
        bunkerGun = GetComponent<BunkerGun>();
    }

    private void OnDrawGizmosSelected()
    {
        if (bunkerGun == null) bunkerGun = GetComponent<BunkerGun>();
        if (bunkerGun == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, bunkerGun.Range);
    }
}
