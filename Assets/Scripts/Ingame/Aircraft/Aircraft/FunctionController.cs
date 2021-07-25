using System.Collections.Generic;
using ModelAirCraft;
using UnityEngine;

namespace Aircraft
{
    [RequireComponent(typeof(GunController), typeof(WingController), typeof(EngineController))]
    public class FunctionController : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private GunController _gunController;
        [SerializeField] private WingController _wingController;
        [SerializeField] private EngineController _engineController;
        [SerializeField] private BodyController _bodyController;

        [Header("In game")]
        [SerializeField] private Transform _target;
        [SerializeField] private Vector2 _currentDir;

        void Reset()
        {
            _gunController = GetComponent<GunController>();
            _wingController = GetComponent<WingController>();
            _engineController = GetComponent<EngineController>();
            _bodyController = gameObject.AddComponent<BodyController>();
        }

        //void Start()
        //{
        //   // _gunController.CreatePoolProjectile();
        //}

        public void Update()
        {
            //if (_target)
            //    if (ProCamera2D.Instance.CameraTargets.Find(s=>s.TargetTransform==_target)==null)
            //        ProCamera2D.Instance.AddCameraTarget(_target);

            var deltaTime = Time.deltaTime;
            GetPad();

            _gunController.AimTarget(_target);
            _gunController.PerUpdate(deltaTime);

            var isHavePad = _currentDir.magnitude > 0;

            if (isHavePad)
            {
                _wingController.SetDirection(_currentDir);
                _wingController.PerUpdate(deltaTime);
                _engineController.PerUpdate(deltaTime);
            }

        }

        private void GetPad()
        {
            _currentDir.y = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
            _currentDir.x = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;

            _currentDir.Normalize();
        }


        #region For setting aircraft

        public void SetGunBarrel(List<Transform> gunBarrels)
        {
            _gunController.GunBarrel = gunBarrels;
        }

        public void SettingData(PartData[] data)
        {
            _wingController.Setting(ConvertPart<WingData>(data.GetPath(TypePart.Wind)));
            _gunController.Setting(ConvertPart<WeaponData>(data.GetPath(TypePart.Gun)));
            _bodyController.Setting(ConvertPart<BodyData>(data.GetPath(TypePart.Body)));
            _engineController.Setting(ConvertPart<EngineData>(data.GetPath(TypePart.Engine)));
        }

        private T ConvertPart<T>(PartData part) where T : PartData
        {
            var convert = part as T;
            if (convert == null)
            {
                Debug.LogError($"This is not part need {typeof(T)}");
                return null;
            }

            return convert;
        }


        #endregion
    }
}