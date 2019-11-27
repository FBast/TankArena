using System;
using System.Collections.Generic;
using System.Linq;
using AI;
using Components;
using Data;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Utils;

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
        public MeshRenderer FactionDisk;
        public GameObject TurretCamera;
        
        [Header("Prefabs")]
        public GameObject ShellPrefab;
        public GameObject CanonShotPrefab;
        public GameObject TankExplosionPrefab;
        public GameObject BustedTankPrefab;

        public int CanonDamage { get; private set; }
        public int CanonPower { get; private set; }
        public int TurretSpeed { get; private set; }
        public int MaxHP { get; private set; }
        public int ReloadTime { get; private set; }
        public int TeamNumber { get; private set; }
        public int CurrentHP { get; private set; }
        public bool IsShellLoaded = true;
        public GameObject Target;
        public GameObject Destination;

        private int _waypointRadius;
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
            _waypointRadius = PlayerPrefs.GetInt(Properties.PlayerPrefs.WaypointSeekRadius, Properties.PlayerPrefsDefault.WaypointSeekRadius);
            MaxHP = PlayerPrefs.GetInt(Properties.PlayerPrefs.HealthPoints, Properties.PlayerPrefsDefault.HealthPoints);
            CurrentHP = MaxHP;
            CanonDamage = PlayerPrefs.GetInt(Properties.PlayerPrefs.CanonDamage, Properties.PlayerPrefsDefault.CanonDamage);
            CanonPower = PlayerPrefs.GetInt(Properties.PlayerPrefs.CanonPower, Properties.PlayerPrefsDefault.CanonPower);
            TurretSpeed = PlayerPrefs.GetInt(Properties.PlayerPrefs.TurretSpeed, Properties.PlayerPrefsDefault.TurretSpeed);
            ReloadTime = PlayerPrefs.GetInt(Properties.PlayerPrefs.ReloadTime, Properties.PlayerPrefsDefault.ReloadTime);
        }

        public void Init(TankSetting setting, int teamNumber, Color factionColor) {
            if (!setting)
                throw new Exception("Each tank need a tank setting to be set");
            TeamNumber = teamNumber;
            TurretMeshRenderer.material.color = setting.TurretColor;
            HullMeshRenderer.material.color = setting.HullColor;
            RightTrackMeshRender.material.color = setting.TracksColor;
            LeftTrackMeshRender.material.color = setting.TracksColor;
            _tankAiComponent.UtilityAiBrains = setting.Brains;
            FactionDisk.material.color = factionColor;
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
            if (CurrentHP > 0) return;
            Die();
        }

        private void Die() {
            Instantiate(TankExplosionPrefab, transform.position, TankExplosionPrefab.transform.rotation);
            if (PlayerPrefsUtils.GetBool(Properties.PlayerPrefs.ExplosionCreateBustedTank, 
                Properties.PlayerPrefsDefault.ExplosionCreateBustedTank))
                Instantiate(BustedTankPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
        private void Reload() {
            IsShellLoaded = true;
        }
        
        public void Heal(int healing) {
            CurrentHP += healing;
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
