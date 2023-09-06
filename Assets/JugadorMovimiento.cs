using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimiento : MonoBehaviour {
    public String nombre;
    public float velocidad;
    public float rotacion;
    public Rigidbody fisicas;
    public float fuerzaSalto;
    public Transform camara;
    private float camaraRotacionX = 0.0f;
    public bool canjump = true;
    void Start() {
        fisicas = GetComponent<Rigidbody>();
        camara = transform.Find("Main Camera");
        if (camara == null)
        {
            Debug.LogError("No se encontró una cámara");
        }
    }
    void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movimiento = new Vector3(horizontal, 0, vertical).normalized *
                                (velocidad * Time.deltaTime);

        transform.Translate(movimiento);

        if (Input.GetKey(KeyCode.Space) && canjump) {
            var salto = new Vector3(0, fuerzaSalto, 0);
            fisicas.AddForce(salto);
            canjump = false;
        }
        var mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, mouseX * rotacion * Time.deltaTime, 0));
        var mouseY = Input.GetAxis("Mouse Y");
        camaraRotacionX -= mouseY * rotacion * Time.deltaTime;
        camaraRotacionX = Mathf.Clamp(camaraRotacionX, -90.0f, 90.0f);
        camara.localRotation = Quaternion.Euler(camaraRotacionX, 0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terreno")
        {
            canjump = true;
        }
    }
}