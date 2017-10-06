package workshop2_src;

import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import javax.xml.parsers.DocumentBuilder;
import org.w3c.dom.Document;
import org.w3c.dom.NodeList;
import org.w3c.dom.Node;
import org.w3c.dom.Element;
import java.io.File;
import java.util.ArrayList;

public class MemberRegistry {

	private ArrayList<Member> memberList = new ArrayList<Member>();
	private ArrayList<Boat> boatList;

	/*
	 * Creates a member and stores them in the database.
	 * 
	 * Member: name, personal number, a unique id.
	 */
	public void registerMember(Member newMem) {

		// Read XML-file.
		Document doc = readXMLfile();

		Node root = doc.getFirstChild();
		// Create unique tag ID
		NodeList members = doc.getElementsByTagName("member");
		int lastID = Integer
				.parseInt(members.item(members.getLength() - 1).getAttributes().getNamedItem("id").getNodeValue()) + 1;

		// Create elements for each member value.
		Element memberEle = doc.createElement("member");
		memberEle.setAttribute("id", Integer.toString(lastID));

		Element firstname = doc.createElement("firstname");
		firstname.appendChild(doc.createTextNode(newMem.getFirstname()));
		memberEle.appendChild(firstname);

		Element lastname = doc.createElement("lastname");
		lastname.appendChild(doc.createTextNode(newMem.getLastname()));
		memberEle.appendChild(lastname);

		Element memberid = doc.createElement("memberid");
		memberid.appendChild(doc.createTextNode(Integer.toString(lastID)));
		memberEle.appendChild(memberid);

		Element persnum = doc.createElement("persnum");
		persnum.appendChild(doc.createTextNode(Integer.toString(newMem.getNumber())));
		memberEle.appendChild(persnum);

		Element boats = doc.createElement("boats");
		Element boatAmount = doc.createElement("amount");
		boatAmount.appendChild(doc.createTextNode("0"));
		boats.appendChild(boatAmount);
		memberEle.appendChild(boats);

		root.appendChild(memberEle);

		// Write to XML file.
		writeXMLfile(doc);

	}

	/*
	 * Return a list of all members in two different ways.
	 * 
	 * Compact List: name, member id, number of boats
	 * 
	 * Verbose List: name, personal number, member id, boats, boat information
	 */
	public String listMembers(boolean list) {
		String listOut = "";

		for (int memTemp = 0; memTemp < memberList.size(); memTemp++) {
			Member memberIndex = memberList.get(memTemp);

			if (list) {
				// Create compact list
				listOut += "Name: " + memberIndex.getFullName() + "\n";
				listOut += "Unique ID: " + memberIndex.getID() + "\n";
				listOut += "Amount of boats: " + memberIndex.getAmountBoats() + "\n";
				listOut += "---------------\n";
			} else {
				// Create verbose list
				listOut += "Name: " + memberIndex.getFullName() + "\n";
				listOut += "Personal Number: " + memberIndex.getNumber() + "\n";
				listOut += "Unique ID: " + memberIndex.getID() + "\n";
				listOut += "Amount of boats: " + memberIndex.getAmountBoats() + "\n\n";
				ArrayList<Boat> listBoat = memberIndex.getBoats();
				for (int bTemp = 0; bTemp < listBoat.size(); bTemp++) {
					Boat boatIndex = listBoat.get(bTemp);

					listOut += "Boat type: " + boatIndex.getType() + "\n";
					listOut += "Boat length: " + boatIndex.getLength() + " metres\n\n";
				}
				listOut += "---------------\n";

			}
		}

		return listOut;
	}

	/*
	 * Delete a member from the database. Send confirmation.
	 */
	public void deleteMember(String selMember) {

		// Read from XML file.
		Document doc = readXMLfile();

		NodeList nList = doc.getElementsByTagName("member");
		for (int temp = 0; temp < nList.getLength(); temp++) {

			Node nNode = nList.item(temp);
			Node root = doc.getFirstChild();
			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
				Element eElement = (Element) nNode;

				// Compile first name to be able to select what user to
				// delete.
				String fullName = eElement.getElementsByTagName("firstname").item(0).getTextContent() + " "
						+ eElement.getElementsByTagName("lastname").item(0).getTextContent();

				// Use to find selected member
				if (fullName.contains(selMember)) {
					root.removeChild(eElement);
				}

			}

		}

		// Write to XML file.
		writeXMLfile(doc);

	}

	/*
	 * Edit a member information then save it in database. Input member to edit
	 * or use super.
	 */
	public void editMember(String selMember, String change, String newData) {

		Document doc = readXMLfile();

		NodeList nList = doc.getElementsByTagName("member");
		for (int temp = 0; temp < nList.getLength(); temp++) {

			Node nNode = nList.item(temp);

			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
				Element eElement = (Element) nNode;

				// Compile first name to be able to select what user to
				// edit.
				String fullName = eElement.getElementsByTagName("firstname").item(0).getTextContent() + " "
						+ eElement.getElementsByTagName("lastname").item(0).getTextContent();

				// Use to find selected member
				if (fullName.equals(selMember)) {
					if (change == "firstname") {
						eElement.getElementsByTagName("firstname").item(0).setTextContent(newData);
					} else if (change == "lastname") {
						eElement.getElementsByTagName("lastname").item(0).setTextContent(newData);
					} else if (change == "persnum") {
						eElement.getElementsByTagName("persnum").item(0).setTextContent(newData);
					}
				}

			}

		}

		// Write to XML file.
		writeXMLfile(doc);

	}

	/*
	 * Return a members info.
	 */
	public String listMember(String selMember) {
		String listOut = "";
		// Iterate over members until selected Member is found, then return the info of that member.
		for (int memTemp = 0; memTemp < memberList.size(); memTemp++) {
			Member memberIndex = memberList.get(memTemp);

			if (memberIndex.getFullName().equals(selMember)) {
				listOut += "Name: " + memberIndex.getFullName() + "\n";
				listOut += "Personal Number: " + memberIndex.getNumber() + "\n";
				listOut += "Unique ID: " + memberIndex.getID() + "\n";
				listOut += "Amount of boats: " + memberIndex.getAmountBoats() + "\n\n";
				ArrayList<Boat> listBoat = memberIndex.getBoats();
				for (int bTemp = 0; bTemp < listBoat.size(); bTemp++) {
					Boat boatIndex = listBoat.get(bTemp);

					listOut += "Boat ID: " + boatIndex.getId() + "\n";
					listOut += "Boat type: " + boatIndex.getType() + "\n";
					listOut += "Boat length: " + boatIndex.getLength() + " metres\n\n";
				}
				listOut += "---------------\n";
			}
		}

		return listOut;
	}

	/*
	 * Register a boat with its information and writes it to the XML-file.
	 */
	public void registerBoat(Boat createBoat, String selMember) {
		// Read XML-file.
		Document doc = readXMLfile();

		NodeList nList = doc.getElementsByTagName("member");
		for (int temp = 0; temp < nList.getLength(); temp++) {

			Node nNode = nList.item(temp);

			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
				Element eElement = (Element) nNode;
				String fullName = eElement.getElementsByTagName("firstname").item(0).getTextContent() + " "
						+ eElement.getElementsByTagName("lastname").item(0).getTextContent();

				if (fullName.contains(selMember)) {
					Node boats = eElement.getElementsByTagName("boats").item(0);
					NodeList boatsChildren = eElement.getElementsByTagName("boats").item(0).getChildNodes();

					Element newBoat = doc.createElement("boat");
					Element newBoatType = doc.createElement("type");
					Element newBoatLength = doc.createElement("length");
					newBoatType.appendChild(doc.createTextNode(createBoat.getType()));
					newBoatLength.appendChild(doc.createTextNode(Integer.toString(createBoat.getLength())));
					newBoat.appendChild(newBoatType);
					newBoat.appendChild(newBoatLength);
					int lastID;

					// Grab the correct boat.
					if (boatsChildren.item(boatsChildren.getLength() - 2).getNodeName().toLowerCase().equals("boat")) {
						lastID = Integer.parseInt(boatsChildren.item(boatsChildren.getLength() - 2).getAttributes()
								.getNamedItem("id").getNodeValue()) + 1;
						newBoat.setAttribute("id", Integer.toString(lastID));
					} else {
						newBoat.setAttribute("id", "1");
					}

					int amount = Integer.parseInt(boatsChildren.item(1).getTextContent()) + 1;
					boatsChildren.item(1).setTextContent(Integer.toString(amount));

					boats.appendChild(newBoat);
				}

			}
		}

		// Write to XML file.
		writeXMLfile(doc);

	}

	/*
	 * Delete a boat from the database.
	 */
	public void deleteBoat(String selMember, String selID) {

		// Read XML-file.
		Document doc = readXMLfile();

		NodeList nList = doc.getElementsByTagName("member");
		for (int temp = 0; temp < nList.getLength(); temp++) {

			Node nNode = nList.item(temp);

			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
				Element eElement = (Element) nNode;
				String fullName = eElement.getElementsByTagName("firstname").item(0).getTextContent() + " "
						+ eElement.getElementsByTagName("lastname").item(0).getTextContent();

				if (fullName.equals(selMember)) {
					Node boats = eElement.getElementsByTagName("boats").item(0);
					NodeList boatsChildren = eElement.getElementsByTagName("boats").item(0).getChildNodes();

					for (int i = 0; i < boatsChildren.getLength(); i++) {
						if (boatsChildren.item(i).getNodeName().toLowerCase().equals("boat")) {
							if (boatsChildren.item(i).getAttributes().getNamedItem("id").getNodeValue().equals(selID)) {
								int amount = Integer.parseInt(boatsChildren.item(1).getTextContent()) - 1;
								boatsChildren.item(1).setTextContent(Integer.toString(amount));
								boats.removeChild(boatsChildren.item(i));
							}

						}

					}
				}

			}
		}

		// Write to XML file.
		writeXMLfile(doc);

	}

	/*
	 * Edit a boat currently in the database and then save it.
	 */
	public void editBoat(String selMember, String selID, String change, String data) {
		// Read XML-file.
		Document doc = readXMLfile();

		NodeList nList = doc.getElementsByTagName("member");
		for (int temp = 0; temp < nList.getLength(); temp++) {

			Node nNode = nList.item(temp);

			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
				Element eElement = (Element) nNode;
				String fullName = eElement.getElementsByTagName("firstname").item(0).getTextContent() + " "
						+ eElement.getElementsByTagName("lastname").item(0).getTextContent();

				if (fullName.equals(selMember)) {
					NodeList boatsChildren = eElement.getElementsByTagName("boats").item(0).getChildNodes();

					// Iterate over the boats until selected boat is targetted.
					for (int i = 0; i < boatsChildren.getLength(); i++) {
						if (boatsChildren.item(i).getNodeName().toLowerCase().equals("boat")) {
							if (boatsChildren.item(i).getAttributes().getNamedItem("id").getNodeValue().equals(selID)) {

								// Update the value edited.
								Node boatNode = boatsChildren.item(i);
								Element boatElement = (Element) boatNode;
								boatElement.getElementsByTagName(change).item(0).setTextContent(data);
							}

						}

					}
				}

			}
		}

		// Write to XML file.
		writeXMLfile(doc);

	}

	/*
	 * Creates a structure within an ArrayList, containing Members and their respective Boats.
	 */
	public void initialize() {
		Document doc = readXMLfile();

		NodeList members = doc.getElementsByTagName("member");
		for (int temp = 0; temp < members.getLength(); temp++) {
			Node nNode = members.item(temp);
			if (nNode.getNodeType() == Node.ELEMENT_NODE) {
				Element eElement = (Element) nNode;
				String firstname = eElement.getElementsByTagName("firstname").item(0).getTextContent();
				String lastname = eElement.getElementsByTagName("lastname").item(0).getTextContent();
				int memberID = Integer.parseInt(eElement.getElementsByTagName("memberid").item(0).getTextContent());
				int persNum = Integer.parseInt(eElement.getElementsByTagName("persnum").item(0).getTextContent());

				NodeList boatsChildren = eElement.getElementsByTagName("boats").item(0).getChildNodes();
				int bAmount = Integer.parseInt(boatsChildren.item(1).getTextContent());
				// Iterate over the boats and add them to list.
				// Reset the array of boats.
				boatList = new ArrayList<Boat>();
				for (int bTemp = 0; bTemp < boatsChildren.getLength(); bTemp++) {
					if (boatsChildren.item(bTemp).getNodeName().toLowerCase().equals("boat")) {
						Node boatNode = boatsChildren.item(bTemp);
						Element boatElement = (Element) boatNode;
						String bType = boatElement.getElementsByTagName("type").item(0).getTextContent();
						int bLength = Integer
								.parseInt(boatElement.getElementsByTagName("length").item(0).getTextContent());
						int bID = Integer.parseInt(boatElement.getAttributes().getNamedItem("id").getNodeValue());
						Boat newBoat = new Boat(bType, bLength, bID);
						boatList.add(newBoat);
					}

				}
				Member newMem = new Member(firstname, lastname, persNum, memberID, bAmount, boatList);
				// boatList.clear();
				memberList.add(newMem);

			}
		}		
	}

	private Document readXMLfile() {
		try {
			File fXmlFile = new File("src/workshop2_src/data.xml");
			DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
			DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
			Document doc = dBuilder.parse(fXmlFile);
			doc.getDocumentElement().normalize();

			return doc;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	private void writeXMLfile(Document doc) {
		try {

			TransformerFactory transformerFactory = TransformerFactory.newInstance();
			Transformer transformer = transformerFactory.newTransformer();
			DOMSource source = new DOMSource(doc);
			StreamResult result = new StreamResult(new File("src/workshop2_src/data.xml"));
			transformer.setOutputProperty(OutputKeys.INDENT, "yes");
			transformer.setOutputProperty(OutputKeys.DOCTYPE_PUBLIC, "yes");
			transformer.setOutputProperty("{http://xml.apache.org/xslt}indent-amount", "10");
			transformer.transform(source, result);

		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
