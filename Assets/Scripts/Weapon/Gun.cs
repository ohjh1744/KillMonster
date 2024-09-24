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
    public int Damage;
    public float AttackTime;
    public int Bullet;
    public float ReLoadTime;
    public float ReCoil;
    public float MinFov;
    public float MaxFov;
    public float FlashTime;
    public GameObject FireFlash;
    public Image ReLoadImage;
}

public class Gun : MonoBehaviour, IAttackTime, IShootable, IZoomable
{
   [SerializeField] private GunData _gunData;

    private Coroutine _fireFlashRoutine;
    private WaitForSeconds _flashWaitForSeconds;
    private Coroutine _reLoadRoutine;
    private WaitForSeconds _reLoadWaitForSeconds;
    private LayerMask _layerMask;

    public float AttackTime { get{ return _gunData.AttackTime; } set { _gunData.AttackTime = value; } }
    public int Bullet { get { return _gunData.Bullet; } set { _gunData.Bullet = value; } }
    public float ReCoil { get { return _gunData.ReCoil; } set { _gunData.ReCoil = value; } }
    public bool IsReLoad { get; set; }
    
    public GameObject FireFlash { get { return _gunData.FireFlash; } set { _gunData.FireFlash = value; } }

    public void Start()
    {
        _layerMask = LayerMask.NameToLayer("Damagable");
        _flashWaitForSeconds = new WaitForSeconds(_gunData.FlashTime);
        _reLoadWaitForSeconds = new WaitForSeconds(_gunData.ReLoadTime);
    }
    public void Shoot(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if(hit.collider.tag != "Player")
            {
                GameObject bulletImpact = BulletPullManager.Instance.Get();
                bulletImpact.transform.position = hit.point;
                Vector3 bulletAngle = camera.transform.position - hit.point;
                bulletImpact.transform.rotation = Quaternion.LookRotation(bulletAngle);
            }

            if(hit.collider.gameObject.layer == _layerMask)
            {
                Debug.Log(hit.collider.name);
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                damagable.TakeDamage(_gunData.Damage);
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

    public void ZoomIn(CinemachineVirtualCamera camera)
    {
        camera.m_Lens.FieldOfView = _gunData.MinFov;
    }

    public void ZoomOut(CinemachineVirtualCamera camera)
    {
        camera.m_Lens.FieldOfView = _gunData.MaxFov;
    }
}
