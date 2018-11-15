using System;

namespace ZuulCS {

    public class Player {

      private Room currentRoom;
      internal Room CurrentRoom { get => currentRoom; set => currentRoom = value; }
		  private Int32 health;

      private Inventory inventory;
      internal Inventory Inventory { get => inventory; }

		  public Player() {
        inventory = new Inventory(10);
        health = 100;
      }

		  public double getPlayerHealth() {
			  return this.health;
		  }

		  public double damage(Int32 amount) {
			  this.health -= amount;
			  return health;
		  }

		  private double heal(Int32 amount) {
			  this.health += amount;
			  return health;
		  }

		  private bool isAlive() {
			  return true;
		  }

      /**
         * Try to go to one direction. If there is an exit, enter the new
         * room, otherwise print an error message.
         */
      public void goRoom(Command command) {

        if(!command.hasSecondWord()) {
          // if there is no second word, we don't know where to go...
          Console.WriteLine("Go where?");
          return;
        }

        string direction = command.getSecondWord();

        // Try to leave current room.
        Room nextRoom = currentRoom.getExit(direction);
        // Variable for room

        if (currentRoom.isLocked == true) {
          Console.WriteLine("Door is locked. You need a key to open the door");
        } else if (nextRoom == null) {
          Console.WriteLine("There is no door to "+direction+"!");
        } else {
          damage(1);
          currentRoom = nextRoom;
          Console.WriteLine(currentRoom.getLongDescription());
        }
    }
  }
}
