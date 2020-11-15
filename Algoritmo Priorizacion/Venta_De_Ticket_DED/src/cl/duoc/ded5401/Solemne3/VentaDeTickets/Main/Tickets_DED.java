/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.duoc.ded5401.Solemne3.VentaDeTickets.Main;

import cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos.Ticket;
import cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos.ArbolBalanceado;
import cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos.ColaE;
import cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos.Lista_Enlazada;
import cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos.Nodo;
import cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos.PilaE;
import cl.duoc.ded5401.Solemne3.VentaDeTickets.Util.Leer;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 *
 * @author Cristobal
 */
public class Tickets_DED {
    
   public static int menu() {
        System.out.println("MENU DE SISTEMA");
        System.out.println("1. Ingreso de datos");
        System.out.println("2- Ver los datos en la lista enlazada");
        System.out.println("3. Traspaso de datos de Lista a Pila");
        System.out.println("4. Ver los datos en la pila ");
        System.out.println("5. Traspaso de datos de Pila a Cola");
        System.out.println("6. Ver los datos en la cola ");
        System.out.println("7. Traspaso de datos a un ArbolB");
        System.out.println("8.  Mostrar los datos ordenados en el arbol (in order) ");
        System.out.println("9. Salir");
        System.out.print("Ingrese opcion : ");
        return Leer.datoInt();
    }

    public static void main(String[] arg) {
        ArbolBalanceado arbolTickets = null;
        //TicketArbol auxArbol = new TicketArbol();
        Lista_Enlazada listaTickets=null;
        //TicketLista auxLista = new TicketLista();
        PilaE pilaTickets=null;
        //TicketPila auxPila = new TicketPila();
        ColaE  colaTickets=null;
        int op;

        do {
            op = menu();
            switch (op) {
                case 1:
                    if(listaTickets==null && (arbolTickets!=null || pilaTickets!=null)){
                        System.out.println("Lista está vacía porque los datos fueron movidos a la PILA o el ARBOL");
                        pausa();
                    }
                    String resp="";
                    do {
                        System.out.println("-----------------------------");
                        System.out.println("INGRESO DATOS DE Ticket");
                        int valida = -1;
                        do {
                            System.out.println("Ingrese Nro Ticket : ");
                            int nroTicket = Leer.datoInt();
                            Date fechaTicket=null;
                            System.out.println("Ingrese Fecha de Ticket dd/MM/yyyy: ");
                            String strFechaTicket = Leer.dato();
                            String pattern = "dd/MM/yyyy";
                            try{
                                SimpleDateFormat sdf = new SimpleDateFormat(pattern);  
                                fechaTicket = sdf.parse(strFechaTicket);
                            }catch(Exception ex){
                                System.out.println("Error : "+ex);
                            }
                            
                            System.out.println("Ingrese Nro Asiento : ");
                            int nroAsiento = Leer.datoInt();                           
                            
                            Ticket objTicket = new Ticket(nroTicket, fechaTicket, nroAsiento);
                            
                            Nodo nodo = new Nodo(objTicket);
                            boolean primerNodo = false;
                            if (listaTickets == null) {
                                System.out.println("Lista vacia, creando...");
                                listaTickets = new Lista_Enlazada();
                                System.out.println("Primer nodo insertado OK, ahora la lista tiene largo 1");
                                listaTickets.setCabecera(nodo);
                                listaTickets.setTam(1);                               
                                primerNodo = true;
                            }
                            
                            int x = 7;
                            if (listaTickets.getTam() > 1) {
                                System.out.println("Buscando nodo en lista mayor a 1");
                                valida = listaTickets.buscarNodo(objTicket);
                               
                                if (valida > -1 && listaTickets.getTam() > 1) {
                                    
                                    System.out.println("ID de Ticket ya existe, NO se ingresará !");
                                    pausa();
                                    int z = 9;
                                } else if (valida != -1 && listaTickets.getTam() == 1) {
                                    
                                } else {

                                    if (listaTickets.getTam() > 1) {
                                        System.out.println("Lista largo mayor que 1 y NODO no fue encontrado, insertando nodo al final");
                                        listaTickets.insertaNodoalFinal(objTicket);
                                    }

                                }
                                
                                
                            } else if (listaTickets.getTam() == 1)
                            {
                                System.out.println("Buscando nodo en lista de largo 1");
                                valida = listaTickets.buscarNodo(objTicket);
                                
                                
                                if (valida < 0) {
                                    System.out.println("Nodo no existe, se insertará al final");
                                    listaTickets.insertaNodoalFinal(objTicket);
                                    primerNodo = false;
                                    
                                } else {
                                    System.out.println("Nodo ya existe en lista de largo 1, no se insertará");      
                                    
                                }
                            } 
                            
                            int z = 10;
                        } while (valida != -1 && valida!=0);
                        int z=9;
                        System.out.println("Desea Ingresar otro TICKET ? S/N :");
                        resp = Leer.dato();
                    } while (resp.equals("S") || resp.equals("s"));          
                    
                    break;
                case 2:
                    if (listaTickets == null) {
                        System.out.println("LISTA VACIA, esta opcion NO está disponible");
                        pausa();
                    } else {
                        listaTickets.imprimeLista();
                        pausa();
                        break;
                    }
                case 3:
                    boolean pilaProcesada = false;
                    if (listaTickets == null && pilaTickets == null) {
                        System.out.println("Lista Vacía, no hay datos por pasar a la Pila !!!");
                        int t=0;
                    } else if(listaTickets ==  null || pilaProcesada == true) {
                        System.out.println("Lista Vacía, no hay datos por traspasar a la pila !! ");
                    }else if(pilaTickets == null){
                        System.out.println("Proceso de Traspaso de Cola a Pila");
                        pilaTickets = new PilaE();
                        int tam = listaTickets.getTam();
                        for(int i=0;i<tam;i++){
                                /*Ticket objTicket = new Ticket(1,"Ticket1", "Raza1");
                                Ticket objTicket2 = new Ticket(2,"Ticket2", "Raza2");
                                Ticket objTicket3 = new Ticket(3,"Ticket3", "Raza3");
                                listaTickets.insertaNodoalFinal(objTicket);*/
                                int g=5;
                                pilaTickets.push(listaTickets.getCabecera().getTicket());
                                g=6;
                                listaTickets.setCabecera(listaTickets.getCabecera().getSiguiente());
                                g=7;
                                listaTickets.setTam((listaTickets.getTam())-1);
                                g=8;
                                listaTickets.disminuirTamano();
                                g=9;         
                                System.out.println("La lista de tickets disminuyó en 1 elemento");
                                listaTickets.imprimeLista();
                                g=10;
                                System.out.println("La Pila aumetó en 1 elemento");
                                pilaTickets.imprimir();
                                g=11;
                                
                        }
                        System.out.println("PROCESO TERMINADO !");
                        pausa();                                
                        pilaProcesada = true;                       
                        break;                         
                    }
                case 4:
                    if (pilaTickets == null) {
                        System.out.println("Pila está vacia, no hay datos para mostrar");
                    } else {
                        pilaTickets.imprimir();
                    }
                    break;
               
                case 5:
                    boolean colaProcesada = false;
                    if (pilaTickets == null && colaTickets == null) {
                        System.out.println("Pila Vacía, no hay datos por pasar a la Cola !!!");
                        int t=0;
                    } else if(pilaTickets ==  null || colaProcesada == true) {
                        System.out.println("Pila Vacía, no hay datos por traspasar a la cola !! ");
                    }else if(colaTickets == null){
                        System.out.println("Proceso de Traspaso de Pila a Cola");
                        colaTickets = new ColaE();
                        int tam = pilaTickets.getTam();
                        for(int i=0;i<tam;i++){
                                /*Ticket objTicket = new Ticket(1,"Ticket1", "Raza1");
                                Ticket objTicket2 = new Ticket(2,"Ticket2", "Raza2");
                                Ticket objTicket3 = new Ticket(3,"Ticket3", "Raza3");
                                listaTickets.insertaNodoalFinal(objTicket);*/
                                int g=5;
                                colaTickets.encolar(pilaTickets.getCima().getTicket());                                
                                System.out.println("La Cola de tickets aumentó en 1 elemento");
                                g=6;
                                colaTickets.imprimir();
                                g=7;
                                pilaTickets.pop();
                                System.out.println("La pila de tickets disminuyó en 1 elemento");
                                g=7;
                                pilaTickets.imprimir();
                                
                        }
                        System.out.println("PROCESO TERMINADO !");
                        pausa();                                
                        colaProcesada = true;                       
                        break;                         
                    }
                    break;
                case 6:
                    if (colaTickets == null) {
                        System.out.println("COLA está vacia, no hay datos para mostrar");
                    } else {
                        colaTickets.imprimir();
                    }
                    break;
                  
                case 7:
                    boolean arbolProcesado = false;
                    if (pilaTickets == null && colaTickets == null) {
                        System.out.println("No hay pilas o colas con datos, por lo tanto, no hay información para procesar en el arbol. ");
                        int t=0;
                    } else if(colaTickets ==  null || arbolProcesado == true) {
                        System.out.println("Cola Vacía, no hay datos por traspasar al Arbol !! ");
                    }else if(arbolTickets == null){
                        System.out.println("Proceso de Traspaso de COLA a Arbol");
                        arbolTickets = new ArbolBalanceado();
                        int tam = colaTickets.size();
                        for(int i=0;i<tam;i++){
                                /*Ticket objTicket = new Ticket(1,"Ticket1", "Raza1");
                                Ticket objTicket2 = new Ticket(2,"Ticket2", "Raza2");
                                Ticket objTicket3 = new Ticket(3,"Ticket3", "Raza3");
                                listaTickets.insertaNodoalFinal(objTicket);*/
                                int g=5;
                                arbolTickets.insertarNodo(colaTickets.desencolar());
                                System.out.println("Arbol de tickets aumentó en 1 elemento");
                                System.out.println(arbolTickets.toString());
                                                     
                                System.out.println("Cola de tickets disminuyó en 1 elemento");
                                colaTickets.imprimir();
                                g=6;
                        }
                        System.out.println("PROCESO TERMINADO !");
                        pausa();                                
                        arbolProcesado = true;                       
                        break;                         
                    }

                case 8:
                    if (arbolTickets == null) {
                        System.out.println("Arbol vacío, no hay datos para mostrar");
                    } else {
                        arbolTickets.recorreInOrden();
                    }
                    break;
                case 9 :
                    System.out.println("Ha elegido salir... adios !");
                    pausa();
                    break;
                default :
                    System.out.println("Opcion no existe !");
                    pausa();
                    break;                     
            }
        }while (op != 9);
    }
    
    public static void p(String msg){
        System.out.println(msg);
    }
    
    public static void pausa(){
        System.out.println("Presione ENTER para continuar...");
        Leer.dato();
    }
}