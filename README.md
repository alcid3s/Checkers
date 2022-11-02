# Checkers
This project is based on the raylib library for C#. This variant of Checkers is based on the English variation.

# Playing
To play the game open the solution file in microsoft visual studio and build the game. Then run it.
To host a game click 'play game' and then click 'host game'. To join a game click 'play game' and then click 'join game',
write down the IP address and port of the person hosting the game and enjoy playing.

# code structure
The code structure is rather straight forward. A Screenmanager is made and contains a draw and update method.
The update and draw both contain switch statements to check what state the game is in, or rather, what state the menu is in.

There are some custom made object like buttons, inputboxes and checkboxes. Those are used for different screens which make
human input possible.

If the player wants to host a game and clicks on the button, a server will be instantiated and will be listening on the local
ip address of the machine. The port number will always be 1337 and cannot be changed unless you alter the code.

To join a game you'll have 2 input boxes, the input box will only be active if one hovers over it with a mousebutton.
There are possible errors when joining a game, those errors occur due to the firewall of the computer not trusting the connection.
This happens on the client and server side.

Once in the game the pieces with possible moves will be highlighted, if only one piece is highlighted there's most likely a forced capture.
The game logic is mostly found in the pieceManager class.

Once the game is finished it'll show a winner screen for the person who won and a loser screen for the person that lost the game.
You can safely exit the game by using the exit button.