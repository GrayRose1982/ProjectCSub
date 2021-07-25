using System.Collections;
using System.Collections.Generic;
using Aircraft;
using Factory;
using ModelAirCraft;
using UnityEngine;

namespace Aircraft
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AircraftCenter : MonoBehaviour
    {
        [SerializeField] private PartData[] PartData;
        [SerializeField] private Builder _builder;
        [SerializeField] private FunctionController _controller;

        void Reset()
        {
            var builder = new GameObject("Builder");
            var controller = new GameObject("Controller");
            _builder = builder.AddComponent<Builder>();
            _controller = controller.AddComponent<FunctionController>();

            _builder.transform.SetParent(transform);
            _controller.transform.SetParent(transform);

            _builder.transform.localPosition = Vector3.zero;
            _controller.transform.localPosition = Vector3.zero;
        }

        public void SettingGunBarrels()
        {
            var gunBarrel = _builder.GetGunBarrels();
            _controller.SetGunBarrel(gunBarrel);
        }


        public void TestBuildAircraft()
        {
            BuildAircraft(PartData);
            SettingData(PartData);
        }

        public void BuildAircraft(PartData[] part)
        {
            PartData = part;
            _builder.StartBuildAircraft(PartData);

            SettingGunBarrels();
        }

        public void SettingData(PartData[] part)
        {
            _controller.SettingData(part);
        }

    }
}
