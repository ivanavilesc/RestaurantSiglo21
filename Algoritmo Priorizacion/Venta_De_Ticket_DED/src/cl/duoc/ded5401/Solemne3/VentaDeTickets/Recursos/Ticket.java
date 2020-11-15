/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.duoc.ded5401.Solemne3.VentaDeTickets.Recursos;

import java.util.Date;

/**
 *
 * @author iaviles
 */
public class Ticket {
        
    private int nroTicket;
    private Date fechaTicket;
    private int nroAsiento;

    public Ticket() {
    }

    public Ticket(int nroTicket, Date fechaTicket, int nroAsiento) {
        this.nroTicket = nroTicket;
        this.fechaTicket = fechaTicket;
        this.nroAsiento = nroAsiento;
    }

    public int getNroTicket() {
        return nroTicket;
    }

    public void setNroTicket(int nroTicket) {
        this.nroTicket = nroTicket;
    }

    public Date getFechaTicket() {
        return fechaTicket;
    }

    public void setFechaTicket(Date fechaTicket) {
        this.fechaTicket = fechaTicket;
    }

    public int getNroAsiento() {
        return nroAsiento;
    }

    public void setNroAsiento(int nroAsiento) {
        this.nroAsiento = nroAsiento;
    }

    @Override
    public String toString() {
        return "Ticket{" + "NroTicket=" + nroTicket + ", Fecha=" + fechaTicket + ", NroAsiento=" + nroAsiento + '}';
    }
    
    
    
    
}
