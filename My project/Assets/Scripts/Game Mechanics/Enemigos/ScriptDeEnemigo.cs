using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class ScriptDeEnemigo : MonoBehaviour
{
	
	//------ GAME OBJECTS USADOS
	public Transform objetivoAPerseguir;
	public GameObject objetivoAtacado;
	public GameObject visionIzquierda;
	public GameObject visionDerecha;
	public Rigidbody2D rigidb;
	//------- MOVIMIENTO
	public float distanciaMirar = 20.0F;
	public float distanciaAtacar = 5.0F;
	public float velocidad = 2.0F;
	public float suavidad = 6.0F;
	//----- ESTADÍSTICAS
	public float dano = 10F;
	public float danoPneuma = +5F;
	//Daño que produce el enemigo
	public float cooldown = 3.0F;
	//Tiempo que tarda en hacer de nuevo el daño
	public float vida = 45.0F;
	//Vida total del enemigo
	//-------------------------------------
	public AudioClip SonidoMonstruo;
	public AudioClip SonidoMuerte;
	public AudioClip SonidoGolpeado;
	public AudioClip SonidoGolpe;
	public AudioClip GolpeSeco;
	//-------------------------------------
	private float DPS;

	private bool ataca = false;
	private bool golpeBool = true;
	private bool muerte = true;
    private bool hold = false;

	private AudioSource aS;
	float distancia;
	private BarraDeVida bdv;
	// Use this for initialization
	void Awake ()
	{
		aS = (AudioSource)gameObject.GetComponent<AudioSource> ();
		DPS = 0;

		if (gameObject.name == "Boss")
		{
			if (PlayerPrefs.GetInt("canDash") == 1) {
				gameObject.SetActive(false);
			}
		}
	}

	void Start ()
	{
		GetComponent<AudioSource> ().PlayOneShot (SonidoMonstruo, 1.0F);
		gameObject.GetComponent<BarraDeVida> ().totalVida = vida;
		gameObject.GetComponent<BarraDeVida> ().vidaActual = vida;
		bdv = gameObject.GetComponent<BarraDeVida> ();
		rigidb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        
		distancia = Vector3.Distance (objetivoAPerseguir.position, transform.position);
		vida = bdv.vidaActual;

		if (ataca && !hold) {
			atacar ();
		}
		
		if (vida <= 0) {
			if (muerte) {
				muerte = false;
                hold = true;
				StartCoroutine (Muerte ());
			}
		}
		
	}

	void OnTriggerEnter2D (Collider2D col){
		if(col.tag == "Player"){
            if(!hold)
			    ataca = true;
		}
	}

	void OnTriggerExit2D (Collider2D col){
		if(col.tag == "Player"){
			ataca = false;
		}
	}


	//Cuando la vida del enemigo llega a 0, muere
	IEnumerator Muerte ()
	{
				
		gameObject.GetComponent<ParticleSystem> ().Play ();
		GetComponent<AudioSource> ().PlayOneShot (SonidoMuerte, 1.0F);
        
        yield return new WaitForSeconds (gameObject.GetComponent<ParticleSystem> ().main.duration + 0.2F);
		if (gameObject.name == "Boss") {
			PlayerPrefs.SetInt("canDash", 1);
		}
        Destroy(gameObject);

    }

		
	//El enemigo se lanza al ataque
	void atacar ()
	{
		ataca = true;
				
  
		if (visionDerecha.GetComponent<Vision>().GetSide()) {
			gameObject.GetComponent<Animator> ().SetBool ("Left", false);
			transform.Translate (Vector3.right * velocidad * Time.deltaTime);
			Debug.Log("paladerecha");
		} else if (visionIzquierda.GetComponent<Vision>().GetSide()) {
			gameObject.GetComponent<Animator> ().SetBool ("Left", true);
			transform.Translate (Vector3.left * velocidad * Time.deltaTime);
			Debug.Log("palaizquierda");
		}
		else{
			ataca = false;
		}
		golpe ();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.collider.tag == "Player") {
            if (!hold) {
                if (Time.time > DPS) {
                    gameObject.GetComponent<Animator>().SetBool("Attack", true);
                    DPS = Time.time + cooldown;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().hurt(dano);
                    if (golpeBool) {
                        golpeBool = false;
                        StartCoroutine("TimelineDano");
                    }
                    GetComponent<AudioSource>().PlayOneShot(SonidoGolpe);
                    GetComponent<AudioSource>().PlayOneShot(GolpeSeco, 1.0F);
                }
            }
		}
	}

	void golpe ()
	{
		
		if (distancia < 1f) {
						
		} else {
			gameObject.GetComponent<Animator> ().SetBool ("Attack", false);
		}

	}

	public void golpeado ()
	{
		GetComponent<AudioSource> ().PlayOneShot (SonidoGolpeado, 1.0F);

		Vector2 newVelocity;
		newVelocity.x = 0;

		if (gameObject.GetComponent<Animator>().GetBool("Left"))
		{
			newVelocity.x = 3;
		}
		else if (!gameObject.GetComponent<Animator>().GetBool("Left")) {
			newVelocity.x = -3;
		}


		newVelocity.y = rigidb.velocity.y;

		rigidb.velocity = newVelocity;

		GetComponent<Rigidbody2D>().velocity = newVelocity;
	}



	IEnumerator TimelineDano ()
	{
		yield return new WaitForSeconds (cooldown);
		golpeBool = true;
		gameObject.GetComponent<Animator> ().SetBool ("Attack", false);
	}
	
}
