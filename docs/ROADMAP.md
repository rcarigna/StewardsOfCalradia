# Stewards of Calradia Roadmap

> A long-term project to enrich Mount & Blade II: Bannerlord by introducing institutions, communication, and persistent world systems.

## Development Approach

This roadmap is guided by the design pillars in the main [README](../README.md).

Stewards of Calradia is developed alongside separate Bannerlord architecture research. That research informs implementation decisions, but this roadmap tracks the mod itself: its foundations, systems, and gameplay features.

---

## Development Roadmap

### Phase 0 - Bare Bones Foundation

**Goal:** Prove the mod can load custom code in Bannerlord.

Research Focus:

* Module loading
* `MBSubModuleBase`
* Basic build output

Implementation:

* Verify DLL loading
* Create first `MBSubModuleBase`
* Show a simple "Hello World" signal in game
* Establish a basic build and debugging workflow

---

### Phase 1 - Heralds

**Goal:** The world communicates with the player.

Potential Features:

* Tournament announcements
* Active tournament list
* World event notifications
* Royal announcements
* Feast notifications

Research Focus:

* Tournament lifecycle
* Campaign events
* Notification systems
* Event subscriptions

---

### Phase 2 - Couriers

**Goal:** The player communicates with the world.

Potential Features:

* Request meetings with heroes
* Deliver letters
* Invitations
* Delayed message delivery
* Hero responses

Research Focus:

* Hero AI
* Parties
* Campaign behaviors
* Travel simulation

---

### Phase 3 - Institutions

**Goal:** Introduce persistent organizations.

Potential Features:

* Herald's Office
* Courier Service
* Steward's Office
* Guilds
* Royal Archives

Research Focus:

* Persistent campaign entities
* Institution ownership
* Save/load integration

---

### Phase 4 - Registries

**Goal:** The world remembers.

Potential Features:

* Horse registry
* Named horses
* Ownership history
* Stud books
* Animal records

Research Focus:

* Persistent data
* Entity identity
* Encyclopedia integration

---

### Phase 5 - Sunmark

**Goal:** Expand horses from equipment into living entities.

Potential Features:

* Breeding
* Bloodlines
* Genetics
* Temperament
* Training
* Riding Academy
* Elite warhorse program

Research Focus:

* New entity systems
* Simulation
* AI
* Economy integration

---

### Phase 6 - Stewardship

**Goal:** Expand administrative gameplay.

Potential Features:

* Estate management
* Stable masters
* Quartermasters
* Royal appointments
* Administrative improvements

Research Focus:

* Kingdom management
* Clan systems
* Economy
* Governance
