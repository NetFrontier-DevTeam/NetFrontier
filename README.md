Net Frontier

Net Frontier is an open-source multiplayer 2.5D platformer MMORPG project. This repository focuses on developing both server-side and client-side code to create a fully functional multiplayer game with scalable server architecture.
Project Overview

Net Frontier aims to create a networked multiplayer experience where players can connect to a dedicated server, create characters, and interact in an online world. It includes both the server-side logic to manage game sessions, player data, and patch management, as well as client-side code for character creation, movement, and interaction.

The project is being developed using Unity with a focus on smooth, real-time gameplay across multiple connected players.
Features:

    Dedicated Server: Manage player sessions, game logic, and synchronize client data.
    Client-Side: Character creation, player movement, interaction, and network communication.
    Patch Management: Sync client data with the server and manage updates.
    Open Source: Welcoming contributions from developers interested in server/client architecture, gameplay mechanics, and network optimization.

Getting Started
Prerequisites

Before you begin, ensure you have the following installed:

    Unity (version 2021 or later recommended)
    Git
    Basic knowledge of C# and Unity is recommended for contributors.

Setup Instructions

    Clone the Repository:

    git clone https://github.com/YourUsername/NetFrontier.git
     Open the Project in Unity:
         Once the repository is cloned, open Unity Hub, click on the "Open" button, and navigate to the folder where you cloned the project. Select it to open the project in Unity.

    2. Install Dependencies:
         The project uses Unity's Netcode for GameObjects (or another chosen networking solution). You may need to install the required packages via Unity's Package Manager.
         Navigate to Window > Package Manager in Unity, search for the networking packages (such as "Netcode for GameObjects"), and install them if they aren’t already present.

    3. Set Up a Dedicated Server:
         To run a dedicated server, navigate to the Server folder in the project. You can configure and run the server locally or on a remote machine.
         Build the project with the "Server" build target selected to create the server executable.

    4. Run the Client:
         Once the server is running, open a new Unity instance and run the client-side code. The client will attempt to connect to the server.
         For testing, the client and server can both run on the same machine (localhost), or you can set the IP of the server to a remote machine's IP if hosted elsewhere.

    Character Creation & Lobby: After logging in, the client should navigate to the character creation screen. This communicates with the server to store character data. Once a character is created, it will load into the lobby with the ability to see other players connected to the same server.

    Patch Management: Upon startup, the client will check for any patches or updates from the server and sync the data accordingly.

Contributing

We welcome contributions to Net Frontier! To contribute:

Fork the repository on GitHub.
Create a feature branch (git checkout -b feature/YourFeatureName).
Commit your changes (git commit -m 'Add new feature').
Push to the branch (git push origin feature/YourFeatureName).
Open a pull request for review.

License

This project is licensed under the MIT License - see the LICENSE file for details.
