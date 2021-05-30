using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlEnemigo : MonoBehaviour {

	public float velocidad=2f;
	private Rigidbody2D rb2d;//esto de rigid hace que el muñequito se mucva y no se quede como momia
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.velocity = Vector2.left * velocidad;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D otraParte){//es pa almacenar la otra parte con la que coliciono
		if(otraParte.gameObject.tag=="destruido"){//eso de tag destruido se crea en la esquina de arriba :)
			Destroy (gameObject);//Para que se destruya el enemigo cuando sale de la pantalla
		}
	}
}
