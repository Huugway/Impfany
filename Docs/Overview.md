# Impfany rule-set and overview
## Player:

- 3 weapon slots
- Attack progression is cycling through weapon 1 -> 2 -> 3
- to level up attack progression, the next attack has to be casted in a short time frame after the previous attack or the progression is reset to the first state (around 4 seconds * might be adjusted)
- Player is able to dash
- The dash grants invincibility frames and is upgradable after boss waves or by specific Items
- Player is able to collect Souls in the *Soul-lantern* 

## Weapons:

- Weapons are tied to specific slots:

- The first slot is the *Defense* Weapon 
- -> might be able to knock away enemy's, reflect or destroy projectiles or grant temporary invincibility frames

- The second slot is the *Effect* Weapon
- -> might be able to stun enemy's, poison or control them in any way

- The third slot is the *Precision* Weapon
- -> is able to deal heavy damage but will be a harder to hit, rather single focused attack

- Weapons are upgradable via *Cards*:
- Cards will be dropped by enemy's
- Ace Cards will exclusively be dropped by mini-bosses
- Wildcards will be dropped by bosses
- Once per wave you can convert any 2 Cards to a number card of your liking on the *devil's table* (excluding face cards, aces or wildcards)
- Weapons have 4 card stack slots (one for each symbol)
- Card stack slots have to be unlocked by placing an Ace of the corresponding symbol
- Aces will be stuck on the Weapon and are not able to be redistributed like other cards.
- Wildcards are able to transform into the corresponding ace card if placed in the ace slot, but wont be able to be redistributed, just like normal aces.
- Normal cards can stack upon the predecessor card of them, just like Solitaire
- the effect the Cards cast on the weapon is tied to their symbol (the effect is enhanced to the count of the card stack):
- -> Spade will enhance the weapons damage
- -> Heart will enhance the number of *souls* gained by the weapon when dealing damage (*Soul lantern*)
- -> Diamond will enhance the stun or recovery time of enemy's after being hit by the weapon
- -> Club will enhance the knock back of the weapon

## Soul Lantern

- The Soul lantern will be charged up by dealing damage to enemy's
- After filling the lantern, the charge will be reset and the player will regain a health point (Half a Heart)
- If the Player hasn't lost any health, the lantern will keep it's charge until he gets hit.
- While being fully charged the lantern will buff the Player.
- Collecting Souls can be enhanced by Items or Weapons with Heart upgrades

## Arena:

- Round Arena with 4 entrances surrounded by a lava pit.
- The Lava pit will kill enemy's and deal damage to the Player.
- During waves enemy's will come through the 4 entrances.
- After each wave, the player is able to choose one of the "entrances" to get stronger.

- The North entrance is a Shop, selling 2 weapons and a choice of 3 Items and another item of rarer quality for gold
- The Shop is rerollable for gold
- Rerolling will increase the cost for another reroll (gets reset after each wave)
- You can sell Items or weapons for a bit of gold

- The East entrance is the room of the *Devils Table* 
- Here you are able to distribute or redistribute your deck onto the weapons
- Once per wave you are able to exchange 2 cards for one of your liking (excluding face cards, aces or wildcards).
- The card dealer is the devil and thus the end boss of the first level.

- The West entrance might be a place to upgrade your character at given waves?
- The South entrance might be the main entrance of the Arena, not serving a specific purpose?

- After leaving the chosen room, the next wave will start.

## Items:

- Items can be bought at the Shop
- Items of lower quality will only enhance base stats
- Items of higher quality are able to give certain effects to the Player or Weapon slots
- You can hold an infinite number of items and certain items might interact with each other
- Items are part of a set
- if you collect many items of one set, you can get specific bonus stats corresponding to the collected set
- some sets might only give bonus stats if you only hold one or a limited given number of items from the set.

## Enemy's:

- Enemy's spawn in waves
- Waves will become increasingly more difficult by spawning more and stronger enemy's
- Enemy's will drop souls when hit
- Enemy's will drop gold and or cards when being killed
- Enemy's will have different attack patterns depending on their type
- After each 5th wave a mini Boss will spawn, dropping an ace.
- After each 10th wave a Boss will spawn, dropping a wildcard.

## Other Information

- The game is a wave based 2D top down arena fighter
- Focus of the games is the chain based attack pattern, casting special effects and unique upgrade mechanics
- You play in an arena in hell against imps and other servants of the devil.
- The devil and servants have a crook or thug appearance, explaining the theme of card games.
- Your goal is to defeat the devil, by reaching wave 20, where the devil (the dealer at the devils table) snaps and starts to fight against you.

- We are using the Unity editor version 6.3 LTS (6000.3.28f1) and are pushing via GitHub Desktop.

- Owner 1 (Hugo) is responsible for Player & game feel
- -> Movement, input, attacks, hit feedback, camera shake etc.
- (Link to Hugo.md)

- Owner 2 (Maxim) is responsible for Enemies & spawning
- -> Enemy types, state machines, wave curve, pooling
- (Link to Maxim.md)

- Owner 3 (Jannis) is responsible for Progression & balance
- -> XP, level-up choice, upgrades, BalanceConfig
- (Link to Jannis.md)

- Owner 4 (Merlin) is responsible for Flow, UI & audio
- -> Start, HUD, pause, game over, restart, sound
- (Link to Merlin.md)