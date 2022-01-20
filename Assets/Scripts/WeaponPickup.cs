using UnityEngine;
using DG.Tweening;
public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon m_weapon = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Const.TAG_PLAYER))
        {
            other.GetComponent<Attack>().EquipWeapon(m_weapon);
            gameObject.SetActive(false);
        }
    }
}
