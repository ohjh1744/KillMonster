using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GunData
{
    [SerializeField] private int _damage;
    public int Damage { get { return _damage; } private set { } }

    [SerializeField] private float _attackTime;
    public float AttackTime { get { return _attackTime; } private set { } }

    [SerializeField] private int _bullet;
    public int Bullet { get { return _bullet; } private set { } }

    [SerializeField] private float _reLoadTIme;
    public float ReLoadTime { get { return _reLoadTIme; } private set { } }


    [SerializeField] private float _reColi;
    public float ReCoil { get { return _reColi; } private set { } }

    [SerializeField] private float _minFov;
    public float MinFov { get { return _minFov; } private set { } }

    [SerializeField] private float _maxFov;
    public float MaxFov { get { return _maxFov; } private set { } }

    [SerializeField] private float _flashTime;
    public float FlashTime { get { return _flashTime; } private set { } }

    [SerializeField] private GameObject _fireFlash;
    public GameObject FireFlash { get { return _fireFlash; } private set { } }

    [SerializeField] private Image _reLoadImage;
    public Image ReLoadImage { get { return _reLoadImage; } private set { } }

    [SerializeField] private BulletHitImpactPull _bulletHitImpactPull;
    public BulletHitImpactPull BulletHitImpactPull { get { return _bulletHitImpactPull; } private set { } }
}

public class Gun : MonoBehaviour, IAttackTime, IShootable, IZoomable
{
    [SerializeField] private GunData _gunData;

    [SerializeField] private AudioClip _shootClip;

    [SerializeField] private AudioClip _reLoadClip;

    [SerializeField] private Camera _camera;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private AudioSource _audioSource;

    private Coroutine _fireFlashRoutine;

    private WaitForSeconds _flashWaitForSeconds;

    private Coroutine _reLoadRoutine;

    private WaitForSeconds _reLoadWaitForSeconds;

    private LayerMask _layerMask;

    public float AttackTime { get { return _gunData.AttackTime; } private set { } }
    public int Bullet { get { return _gunData.Bullet; } private set { } }
    public float ReCoil { get { return _gunData.ReCoil; } private set { } }
    public bool IsReLoad { get; set; }
    public GameObject FireFlash { get { return _gunData.FireFlash; } private set { } }

    public void Start()
    {
        _layerMask = LayerMask.NameToLayer("Damagable");
        _flashWaitForSeconds = new WaitForSeconds(_gunData.FlashTime);
        _reLoadWaitForSeconds = new WaitForSeconds(_gunData.ReLoadTime);
    }
    public void Shoot(float playerDamage)
    {
        _audioSource.PlayOneShot(_shootClip);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if(hit.collider.tag != "Player")
            {
                GameObject bulletImpact = _gunData.BulletHitImpactPull.Get();
                bulletImpact.transform.position = hit.point;
                Vector3 bulletAngle = _camera.transform.position - hit.point;
                bulletImpact.transform.rotation = Quaternion.LookRotation(bulletAngle);
            }

            if(hit.collider.gameObject.layer == _layerMask && hit.collider.gameObject.tag != "Player")
            {
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                damagable.TakeDamage(_gunData.Damage + playerDamage);
            }
        }
        _fireFlashRoutine = StartCoroutine(FlashEffect());
    }

    public IEnumerator FlashEffect()
    {
        FireFlash.SetActive(true);

        yield return _flashWaitForSeconds;

        FireFlash.SetActive(false);
        _fireFlashRoutine = null;
    }

    public void ReLoad()
    {
        _audioSource.PlayOneShot(_reLoadClip);
        _reLoadRoutine = StartCoroutine(ReLoading());
    }

    public IEnumerator ReLoading()
    {
        _gunData.ReLoadImage.gameObject.SetActive(true);
        _gunData.ReLoadImage.fillAmount = 0;
        float filled = 0;

        while (filled < _gunData.ReLoadTime)
        {
            filled += Time.deltaTime;
            _gunData.ReLoadImage.fillAmount = Mathf.Clamp01(filled / _gunData.ReLoadTime);
            yield return null;
        }
        _gunData.ReLoadImage.fillAmount = 1;
        _gunData.ReLoadImage.gameObject.SetActive(false);
        IsReLoad = false;
        _reLoadRoutine = null;

    }

    public void ZoomIn()
    {
        _virtualCamera.m_Lens.FieldOfView = _gunData.MinFov;
    }

    public void ZoomOut()
    {
        _virtualCamera.m_Lens.FieldOfView = _gunData.MaxFov;
    }
}
