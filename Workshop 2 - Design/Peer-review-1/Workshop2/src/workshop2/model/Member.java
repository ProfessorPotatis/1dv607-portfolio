/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package workshop2.model;

import java.io.Serializable;
import java.util.ArrayList;

/**
 *
 * @author beysimeryalmaz
 */
public class Member implements Serializable {
    
    private String name;
    private int personalNumber;
    private int memberId;
    private ArrayList<Boat> boatList;
    
    
    public Member(String name,int personalNumber){
        this.name=name;
        this.personalNumber=personalNumber;
        boatList=new ArrayList();
    }

    /**
     * @return the name
     */
    public String getName() {
        return name;
    }

    /**
     * @param name the name to set
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * @return the personalNumber
     */
    public int getPersonalNumber() {
        return personalNumber;
    }

    /**
     * @param personalNumber the personalNumber to set
     */
    public void setPersonalNumber(int personalNumber) {
        this.personalNumber = personalNumber;
    }

    /**
     * @return the memberId
     */
    public int getMemberId() {
        return memberId;
    }

    /**
     * @param memberId the memberId to set
     */
    public void setMemberId(int memberId) {
        this.memberId = memberId;
    }

    /**
     * @return the boatList
     */
    public ArrayList<Boat> getBoatList() {
        return boatList;
    }

    /**
     * @param boatList the boatList to set
     */
    public void setBoatList(ArrayList boatList) {
        this.boatList = boatList;
    }
    
}
