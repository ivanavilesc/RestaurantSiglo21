/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.duoc.ded5401.Solemne3.Veterinaria.Recursos;

/**
 *
 * @author iaviles
 */
public class ColaE {

    class Nodo {

        Object elem;
        Nodo siguiente;

        public Nodo(Object o) {

            elem = o;
            siguiente = null;

        }

    }

    Nodo primero;
    Nodo fin;
    int size;
    
    public ColaE() {

        fin = null;
        size = 0;

    }

    public void encolar(Object o) {

        Nodo nodoNuevo = new Nodo(o);
        if (primero == null) {
            primero = nodoNuevo;
            fin = nodoNuevo;
        } else {

            fin.siguiente = nodoNuevo;

            fin = nodoNuevo;

        }

        size++;

    }

    ; // inserts an object onto the queue



  public Object desencolar() {

        if (primero == null) {
            return null;
        }
        ;

        Object o = primero.elem;

        primero = primero.siguiente;

        size--;

        return o;

    } // gets the object from the queue

    public boolean isEmpty() {

        return (size == 0);

    }

    public int size() {

        return size;

    }

    public Object primero() {

        if (primero == null) {
            return null;
        } else {
            return primero.elem;
        }

    }

} // class LinkedQueue
