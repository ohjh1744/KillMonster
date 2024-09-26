using UnityEngine;

public class Knife : MonoBehaviour, IAttackTime, ICuttable
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _attackTime;
    [SerializeField]
    private float _range;

    public float AttackTime { get { return _attackTime; } set { _attackTime = value; } }

    public void Cut(Vector3 pos, float playerDamage)
    {
        Collider[] hits = Physics.OverlapSphere(pos, _range,  LayerMask.GetMask("Damagable"));
        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                IDamagable damagable = hit.GetComponent<IDamagable>();
                if (hit.gameObject.tag != "Player")
                {
                    damagable.TakeDamage(_damage + playerDamage);
                }
            }
        }

    }
}
