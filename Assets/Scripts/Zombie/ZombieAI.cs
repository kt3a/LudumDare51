using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour {
  public float SightRange = 20;
  public float HearingRange = 5;
  public float SightAngle = 20;
  public float RoamRange = 20;
  public Transform SpineBone;
  public ParticleSystem HitParticles;

  public float RunSpeed = 4.8f;
  public float IdleSpeed = 0.8f;

  private Animator _animator;
  private NavMeshAgent _navMeshAgent;
  private Transform _player;
  private bool _knowsAboutPlayer = true;
  private bool _idiling = true;
  private bool _dead;
  private Vector3 _playerLastKnowPosition;
  private Vector3 _prevPos;
  private Vector3 _startPos;
  private float _spineBoneOffset = 0;
  private float _health = 100;
  private float _omniscienceTimer = 0;

  void Start()
  {
    _navMeshAgent = GetComponent<NavMeshAgent>();
    _animator = GetComponent<Animator>();
    _player = GameObject.FindGameObjectWithTag("Player").transform;
    _prevPos = transform.position;
    _navMeshAgent.speed = IdleSpeed;
    _startPos = transform.position;
    StartCoroutine(Idle());
  }

  void Update()
  {
    if (_dead)
      return;

    _omniscienceTimer -= Time.deltaTime;

    LookForPlayer();
    float speed = (_prevPos - transform.position).magnitude / Time.deltaTime;
    _animator.SetFloat("Speed", speed);

    if (!_idiling)
    {
      _navMeshAgent.destination = _playerLastKnowPosition;
    }

    if (_navMeshAgent.remainingDistance < 2f && _knowsAboutPlayer)
    {
      _animator.SetTrigger("Attack");
    } else if (!_knowsAboutPlayer && _navMeshAgent.remainingDistance < 2f && !_idiling)
    {
      _idiling = true; 
      _navMeshAgent.speed = IdleSpeed;
      StartCoroutine(Idle());
      _startPos = transform.position;
    }

    _prevPos = transform.position;
  }

  private void LateUpdate()
  {
    if (_dead)
      return;
    _spineBoneOffset = Mathf.Lerp(_spineBoneOffset, 0, Time.deltaTime);

    if (_spineBoneOffset > 0.1f)
    {
      Quaternion rot = Quaternion.AngleAxis(-_spineBoneOffset, SpineBone.right);
      SpineBone.forward = rot * SpineBone.forward;
    }
  }

  private void LookForPlayer()
  {
    Vector3 playerVec = _player.position - transform.position;
    float playerDist = playerVec.magnitude;
    bool hasLineOfSight = Physics.Linecast(transform.position, _player.position, out RaycastHit hit) && hit.collider.CompareTag("Player");


    if (playerDist < HearingRange || _omniscienceTimer > 0)
    {
      _knowsAboutPlayer = true;
      _playerLastKnowPosition = _player.position;
      _navMeshAgent.speed = RunSpeed;
      _idiling = false;
      StopCoroutine(Idle());
    } else if (playerDist < SightRange && Vector3.Angle(transform.forward, playerVec) < SightAngle && hasLineOfSight)
    {
      _knowsAboutPlayer = true;
      _playerLastKnowPosition = _player.position;
      _navMeshAgent.speed = RunSpeed;
      _idiling = false;
      StopCoroutine(Idle());
    } else
    {
      _knowsAboutPlayer = false;
    }
  }

  IEnumerator Idle()
  {
    while(true)
    {
      yield return new WaitForSecondsRealtime(Random.Range(5, 10));

      do {
        yield return new WaitForEndOfFrame();
        if (_navMeshAgent.pathPending)
          continue;
        Vector3 randDir = Random.insideUnitCircle;
        randDir.z = randDir.y;
        randDir.y = 0;
        _navMeshAgent.destination = _startPos + randDir * Random.Range(0, RoamRange);
      } while (_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete);

      float timer = 0;
      while (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance && timer < 25)
      {
        yield return new WaitForEndOfFrame();
        timer += Time.deltaTime;
      }
    }
  }

  public void HitZombie()
  {
    if (_dead)
      return;
    _spineBoneOffset = 40;
    HitParticles.Play();
    _health -= 34;

    _omniscienceTimer = 5;
    if (_health <= 0)
    {
      _animator.SetTrigger("Die");
      _dead = true;
      StopAllCoroutines();
      _navMeshAgent.baseOffset = -.8f;
    }
  }
}
