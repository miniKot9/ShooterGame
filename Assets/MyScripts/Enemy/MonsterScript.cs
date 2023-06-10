using UnityEngine;

public class MonsterScript : MonoBehaviour, ITakeDamage
{
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject _dropItem;
    [Header("Parameters")]
    [SerializeField] private float _movmentSpeed = 0.5f;
    [SerializeField] private float _distanceOfDetectPlayer = 7;
    [SerializeField] private float _distanceOfHitPlayer = 1;
    [SerializeField] private int _damageFromHit = 5;
    [SerializeField] private float _hit—ooldown = 2;
    private float _hit—ooldownTimer;

    [SerializeField] private float _maxHealth = 20;

    [Header("Stats")]
    [SerializeField] private float _currentHealth;

    private GameObject _playerObject;

	private void Start()
	{
        _playerObject = GameObject.FindGameObjectWithTag("Player");
        _currentHealth = _maxHealth;
        _healthBar.transform.localScale = new Vector3(_currentHealth / _maxHealth, 0.13f, 1);
    }
	void FixedUpdate ()
    {
        if (_hit—ooldownTimer < _hit—ooldown)
            _hit—ooldownTimer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, _playerObject.transform.position);
        if (distance < _distanceOfDetectPlayer)
		{
            gameObject.GetComponent<Rigidbody2D>().velocity = (_playerObject.transform.position- transform.position).normalized *Time.deltaTime * 100 * _movmentSpeed;
            if (distance < _distanceOfHitPlayer && _hit—ooldownTimer >= _hit—ooldown)
			{
                _playerObject.GetComponent<ITakeDamage>().OnTakeDamage(_damageFromHit);
                _hit—ooldownTimer = 0;

            }
        }
    }

    public void OnTakeDamage(int Damage)
    {
        _currentHealth -= Damage;
        if (_currentHealth <= 0)
        {
            Instantiate(_dropItem,transform.position,transform.rotation);
            _playerObject.GetComponent<PlayerMainScript>().MonsterList.Remove(gameObject);
            Destroy(gameObject);
        }

        _healthBar.transform.localScale = new Vector3(_currentHealth / _maxHealth, 0.13f, 1);
    }
}
