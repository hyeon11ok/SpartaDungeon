using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour, ICollsionEnter, ICollsionExit
{
    private IEnumerator fireCoroutine;
    private Player player;

    [SerializeField] private float fireRate;
    [SerializeField] private float fireDamage;

    public void EnterEvent(GameObject other)
    {
        if(other.TryGetComponent<Player>(out player))
        {
            fireCoroutine = FireCoroutine();
            StartCoroutine(fireCoroutine);
        }
    }

    public void ExitEvent(GameObject other)
    {
        if(other.gameObject.Equals(player.gameObject))
        {
            StopCoroutine(fireCoroutine);
            player = null;
        }
    }

    IEnumerator FireCoroutine()
    {
        while(player != null)
        {
            player.Condition.TakeDamage(fireDamage);
            yield return new WaitForSeconds(fireRate);
        }
    }
}
