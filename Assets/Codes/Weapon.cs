/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

//* Weapon class to handle weapon logic *//
public class Weapon : MonoBehaviour
{
    //* GameObject *//
    Player player;

    //* Weapon Information *//
    public int id; // 0: Melee, 1: Range, 2: Glove, 3: Shoe, 4: Heal
    public int prefabId; // store prefab id in pool
    public float damage;
    public int count;
    public float speed;

    //* Variable *//
    float timer;

    //* Unity's Functions *//
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        player = GameManager.instance.player;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isAlive) // if player is dead do nothing
            return;

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
    }

    //* Custom Functions *//
    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
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
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;
            default:
                speed = 0.3f * Character.WeaponRate;
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
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector2.zero); // -100 means infinite
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

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
