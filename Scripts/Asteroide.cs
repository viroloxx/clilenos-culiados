using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroide : MonoBehaviour
{

    public Sprite[] asteroidSprites; 
    
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float maxLife = 30.0f;
    private bool _isSplitPiece = false;
    public float speed = 50.0f;
    
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    //Llama a los componentes que estan en unity
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (asteroidSprites != null && asteroidSprites.Length > 0)
        {
            _spriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
        }
        else
        {
            Debug.LogWarning("Asteroide: asteroidSprites array is empty or null. No sprite will be assigned.", this);
        }

        // aplica la rotacion inicial del asteroide
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        
        // escala del asteroide
        this.transform.localScale = Vector3.one * this.size;

        // establece la masa dependiendo la escala
        _rigidbody.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction *this.speed);

        Destroy(this.gameObject, this.maxLife);
    }
// Elimina los asteroides al momento de ser tocados por las balas, y no la nave ni los bordes
    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag == "Bala")
        {
            if (!this._isSplitPiece && ((this.size * 0.5f) >= this.minSize))
            {
                CreateSplit();
                CreateSplit();
            }

            Destroy(this.gameObject);
        }

    }
    //el asteroide se dividira al momento de ser disparado
    private void CreateSplit()

    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroide half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half._isSplitPiece = true;

        //esto le da una nueva trajectoria al asteroide dividido
        half.SetTrajectory(Random.insideUnitCircle.normalized);

    }

}