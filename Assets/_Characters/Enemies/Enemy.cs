using UnityEngine;

using RPG.Core; // TODO: rewire


namespace RPG.Characters
{
	public class Enemy : MonoBehaviour, IDamageable
	{

		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] float attackRadious = 5f;

		[SerializeField] float chaceRadious = 5f;
		[SerializeField] float damagePerShot = 5f;
		[SerializeField] float firingPeriodInS = 1f;
		[SerializeField] float firingPeriodVariation = 0.1f;

		[SerializeField] GameObject projectileToUse;
		[SerializeField] GameObject projectileSocket;
		[SerializeField] Vector3 aimOffset = new Vector3 (0, 1f, 0);


		Player player = null;

		float currentHealthPoints;
		
		public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; }	}

		bool isAttacking = false;

		void Start()
		{
			currentHealthPoints = maxHealthPoints;
			player = FindObjectOfType <Player>();		// Find the player and fetchit
		}

		void Update()
		{
			if (player.healthAsPercentage <= Mathf.Epsilon)
			{
				StopAllCoroutines();
				Destroy(this);
			}

			float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
			
			// ATTACAD
			if (distanceToPlayer <= attackRadious && !isAttacking)
			{
				isAttacking = true;
				// Repro el attack anim
				float randomisedDelay = Random.Range(firingPeriodInS - firingPeriodVariation, firingPeriodInS + firingPeriodVariation);
				
				InvokeRepeating ("FireProjectile", 0f, randomisedDelay); // TODO: switch to coroutines
				// SpawnProjectile();
			}

			if (distanceToPlayer > attackRadious)
			{
				isAttacking = false;
				CancelInvoke();
			}


			// CHACE
			if (distanceToPlayer <= chaceRadious)
			{
				// Let's talk to the AICharacter control
				//aiCharacterControlRef.SetTarget (player.transform);
			}
			else
			{
				//aiCharacterControlRef.SetTarget (transform);
			}
		}


		// TODO: separate character out character firing logic
		void FireProjectile()
		{
			GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
			Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
			projectileComponent.SetDamage(damagePerShot);
			projectileComponent.SetShooter(gameObject);

			Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
			float projectileSpeed = projectileComponent.GetDefaultLaunchSpeed();
			// Antes teníamos projectileComponent.projectileSpeed... o sea que buscamos una instancia de la clase projectiles
			//	Ahora vamos a crear directamente un método dentro de esa clase.
			newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
		}


		public void TakeDamage(float damage)
		{
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
			
			if (currentHealthPoints <= 0 )
			{ Destroy (gameObject); }
		}


		void OnDrawGizmos()
		{
			// ATTACKRADIOUS GIZMOS RED
			Gizmos.color = new Color (255f, 0f, 0, .5f); 
			Gizmos.DrawWireSphere (transform.position, attackRadious);

			// CHACERAIOUS GIZMOS BLUE
			Gizmos.color = new Color (0f, 0f, 255f, .5f); 
			Gizmos.DrawWireSphere (transform.position, chaceRadious);
		}	
	}
}