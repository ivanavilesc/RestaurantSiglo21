/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.duoc.ded5401.Solemne3.Veterinaria.Main;

import cl.duoc.ded5401.Solemne3.Veterinaria.Recursos.Animal;

import cl.duoc.ded5401.Solemne3.Veterinaria.Recursos.ArbolBalanceado;
import cl.duoc.ded5401.Solemne3.Veterinaria.Recursos.Lista_Enlazada;
import cl.duoc.ded5401.Solemne3.Veterinaria.Recursos.Nodo;
import cl.duoc.ded5401.Solemne3.Veterinaria.Recursos.PilaE;
import cl.duoc.ded5401.Solemne3.Veterinaria.Util.Leer;

/**
 *
 * @author Cristobal
 */
public class Veterinaria_DED {
    
   public static int menu() {
        System.out.println("MENU DE SISTEMA");
        System.out.println("1. Ingreso de datos");
        System.out.println("2- Ver los datos en la lista enlazada");
        System.out.println("3. Traspaso de datos de Lista a Pila");
        System.out.println("4. Ver los datos en la pila ");
        System.out.println("5. Traspaso de datos a un ArbolB");
        System.out.println("6.  Mostrar los datos ordenados en el arbol (in order) ");
        System.out.println("7. Salir");
        System.out.print("Ingrese opcion : ");
        return Leer.datoInt();
    }

    public static void main(String[] arg) {
        ArbolBalanceado arbolAnimales = null;
        //AnimalArbol auxArbol = new AnimalArbol();
        Lista_Enlazada listaAnimales=null;
        //AnimalLista auxLista = new AnimalLista();
        PilaE pilaAnimales=null;
        //AnimalPila auxPila = new AnimalPila();
        int op;

        do {
            op = menu();
            switch (op) {
                case 1:
                    if(listaAnimales==null && (arbolAnimales!=null || pilaAnimales!=null)){
                        System.out.println("Lista está vacía porque los datos fueron movidos a la PILA o el ARBOL");
                        pausa();
                    }
                    String resp="";
                    do {
                        System.out.println("-----------------------------");
                        System.out.println("INGRESO DATOS DE ANIMAL");
                        int valida = -1;
                        do {
                            System.out.println("Ingrese ID Animal : ");
                            int idAnimal = Leer.datoInt();
                            System.out.println("Ingrese Nombre Animal : ");
                            String nombreAnimal = Leer.dato();
                            System.out.println("Ingrese Raza Animal : ");
                            String razaAnimal = Leer.dato();
                            Animal objAnimal = new Animal(idAnimal, nombreAnimal, razaAnimal);
                            Nodo nodo = new Nodo(objAnimal);
                            boolean primerNodo = false;
                            if (listaAnimales == null) {
                                System.out.println("Lista vacia, creando...");
                                listaAnimales = new Lista_Enlazada();
                                System.out.println("Primer nodo insertado OK, ahora la lista tiene largo 1");
                                listaAnimales.setCabecera(nodo);
                                listaAnimales.setTam(1);                               
                                primerNodo = true;
                            }
                            System.out.println("Ojo en esta validación cuando sea el 2do nodo");
                            int x = 7;
                            if (listaAnimales.getTam() > 1) {
                                System.out.println("Buscando nodo en lista mayor a 1");
                                valida = listaAnimales.buscarNodo(objAnimal);
                                System.out.println(" valor de VALIDA despues de buscar :"+valida);
                                if (valida > -1 && listaAnimales.getTam() > 1) {
                                    System.out.println("Si objeto se encontró y lista es de tamaño 2 hacia arriba");
                                    System.out.println("ID de Animal ya existe, NO se ingresará !"+" valor de VALIDA :"+valida);
                                    pausa();
                                    int z = 9;
                                } else if (valida != -1 && listaAnimales.getTam() == 1) {
                                    System.out.println("Lista de largo 1 y VALIDA encontró nodo, no hace nada"+" valor de VALIDA :"+valida);
                                } else {

                                    if (listaAnimales.getTam() > 1) {
                                        System.out.println("Lista largo mayor que 1 y NODO no fue encontrado, insertando nodo al final"+" valor de VALIDA :"+valida);
                                        listaAnimales.insertaNodoalFinal(objAnimal);
                                    }

                                }
                                
                                
                            } else if (listaAnimales.getTam() == 1)
                            {
                                System.out.println("Buscando nodo en lista de largo 1");
                                valida = listaAnimales.buscarNodo(objAnimal);
                                System.out.println(" valor de VALIDA despues de buscar :"+valida);
                                
                                if (valida < 0) {
                                    System.out.println("Nodo no existe, se insertará al final");
                                    listaAnimales.insertaNodoalFinal(objAnimal);
                                    primerNodo = false;
                                    
                                } else {
                                    System.out.println("Nodo ya existe en lista de largo 1, no se insertará");      
                                    System.out.println(" valor de VALIDA :"+valida);
                                }
                            } 
                            System.out.println(" valor de VALIDA :"+valida);
                            int z = 10;
                        } while (valida != -1 && valida!=0);
                        int z=9;
                        System.out.println("Desea Ingresar otro ANIMAL ? S/N :");
                        resp = Leer.dato();
                    } while (resp.equals("S") || resp.equals("s"));          
                    
                    break;
                case 2:
                    if (listaAnimales == null) {
                        System.out.println("\nLISTA VACIA, esta opcion NO está disponible");
                        pausa();
                    } else {
                        listaAnimales.imprimeLista();
                        pausa();
                        break;
                    }
                case 3:
                    boolean pilaProcesada = false;
                    if (listaAnimales == null && pilaAnimales == null) {
                        System.out.println("Lista Vacía, no hay datos por pasar a la Pila !!!");
                        int t=0;
                    } else if(listaAnimales ==  null || pilaProcesada == true) {
                        System.out.println("Lista Vacía, no hay datos por traspasar a la pila !! ");
                    }else if(pilaAnimales == null){
                        System.out.println("Proceso de Traspaso de Cola a Pila");
                        pilaAnimales = new PilaE();
                        int tam = listaAnimales.getTam();
                        for(int i=0;i<tam;i++){
                                /*Animal objAnimal = new Animal(1,"Animal1", "Raza1");
                                Animal objAnimal2 = new Animal(2,"Animal2", "Raza2");
                                Animal objAnimal3 = new Animal(3,"Animal3", "Raza3");
                                listaAnimales.insertaNodoalFinal(objAnimal);*/
                                int g=5;
                                pilaAnimales.push(listaAnimales.getCabecera().getAnimal());
                                g=6;
                                listaAnimales.setCabecera(listaAnimales.getCabecera().getSiguiente());
                                g=7;
                                listaAnimales.setTam((listaAnimales.getTam())-1);
                                g=8;
                                listaAnimales.disminuirTamano();
                                g=9;         
                                System.out.println("La lista de animales disminuyó en 1 elemento");
                                listaAnimales.imprimeLista();
                                g=10;
                                System.out.println("La Pila aumetó en 1 elemento");
                                pilaAnimales.imprimir();
                                g=11;
                                
                        }
                        System.out.println("PROCESO TERMINADO !");
                        pausa();                                
                        pilaProcesada = true;                       
                        break;                         
                    }
                case 4:
                    if (pilaAnimales == null) {
                        System.out.println("Pila está vacia, no hay datos para mostrar");
                    } else {
                        pilaAnimales.imprimir();
                    }
                    break;
               
                case 5:
                    boolean arbolProcesado = false;
                    if (listaAnimales == null && pilaAnimales == null) {
                        System.out.println("No hay pilas o listas con datos, por lo tanto, no hay información para procesar en el arbol. ");
                        int t=0;
                    } else if(pilaAnimales ==  null || arbolProcesado == true) {
                        System.out.println("Pila Vacía, no hay datos por traspasar al Arbol !! ");
                    }else if(arbolAnimales == null){
                        System.out.println("Proceso de Traspaso de Pila a Arbol");
                        arbolAnimales = new ArbolBalanceado();
                        int tam = pilaAnimales.getTam();
                        for(int i=0;i<tam;i++){
                                /*Animal objAnimal = new Animal(1,"Animal1", "Raza1");
                                Animal objAnimal2 = new Animal(2,"Animal2", "Raza2");
                                Animal objAnimal3 = new Animal(3,"Animal3", "Raza3");
                                listaAnimales.insertaNodoalFinal(objAnimal);*/
                                int g=5;
                                arbolAnimales.insertarNodo(pilaAnimales.getCima().getAnimal());
                                System.out.println("Arbol de animales aumentó en 1 elemento");
                                System.out.println(arbolAnimales.toString());
                                pilaAnimales.pop();                                
                                System.out.println("Pïla de animales disminuyó en 1 elemento");
                                pilaAnimales.imprimir();
                                g=6;
                                
                                
                        }
                        System.out.println("PROCESO TERMINADO !");
                        pausa();                                
                        arbolProcesado = true;                       
                        break;                         
                    }

                case 6:
                    if (arbolAnimales == null) {
                        System.out.println("Arbol vacío, no hay datos para mostrar");
                    } else {
                        arbolAnimales.recorreInOrden();
                    }
                    break;
                case 7:
                    System.out.println("Ha elegido salir... adios !");
                    pausa();
                    break;
                default :
                    System.out.println("Opcion no existe !");
                    pausa();
                    break;                     
            }
        }while (op != 7);
    }
    
    public static void p(String msg){
        System.out.println(msg);
    }
    
    public static void pausa(){
        System.out.println("Presione ENTER para continuar...");
        Leer.dato();
    }
}