/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos;

/**
 * @date : 11-nov-2018
 * @author :Cristóbal Molina | Ivan Aviles C.
 * DED4501
 */

public class NodoArbol {
    public Ticket objTicket;
    public int altura = -1;
    public NodoArbol izquierda;
    public NodoArbol derecha;
    
    public NodoArbol() {
    }

    public NodoArbol(Ticket objTicket, NodoArbol izquierda, NodoArbol derecha) {
        this.objTicket = objTicket;
        this.izquierda = izquierda;
        this.derecha = derecha;
    }
 
    public NodoArbol(Ticket objTicket) {
        this.objTicket = objTicket;
        altura = 0;
        izquierda = null;
        derecha = null;
    }
    
    public Ticket getObjTicket() {
        return objTicket;
    }

    public void setObjTicket(Ticket objTicket) {
        this.objTicket = objTicket;
    }
    
    
    public int getAltura() {
        return altura;
    }

    public void setAltura(int altura) {
        this.altura = altura;
    }

    public NodoArbol getIzquierda() {
        return izquierda;
    }

    public void setIzquierda(NodoArbol izquierda) {
        this.izquierda = izquierda;
    }

    public NodoArbol getDerecha() {
        return derecha;
    }

    public void setDerecha(NodoArbol derecha) {
        this.derecha = derecha;
    }

    @Override
    public String toString() {
        return "NodoArbol{" + "objTicket=" + objTicket + ", altura=" + altura + ", izquierda=" + izquierda + ", derecha=" + derecha + '}';
    }
 
    

   

    
}
