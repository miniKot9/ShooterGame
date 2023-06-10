using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerMainScript : MonoBehaviour, ITakeDamage
{
    [Header("Objects")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private GameObject _healthBar;
    [Header("Parameters")]

    [SerializeField] private float _movmentSpeed = 1;

    [SerializeField] private float _sootDistance = 10;
    [SerializeField] private int _sootDamage = 10;
    [SerializeField] private int _currentAmmo = 10;
    public int CurrentAmmo => _currentAmmo;
    [SerializeField] private float _distanceOfDropUp = 2;

    [SerializeField] private float _maxHealth = 20;
    private float _currentHealth;

    [HideInInspector] public List<GameObject> ItemList;
    public List<GameObject> MonsterList;

    private void Awake()
    {
        GlobalEventControl.ShootAction += ShootInEnemy;
        GlobalEventControl.HorizontalVerticalMove = PlayerMove;
        GlobalEventControl.PlayerAmmo = _currentAmmo;
    }

	private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.transform.localScale = new Vector3(_currentHealth / _maxHealth, 0.13f, 1);
        MonsterList = GameObject.FindGameObjectsWithTag("Monster").ToList(); 
        GlobalEventControl.UpdateInventory();
    }

	private void FixedUpdate()
	{
        for (int i = 0; i < ItemList.Count; i++)
		{
            if (Vector3.Distance(transform.position, ItemList[i].transform.position) < _distanceOfDropUp) {
                gameObject.GetComponent<Inventory>().TryToPutItem(ItemList[i].GetComponent<Item>().ItemName, ItemList[i], ItemList[i].GetComponent<Item>().IsStaketable);
            }
        }
    }

	private void ShootInEnemy ()
    {
        if (_currentAmmo <= 0 || MonsterList.Count == 0) return;
        
        GameObject MonsterTarget = null;
        float SootDistance = _sootDistance;

        for (int i = 0; i < MonsterList.Count; i++)
        {
            if (Vector3.Distance(gameObject.transform.position, MonsterList[i].transform.position) < SootDistance)
            {
                MonsterTarget = MonsterList[i];
                SootDistance = Vector3.Distance(gameObject.transform.position, MonsterList[i].transform.position);
            }
        }
        if (MonsterTarget != null)
        {
            MonsterTarget.GetComponent<ITakeDamage>().OnTakeDamage(_sootDamage);
            _currentAmmo -= 1;
            GlobalEventControl.PlayerAmmo = _currentAmmo;
            if (_animator != null)
                _animator.SetTrigger("Shoot");
        }
    }

    private void PlayerMove(float Horizontal, float Vertical)
    {
        _rigidbody2D.velocity = new Vector2(Horizontal * _movmentSpeed, Vertical * _movmentSpeed) * Time.deltaTime * 100;

        PlayAnimationWalk(Horizontal, Vertical);
    }

    private void PlayAnimationWalk (float Horizontal, float Vertical)
    {
        if (_animator == null) return;

        _animator.SetFloat("Horizontal", Horizontal);
        _animator.SetFloat("Vertical", Vertical);
    }

	public void OnTakeDamage(int Damage)
	{
        _currentHealth -= Damage;
        _healthBar.transform.localScale = new Vector3(_currentHealth / _maxHealth, 0.13f, 1);
        if (_currentHealth <= 0) Destroy(gameObject);
    }
}
