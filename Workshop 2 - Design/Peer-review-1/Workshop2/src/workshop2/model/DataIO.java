/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package workshop2.model;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;

/**
 *
 * 
 */
public class DataIO {
    
    public void writeMemberDatatoFile(ArrayList memberList) throws IOException{
        
         try(FileOutputStream fs=new FileOutputStream("memberData.bin")){
           
           ObjectOutputStream os=new ObjectOutputStream(fs);
           
           os.writeObject(memberList);
           
           os.close();
           fs.close();
           
       } catch (FileNotFoundException ex) {
            
        }
    }
    
    public ArrayList<Member> readMemberDataFromFile() throws IOException, ClassNotFoundException{
        
        try(FileInputStream is=new FileInputStream("memberData.bin")){
            
            ObjectInputStream oos=new ObjectInputStream(is);
            
            ArrayList memberList=  (ArrayList) oos.readObject();
            
            
            
            
            oos.close();
            is.close();
            return memberList;
            
        } catch (FileNotFoundException ex) {
            return new ArrayList<>();
        } 
        
     
    }
    
    
}
