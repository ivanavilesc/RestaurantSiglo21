package cl.duoc.ded5401.Solemne3.Veterinaria.Recursos;

/**
 *
 * @author Javier Chamorro
 */
public class PilaE {

    private Nodo cabecera;
    private int tam;

    public PilaE() {
    }

    public PilaE(Nodo cabecera, int tam) {
        this.cabecera = cabecera;
        this.tam = tam;
    }

    public Nodo getCabecera() {
        return cabecera;
    }

    public void setCabecera(Nodo cabecera) {
        this.cabecera = cabecera;
    }

    public int getTam() {
        return tam;
    }

    public void setTam(int tam) {
        this.tam = tam;
    }

    //La pila esta vacia
    public boolean isEmpty() {

        if (this.cabecera == null) {
            return true;
        } else {
            return false;
        }

    }

    public Nodo getCima() {

        /*si la pila NO está vacía, recorra, mientras el nodo siguiente al nodo de posicion,
		no tenga un valor null en su apuntador, vale decir, mientras no sea el ultimo, avanzará al siguiente,
		cuando sea el ultimo, se detendrá y retornará el valor */
        if (!this.isEmpty()) {
            Nodo temp = cabecera;
            while (temp.getSiguiente() != null) {
                temp = temp.getSiguiente();

            }
            return temp;
            // aquí entra cuando el isEmpty() retorna true
        } else {
            return null;
        }
    }

    public void imprimir() {

        /* Si la pila no está vacía, entra y recorre, con un nodo de posicion, va mostrando el valor de cada nodo
		de la pila, mientras el contador sea menor en 1, al tamaño de la pila (emula ciclo for), toma la forma del 
		siguiente Nodo y aumenta el contador de recorrido.		
         */
        if (!this.isEmpty()) {
            int cont = 0;
            Nodo temp = cabecera;
            System.out.println("Los elementos de la pila son ");
            while (cont < this.tam) {
                System.out.println(temp.getAnimal().toString());
                temp = temp.getSiguiente();
                cont++;
            }
        } else {
            System.out.println("La pila está vacía ");
        }

    }

    /*
		el metodo push, agrega un valor a la pila, este por definicion, debiese convertirse en la cima de la pila
		Si la pila está vacía, toma el valor de la cabecera, en caso que la pila tenga al menos un nodo, usa el metodo
		que retorna el ultimo objeto de la pila (CIMA) y le setea el nuevo valor a su apuntador de Nodo siguiente
		además, aumenta el tamaño de la pila
     */
    public void push(Animal objAnimal) {

        if (this.isEmpty()) {
            this.cabecera = new Nodo(objAnimal);
        } else {
            this.getCima().setSiguiente(new Nodo(objAnimal));
        }
        this.tam++;
    }

    /* El método POP, saca un valor de la lista, en este caso, es el valor que está en la CIMA
	Si la pila NO está vacia, la recorre iniciando el contador en 1, y va avanzando de posiciones, hasta
	que llega al penultimo valor, desde esa posicion, saca de la pila al ultimo valor de la pila
	(actual cima), dejando el puntero de la penultima posicion, apuntando a null, lo que lo convierte en ultimo valor
	
     */
    public void pop() {

        if (!this.isEmpty()) {
            Nodo temp = cabecera;
            int cont = 1;

            while (cont < this.tam - 1) {
                temp = temp.getSiguiente();
                cont++;
            }
            this.tam--;

            if (this.tam == 0) {
                this.cabecera = null;
            } else {
                temp.setSiguiente(null);
            }
        }

    }
    
    public double calculaPesoPila(){
        int sumaValor = 0;
        double pesoPila = 0;
        if (!this.isEmpty()) {
            
            int cont = 0;
            Nodo temp = cabecera;
            
            while (cont < this.tam) {
                sumaValor+=temp.getAnimal().getIdAnimal();
                temp = temp.getSiguiente();
                cont++;
            }
        } else {
            System.out.println("La pila está vacía ");
        }
        System.out.println("suma del total :"+sumaValor);
        System.out.println("largo pila :"+(this.tam));
        pesoPila = (double)sumaValor/this.tam;
        //double pesoDouble = (double)pesoPila;
        return pesoPila;        
    }
    
    public boolean comparaPilas(PilaE pila1, PilaE pila2){
        Boolean valida = false;
        if(pila1.tam==pila2.tam){
            Nodo tempP1 = pila1.getCabecera();
            Nodo tempP2 = pila2.getCabecera();
            
            for(int i=0; i<this.tam;i++){
                if(tempP1.getAnimal().getIdAnimal()==tempP2.getAnimal().getIdAnimal()){
                    valida = true;
                    tempP1 = tempP1.getSiguiente();
                    tempP2 = tempP2.getSiguiente();
                }else{
                    return false;
                }
            }
        return true;    
        }else{
            return false;        
        }
        
    }
    
    

}
