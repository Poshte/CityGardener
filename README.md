# CityGardener 🌱🏙️

**CityGardener** is a 2D top-down resource management and urban planning game built in **Unity** with **C#**. Players take on the role of a city manager tasked with balancing urban growth, industrial production, and environmental sustainability.

---

## 🎮 Core Gameplay & Simulation Mechanics

* **Grid-Based Construction & Infrastructure:** Place residential houses, factories, and irrigation networks using an interactive placement system with real-time preview and validation.
* **Environmental & Economic Simulation:** Invest your wealth to expand your city while balancing economic growth with environmental sustainability.
* **Botanical Systems:** Plant and cultivate multiple tree species using tools such as a shovel, watering can, and drip-pipe networks.
* **Inventory & Action Bar System:** Equip tools and seeds through a modular inventory and construct buildings via dedicated UI action bars.
* **Level Progression & Objectives:** Unlock and play through structured levels with dynamic goal tracking and interactive onboarding.

---

## 🏗️ Key Technical Highlights
* **Software Architecture:** The codebase is organized around modular, object-oriented principles to ensure maintainability and decoupled communication between systems.
* **Event-Driven Architecture:** Uses `GameEvents` to decouple simulation state changes (pollution levels, wealth updates, goal completion) from UI updates and visual feedback.
* **Object-Oriented Design:** Leverages interfaces and abstract base classes to make adding new items, tools, and entities straightforward.
* **Responsive UI & Input Handling:** Supports both mouse hover interactions and keyboard shortcuts with dynamic canvas state switching.

---

## 🛠️ Tech Stack

* **Engine:** Unity (2D)
* **Language:** C#
* **Version Control:** Git & GitHub

---

## 👥 Collaboration & Credits

* **Alireza Poshtahani** — Gameplay & simulation design, software architecture, UI systems, and level design.
* **Mahdis Choobdar** — 2D graphical assets, sprites, visual effects, and animations.

---

## 🚀 Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/Poshte/CityGardener.git
   ```
2. Open the project folder in **Unity Hub** (recommended Unity 2022 LTS or newer).
3. Load the main menu scene from `Assets/Scenes` and press **Play**.
