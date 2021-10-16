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
    Jail, //Passive
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

export enum HoverEnum {
    Card,
    Role,
    Character
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

export interface PlayCardDto {
    playerCardId: number;
    targetPlayerId?: number;
    targetPlayerCardId?: number;
}

export interface Role {
    id: number;
    name: string;
    description: string;
    roleType: RoleType;
}

export interface Character {
    id: number;
    name: string;
    description: string;
    characterType: CharacterType;
}

export interface Player {
    id: number;
    userId: string;
    userName: string;
    gameBoardId: number;
    characterType: CharacterType;
    roleType: RoleType;
    actualHP: number;
    maxHP: number;
    shootingRange: number;
    placement: number;
    handPlayerCards: Card[];
    tablePlayerCards: Card[];
}

export interface OtherPlayer {
    id: number;
    userId: string;
    userName: string;
    gameBoardId: number;
    characterType: CharacterType;
    roleType: RoleType;
    actualHP: number;
    maxHP: number;
    shootingRange: number;
    placement: number;
    handPlayerCardCount: number;
    tablePlayerCards: Card[];
}

export interface GameBoard {
    id: number;
    actualPlayerId: number;
    targetedPlayerId: number;
    maxTurnTime: number;
    isOver: boolean;

    ownPlayer: Player;
    otherPlayers: OtherPlayer[];

    lastDiscardedGameBoardCard: Card;
}

export interface PostGameBoard {
    maxTurnTime: number;
    userIds: {userId: string, userName: string}[];
}

export interface LoginDto {
    username: string;
    password: string;
    grant_type?: string;
    client_id?: string;
    client_secret?: string;
    scope?: string;
}

export interface LoginResponse {
    access_token: string,
    refresh_token: string,
    detail: string,
    expires_in: number
}

export interface RegistrationDto {
    username: string;
    password: string;
    confirmedPassword: string;
}

export interface Permissions {
    canDoAnything: boolean;
    canPlayCard: boolean;
    canDrawCard: boolean;
    canDiscardCard: boolean;
    canPlayBangCard: boolean;
    canLoseHealth: boolean;
    canPlayMissedCard: boolean;
    canDiscardFromDrawCard: boolean;
    canDrawFromDiscardCard: boolean;
    canTargetPlayers: boolean;
    canDrawFromMiddle: boolean;
    canSeeMiddleCards: boolean;
    canDrawFromOthersHands: boolean;
    canDrawFromOthersTable: boolean;
    canPlayBeerCard: boolean;
    canUseBarrelCard: boolean;
}

export interface TargetPermission {
    canTargetPlayers?: boolean;
    canTargetCards?: boolean;
    canDrawFromMiddle?: boolean;
}