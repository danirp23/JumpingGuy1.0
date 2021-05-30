using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlJugador : MonoBehaviour {

	private Animator animator;//componente animador
	public GameObject juego;
	public GameObject generadorEnemigo;

	public AudioClip audioMorir;
	public AudioClip audioJump;
	public AudioClip audioPunto;
	private AudioSource audioJugador;

	public float startV;//saber posicion
	public ParticleSystem dust;//polvo
	// Use this for initialization
	void Start () {
		//se detetcta automatico este componente
		animator = GetComponent<Animator>();
		audioJugador= GetComponent<AudioSource>();
		startV = transform.position.y;//pocicion del suelo
	}
	
	// Update is called once per frame
	void Update () {
		bool enSuelo = transform.position.y == startV;
		bool juegoPlay=juego.GetComponent<GameControles> ().gameState ==  GameState.Playing;
		bool accionJugador = Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0);
		if (enSuelo && juegoPlay && (accionJugador)) {
			cargarEstado ("jugadorSaltar");
			audioJugador.clip = audioJump;
			audioJugador.Play();
		}
	}
	public void cargarEstado(string estado = null){//
		if(estado!=null){
			animator.Play (estado);
		}
	}
	void OnTriggerEnter2D(Collider2D otraParte){//es pa almacenar la otra parte con la que coliciono
		if (otraParte.gameObject.tag == "enemigo") {//eso de tag enemigo se crea en la esquina de arriba :)
			//Debug.Log("me muero");
			cargarEstado ("muerteJugador");
			juego.GetComponent<GameControles> ().gameState = GameState.finalizado;
			generadorEnemigo.SendMessage ("cancelarGenerador", true);
			juego.SendMessage ("mostrarLoser");

			juego.GetComponent<AudioSource> ().Stop ();
			audioJugador.clip = audioMorir;
			audioJugador.Play ();

			juego.SendMessage ("resetTimeScale", 0.5f);
			dustStop ();
		} else if (otraParte.gameObject.tag == "point") {
			juego.SendMessage ("incrementarPuntos");
			audioJugador.clip = audioPunto;
			audioJugador.Play ();
		}
	}
	void reinicioJuego(){//para que en android reinicie despues de morir
		juego.GetComponent<GameControles> ().gameState = GameState.reinicio;
	}
	void dustPlay(){
		dust.Play ();
	}
	void dustStop(){
		dust.Stop ();
	}

}
