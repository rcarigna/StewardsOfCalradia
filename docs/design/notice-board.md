# Design Note: Town Notice Board

## Purpose

Introduce the Notice Board as the player's primary source of public information within settlements.

Rather than exposing game state through a dedicated mod UI, the Notice Board presents information as notices that naturally belong within the world.

The first supported notice type is tournament announcements.

## Institution

Every town contains a Notice Board.

Official notices are posted by the town's herald or governing authority.

Private notices may later be posted by citizens, guilds, and the player.

## Initial Notice Types

Tournament Announcements

### Future releases may add

- Horse sales
- Horses at stud
- Courier messages
- Guild notices
- Recruitment
- Royal proclamations
- Festivals
- Rumors

### Example

> TO ALL WHO SEEK HONOR
>
> Hear ye! Hear ye!
>
> Lord Aldric invites all brave men and women
> to Pravend to compete in a grand tournament.
>
> Festivities shall conclude on the
> 12th day of Summer.
>
> Rewards shall be bestowed upon the worthy.

### Milestone 1 Acceptance Criteria

- Every town menu exposes a Notice Board interaction.
- The board displays every currently active tournament.
- Selecting a notice opens the corresponding settlement encyclopedia page.
- Notices use localized, immersive text rather than raw game data.
- The implementation does not modify tournament behavior.
