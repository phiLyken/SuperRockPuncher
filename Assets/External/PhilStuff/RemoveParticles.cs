using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RemoveParticles : MonoBehaviour {

    List<ParticleSystem> systems;

    void Awake()
    {
        systems = new List<ParticleSystem>();
        ParticleSystem _this = GetComponent<ParticleSystem>();
        if (_this != null)
            systems.Add(_this);

        systems.AddRange(GetComponentsInChildren<ParticleSystem>().ToList());

    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
 
            foreach (ParticleSystem sys in systems)
            {

                if (sys.IsAlive())
                    return;
            }

            Destroy(this.gameObject);
            gameObject.SetActive(false);
        }

    }
    
}
