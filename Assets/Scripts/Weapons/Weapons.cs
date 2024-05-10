using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public abstract class Weapons : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] protected GameObject Camera;
    [SerializeField] protected MouseLook cameraShake;
    [Header("For shake effect")]
    [SerializeField] protected float shakeDuration;
    [SerializeField] protected float magntitude;
    [Header("Gun stats")]
    [SerializeField] protected int Damage;
    [SerializeField] protected float Distance;
    [SerializeField] protected int MagazineSize;
    [SerializeField] protected float reloadTime;
    [Header("Lists")]
    [SerializeField] protected List<AudioClip> AudioClips;
    [SerializeField] protected List<VisualEffect> Effects;
    [SerializeField] protected List<float> shootDelays;
    protected Animator _animator;
    protected AudioSource _source;
    protected bool _isShoot;
    protected bool _isReload;
    protected int _currMagazineSize;
    protected RaycastHit _hit;


    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }
    protected virtual void Start()
    {
        _currMagazineSize = MagazineSize;
    }
    protected virtual void Update()
    {
        //if (!_isShoot && !_isReload)
        //{
        //    if (Input.GetButton("Fire1") && _currMagazineSize >= 1)
        //    {
        //        StopAllCoroutines();
        //        StartCoroutine(C_Shoot());
        //        DealDamage();
        //    }
        //    else if (_currMagazineSize <= 0)
        //    {
        //        StopAllCoroutines();
        //        StartCoroutine(C_Reload());
        //    }
        //    else if (Input.GetKeyDown(KeyCode.R) && _currMagazineSize < MagazineSize)
        //    {
        //        StopAllCoroutines();
        //        StartCoroutine(C_Reload());
        //    }
        //    else
        //    {
        //        _animator.Play("SHOTGUN_idle");
        //    }
        //}
    }

    protected virtual void DealDamage()
    {
        //if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out _hit, Distance))
        //{
        //// Enemy enemy = _hit.collider.GetComponent<Enemy>();
        //// if (enemy != null)
        ////{
        //// enemy.TakeDamage(Damage);
        //// }
        //}
    }
    protected virtual IEnumerator C_Shoot()
    { 
        //_isShoot = true;
        //_currMagazineSize--;
        //Effects[0].gameObject.SetActive(true);
        //_animator.Play("");
        //_source.PlayOneShot(AudioClips[0]);
        //yield return new WaitForSeconds(1f);
        //Effects[0].gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        //_isShoot = false;
    }
    protected virtual IEnumerator C_Reload()
    { 

        //_isReload = true;
        //_currMagazineSize = MagazineSize;
        //_source.PlayOneShot(AudioClips[1]);
        //_animator.Play("");
        yield return new WaitForSeconds(1f);
        //_isReload = false;
    }
}
