/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos;

/**
 *
 * @author cetecom
 */
public class Nodo {
    private Ticket objTicket;
    private Nodo siguiente;

    public Nodo() {
    }

    public Nodo(Ticket objTicket, Nodo siguiente) {
        this.objTicket = objTicket;
        this.siguiente = siguiente;
    }
    
    public Nodo(Ticket objTicket) {
        this.objTicket = objTicket;
        this.siguiente = null;
    }
    
    public Ticket getTicket() {
        return objTicket;
    }

    public void setTicket(Ticket objTicket) {
        this.objTicket = objTicket;
    }

    public Nodo getSiguiente() {
        return siguiente;
    }

    public void setSiguiente(Nodo siguiente) {
        this.siguiente = siguiente;
    }
    
    
}
