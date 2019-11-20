using System;
using System.Collections.Generic;
using System.Linq;
using AI;
using Components;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Entities {
    public class TankEntity : MonoBehaviour {

        [Header("References")] 
        public Transform CanonOut;
        public Transform Turret;
        public AudioSource ShotFiring;
        public ParticleEmissionSetter SmokeSetter;
        public ParticleEmissionSetter FireSetter;
        public MeshRenderer TurretMeshRenderer;
        public MeshRenderer HullMeshRenderer;
        public MeshRenderer RightTrackMeshRender;
        public MeshRenderer LeftTrackMeshRender;
        
        [Header("Prefabs")]
        public GameObject ShellPrefab;
        public GameObject CanonShotPrefab;
        public GameObject TankExplosionPrefab;

        [Header("Parameters")]
        public int CanonDamage;
        public int CanonPower;
        public int TurretSpeed;
        public int MaxHP;
        public int ReloadTime;
        
        [Header("Variables")]
        public int CurrentHP;
        public bool IsShellLoaded = true;
        public GameObject Target;
        public GameObject Destination;

        public Action<float> OnLifeChanged;
        [HideInInspector] public TankData tankData;
        
        private readonly int _waypointRadius = 15;
        private NavMeshAgent _navMeshAgent;
        private TankAIComponent _tankAiComponent;

        public List<GameObject> Aggressors => GameManager.Instance.TankEntities
            .Where(go => go.GetComponent<TankEntity>().Target == gameObject).ToList();
        
        private int _totalDamages => MaxHP - CurrentHP;
        private float _damagePercent => (float) _totalDamages / MaxHP;
        private bool _isAtDestination => _navMeshAgent.remainingDistance < Mathf.Infinity &&
                                          _navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                                          _navMeshAgent.remainingDistance <= 0;

        private void Awake() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _tankAiComponent = GetComponent<TankAIComponent>();
        }

        public void InitTank(TankData tankData) {
            if (!tankData)
                throw new Exception("Each tank need a tank setting to be set");
            this.tankData = tankData;
            TurretMeshRenderer.material.color = tankData.TurretColor;
            HullMeshRenderer.material.color = tankData.HullColor;
            RightTrackMeshRender.material.color = tankData.TracksColor;
            LeftTrackMeshRender.material.color = tankData.TracksColor;
            _tankAiComponent.UtilityAiBrains = tankData.Brains;
            CurrentHP = MaxHP;
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawSphere(transform.position, _waypointRadius);
        }
        
        private void Update() {
            Debug.DrawRay(CanonOut.position, CanonOut.forward * 100, Color.red);
            if (Target) {
                Vector3 newDir = Vector3.RotateTowards(Turret.forward, Target.transform.position - Turret.position, TurretSpeed * Time.deltaTime, 0.0f);
                Turret.rotation = Quaternion.LookRotation(newDir);
                Turret.eulerAngles = new Vector3(0, Turret.eulerAngles.y, 0);
            }
            if (Destination) {
                Vector3[] pathCorners = _navMeshAgent.path.corners;
                if (pathCorners.Length > 0) {
                    Debug.DrawLine(transform.position, pathCorners[0], Color.green);
                    for (int i = 1; i < pathCorners.Length - 1; i++) {
                        Debug.DrawLine(pathCorners[i - 1], pathCorners[i], Color.green);
                    }
                }
                _navMeshAgent.SetDestination(Destination.transform.position);
                if (_isAtDestination) Destination = null;
            }
            SmokeSetter.SetEmissionPercent(_damagePercent);
            FireSetter.SetEmissionPercent(_damagePercent < 0.5 ? 0 : _damagePercent * 2);
        }

        public void Fire() {
            if (!IsShellLoaded) return;
            Instantiate(CanonShotPrefab, CanonOut.position, CanonShotPrefab.transform.rotation);
            ShotFiring.Play();
            GameObject instantiate = Instantiate(ShellPrefab, CanonOut.position, CanonOut.rotation);
            instantiate.GetComponent<Rigidbody>().AddForce(CanonOut.transform.forward * CanonPower, ForceMode.Impulse);
            instantiate.GetComponent<ShellEntity>().TankEntityOwner = this;
            instantiate.GetComponent<ShellEntity>().Damage = CanonDamage;
            IsShellLoaded = false;
            Invoke(nameof(Reload), ReloadTime);
        }

        public void Damage(int damage) {
            CurrentHP -= damage;
            OnLifeChanged.Invoke((float) CurrentHP / MaxHP);
            if (CurrentHP > 0) return;
            Instantiate(TankExplosionPrefab, transform.position, TankExplosionPrefab.transform.rotation);
            Destroy(gameObject);
        }

        private void Reload() {
            IsShellLoaded = true;
        }
        
        public void Heal(int healing) {
            CurrentHP += healing;
            OnLifeChanged.Invoke((float) CurrentHP / MaxHP);
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }

        public List<GameObject> SeekWaypointInRadius() {
            return GameManager.Instance.WaypointEntities
                .Where(go => Vector3.Distance(transform.position, go.transform.position) < _waypointRadius).ToList();
        } 
        
        public GameObject TankInRay() {
            RaycastHit hit;
            if (Physics.Raycast(CanonOut.position, CanonOut.forward, out hit, Mathf.Infinity)) {
                if (hit.transform.GetComponent<TankEntity>()) return hit.transform.gameObject;
            }
            return null;
        }
        
    }
}
