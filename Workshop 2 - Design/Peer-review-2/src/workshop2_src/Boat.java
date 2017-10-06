package workshop2_src;

public class Boat {
	private String type;
	private int length;
	private int uniqueID;
	
	public Boat(String x_type, int x_length, int boatID) {
		type = x_type;
		length = x_length;
		uniqueID = boatID;
	}
	
	public String getType() {
		return type;
	}
	
	public int getLength() {
		return length;
	}
	
	public int getId() {
		return uniqueID;
	}
}
