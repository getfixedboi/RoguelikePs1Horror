using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEnemy : MonoBehaviour
{
    public int maxHP;
    public int currentHp;
    public Transform target;
    public NavMeshAgent agent;
    public Animator anim;
    public bool isDead = false;
    public int dealDamage = 20;

    private static List<GameObject> _itemPool = new List<GameObject>();

    public virtual void Awake()
    {
        currentHp = maxHP;
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            isDead = true;
            Destroy(gameObject, 2f);
        }
    }

    public void OnDestroy()
    {
        if (UnityEngine.Random.Range(0f, 1f) >= BaseItemBehaviour.ItemDropChance)
        {
            if (_itemPool != null && _itemPool.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, _itemPool.Count);
                Instantiate(_itemPool[index], transform.position + new Vector3(0, 1, 0), transform.rotation);
            }
        }
        if(UnityEngine.Random.Range(0f, 1f) >= 0.15)
        {
            BuffEnemies();
        }
    }

    public static void AddPrefabToList(params GameObject[] items)
    {
        foreach (var item in items)
        {
            if (item != null && !_itemPool.Contains(item))
            {
                if (item.GetComponent<BaseItemBehaviour>() != null)
                {
                    _itemPool.Add(item);
                }
            }
        }
    }
    public void BuffEnemies()
    {
        dealDamage += 5;
        maxHP += 5;
    }

}
