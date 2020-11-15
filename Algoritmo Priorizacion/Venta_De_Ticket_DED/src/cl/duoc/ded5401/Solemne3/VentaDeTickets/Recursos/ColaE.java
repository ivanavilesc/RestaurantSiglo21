/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos;

/**
 *
 * @author Ivan Aviles C.
 */
public class ColaE {

    private Nodo nodoPrimero;
    private Nodo nodoFinal;
    private int size;

    public ColaE() {

        nodoFinal = null;
        size = 0;

    }

    public void encolar(Ticket objTicket) {

        Nodo nodoNuevo = new Nodo(objTicket);
        if (nodoPrimero == null) {
            nodoPrimero = nodoNuevo;
            nodoFinal = nodoNuevo;
        } else {

            nodoFinal.setSiguiente(nodoNuevo);

            nodoFinal = nodoNuevo;

        }

        size++;

    }

    ; // inserts an object onto the queue



  public Ticket desencolar() {

        if (nodoPrimero == null) {
            return null;
        }
        ;

        Ticket objTicket = nodoPrimero.getTicket();

        nodoPrimero = nodoPrimero.getSiguiente();

        size--;

        return objTicket;

    } // gets the object from the queue

    public boolean isEmpty() {

        return (size == 0);

    }

    public int size() {

        return size;

    }

    public Ticket nodoPrimero() {

        if (nodoPrimero == null) {
            return null;
        } else {
            return nodoPrimero.getTicket();
        }

    }
    
    public void imprimir(){
        Nodo temp = this.nodoPrimero;
        if(temp!=null){           
            for(int i=0;i<this.size();i++){
                System.out.println("Ticket : "+temp.getTicket().toString());
                temp=temp.getSiguiente();
            }
        
        }else{
            System.out.println("COLA VACIA");
        }
        
        
    }

        } // class LinkedQueue
