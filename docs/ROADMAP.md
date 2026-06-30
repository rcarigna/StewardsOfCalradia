# Stewards of Calradia Roadmap

> A long-term project to enrich Mount & Blade II: Bannerlord by introducing institutions, communication, and persistent world systems.

## Development Approach

This roadmap is guided by the design pillars in the main [README](../README.md).

Stewards of Calradia is developed alongside separate Bannerlord architecture research. That research informs implementation decisions, but this roadmap tracks the mod itself: its foundations, systems, and gameplay features.

---

## Roadmap

### 🌱 Milestone 0 — First Breath (Complete)

**Question:** Can we safely extend Bannerlord?

#### M0 - Player Features

- Initial proof-of-concept module
- Campaign behavior registration
- Basic debugging workflow

#### Technical Foundation

- Module loading
- `MBSubModuleBase`
- Campaign behaviors
- Development workflow

### 👀 Milestone 1 — Observe

**Question:** What information already exists that the player struggles to access?

#### M1 - Player Features

📣 Tournaments

- Active tournament list
- Settlement links
- Map integration (future)

🐴 Horses

- Horse encyclopedia
- Horse categories
- Where breeds can be purchased
- Culture associations

🏘️ Settlements

- Better settlement lookup
- Quick navigation

#### Data Models & Systems

- Campaign models
- Encyclopedia
- Settlements
- Items
- HorseComponent
- Campaign time/events

### 📢 Milestone 2 — Inform

**Question:** How should the world communicate with the player?

#### M2 - Player Features

- Tournament notifications
- Market notifications
- Herald announcements
- Favorite settlement alerts

#### Notification System

- Campaign events
- Notifications
- Event listeners

### 📖 Milestone 3 — Remember

**Question:** What should the world remember over time?

#### M3 - Player Features

🐴 Horse Registry

- Named horses
- Ownership history
- Known locations
- First acquired

🏰 Records

- Visited settlements
- Tournament history
- Personal discoveries

#### Persistence Layer

- Save data
- Persistent entities
- Serialization

### 🏛️ Milestone 4 — Institutions

**Question:** What organizations make Calradia feel alive?

#### M4 - Player Features

- Herald's Office
- Courier Service
- Registry Office
- Steward's Office

#### World Systems

- Persistent world systems
- Campaign behaviors
- AI interactions

### 🐎 Milestone 5 — Horse Husbandry

**Question:** What happens when horses stop being items?

#### M5 - Player Features

- Horse sex
- Age
- Temperament
- Bloodlines
- Breeding
- Training
- Sunmark Riding Academy

#### Simulation Framework

- New entity systems
- Simulation
- AI
- Economy integration
