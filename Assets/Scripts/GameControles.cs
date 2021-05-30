using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState{Idle,Playing,finalizado,reinicio};
public class GameControles : MonoBehaviour {
	
	[Range (0f,0.20f)]//pa ponerle un rango
	public float parallaxSpeed = 0.02f;//velocidad en que se mueve
	public RawImage background;//imagen de fondo de las montañitas
	public RawImage platform;//imagen del piso, pasto 
	public GameObject UiIdle;//objeto del titulo
	public GameObject UiPuntos;//objeto de los puntos
	public GameObject UiLoser;//objeto de perdedor
	public GameObject Jugador;//es el jugardor pa pasar al control jugador
	public Text textoPuntos;
	public Text recortText;

	public GameState gameState = GameState.Idle;
	public GameObject generadorEnemigo;
	private AudioSource musicaJuego;
	private int puntos=0;

	public float tiempoEscala=6f;//cada cuanto se escala el juego
	public float scaleInc=.25f;//cuanto se va a incrementar
	// Use this for initialization
	void Start () {
		musicaJuego= GetComponent<AudioSource>();
		recortText.text = "LO MAS HELOW: " + GetMaxScores().ToString ();  
	}
	
	// Update is called once per frame
	void Update () {
		bool usuarioPress = Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0);
		//pa saber cuando empieza el juego 
		if (gameState == GameState.Idle && usuarioPress) {
			gameState = GameState.Playing;//esto es pa que empiece el juego
			UiIdle.SetActive(false);
			UiPuntos.SetActive(true);
			Jugador.SendMessage ("cargarEstado","jugadorCorre");//el primero es el metodo y el segundo es el parametro String
			generadorEnemigo.SendMessage("comenzarGenerador");
			musicaJuego.Play();
			Jugador.SendMessage ("dustPlay");
			InvokeRepeating ("GameTimeScale", tiempoEscala, tiempoEscala);//pa que se repita cada 6 segundos
		} else //si el gamestate fuera ya playing pues se ejecuta el efecto paralax
			if(gameState == GameState.Playing){
				Parallax ();
			}
		else //reinicio del juego
				if(gameState == GameState.reinicio){
					if(usuarioPress){
						RestartGame ();
					}
			}
	}
	void Parallax(){//El efecto parallax es que el fondo y el piso se muevan 
		//pero que el piso se mueva mas rapido que el fondo
		float finalSpeed = parallaxSpeed * Time.deltaTime;
		background.uvRect = new Rect (background.uvRect.x + finalSpeed,0f,1f,1f);
		platform.uvRect = new Rect (platform.uvRect.x + finalSpeed*4,0f,1f,1f);
	}
	public void RestartGame(){
		resetTimeScale ();
		SceneManager.LoadScene ("Jumping Guy");
	}
	void GameTimeScale(){
		Time.timeScale += scaleInc;//le sumao.25 a la escala de tiempo
		Debug.Log("Ritmo incrementado: "+Time.timeScale.ToString());
		int cont = 1;
		if(Time.timeScale > 2.75 && cont==1){
			tiempoEscala = 8f;
			cont = cont + 1;
			Debug.Log("ESCALA CAMBIADA: "+tiempoEscala);
		}
		

	}
	public void resetTimeScale(float newTimeScale=1f){
		CancelInvoke ("GameTimeScale");
		Time.timeScale = newTimeScale;
		Debug.Log ("Ritmo de juego restablecido: "+Time.timeScale.ToString());
	}
	public void incrementarPuntos(){
		textoPuntos.text = (++puntos).ToString();
		if(puntos > GetMaxScores()){
			recortText.text = "LO MAS HELOW: " + puntos.ToString ();
			SavesScores (puntos);
		}
	}
	public int GetMaxScores(){
		return PlayerPrefs.GetInt ("Max Points",0);
	}
	public void SavesScores(int currentPoints){
		PlayerPrefs.SetInt ("Max Points",currentPoints);
		
	}
	public void mostrarLoser(){
		UiLoser.SetActive(true);
	}

}
