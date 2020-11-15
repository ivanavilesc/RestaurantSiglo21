/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package cl.duoc.ded5401.Solemne3.Veterinaria.Recursos;

/**
 * @date : 27-nov-2018
 * @author :Ivan Aviles C.
 * DEJ4501
 */
public class Animal {
    private int idAnimal;
    private String nombreAnimal;
    private String razaAnimal;

    public Animal() {
    }

    public Animal(int idAnimal, String nombreAnimal, String razaAnimal) {
        this.idAnimal = idAnimal;
        this.nombreAnimal = nombreAnimal;
        this.razaAnimal = razaAnimal;
    }

    public int getIdAnimal() {
        return idAnimal;
    }

    public void setIdAnimal(int idAnimal) {
        this.idAnimal = idAnimal;
    }

    public String getNombreAnimal() {
        return nombreAnimal;
    }

    public void setNombreAnimal(String nombreAnimal) {
        this.nombreAnimal = nombreAnimal;
    }

    public String getRazaAnimal() {
        return razaAnimal;
    }

    public void setRazaAnimal(String razaAnimal) {
        this.razaAnimal = razaAnimal;
    }

    @Override
    public String toString() {
        return "Datos de Animal (" + "id =" + idAnimal + ", NOMBRE= " + nombreAnimal + ", RAZA= " + razaAnimal + " }";
    }
    
    
}
