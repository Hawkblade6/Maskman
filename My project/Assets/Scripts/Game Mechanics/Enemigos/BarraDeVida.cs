using UnityEngine;
using System.Collections;

public class BarraDeVida : MonoBehaviour {

	public float tamañoBarra = 100;
	public float totalVida;
	public float vidaActual;
	private float sizeBarra;

	public TextureWrapMode TipoDeRepeticion;
	public Texture insideTexture;
	public Texture frameTexture;
	public float positionX;
	public float positionY;
	public float barWidth;

	

	// Use this for initialization
	void Start () {
		sizeBarra = tamañoBarra; 
		insideTexture.wrapMode = TipoDeRepeticion;
	}
	
	// Update is called once per frame
	void Update () {
		modificacionVida(0);
		if(vidaActual < 0)
			vidaActual = 0;
	}
	
	void OnGUI(){

		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);

		GUI.DrawTexture(new Rect(targetPos.x - positionX, Screen.height - targetPos.y - positionY, tamañoBarra, barWidth+1), frameTexture, ScaleMode.StretchToFill, true);
		GUI.DrawTexture(new Rect(targetPos.x-positionX, Screen.height - targetPos.y-positionY, sizeBarra, barWidth), insideTexture, ScaleMode.StretchToFill, true);

	}
	
	public void modificacionVida(float cantidad){
		vidaActual += cantidad;
		//Si la vida actual supera el maximo, la actual se iguala al maximo
		if(vidaActual > totalVida) vidaActual = totalVida;
		//La vida total nunca sera menor que 1
		if(totalVida < 1) totalVida = 1;
		//Si la vida actual llega a 0, game over
		if(vidaActual <= 0 && gameObject.tag.Equals("Player"));
		sizeBarra = tamañoBarra * (vidaActual / (float)totalVida);
	}


}
