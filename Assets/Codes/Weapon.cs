using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;

        }
        if (Input.GetButtonDown("Jump"))
            LevelUp(20, 5);
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
        if (id == 0)
            Batch();
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector2.zero;

        for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.sprite.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
            }

            bullet.parent = transform;



            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotateVector = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotateVector);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector2.zero); // -1 means infinite
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPosition = player.scanner.nearestTarget.position;
        Vector3 fireDirection = targetPosition - transform.position;

        fireDirection = fireDirection.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, fireDirection);
        bullet.GetComponent<Bullet>().Init(damage, count, fireDirection);
    }
}
