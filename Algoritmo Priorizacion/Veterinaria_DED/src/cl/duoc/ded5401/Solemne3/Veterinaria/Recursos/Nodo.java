/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.duoc.ded5401.Solemne3.Veterinaria.Recursos;

/**
 *
 * @author cetecom
 */
public class Nodo {
    private Animal objAnimal;
    private Nodo siguiente;

    public Nodo() {
    }

    public Nodo(Animal objAnimal, Nodo siguiente) {
        this.objAnimal = objAnimal;
        this.siguiente = siguiente;
    }
    
    public Nodo(Animal objAnimal) {
        this.objAnimal = objAnimal;
        this.siguiente = null;
    }
    
    public Animal getAnimal() {
        return objAnimal;
    }

    public void setAnimal(Animal objAnimal) {
        this.objAnimal = objAnimal;
    }

    public Nodo getSiguiente() {
        return siguiente;
    }

    public void setSiguiente(Nodo siguiente) {
        this.siguiente = siguiente;
    }
    
    
}
