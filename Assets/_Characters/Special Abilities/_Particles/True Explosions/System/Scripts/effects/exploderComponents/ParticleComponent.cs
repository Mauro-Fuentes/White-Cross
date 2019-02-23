using UnityEngine;
using UnityEditor;
using System.Collections;

public class ParticleComponent : ExploderComponent {
	public GameObject explosionEffectsContainer;
	public float scale = 1;
	public float playbackSpeed = 1;
	public override void onExplosionStarted(Exploder exploder)
	{
		GameObject GameObjectContainer = (GameObject)GameObject.Instantiate(explosionEffectsContainer, transform.position, Quaternion.identity);
		ParticleSystem[] particleSystemsArray = GameObjectContainer.GetComponentsInChildren<ParticleSystem>();
		

		foreach (ParticleSystem OneParticleSystem in particleSystemsArray) {
			var oneInstanceOfParticleSystem = OneParticleSystem.main; 

			oneInstanceOfParticleSystem.startSpeed = scale;
			oneInstanceOfParticleSystem.startSize = scale;
			OneParticleSystem.transform.localScale *= scale;
			oneInstanceOfParticleSystem.simulationSpeed = playbackSpeed;
		}
	}

}
