using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons
{
    public int DamageValue
    {
        get
        {
            return Damage;
        }
        set
        {
            Damage = value;
        }
    }
    public int AmmoValue
    {
        get
        {
            return MagazineSize;
        }
        set
        {
            MagazineSize = value;
        }
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        if (!_isShoot && !_isReload)
        {
            if (Input.GetButton("Fire1") && _currMagazineSize >= 1)
            {
                StopAllCoroutines();
                StartCoroutine(C_Shoot());
            }
            else if (_currMagazineSize <= 0)
            {
                StopAllCoroutines();
                StartCoroutine(C_Reload());
            }
            else if (Input.GetKeyDown(KeyCode.R) && _currMagazineSize < MagazineSize)
            {
                StopAllCoroutines();
                StartCoroutine(C_Reload());
            }
            else
            {
                _animator.Play("SHOTGUN_idle");
            }
        }
    }
    protected override void DealDamage()
    {
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out _hit, Distance))
        {
            BaseEnemy enemy = _hit.collider.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
    protected override IEnumerator C_Reload()
    {
        _isReload = true;
        _currMagazineSize = MagazineSize;
        _source.PlayOneShot(AudioClips[1]);
        _animator.Play("SHOTGUN_reload2");
        yield return new WaitForSeconds(reloadTime);
        _isReload = false;
    }
    protected override IEnumerator C_Shoot()
    {
        _isShoot = true;

        _currMagazineSize--;

        StartCoroutine(cameraShake.C_Shake(shakeDuration, magntitude));

        DealDamage();

        Effects[0].gameObject.SetActive(true);

        _animator.Play("SHOTGUN_fire");

        _source.PlayOneShot(AudioClips[0]);

        yield return new WaitForSeconds(shootDelays[0]);

        Effects[0].gameObject.SetActive(false);

        yield return new WaitForSeconds(shootDelays[1]);

        _isShoot = false;
    }


}
