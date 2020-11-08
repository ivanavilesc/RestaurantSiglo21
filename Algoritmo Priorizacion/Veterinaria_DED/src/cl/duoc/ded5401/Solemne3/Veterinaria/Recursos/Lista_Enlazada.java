
package cl.duoc.ded5401.Solemne3.Veterinaria.Recursos;

import cl.duoc.ded5401.Solemne3.Veterinaria.Util.Leer;

    /*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Ivan Aviles C.
 * Fecha    : 2018/08/26
 * Clase Lista_Enlazada (Controller), tarea busqueda, lista e inserción de nodos.
 * Seccion  : DED4501-002V
 */
public class Lista_Enlazada {
    private int tam;
    private Nodo cabecera;

    public Lista_Enlazada(){
        this.cabecera=null;
        this.tam=0;
    }
    
    public int getTam() {
        return tam;
    }

    public void setTam(int tam) {
        this.tam = tam;
    }
    
    public void disminuirTamano() {
        this.tam = this.tam - 1;        
    }

    public Nodo getCabecera() {
        return cabecera;
    }

    public void setCabecera(Nodo cabecera) {
        this.cabecera = cabecera;
        this.tam++;
    }
    
    public Lista_Enlazada(int tam, Nodo cabecera) {
        this.tam = tam;
        this.cabecera = cabecera;
    }

    
    
    public void imprimeLista(){
        Nodo temp = this.getCabecera();
        if(temp!=null){           
            for(int i=0;i<this.tam;i++){
                System.out.println("Animal : "+temp.getAnimal().toString());
                temp=temp.getSiguiente();
            }
        
        }else{
            System.out.println("LISTA VACIA");
        }
        
        
    }
    
    public void insertaNodoalInicio(Animal objAnimal){
        if(this.cabecera==null){
            Nodo nuevo = new Nodo(objAnimal);
            this.cabecera = nuevo;
            this.tam++;
        }else{
            Nodo temp = this.cabecera;            
            temp.setSiguiente(this.cabecera.getSiguiente());
            Nodo nuevo = new Nodo(objAnimal,temp);            
            this.cabecera = nuevo;
            this.tam++;
        }
    }
    
    public Nodo devuelveNodoFinal(){
        Nodo nodoTemp;
        if(this.getTam()!=0){
            nodoTemp = this.cabecera;}
        else{
            return null;
        }
        
        for(int i=0;i<this.getTam();i++){
            if(nodoTemp.getSiguiente()!=null){
                nodoTemp = nodoTemp.getSiguiente();
            }                
        }
        return nodoTemp;
    }
    
    public void insertaNodoalFinal(Animal objAnimal){
        Nodo ultimoActual = this.devuelveNodoFinal();
        Nodo nuevoUltimoNodo = new Nodo(objAnimal);
        ultimoActual.setSiguiente(nuevoUltimoNodo);
        this.tam++;
        System.out.println("nuevo ultimo nodo es : "+nuevoUltimoNodo.getAnimal().toString());      
                
    }
    
    public boolean insertaNodoPosicionDada(int pos, Animal objAnimal){
        
        //Si la posición a insertar es 1 , puede insertar al inicio, usa el metodo construido
        if(pos==1){
            insertaNodoalInicio(objAnimal);
            return true;
        }
        //Si la posición a insertar es 1 mas largo que el tamaño, puede insertar al final, usa el metodo construido
        if(pos==this.tam + 1){
            insertaNodoalFinal(objAnimal);
            return true;
        }
        
        Nodo nuevoNodo = new Nodo(objAnimal);
        Nodo temp = this.cabecera;
        Nodo anterior = null;
        Nodo posterior = null;
        
        //Controla que la posición para reemplazar nodo, nunca sea mayor que el largo de la lista.
        if(pos <= this.tam){
            
            for(int i=0 ; i<pos ; i++){
                
                if(i+2 == pos ){
                    //En el ciclo, esta logica se cumple, cuando el nodo temporal está justo antes de la posición que debe insertar.                    
                    anterior = temp;
                    posterior = temp.getSiguiente();
                    anterior.setSiguiente(nuevoNodo);
                    nuevoNodo.setSiguiente(posterior);
                    this.tam++;
                    //System.out.println("Nodo inserto satisfactoriamente");
                }else{
                    //Si no se cumple condicion anterior, el nodo temporal, toma la forma del siguiente y la iteracion avanza un ciclo.
                    temp = temp.getSiguiente();
                }
            }
            
        }    
        else{
            System.out.println("Posicion dada es mayor al largo actual de la Lista");
            return false;
        }
        return true;
    }
    
    public int buscarNodo(Animal objAnimal){
        Nodo temp = this.cabecera;
        for(int i=0; i<this.tam; i++){
            if(temp.getAnimal().getIdAnimal() == objAnimal.getIdAnimal()){
                return i;
            }else{
                temp = temp.getSiguiente();
            }
        }
        return -1;
    }
    
    public boolean insertaPosicionAntes(Animal objAnimal){
        //retorna valor de la posicion donde se encontró el valor, dentro de la lista
        int pos = buscarNodo(objAnimal);
        //Si posición retorna distinto de -1, es posición valida.
        if(pos>=0){
            System.out.println("Ingrese ID de Animal : "); //un animal !!!!
            int idAnimal = Leer.datoInt();
            System.out.println("Ingrese Nombre de Animal : "); //un animal !!!!
            String nombreAnimal = Leer.dato();
            System.out.println("Ingrese Raza de Animal : "); //un animal !!!!
            String razaAnimal = Leer.dato();
            Animal nuevoAnimal;
            nuevoAnimal = new Animal(idAnimal, nombreAnimal, razaAnimal);
            insertaNodoPosicionDada((pos+1), nuevoAnimal);            
        }else{
            return false;
        }        
        return true;
    }
    
    public boolean insertaPosicionDespues(Animal objAnimal){
        //retorna valor de la posicion donde se encontró el valor, dentro de la lista
        int pos = buscarNodo(objAnimal);
        //Si posición retorna distinto de -1, es posición valida.
        if(pos>=0){
            //Pide valor a insertar
            System.out.println("Ingrese ID de Animal : "); //un animal !!!!
            int idAnimal = Leer.datoInt();
            System.out.println("Ingrese Nombre de Animal : "); //un animal !!!!
            String nombreAnimal = Leer.dato();
            System.out.println("Ingrese Raza de Animal : "); //un animal !!!!
            String razaAnimal = Leer.dato();
            Animal nuevoAnimal;
            nuevoAnimal = new Animal(idAnimal, nombreAnimal, razaAnimal);
            //Utiliza metodo ya construido para insertar en posicion dada, aprovechando que conoce posición.
            insertaNodoPosicionDada((pos+2), nuevoAnimal);            
        }else{
            return false;
        }        
        return true;
    }
    
}    

