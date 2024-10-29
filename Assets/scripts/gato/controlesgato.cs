using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlesgato : MonoBehaviour
{
    public float velocidad;
    public float fuerzasalto;
    public float saltosMaximos;
    public LayerMask capasuelo;
    private new Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private float saltosRestantes;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
    }
    // Update is called once per frame
    void Update()
    {
        procesarMovimiento();
        ProcesarSalto();
    }

    void procesarMovimiento()
    {
        //logica movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        gestionarOrientacion(inputMovimiento);
    }
    void gestionarOrientacion(float inputMovimiento)
    {
        //condicion
        if((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale =  new Vector2(-transform.localScale.x, transform.localScale.y);

        }
    }
    bool estaensuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capasuelo); 
        return raycastHit.collider != null;
    }
    void ProcesarSalto()
    {
        if(estaensuelo())
        {
            saltosRestantes = saltosMaximos;
        }
        if(Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes --;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * fuerzasalto, ForceMode2D.Impulse);
        }
    }
}
