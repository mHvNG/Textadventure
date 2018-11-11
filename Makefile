all: run

Zuul:
	@mcs Zuul.cs Game.cs Parser.cs Player.cs Room.cs Command.cs CommandLibrary.cs Key.cs Item.cs Knife.cs Inventory.cs Weapon.cs Keys.cs

clean:
	@rm -f Zuul.exe

run: Zuul
	@mono Zuul.exe
