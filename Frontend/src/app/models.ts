export enum CharacterType {
    BartCassidy,
    BlackJack,
    CalamityJanet,
    ElGringo,
    JesseJones,
    Jourdonnais,
    KitCarlson,
    LuckyDuke,
    PaulRegret,
    PedroRamirez,
    RoseDoolan,
    SidKetchum,
    SlabTheKiller,
    SuzyLafayette,
    VultureSam,
    WillyTheKid
}

export enum CardType
{
    Bang,
    Missed,
    Beer,
    CatBalou,
    Panic,
    Duel,
    GeneralStore,
    Indians,
    Stagecoach,
    Gatling,
    Saloon,
    WellsFargo,
    Jail,
    Mustang,
    Barrel,
    Scope,
    Dynamite,
    Volcanic,
    Schofield,
    Remingtion,
    Karabine,
    Winchester
}

export enum RoleType
{ 
    Outlaw,
    Renegade,
    Sheriff,
    Vice      
}

export enum CardColorType
{
    Spades,
    Clubs,
    Hearts,
    Diamonds
}

export interface Card {
    id: number;
    name: string;
    description: string;
    cardEffectType: string;
    cardType: CardType;
    cardColorType: CardColorType;
    frenchNumber: number;
}

export interface Player {
    id: number;
    userid: string;
    username: string;
    gameboardId: number;
    characterType: CharacterType;
    roleType: RoleType;
    actualHP: number;
    maxHP: number;
    shootingRange: number;
    placement: number;
    handPlayerCards: Card[];
    tablePlayerCards: Card[];
}

export interface GameBoard {
    id: number;
    actualPlayerId: number;
    maxTurnTime: number;
    isOver: boolean;

    players: Player[];
}