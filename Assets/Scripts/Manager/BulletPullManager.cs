using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPullManager : MonoBehaviour
{
    public static BulletPullManager Instance  {get; private set; }
    [SerializeField]private GameObject BulletImpact;
    private List<GameObject>_pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _pool = new List<GameObject>();
    }

    public GameObject Get()
    {
        GameObject select = null;

        foreach (GameObject item in _pool)
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(BulletImpact, transform);
            _pool.Add(select);
        }

        return select;
    }
}
