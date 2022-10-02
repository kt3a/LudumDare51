using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour {
  public float SightRange = 20;
  public float HearingRange = 5;
  public float SightAngle = 20;
  public float RoamRange = 20;
  public AudioClip MissSoundEffect;
  public List<AudioClip> AttackSoundEffects;
  public List<AudioClip> IdleSoundEffects;
  public List<AudioClip> HitSoundEffects;
  public AudioClip EatSound;
  public Transform SpineBone;
  public ParticleSystem HitParticles;
  public static List<ZombieAI> AllZombies = new List<ZombieAI>();
  public List<Material> InvisibleMats;
  public List<Material> GlowMats;
  public List<Material> NormalMats;
  public GameObject SmokeEffects;

  public float RunSpeed = 4.8f;
  public float IdleSpeed = 0.8f;

  public bool canDamage = false;

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
  private SkinnedMeshRenderer _skinnedMeshRenderer;
  private AudioSource _audioSource;

  void Start()
  {
    AllZombies.Add(this);
    _navMeshAgent = GetComponent<NavMeshAgent>();
    _animator = GetComponent<Animator>();
    _player = GameObject.FindGameObjectWithTag("Player").transform;
    _prevPos = transform.position;
    _navMeshAgent.speed = IdleSpeed;
    _startPos = transform.position;
    _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    _audioSource = GetComponent<AudioSource>();
    StartCoroutine(Idle());
  }

  void Update()
  {
    if (_dead)
      return;
    canDamage = checkDistance();    //check for distance
    if (canDamage)
    {
      _skinnedMeshRenderer.materials = NormalMats.ToArray();
      SmokeEffects.SetActive(true);
    } else
    {

      SmokeEffects.SetActive(false);
      if (!_idiling)
        _skinnedMeshRenderer.materials = GlowMats.ToArray();
      else
        _skinnedMeshRenderer.materials = InvisibleMats.ToArray();
    }

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
      if (HealthScript.totalhealth <= 0)
      {
        _audioSource.clip = EatSound;
        _audioSource.loop = true;
        _audioSource.Play();
        _audioSource.volume = 1;
        _animator.SetBool("Eating", true);
      } else
      {
        if (!_audioSource.isPlaying)
        {
          _audioSource.clip = AttackSoundEffects[Random.Range(0, AttackSoundEffects.Count)];
          _audioSource.PlayDelayed(.4f);
        }
        _animator.SetTrigger("Attack");
      }
    }
    else if (!_knowsAboutPlayer && _navMeshAgent.remainingDistance < 2f && !_idiling)
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
    bool knewAboutPlayer = _knowsAboutPlayer;

    if (playerDist < HearingRange || _omniscienceTimer > 0)
    {
      _knowsAboutPlayer = true;
      _playerLastKnowPosition = _player.position;
      _navMeshAgent.speed = RunSpeed;
      _idiling = false;
      StopCoroutine(Idle());
    }
    else if (playerDist < SightRange && Vector3.Angle(transform.forward, playerVec) < SightAngle && hasLineOfSight)
    {
      _knowsAboutPlayer = true;
      _playerLastKnowPosition = _player.position;
      _navMeshAgent.speed = RunSpeed;
      _idiling = false;
      StopCoroutine(Idle());
    }
    else
    {
      _knowsAboutPlayer = false;  
    }

    if (_knowsAboutPlayer && !knewAboutPlayer)
    {
      _audioSource.clip = AttackSoundEffects[Random.Range(0, AttackSoundEffects.Count)];
      _audioSource.Play();
    }
  }

  IEnumerator Idle()
  {
    while (true)
    {
      yield return new WaitForSecondsRealtime(Random.Range(5, 10));

      do
      {
        yield return new WaitForEndOfFrame();
        if (_navMeshAgent.pathPending)
          continue;
        Vector3 randDir = Random.insideUnitCircle;
        randDir.z = randDir.y;
        randDir.y = 0;
        _navMeshAgent.destination = _startPos + randDir * Random.Range(0, RoamRange);
      } while (_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete);

      _audioSource.Stop();
      _audioSource.clip = IdleSoundEffects[Random.Range(0, IdleSoundEffects.Count)];
      AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position, .3f);
      //_audioSource.Play();

      float timer = 0;
      while (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance && timer < 25)
      {
        yield return new WaitForEndOfFrame();
        timer += Time.deltaTime;
      }
    }
  }

  //Returns true if you did damage (or hit a dead zombie)
  public bool HitZombie(float damage = 34)
  {
    if (_dead)
      return true;
    if (!canDamage) { 
      AudioSource.PlayClipAtPoint(MissSoundEffect, transform.position);
      return false;
    }
    _spineBoneOffset = 40;
    HitParticles.Play();
    _health -= damage;
    _audioSource.Stop();
    _audioSource.clip = HitSoundEffects[Random.Range(0, HitSoundEffects.Count)];
    _audioSource.Play();
    _omniscienceTimer = 5;
    if (_health <= 0)
    {
      _animator.SetBool("Dead", true);
      _dead = true;
      SmokeEffects.SetActive(false);
      StopAllCoroutines();
      _navMeshAgent.baseOffset = -.8f;
    }

    return true;
  }

  bool checkDistance()
  {
    float damagedistance = 3;
    float realdist = Vector3.Distance(gameObject.transform.position, _player.position);
    if ((realdist <= damagedistance && MatchManager.matchlit) || LightningController.CurrentlyLightninging)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  public void Alert()
  {
    _omniscienceTimer = 5;
  }
}
