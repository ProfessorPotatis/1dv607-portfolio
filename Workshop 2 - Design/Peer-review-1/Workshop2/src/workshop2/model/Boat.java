/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package workshop2.model;

import java.io.Serializable;

/**
 *
 * 
 */
public class Boat implements Serializable {
    
    private String boatType;
    private double length;
    private int ownersId;
    private int boatId;
    
    public Boat(String boatType,double length,int ownersId){
        this.boatType=boatType;
        this.length=length;
        this.ownersId=ownersId;
    }

    /**
     * @return the boatType
     */
    public String getBoatType() {
        return boatType;
    }

    /**
     * @param boatType the boatType to set
     */
    public void setBoatType(String boatType) {
        this.boatType = boatType;
    }

    /**
     * @return the length
     */
    public double getLength() {
        return length;
    }

    /**
     * @param length the length to set
     */
    public void setLength(double length) {
        this.length = length;
    }

    /**
     * @return the ownersId
     */
    public int getOwnersId() {
        return ownersId;
    }

    /**
     * @param ownersId the ownersId to set
     */
    public void setOwnersId(int ownersId) {
        this.ownersId = ownersId;
    }

    /**
     * @return the boatId
     */
    public int getBoatId() {
        return boatId;
    }

    /**
     * @param boatId the boatId to set
     */
    public void setBoatId(int boatId) {
        this.boatId = boatId;
    }
    
}
