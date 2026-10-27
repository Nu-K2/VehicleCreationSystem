**English** | [Русский](README.md)
# README

## Practical Work No. 2: Implementing the Creational "Abstract Factory" Pattern in C#

---

## Project Description

A console application **"Vehicle Creation System"** in C#, demonstrating the operation of the creational design pattern **"Abstract Factory"**.

The program allows the user to create families of related objects (cars and motorcycles) with different engine types (electric and gasoline), manage them, and perform type-specific actions.

---

## Objective of the Work

To consolidate theoretical knowledge on the topic "Purpose and Types of Patterns" and to gain practical skills in implementing the "Abstract Factory" pattern in a console application in C#.

---

## Architecture

### Vehicle Interfaces

| Interface | Methods | Purpose |
|-----------|--------|------------|
| `IVehicle` | `Start()`, `Stop()`, `GetInfo()`, `Id` | Base interface for all vehicles |
| `ICar` | `OpenTrunk()`, `CloseTrunk()` | Additional car methods |
| `IMotocycle` | `DoWheelie()` | Additional motorcycle method |

### Class Hierarchy

```
IVehicle
├── Vehicle (abstract)
│   ├── Car (abstract) : ICar
│   │   ├── ElectricCar
│   │   └── GasCar
│   └── Motocycle (abstract) : IMotocycle
│       ├── ElecticMotocycle
│       └── GasMotocycle
```

### Factories

| Factory | Creates |
|---------|---------|
| `ElectricVehicleFactory` | `ElectricCar`, `ElecticMotocycle` |
| `GasVehicleFactory` | `GasCar`, `GasMotocycle` |

---

## Functionality

### Main Menu

| Item | Action |
|-------|----------|
| 1 | Create a car (with engine type selection) |
| 2 | Create a motorcycle (with engine type selection) |
| 3 | Show all vehicles |
| 4 | Start all vehicles |
| 5 | Stop all vehicles |
| 6 | Clear the list |
| 7 | Actions with a specific vehicle (by ID) |
| 8 | Exit the program |

### Actions with a Specific Vehicle (Item 7)

**For a car:**
- Start
- Stop
- Open the trunk
- Close the trunk

**For a motorcycle:**
- Start
- Stop
- Perform a trick (Wheelie)

---

## Implementation Features

### Business Logic (`VehicleManager`)

- Stores the list of created vehicles (`List<IVehicle>`)
- `CreateVehicle(string type, IVehicleFactory factory)` — creates a vehicle via the factory
- `ShowAllVehicles()` — displays information about all vehicles
- `StartAllVehicles()` / `StopAllVehicles()` — group management
- `ClearAllVehicles()` — clears the list
- `FindVechicle(int id)` — finds a vehicle by ID

### State Logic

- **A car cannot be started** if the trunk is open
- **The trunk cannot be opened** if the car is moving
- **A trick cannot be performed** if the motorcycle is stationary
- Repeated start/stop displays a corresponding notification

### Automatic Identification

Each vehicle is assigned a unique `Id` upon creation (a static counter `_nextId`).

---

## Running

```bash
dotnet run
```

**Requirements:** .NET 6.0 or higher

---

## Compliance with the Technical Specification Requirements

| No. | Requirement | Status |
|---|-----------|--------|
| 1 | Implementation of the "Abstract Factory" pattern | ✅ |
| 2 | Core business logic (`VehicleManager`) | ✅ |
| 3 | Handling of all menu items | ✅ |
| 4 | Confirmation of actions with the number of vehicles | ⚠️ |
| 5 | Demonstration of the pattern's operation in `Main` | ⚠️ |

> **Note:** For full compliance with requirements No. 4 and No. 5, it is recommended to:
> - Add the current number of vehicles in the manager to the confirmation messages
> - Add a demonstration block to `Main` that creates instances of both factories and displays information about the created vehicles

---

## Technologies Used

- **Language:** C#
- **Platform:** .NET
- **Pattern:** Abstract Factory (creational)
- **Principles:** SOLID, encapsulation, polymorphism
