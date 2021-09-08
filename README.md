Backend kialakítás konvenciók:
- usingra figyelni
   - törölni ami nem kell, minden ABC sorrend
   - elől az project referenciák
   - utána System
   - utána Microsotft
   - utána minden egyéb

- Queryk, Command -> Get{LekérdzésNév}Query.cs/Update{LekérdzésNév}Command.cs pl: GetCharacterTypeQuery.cs
- Handler -> {Név}QueryHandler.cs/{Név}CommandHandler.cs pl: CharacterQueryHandler.cs
- ViewModel, Dto {Név}ViewModel.cs/{Név}Dto.cs pl: CharacterViewModel.cs

- Queryn, Commandon belüli típus mappákat nem jelölni a namespaceben
   - pl: Queries -> Card -> Handlers a mappa struktúra --> namespace ...Queries.Handlers
