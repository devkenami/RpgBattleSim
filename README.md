# RpgBattleSim

Classmates RPG Battle Simulator
===============================

Description of Characters:
--------------------------
1. CamilleDebugger:
   - A precise and strong attacker with consistent damage (15-25).
2. KensemiCollon:
   - Agile and quick attacker with moderate damage (10-18) and a chance for critical hits.
3. LanceBackend:
   - Heavy hitter with slow but powerful strikes (20-30 damage) and a small chance to miss.
4. JeffPancitCanton:
   - Balanced attacker who deals moderate damage (12-22) and sometimes heals self after attack.

How OOP Principles Are Applied:
-------------------------------
- **Abstraction**: The abstract class `StudentHero` defines common properties (Name, Health) and an abstract method `Attack()`, which forces derived classes to provide their own implementation.
- **Encapsulation**: Health is encapsulated using a private field with public property getters/setters that prevent invalid health values (e.g., negative health).
- **Inheritance**: Each character class (`CamilleDebugger`, `KensemiCollon`, etc.) inherits from `StudentHero` and extends/implements specific behavior.
- **Polymorphism**: The `Attack()` method is overridden in each subclass to provide different attack behaviors. The battle system calls `Attack()` on the base class reference but the derived class's method executes.

Challenges Faced:
----------------
- Managing UI updates in a responsive way during the battle, such as updating health labels and appending logs in a readable sequence.
- Handling character image loading dynamically without causing file locks or errors.
- Designing character attacks that balance randomness and fairness for gameplay fun.
- Implementing proper input validation to prevent empty names or unselected characters before starting a battle.
- Ensuring health values do not go below zero or above max health during damage and healing.

---
