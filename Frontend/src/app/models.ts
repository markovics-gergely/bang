import { OwnboardComponent } from "./pages/game/ownboard/ownboard.component";

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
    targetablePlayers: number[];
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
    lobbyOwnerId: string;
    actualPlayerId: number;
    targetedPlayerId: number;
    maxTurnTime: number;
    isOver: boolean;

    ownPlayer: Player;
    otherPlayers: OtherPlayer[];

    lastDiscardedGameBoardCard: Card;
    scatteredGameBoardCards: Card[];
}

export interface PostGameBoard {
    maxTurnTime: number;
    lobbyOwnerId?: string;
    userIds?: UserId[];
}

export interface UserId {
    userId: string; 
    userName: string;
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

export interface Account {
    id: string;
    userName: string;
}

export interface Friend {
    id: string;
    name: string;
    invitedFrom: boolean;
}

export interface Room {
    name: string;
    creationDate: string;
    requiresPasskey: boolean;
}

export interface Message {
    text: string;
    userName: string;
}

export interface Lobby {
    id: number;
    password: string;
    lobbyOwner: string;
}

export interface StatusViewModel {
    lobbyId: number;
    gameBoardId: number;
}

export interface HistoryViewModel {
    id: number;
    placement: number;
    createdAt: Date;
    roleType: RoleType;
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
    canEndTurn: boolean;
}

export interface TargetPermission {
    canTargetPlayers?: boolean;
    canTargetCards?: boolean;
    canDrawTargetScattered?: boolean;
    canTargetOwnCards?: boolean;
}

export enum TargetType {
    None,
    TargetPlayer,
    TargetCard,
    TargetPlayerOrCard
}

export enum PlayerHighlightedType {
    Actual,
    Targeted,
    None
}

export enum CardActionType {
    Play,
    Discard, 
    None
}

export enum TargetReason {
    Bang,
    Duel,
    Gatling,
    GeneralStore,
    Indians,
    KitCarlsonDraw
}

export enum PhaseEnum {
    Discarding,
    Drawing,
    Playing,
    Throwing
}

export interface ServiceDataTransfer {
    gameboard?: GameBoard;
    player?: Player;
    targetPermission?: TargetPermission;
    permissions?: Permissions;
    ownboard?: OwnboardComponent;
}

export enum PermissionQueryType {
    CanSwitchPlayMode,
    CanEndTurn,
    CanDecrementHealth,
    CanUseCardPack,
    CanUseDiscardedPack,
    CanUseBarrel,
    CanUseBang,
    CanUseMissed,
    CanUseBeer,
    CanTargetOthers,
    CanTargetOthersHand,
    CanTargetOthersTable,
    CanDiscardCard
}