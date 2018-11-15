using System;
using System.Linq;
using System.Collections.Generic;

namespace ZuulCS {
	public class Game {

		private Parser parser;
		private Player player;

		public Game () {
			parser = new Parser();
			player = new Player();
			createRooms();
		}

		private void createRooms() {
			Room Isolation, Foodcourt, Detentionblock, Wardensoffice, Prisonhospital, Outside, Wall;

			// create the rooms
			Isolation = new Room("in the isolation cell");
			Foodcourt = new Room("in the food court");
			Detentionblock = new Room("in the detention block");
			Wardensoffice = new Room("in the Wardens office");
			Prisonhospital = new Room("in the prison hospital");
			Outside = new Room("You you need to climb the rope to get the wall");
			Wall = new Room("You are now on the wall. You made it");

			// initialise room exits
			Isolation.setExit("up", Foodcourt);
			Isolation.Inventory.AddItem(new Key("Key", 1, "Its a rusty old key"));
			Isolation.isLocked = true;

			Foodcourt.setExit("west", Detentionblock);

			Detentionblock.setExit("up", Wardensoffice);
			Detentionblock.hasTrap = true;

			Wardensoffice.setExit("east", Prisonhospital);
			Wardensoffice.Inventory.AddItem(new Key("Hospital key", 1, "Its the key for the prison hospital!"));
			Wardensoffice.isLocked = true;

			Prisonhospital.setExit("forward", Outside);

			Outside.setExit("Climb", Wall);

			player.CurrentRoom = Isolation;  // start game outside

			if (player.CurrentRoom.hasTrap == true) {
				Trap();
			}

			if (player.CurrentRoom == Wall) {
				Console.WriteLine("You are free!");
				Console.WriteLine("Thanks for playing Out Break!");
			}
		}


		/**
	     *  Main play routine.  Loops until end of play.
	     */
		public void play()
		{
			printWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
			bool finished = false;
			while (! finished) {
				Command command = parser.getCommand();
				finished = processCommand(command);
			}

			Console.WriteLine("Thank you for playing.");
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Out break!");
			Console.WriteLine("Out break is a new, incredibly boring prison break game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.CurrentRoom.getLongDescription());
		}

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

			string commandWord = command.getCommandWord();
			switch (commandWord) {
				case "help":
					printHelp();
					break;
				case "go":
					player.goRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
				case "look":
					Console.WriteLine(player.CurrentRoom.getLongDescription());
					ReturnInv();
					break;
				case "health":
					Console.WriteLine("Your health: " + player.getPlayerHealth());
					break;
				case "inventory":
					RetPlayerInv();
					break;
				case "take":
					TakeItems();
					break;
				case "drop":
					DropItems();
					break;
				case "use":
					UseItem();
					break;
			}

			return wantToQuit;
		}

		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
		private void printHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
		}

		private void ReturnInv() {
			Console.WriteLine("\nItems in room: \n");
			for (int i = 0; i < player.CurrentRoom.Inventory.Items.Count; i++) {
				Console.WriteLine("Item: " + (i + 1) + " | " + player.CurrentRoom.Inventory.Items[i].GetName + " | " + player.CurrentRoom.Inventory.Items[i].GetDescription);
			}

			if (player.CurrentRoom.Inventory.Items.Count <= 0) {
				Console.WriteLine("Currently 0 items in the room");
			}
		}

		//Take items function
		private void TakeItems() {
			if (player.CurrentRoom.Inventory.Items.Count >= 0) {
				Console.WriteLine("Taking: ");
				for (int i = player.CurrentRoom.Inventory.Items.Count - 1; i > -1; i--) {
					Console.WriteLine((i + 1) + " | " + player.CurrentRoom.Inventory.Items[i].GetName + " | " + player.CurrentRoom.Inventory.Items[i].GetDescription);
					int curItems = player.CurrentRoom.Inventory.Items.Count;
					player.CurrentRoom.Inventory.TakeItem(player.Inventory, i);
				}
				Console.WriteLine("\n");
			}

			if (player.CurrentRoom.Inventory.Items.Count <= 0) {
				Console.WriteLine("Currently 0 items to pick up!");
			}
		}

		private void DropItems() {
			Console.WriteLine("You dropped: \n");
			for (int i = player.Inventory.Items.Count - 1; i > -1; i--) {
				Console.WriteLine((i + 1) + " | " + player.Inventory.Items[i].GetName + " | " + player.Inventory.Items[i].GetDescription);
				int curItems = player.Inventory.Items.Count;
				player.Inventory.DropItem(player.CurrentRoom.Inventory, i);
			}
			Console.WriteLine("\n");
		}

		private void RetPlayerInv() {
			Console.WriteLine("Inventory contains: \n");
			if (player.Inventory.Items.Count >= 1) {
				for (int i = 0; i < player.Inventory.Items.Count; i++) {
					Console.WriteLine("Item: " + (i + 1) + " | " + player.Inventory.Items[i].GetName + " | " + player.Inventory.Items[i].GetDescription);
				}
			}

			if (player.CurrentRoom.isLocked == true) {
				Console.WriteLine("Door is locked");
			}

			if (player.Inventory.Items.Count <= 0) {
				Console.WriteLine("Currently 0 items in your bag");
			}
		}

		private void UseItem() {
			for (int i = player.Inventory.Items.Count - 1; i >= 0; i--) {
				if (player.Inventory.Items[i].GetName == "Key" || player.Inventory.Items[i].GetName == "Hospital key") {
					if (player.CurrentRoom.isLocked == true) {
						Key key = new Key("Key", 5, "Its a key");
						key.Use(player.CurrentRoom);
					} else {
						Console.WriteLine("You can already access the room");
					}
			  } else {
				Console.WriteLine("You dont have key in your inventory");
				}
			}
		}

		private void Trap() {
			Console.WriteLine("Shit! Its a trap");
			player.CurrentRoom.HasTrap();
		}
	}
}
