/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package workshop2;

import workshop2.model.BoatClub;
import workshop2.view.Console;

/**
 *
 * 
 */
public class Workshop2 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        
        BoatClub boatClub=new BoatClub();
        Console ui=new Console(boatClub);
        ui.start();
    }
    
}
