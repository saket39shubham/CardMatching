#Project Title & Overview

Title: Card Matching Game – Unity Prototype

Short Description:

A memory-based card matching game built from scratch in Unity 2021 LTS for Desktop and Android.

Objective / Purpose:

Designed to demonstrate coding skills, smooth gameplay, dynamic layouts, and UI/UX in Unity.

#Features

Multiple board layouts (2x2, 2x3, 4x4)

Card flip animations with smooth transitions

Match detection with opacity reduction for matched cards

Score tracking system and move counter

#Scripts:

GameManager.cs → Game logic, moves, score, timer, layout selection

BoardManager.cs → Manages the grid, shuffling, card creation

CardController.cs → Individual card behavior, flipping, animation, opacity

AudioManager.cs → Handles sounds

Prefabs:

Card prefab with front/back visuals and CanvasGroup for opacity

UI:

Dropdown for layout selection, timer, moves counter, score display, Game Over panel

Game Over panel showing final score

Audio feedback: flip, match, mismatch, game over

Continuous card flipping without waiting for card comparison

Fully resettable board for layout changes
