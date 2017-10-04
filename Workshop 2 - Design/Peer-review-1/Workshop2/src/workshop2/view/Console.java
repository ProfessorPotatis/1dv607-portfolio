/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package workshop2.view;

import java.util.ArrayList;
import java.util.Scanner;
import workshop2.model.BoatClub;
import workshop2.model.Member;

/**
 *
 * 
 */
public class Console {
    
    BoatClub boatClub;
    
    public Console(BoatClub boatClub){
        this.boatClub=boatClub;
    }
    
    
    public void start(){
        int choice=0;
        Scanner reader= new Scanner(System.in); 
        
        printMenu();
    
        try{
        do{
        choice=reader.nextInt();
        switch(choice){
            case 1:{
                
                printCreateMemberUi();
            
                break;
            }
            case 2:{
                printDeleteMemberUi();
                break;
            }
            case 3:{
                printUpdateMemberUi();
                break;
            }
            case 4:{
                printSpesificMemberUi();
                break;
            }
            case 5:{
                printRegisterBoatUi();
                break;
            }
            case 6:{
                printDeleteBoatUi();
                break;
            }
            case 7:{
                printUpdateBoatInfoUi();
                break;
            }
            case 8:{
                printCompactList();
                break;
            }
            case 9:{
                printVerboseList();
                break;
            }
            default :{
               
                printMenu();
                break;
                
            }
        }
        
        }while(choice!=999);}
        catch(Exception e){
            System.out.println("Please don't enter anything but integers in the menu. Program exits.Restart again!!");
        }
        
    }
    
    
    public void printMenu(){
        System.out.println("==========Welcome to BoatClub==========");
        System.out.println("1-Create a member");
        System.out.println("2-Delete a member");
        System.out.println("3-Update Member Info");
        System.out.println("4-Look at a Members info");
        System.out.println("5-Register Boat");
        System.out.println("6-Delete Boat");
        System.out.println("7-Change Boats Info");
        System.out.println("8-View Compact List");
        System.out.println("9-View Verbose List");
        System.out.println("999-Exit");
}
    
    
    public void printCreateMemberUi(){
        Scanner reader= new Scanner(System.in);
        System.out.println("Please enter member's name:");
        String name=reader.nextLine();
        System.out.println("Please enter member's personal number");
        int pNum=reader.nextInt();
        boatClub.createMember(name, pNum);
        System.out.println("**************************");
        System.out.println("Member succesfully Created");
        System.out.println("**************************");
        printFooterMessage();
    }
    
    
    
    public void printDeleteMemberUi(){
        Scanner reader= new Scanner(System.in);
        System.out.println("Please enter memberId of the Member to delete ");
        int memberId=reader.nextInt();
        System.out.println("**************************");
        System.out.println("Delete process success: "+boatClub.deleteMember(memberId));
        System.out.println("**************************");
        printFooterMessage();
    }
    
    
    public void printUpdateMemberUi(){
        Scanner reader= new Scanner(System.in);
        
        System.out.println("Please enter new memberName");
        String name= reader.nextLine();
        
        System.out.println("Please enter new personal Number ");
        int pNum=reader.nextInt();
        
        System.out.println("Please enter memberId of the Member that you want to update info ");
        int memberId=reader.nextInt();
        
        
        
        System.out.println("**************************");
        System.out.println("Update member process success: "+boatClub.updateMember(memberId, name, pNum));
        System.out.println("**************************");
        
        printFooterMessage();
        
    }
    
    
    public void printCompactList(){
        System.out.println("///////////////////////////");
        System.out.println("Compact List");
        System.out.println("///////////////////////////");
        
        ArrayList<Member> list=boatClub.getMemberList();
        
        for (int i = 0; i < list.size(); i++) {
            System.out.println("______________________________________________");
            System.out.println("\nMember name:\n\t"+list.get(i).getName()+"\nMember id:\n\t"+list.get(i).getMemberId()+"\nNumber of boats:\n\t"+ list.get(i).getBoatList().size());
            
        }
        printFooterMessage();
    }
    
    
    public void printVerboseList(){
        System.out.println("///////////////////////////");
        System.out.println("Verbose List");
        System.out.println("///////////////////////////");
        ArrayList<Member> list=boatClub.getMemberList();
        
        for (int i = 0; i < list.size(); i++) {
            System.out.println("______________________________________________");
            System.out.println("\nMember name:\n\t"+list.get(i).getName()+"\nMember id:\n\t"+list.get(i).getMemberId());
            for (int j = 0; j < list.get(i).getBoatList().size(); j++) {
                System.out.println("\nBoat "+(j+1)+":" +"\tBoat type: "+list.get(i).getBoatList().get(j).getBoatType()+
                        "\tBoat length: "+list.get(i).getBoatList().get(j).getLength()+"mt"+
                        "\tBoat id: "+list.get(i).getBoatList().get(j).getBoatId());
                
            }
             
        }
        printFooterMessage();
    }
    
    
     public void printRegisterBoatUi(){
         Scanner reader= new Scanner(System.in);
         System.out.println("Please enter the type of the boat");
         String boatType=reader.nextLine();
         System.out.println("Please enter the length of the boat");
         int boatLenght=reader.nextInt();
         System.out.println("Please enter the member's id who will own the boat");
         int memberId=reader.nextInt();
        
        System.out.println("**************************");
        System.out.println("Boat add process success: "+boatClub.addBoat(memberId, boatType, boatLenght));
        System.out.println("**************************");
        
        
        printFooterMessage();
        }
     
     
     public void printDeleteBoatUi(){
         Scanner reader= new Scanner(System.in);

         System.out.println("Please boat's owner id");
         int memberId=reader.nextInt();
         System.out.println("Please boat's id");
         int boatId=reader.nextInt();
         
        System.out.println("**************************");
        System.out.println("Boat delete process success: "+boatClub.deleteBoat(memberId, boatId));
        System.out.println("**************************");
         
         
     
         printFooterMessage();
     
     }
     
     public void printUpdateBoatInfoUi(){
         
         Scanner reader= new Scanner(System.in);

         System.out.println("Please enter the type of the boat");
         String boatType=reader.nextLine();
         
         System.out.println("Please enter the length of the boat");
         int boatLenght=reader.nextInt();
         
         System.out.println("Please boat's owner id");
         int memberId=reader.nextInt();
         
         System.out.println("Please boat's id");
         int boatId=reader.nextInt();
         
         
        System.out.println("**************************");
        System.out.println("Boat update process success: "+boatClub.updateBoat(memberId, boatId, boatType, boatLenght));
        System.out.println("**************************");
         
         
         
         
        
         printFooterMessage();
     }
     
     
     public void printSpesificMemberUi(){
         
         Scanner reader= new Scanner(System.in);

         System.out.println("Please enter member id");
         int memberId=reader.nextInt();
         Member member=boatClub.getMember(memberId);
         if(member!=null){
             System.out.println("______________________________________________");
            System.out.println("\nMember name:\n\t"+member.getName()+"\nMember id:\n\t"+member.getMemberId()+"\nNumber of boats:\n\t"+ member.getBoatList().size());
        }else{
             System.out.println("Member with "+ memberId+" doesnt exist");
         }
        printFooterMessage();
     }
     
     
     public void printFooterMessage(){
     
         System.out.println("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
         System.out.println("Type 999 to exit");
         System.out.println("Type 0 return to Menu");
         System.out.println("Do not type Strings!");
         System.out.println("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
         
     
     }
    
     
     
    
    
    
}
