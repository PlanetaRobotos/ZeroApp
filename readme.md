# Tic Tac Toe Game

## Overview

This project is a fully functional Tic Tac Toe game implemented using Unity. The game includes **multiplayer functionality**
for two players, **PlayFab authentication**, **play with computer mode** and **data collection through PlayFab**. The project showcases best practices in
Unity development, emphasizing **clean code architecture**, **maintainability**, and **scalability**.

<img src="Recordings/global.gif" width="400" />

## Features

- **Multiplayer Mechanic**: Two players can take turns placing their symbols (X or O) on a 3x3 grid.
- **PlayFab Integration**: Authentication and data collection are handled using PlayFab.
- **Game State Management**: Detects and declares a winner or a draw, and keeps track of each player's win count.
- **Single-Player Mode**: Includes an AI opponent for single-player mode.
- **User Interface**: Displays the game board, player turn indicators, results, and includes a reset button to restart
  the game.

## Getting Started

### Prerequisites

- Unity 2019.4 or later

### Installation

1. Clone the repository.
2. Open the project in Unity.

### Build Testing

- The game can be tested by downloading the build from the following link: [Tic-Tac-Toe Build](https://github.com/PlanetaRobotos/applications/blob/master/tictactoe/tictactoe-app.zip)
- To test the game, download the build or build from unity project and run the executable file in two different windows to play in multiplayer mode.

<img src="Recordings/multiplayer-setup.gif" width="400" />

## Project Structure

- **AI**: Contains scripts related to the artificial intelligence opponent, enabling single-player mode.
- **Core**: The central module that houses the main game logic, including the wins-tracker and boards for network and AI gameplay.
- **GameStates**: Manages different states of the game.
- **Models**: Defines the data models used in the game, including player data and game board configurations.
- **Networking**: Handles all networking-related functionality, enabling multiplayer features using the Photon Fusion package.
- **Windows**: Contains scripts for various UI windows and dialogs within the game.

## Key Features

### Game Start Point
- **ApplicationStateMachine**: The class where the game starts and transitions between different states.
- **LoadApplicationState**: Manages the application load flow.

### Main Registration Flow
- **BoardRegistrator**: Handles the registration and setup of game boards.
- **PlayerRegistrator**: Manages player registration and initialization.

### Authorization and Data Collection
- Implemented using **PlayFab**, which handles player authorization and collects data such as the win counter.

### Multiplayer Flow
- Implemented using the **Photon Fusion package**, enabling real-time multiplayer functionality.

### Asset Management
- All assets are loaded using **Addressables**, ensuring efficient memory management and resource loading.

### Mediator Layer
- Separates the UI from the game logic, promoting a clean architecture and easier maintenance.

## Design Patterns Used

- **Observer**: Used for UI updates and notifications.
- **Factories**: Employed to load models dynamically.
- **State**: Manages the gameplay loop and transitions between different game states.
- **Dependency Injection (DI)**: Facilitates selecting gameplay modes (single and multiplayer).

## Third-Party Packages

- **DoTween**: Used for UI animations, providing smooth and performant animations.
- **UniRx**: Implements reactive programming in Unity, enhancing event handling and state management.
- **UniTask**: Simplifies asynchronous operations, making code easier to read and maintain.
- Custom packages for additional functionality.

## Contacts

For any questions or further information, please contact:

- **Developer Name**: Pavlo Myrskyi
- **Email**: mirskiy.p.2002@gmail.com
- **LinkedIn**: https://www.linkedin.com/in/pavlo-myrskyi-gamedev/

## Conclusion

This Tic Tac Toe game project not only meets the basic requirements but also extends functionality with PlayFab
integration, AI gameplay and multiplayer mechanics, showcasing robust architecture and clean coding practices.