namespace Bang.DAL.Domain.Constants.DescriptionConstants
{
    public static class CardDescriptionConstants
    {
        public const string Bang = "Rálövés egy játékosra, aki lőtávolon belül van. A megtámadott, ha nem tudja kivédeni" +
            "(ld.Barrel(Hordó) és Missed!(Elvétve!)), egy életet veszít.Ha meghal és van Beer (Sör) lapja, " +
            "azonnal kijátszhatja (egyik eset, amikor lapot játszhat ki, aki nincs soron). Egy körben csak " +
            "egy BANG! játszható ki (kivétel Volcanic fegyver és Willy the Kid személyisége).";  //25 db
        public const string Missed = "Nem talált!";                                              //12 db
        public const string Beer = "A játékos egy életet visszanyer. Mindenkinek maximum annyi élete lehet, " +
            "amennyi kezdéskor volt.Ha valaki meghal és van sör lapja, azonnal kijátszhatja(egyik eset, " +
            "amikor lapot játszhat ki, aki nincs soron). Ha már csak két játékos van életben, nem lehet " +
            "sört inni! ";                                                                       //6 db
        public const string CatBalou = "Bármelyik másik játékossal eldobathat egy lapot (magával nem) a kezéből " +
            "véletlenszerűen, vagy az asztalról.Nincs védett lap, minden eldobatható.";          //4 db
        public const string Panic = " Legfeljebb 1 távolságra levő játékostól elhúzhat egy lapot, az asztalról, vagy a " +
            "kezéből, utóbbiból véletlenszerűen.Nincs védett lap, minden elhúzható.";            //4 db
        public const string Duel = "Egy tetszőleges játékos kihívása párbajra (távolságtól függetlenül). A párbaj " +
            "menete: a felek felváltva lőnek(BANG! lerakásával), a kihívott kezd.Aki először nem tud " +
            "rakni, az veszít egy életet. (Semelyik elkerülő lap nem érvényesül a párbaj alatt, és a Duel " +
            "(Párbaj) nem minősül BANG!-nek.) Abban az esetben, ha egy párbajt kezdeményező bandita " +
            "elveszíti az utolsó életét az érte járó 3 lap jutalmat senki nem kapja meg.";       //3 db   
        public const string GeneralStore = "Ahány élő játékos játékban van, annyi lapot a pakliból " +
            "felfordítanak.Mindenki választ egyet, a lapot kijátszó játékos kezdi a sort.";      //2 db
        public const string Indians = " Összes többi játékost megtámadja, csak lövéssel (BANG! berakásával) " +
            "védhető(egyik eset, amikor lapot játszhat ki, aki nincs soron). Nem minősül lövésnek, tehát " +
            "eben a körben még használható BANG! vagy GATLING.";                                 //2 db
        public const string Stagecoach = "Kijátszója 2 lapot húzhat a pakliból.";                //2 db
        public const string Gatling = "A kijátszón kívül mindenkire rálő (hordó és elugrás lehet menedék). Nem minősül " +
            "lövésnek, tehát ebben a körben BANG! még használható. ";                            //1 db
        public const string Saloon = "Mindenki visszanyer egy életet. Úgy használható, mint a Beer (Sör), de " +
            "mindenki kap egy életet, ha tud(maximum annyi élete lehet mindenkinek, mint kezdéskor " +
            "volt). Ha már csak két játékos van életben, sört ugyan nem lehet inni, de Saloon(Szalon) lap " +
            "még kijátszható! ";                                                                 //1 db       
        public const string WellsFargo = "Kijátszója 3 lapot húzhat a pakliból.";                //1 db
        public const string Jail = "A seriff kivételével bárkire rátehető. Akire kijátszották, amikor sorra kerülne, " +
            "először felcsap egy lapot, ha az kör, akkor nem kerül börtönbe(megkezdheti a körét az 1. " +
            "fázissal), különben ebből a körből kimarad(a következőből nem). A lapot mindkét esetben az " +
            "eldobott lapokra helyezzük.Ha a játékos nem kerül sorra, akkor is el kell dobnia a " +
            "többletlapjait! " +
            "Amíg a játékos be van börtönözve, addig lőhetnek rá es használhat Missed! (Elvétve!) es " +
            "Beer(Sör) lapokat, de Barrel-t(Hordó) nem.Ez a lap is eldobatható Cat Balou-val vagy " +
            "Panic!-kal(Pánik!). Ha van az asztalon Jail(Börtön) is Dynamite(Dinamit) mellett, akkor az " +
            "előbb asztalra kerülő érvényesül először. ";                                        //3 db       
        public const string Mustang = "Módosítják a távolságokat. Nem lehet két azonos lovunk. A Mustang tulajdonosa " +
            "mindenkitől egyel messzebb van, ha támadják(védekező lap). Az Scope(Távcső) " +
            "tulajdonosához mindenki egyel közelebb van, ha támadni akar(támadó lap). (ld.még Paul " +
            "Regret, Rose Doolan). ";                                                            //2 db 
        public const string Barrel = "Aki előtt van hordó, amikor rálőnek, felcsaphat egy lapot, ha az ♥ (kör), " +
            "akkor nem találták el, ha nem az, akkor még védekezhet más módon. Egy játékos előtt nem " +
            "lehet több hordó. ";                                                                //2 db
        public const string Scope = "Módosítják a távolságokat. Nem lehet két azonos lovunk. A Mustang tulajdonosa " +
            "mindenkitől egyel messzebb van, ha támadják(védekező lap). Az Scope(Távcső) " +
            "tulajdonosához mindenki egyel közelebb van, ha támadni akar(támadó lap). (ld.még Paul " +
            "Regret, Rose Doolan). ";                                                            //1 db
        public const string Dynamite = "Aki kijátssza, maga elé helyezi. A következő körtől kezdődően, amikor a " +
            "dinamitot birtokló játékosra kerül a sor, először felcsap rá egy lapot.Ha ez ♠ (pikk) 2, 3, ..,9, " +
            "akkor felrobban a dinamit és a játékos 3 életet veszít (nem védhető). Ha nem robbant fel, " +
            "akkor a lapot a következő játékos elé helyezi. Ez addig megy, míg valakinél fel nem robban, " +
            "vagy ellopják (pánik / PANIC), eldobatják(Cat Balou). Aki ettől hal meg, annak nincs gyilkosa " +
            "(ld.jutalmazás-büntetés). Ha van az asztalon Dynamite(Dinamit) is Jail(Börtön) mellett, " +
            "akkor az előbb asztalra kerülő érvényesül először. ";                               //1 db  
        public const string Volcanic = "A kártyákon lévő fegyverek meghatározzák, " +
            "hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. " +
            "Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. " +
            "Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). " +
            "A Volcanic fegyver sajátossága, hogy tulajdonosa több lövést is leadhat körönként(ld. " +
            "BANG!, Gatling, Willy the Kid). ";                                                  //2 db
        public const string Schofield = "A kártyákon lévő fegyverek meghatározzák, " +
            "hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. " +
            "Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. " +
            "Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). "; //3 db
        public const string Remingtion = "A kártyákon lévő fegyverek meghatározzák, " +
            "hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. " +
            "Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. " +
            "Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). "; //1 db
        public const string Karabine = "A kártyákon lévő fegyverek meghatározzák, " +
            "hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. " +
            "Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. " +
            "Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). "; //1 db
        public const string Winchester = "A kártyákon lévő fegyverek meghatározzák, " +
            "hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. " +
            "Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. " +
            "Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). "; //1 db
    }
}
