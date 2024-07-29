# Tic Tac Toe Game

## Overview

This project is a fully functional Tic Tac Toe game implemented using Unity. The game includes **multiplayer functionality**
for two players, **PlayFab authentication**, **play with computer mode** and **data collection through PlayFab**. The project showcases best practices in
Unity development, emphasizing **clean code architecture**, **maintainability**, and **scalability**.

<img src="Recordings/global.gif" width="300" />

## Features

- **Multiplayer Mechanic**: Two players can take turns placing their symbols (X or O) on a 3x3 grid.
- **PlayFab Integration**: Authentication and data collection are handled using PlayFab.
- **Game State Management**: Detects and declares a winner or a draw, and keeps track of each player's win count.
- **User Interface**: Displays the game board, player turn indicators, results, and includes a reset button to restart
  the game.

## Game Mechanics

### Grid and Turns

- A grid is provided where players can place their symbols.
- Players take turns to place their symbols on the grid.

### Win Conditions

- The game detects and declares a winner when a player gets three of their symbols in a row horizontally, vertically, or
  diagonally.
- If all spots are filled without a winner, the game declares a draw.

### Score Tracking

- The game keeps track of the number of wins for each registered player in Play Fab. To check the number of wins open the settings tab

<img src="Recordings/settings-tab.jpg" width="150" />

## User Interface

### Game Board

- The game board displays a grid for players to place their symbols.
- Player turn indicators show whose turn it is to play.

### Result Display

- Displays the result of the game (win/draw) at the end of each match.

## Architecture

### Scalability and Maintainability

- The game uses a microservices-based architecture, ensuring scalability and maintainability.
- Game logic is separated from UI logic, adhering to best practices.
- The project implements appropriate design patterns and follows the SOLID principles.
- Asynchronous implementations are used to enhance performance.

## Additional Features

### AI Opponent

- The game includes an AI opponent for single-player mode.

### PlayFab Integration

- PlayFab is integrated for user authentication and data collection.

### Multiplayer Mode

- The game supports multiplayer mode for two players.

## Getting Started

### Prerequisites

- Unity 2019.4 or later

### Build Testing

[click me to download](https://github.com/)

### Installation

1. Clone the repository.
2. Open the project in Unity.

### Running the Game

1. Open the project in Unity.
2. Press the Play button in the Unity Editor to start the game.

## Design Notes

- The project demonstrates clean code practices and is structured for easy understanding and modification.

## Contacts

For any questions or further information, please contact:

- **Developer Name**: Pavlo Myrskyi
- **Email**: mirskiy.p.2002@gmail.com
- **LinkedIn**: https://www.linkedin.com/in/pavlo-myrskyi-gamedev/

## Conclusion

This Tic Tac Toe game project not only meets the basic requirements but also extends functionality with PlayFab
integration, AI gameplay and multiplayer mechanics, showcasing robust architecture and clean coding practices.