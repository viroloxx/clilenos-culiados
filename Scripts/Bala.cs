using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    //Variables generales del objeto bala
    public float speed = 300.0f;
    public float maxTiempo = 10.0f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
//fisicas de la balas al ser disparadas de diferentes angulos
public void Project(Vector2 direction)
{
    _rigidbody.AddForce(direction *this.speed);

 
}
//Eliminar las balas al momento de hacer colision con cualquier objeto o los bordes
private void OnCollisionEnter2D(Collision2D collision){
    Destroy(this.gameObject);
}


}
