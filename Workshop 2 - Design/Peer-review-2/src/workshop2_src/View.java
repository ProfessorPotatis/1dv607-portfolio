package workshop2_src;

import java.util.InputMismatchException;
import java.util.Scanner;

public class View {

	public void ui() {
		Scanner sc = new Scanner(System.in);
		MemberRegistry members = new MemberRegistry();
		// Initialize the MemberHandler by loading XML-file into ArrayLists
		members.initialize();
		System.out.println(
				"Welcome to the control panel! \nNavigate by using numbers, via clicking a number and then hitting enter.");
		System.out.println(
				"1. Create a new member  ---  2. Edit a member  ---  3. Delete a member  \n4. List a member  ---  5. List all members  ---  6. Register a boat\n7. Edit a boat  ---  8. Delete a boat  ---  9. Exit application");

		int choice1 = 0;
		do {
			try {
				System.out.print("Input: ");
				choice1 = sc.nextInt();

				if (choice1 < 1 || choice1 > 9) {
					throw new InputMismatchException("Wrong number!");
				}
			} catch (InputMismatchException e) {
				System.out.println("Not a valid number, try again!");
				sc.nextLine();
			}

		} while (choice1 < 1 || choice1 > 9);

		sc.nextLine();
		if (choice1 == 1) {
			// Create a new member.
			System.out.println("\nYou selected 1. Create a new member!");
			System.out.println("Please input the new members firstname! (e.g 'John')");
			System.out.print("Input: ");
			String firstname = sc.next();
			System.out.println("Thank you, now please input the members lastname! (e.g 'Doe')");
			System.out.print("Input: ");
			String lastname = sc.next();
			System.out.println("Thank you, now please input the members personal number! (e.g '0703932344')");
			System.out.print("Input: ");
			int persNum = sc.nextInt();
			sc.nextLine();
			Member newMember = new Member(firstname, lastname, persNum, 0, 0, null);
			members.registerMember(newMember);
			System.out.println("The member " + firstname + " " + lastname + " is now created!");
			ui();
		} else if (choice1 == 2) {
			// Edit a member.
			System.out.println("\nYou selected 2. Edit a member!");
			System.out.println("Please input which user you wish to edit. (Full name e.g 'John Doe')");
			System.out.print("Input: ");
			String pickUser = sc.nextLine();
			String listMember = members.listMember(pickUser);
			if (!listMember.isEmpty()) {

				System.out.println(
						"What information do you wish to edit?\n" + "1. Firstname\n2. Lastname\n3. Personal number.");

				// Make sure the information is valid.
				int change = 0;
				do {
					try {
						System.out.print("Input: ");
						change = sc.nextInt();

						if (change <= 0 || change > 3) {
							throw new InputMismatchException("Wrong number");
						}
					} catch (InputMismatchException e) {
						System.out.println("Not a valid number, try again!");
						sc.nextLine();
					}

				} while (change <= 0 || change > 3);

				sc.nextLine();
				String changed = "";
				if (change == 1) {
					changed = "firstname";
				} else if (change == 2) {
					changed = "lastname";
				} else if (change == 3) {
					changed = "persnum";
				}
				System.out.println("What do you want the new value to be?");
				System.out.print("Input: ");
				String newData = sc.next();
				members.editMember(pickUser, changed, newData);
				System.out.println("Member updated!");
				ui();
			} else {
				System.out.println("Member was not found, returning to menu!\n");
				ui();
			}
		} else if (choice1 == 3) {
			// Delete a member.
			System.out.println("\nYou selected 3. Delete a member!");
			System.out.println(
					"Please input the name of the user you wish to delete. (Full name e.g 'John Doe') THIS CAN NOT BE REVERTED");
			System.out.print("Input: ");
			String pickUser = sc.nextLine();
			String listMember = members.listMember(pickUser);
			// If listMember is not empty member name was correct, else return
			// to menu.
			if (!listMember.isEmpty()) {
				members.deleteMember(pickUser);
				System.out.println(pickUser + " have successfully been deleted!");
				ui();
			} else {
				System.out.println("Member was not found, returning to menu!\n");
				ui();
			}
		} else if (choice1 == 4) {
			// List a member.
			System.out.println("\nYou selected 4. List a member!");
			System.out.println("Please input which user you wish to view. (Full name e.g 'John Doe')");
			System.out.print("Input: ");
			String pickUser = sc.nextLine();
			// if Member is not found, return to menu.
			String listMember = members.listMember(pickUser);
			if (!listMember.isEmpty()) {
				System.out.println(listMember);
			} else {
				System.out.println("Member was not found, returning to menu!\n");
			}
			ui();
		} else if (choice1 == 5) {
			// List all members.
			System.out.println("\nYou selected 5. List all members!");
			System.out.println("Select what type of list you want.\n1. Compact\n2. Verbose");

			int list = 0;
			do {
				try {
					System.out.print("Input: ");
					list = sc.nextInt();

					if (list < 1 || list > 2) {
						throw new InputMismatchException("Wrong number");
					}
				} catch (InputMismatchException e) {
					System.out.println("Not a valid number, try again!");
					sc.nextLine();
				}

			} while (list < 1 || list > 2);

			sc.nextLine();
			boolean pickList = false;
			if (list == 1) {
				pickList = true;
			} else if (list == 2) {
				pickList = false;
			}
			System.out.println(members.listMembers(pickList));
			ui();
		} else if (choice1 == 6) {
			// Register a boat.
			System.out.println("\nYou selected 6. Register a boat!");
			System.out.println("Please input which user you wish to register a boat to. (Full name e.g 'John Doe')");
			System.out.print("Input: ");
			String pickUser = sc.nextLine();
			String listMember = members.listMember(pickUser);

			// If listMember returns a value, the member is found, else cancel
			// and go back to menu.
			if (!listMember.isEmpty()) {
				System.out.println("What type is the boat being registered? (e.g Airboat, Barge, Dinghy, Canoe)");
				System.out.print("Input: ");
				String boatType = sc.nextLine();
				System.out.println("How long is the boat being registered? (Input a number only, in metres)");
				System.out.print("Input: ");
				int boatLength = sc.nextInt();
				Boat newBoat = new Boat(boatType, boatLength, 0);
				members.registerBoat(newBoat, pickUser);
				System.out.println("Boat registered to member " + pickUser + "!");
				ui();
			} else {
				System.out.println("Member was not found, returning to menu!\n");
				ui();
			}
		} else if (choice1 == 7) {
			// Edit a boat.
			System.out.println("\nYou selected 7. Edit a boat!");
			System.out.println("Please input which user's boat(s) you wish to edit. (Full name e.g 'John Doe')");
			System.out.print("Input: ");
			String pickUser = sc.nextLine();
			String listMember = members.listMember(pickUser);

			// If listMember returns a value, the member is found, else cancel
			// and go back to menu.
			if (!listMember.isEmpty()) {
				System.out.println(listMember);
				System.out.println("Please input the ID of the boat you wish to edit!");
				System.out.print("Input: ");
				String boatID = sc.next();
				sc.nextLine();
				System.out.println("Select what you want to edit.\n1. Type\n2. Length");
				int type = 0;
				do {
					try {
						System.out.print("Input: ");
						type = sc.nextInt();

						if (type < 1 || type > 2) {
							throw new InputMismatchException("Wrong number");
						}
					} catch (InputMismatchException e) {
						System.out.println("Not a valid number, try again!");
						sc.nextLine();
					}

				} while (type < 1 || type > 2);
				sc.nextLine();
				String change = "";
				if (type == 1) {
					change = "type";
				} else if (type == 2) {
					change = "length";
				}
				System.out.println("What do you want the new value to be?");
				System.out.print("Input: ");
				String data = sc.nextLine();
				members.editBoat(pickUser, boatID, change, data);
				System.out.println("Boat's data updated!");
				ui();
			} else {
				System.out.println("Member was not found, returning to menu!\n");
				ui();
			}
		} else if (choice1 == 8) {
			// Delete a boat.
			System.out.println("\nYou selected 8. Delete a boat!");
			System.out.println("Please input which user's boat(s) you wish to delete. (Full name e.g 'John Doe')");
			System.out.print("Input: ");
			String pickUser = sc.nextLine();
			String listMember = members.listMember(pickUser);
			if (!listMember.isEmpty()) {
				System.out.println(listMember);
				System.out.println("Please input the ID of the boat you wish to delete!");
				System.out.print("Input: ");
				String boatID = sc.next();
				members.deleteBoat(pickUser, boatID);
				System.out.println("Boat's deleted!");
				ui();
			} else {
				System.out.println("Member was not found, returning to menu!\n");
				ui();
			}
		} else if (choice1 == 9) {
			System.out.println("Thank you for using the application - exiting...");
			System.exit(0);
		}
		sc.close();
	}

}
