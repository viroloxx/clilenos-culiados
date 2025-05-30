using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    //Variables generales del Jugador
    public Bala balaPrefab;
    public float _thrusSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;

//Llama a los componentes que estan en unity
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
// Movimiento del jugador y disparos
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            _turnDirection = 1f;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            _turnDirection = -1f;
        } else {
            _turnDirection = 0f;
        }
        if (Input.GetKey(KeyCode.Space)){
            Disparar();
        }
    }


    private void FixedUpdate()
    {
        if (_thrusting){
            _rigidbody.AddForce(this.transform.up *this._thrusSpeed);
        }

        if (_turnDirection != 0.0f){

            _rigidbody.AddTorque(_turnDirection * this.turnSpeed * Time.fixedDeltaTime);
        }
    }
    //proyeccion de las balas disparadas
    private void Disparar()
    {
        Bala bala = Instantiate(this.balaPrefab, this.transform.position, this.transform.rotation);
        bala.Project(this.transform.up);
    }
}
