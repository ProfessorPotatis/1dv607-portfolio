/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package workshop2.model;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Random;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * 
 */
public class BoatClub {
    
    private ArrayList<Member> memberList;
    private ArrayList<Integer> usedMemberIds;
    private ArrayList<Integer> usedBoatIds;
    private Random generator;
    private DataIO data;
    
    public BoatClub() {
        generator=new Random();
        data=new DataIO();
        
        
        try{
        memberList=data.readMemberDataFromFile();
        usedBoatIds=filterUsedBoatIds();
        usedMemberIds=filterUsedMemberIds();
        
        } catch (IOException ex) {
            System.out.println("IOException please start the program againg");
        } catch (ClassNotFoundException ex) {
            System.out.println("ClassNotFoundException please start the program againg");
        }
            
        
        
    }
    
    public void updateDataIO(){
        try {
            data.writeMemberDatatoFile(getMemberList());
           
        } catch (IOException ex) {
            Logger.getLogger(BoatClub.class.getName()).log(Level.SEVERE, null, ex);
        }
        
    }
    
    
    public void createMember(String name,int personalNumber){
        Member member=new Member(name,personalNumber);
        member.setMemberId(generateMemberId());
        memberList.add(member);
        updateDataIO();
        
        
        
    }
    
    public boolean deleteMember(int memberId){
        int index=findMemberIndex(memberId);
        if(index==-1){
            return false;
        }
        else{
            getMemberList().remove(index);
            int indexToRemove=usedMemberIds.indexOf(memberId);
            usedMemberIds.remove(indexToRemove);
            updateDataIO();
            return true;
            
        }
    }
    
    public boolean updateMember(int memberId,String name,int personalNumber){
        int index=findMemberIndex(memberId);
        if(index==-1){
            return false;
        }
        else{
            getMemberList().get(index).setName(name);
            getMemberList().get(index).setPersonalNumber(personalNumber);
            updateDataIO();
            return true;
            
        }
        
    }
    
    public boolean addBoat(int ownersId,String boatType,int length){
        int memberIndex=findMemberIndex(ownersId);
        
        if(memberIndex==-1){
            return false;
        }else{
            Boat boat=new Boat(boatType, length, ownersId);
            boat.setBoatId(generateBoatId());
            getMemberList().get(memberIndex).getBoatList().add(boat);
            updateDataIO();
            return true;
        }
    }
    
    public boolean deleteBoat(int ownersId, int boatId){
        int memberIndex=findMemberIndex(ownersId);
        
        if(memberIndex==-1){
            return false;
        }else{
            int boatIndex=findBoatIndex(getMemberList().get(memberIndex),boatId);
            if(boatIndex!=-1){
                getMemberList().get(memberIndex).getBoatList().remove(boatIndex);
                int indexToremove=usedBoatIds.indexOf(boatId);
                usedBoatIds.remove(indexToremove);
                updateDataIO();
                return true;
            }
            
        }
        
        return false;
    }
    
    public boolean updateBoat(int ownersId, int boatId,String boatType,int length){
        
        int memberIndex=findMemberIndex(ownersId);
        
        if(memberIndex==-1){
            return false;
        }else{
            int boatIndex=findBoatIndex(getMemberList().get(memberIndex),boatId);
            if(boatIndex!=-1){
                Boat boat= (Boat) getMemberList().get(memberIndex).getBoatList().get(boatIndex);
                boat.setBoatType(boatType);
                boat.setLength(length);
                updateDataIO();
                return true;
            }
            
        }
        
        return false;
        
       
    }
    
    
    
    public int findMemberIndex(int memberId){
        
        if(usedMemberIds.contains(memberId)){
            for(int i=0;i<getMemberList().size();i++){
                if(getMemberList().get(i).getMemberId()==memberId){
                    return i;
                }
            }
        }
        
        return -1;
    }
    
    
    public int findBoatIndex(Member member,int boatId){
        
        for(int i=0;i<member.getBoatList().size();i++){
                Boat boat=(Boat) member.getBoatList().get(i);
                if(boat.getBoatId()==boatId){
                    return i;
                }
        
    }
        
        return -1;
    }
    
    
    
    
    
    public int generateMemberId(){
        int memberId;
        do{
            memberId= generator.nextInt(10000000) + 1;
        }while(isMemIdExists(memberId)==true);
        
        usedMemberIds.add(memberId);
        return memberId;
    }
    
    public int generateBoatId(){
        int boatId;
        do{
            boatId= generator.nextInt(10000000) + 1;
        }while(isBoatIdExists(boatId)==true);
        usedBoatIds.add(boatId);
        return boatId;
    }
    
    public boolean isMemIdExists(int memberId){
        if(usedMemberIds.contains(memberId))
            return true;
        else
            return false;
    }
    
    public boolean isBoatIdExists(int boatId){
        if(usedBoatIds.contains(boatId))
            return true;
        else
            return false;
    }
    
    
    
    public ArrayList<Integer> filterUsedMemberIds(){
    
        usedMemberIds=new ArrayList<>();
        
        
        for(int i=0;i<memberList.size();i++){
        
            int memberId=memberList.get(i).getMemberId();
            usedMemberIds.add(memberId);
        }
        
        return usedMemberIds;
        
    
    }
    
    
    public ArrayList<Integer> filterUsedBoatIds(){
    
        usedBoatIds=new ArrayList<>();
        
         for(int i=0;i<memberList.size();i++){
        
            ArrayList<Boat> boatList=memberList.get(i).getBoatList();
           
             for (int j = 0; j < boatList.size(); j++) {
                 usedBoatIds.add(boatList.get(j).getBoatId());
                 
             }
        }
        return usedBoatIds;
    
    }

    /**
     * @return the memberList
     */
    public ArrayList<Member> getMemberList() {
        return memberList;
    }
    
    public Member getMember(int memberId){
        
        for (int i = 0; i < memberList.size(); i++) {
            Member member = memberList.get(i);
            if(member.getMemberId()==memberId){
                return member;
            }
        }
        
        return null;
    }
}
