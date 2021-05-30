using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlGeneradorRnemigo : MonoBehaviour {
	//EL prefab es una planotilla
	public GameObject prefabEnemigo; 
	public float generadorTiempo = 1.75f;//tienpo de generar enemigo
	public int a=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void crearEnemigo(){
		Instantiate (prefabEnemigo,transform.position,Quaternion.identity);
	}
	public void comenzarGenerador(){
		InvokeRepeating ("crearEnemigo",0f,generadorTiempo);//pa que se ejecute cada sierto tiempo
	}
	public void cancelarGenerador(bool clean=false){
		CancelInvoke ("crearEnemigo");
		if(clean){
			Object[] todosEnemigos = GameObject.FindGameObjectsWithTag ("enemigo");
			foreach(GameObject enemigo in todosEnemigos){
				Destroy (enemigo);
			}
		}

	}
}
