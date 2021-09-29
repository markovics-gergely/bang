using Microsoft.EntityFrameworkCore.Migrations;

namespace Bang.DAL.Migrations
{
    public partial class description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Rálövés egy játékosra, aki lőtávolon belül van. A megtámadott, ha nem tudja kivédeni(ld.Barrel(Hordó) és Missed!(Elvétve!)), egy életet veszít.Ha meghal és van Beer (Sör) lapja, azonnal kijátszhatja (egyik eset, amikor lapot játszhat ki, aki nincs soron). Egy körben csak egy BANG! játszható ki (kivétel Volcanic fegyver és Willy the Kid személyisége).");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "A játékos egy életet visszanyer. Mindenkinek maximum annyi élete lehet, amennyi kezdéskor volt.Ha valaki meghal és van sör lapja, azonnal kijátszhatja(egyik eset, amikor lapot játszhat ki, aki nincs soron). Ha már csak két játékos van életben, nem lehet sört inni! ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Bármelyik másik játékossal eldobathat egy lapot (magával nem) a kezéből véletlenszerűen, vagy az asztalról.Nincs védett lap, minden eldobatható.");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: " Legfeljebb 1 távolságra levő játékostól elhúzhat egy lapot, az asztalról, vagy a kezéből, utóbbiból véletlenszerűen.Nincs védett lap, minden elhúzható.");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Egy tetszőleges játékos kihívása párbajra (távolságtól függetlenül). A párbaj menete: a felek felváltva lőnek(BANG! lerakásával), a kihívott kezd.Aki először nem tud rakni, az veszít egy életet. (Semelyik elkerülő lap nem érvényesül a párbaj alatt, és a Duel (Párbaj) nem minősül BANG!-nek.) Abban az esetben, ha egy párbajt kezdeményező bandita elveszíti az utolsó életét az érte járó 3 lap jutalmat senki nem kapja meg.");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "Ahány élő játékos játékban van, annyi lapot a pakliból felfordítanak.Mindenki választ egyet, a lapot kijátszó játékos kezdi a sort.");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: " Összes többi játékost megtámadja, csak lövéssel (BANG! berakásával) védhető(egyik eset, amikor lapot játszhat ki, aki nincs soron). Nem minősül lövésnek, tehát eben a körben még használható BANG! vagy GATLING.");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Kijátszója 2 lapot húzhat a pakliból.");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "A kijátszón kívül mindenkire rálő (hordó és elugrás lehet menedék). Nem minősül lövésnek, tehát ebben a körben BANG! még használható. ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Mindenki visszanyer egy életet. Úgy használható, mint a Beer (Sör), de mindenki kap egy életet, ha tud(maximum annyi élete lehet mindenkinek, mint kezdéskor volt). Ha már csak két játékos van életben, sört ugyan nem lehet inni, de Saloon(Szalon) lap még kijátszható! ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Kijátszója 3 lapot húzhat a pakliból.");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "A seriff kivételével bárkire rátehető. Akire kijátszották, amikor sorra kerülne, először felcsap egy lapot, ha az kör, akkor nem kerül börtönbe(megkezdheti a körét az 1. fázissal), különben ebből a körből kimarad(a következőből nem). A lapot mindkét esetben az eldobott lapokra helyezzük.Ha a játékos nem kerül sorra, akkor is el kell dobnia a többletlapjait! Amíg a játékos be van börtönözve, addig lőhetnek rá es használhat Missed! (Elvétve!) es Beer(Sör) lapokat, de Barrel-t(Hordó) nem.Ez a lap is eldobatható Cat Balou-val vagy Panic!-kal(Pánik!). Ha van az asztalon Jail(Börtön) is Dynamite(Dinamit) mellett, akkor az előbb asztalra kerülő érvényesül először. ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Módosítják a távolságokat. Nem lehet két azonos lovunk. A Mustang tulajdonosa mindenkitől egyel messzebb van, ha támadják(védekező lap). Az Scope(Távcső) tulajdonosához mindenki egyel közelebb van, ha támadni akar(támadó lap). (ld.még Paul Regret, Rose Doolan). ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "Aki előtt van hordó, amikor rálőnek, felcsaphat egy lapot, ha az ♥ (kör), akkor nem találták el, ha nem az, akkor még védekezhet más módon. Egy játékos előtt nem lehet több hordó. ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "Módosítják a távolságokat. Nem lehet két azonos lovunk. A Mustang tulajdonosa mindenkitől egyel messzebb van, ha támadják(védekező lap). Az Scope(Távcső) tulajdonosához mindenki egyel közelebb van, ha támadni akar(támadó lap). (ld.még Paul Regret, Rose Doolan). ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 17,
                column: "Description",
                value: "Aki kijátssza, maga elé helyezi. A következő körtől kezdődően, amikor a dinamitot birtokló játékosra kerül a sor, először felcsap rá egy lapot.Ha ez ♠ (pikk) 2, 3, ..,9, akkor felrobban a dinamit és a játékos 3 életet veszít (nem védhető). Ha nem robbant fel, akkor a lapot a következő játékos elé helyezi. Ez addig megy, míg valakinél fel nem robban, vagy ellopják (pánik / PANIC), eldobatják(Cat Balou). Aki ettől hal meg, annak nincs gyilkosa (ld.jutalmazás-büntetés). Ha van az asztalon Dynamite(Dinamit) is Jail(Börtön) mellett, akkor az előbb asztalra kerülő érvényesül először. ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 18,
                column: "Description",
                value: "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). A Volcanic fegyver sajátossága, hogy tulajdonosa több lövést is leadhat körönként(ld. BANG!, Gatling, Willy the Kid). ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 20,
                column: "Description",
                value: "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 21,
                column: "Description",
                value: "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). ");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 22,
                column: "Description",
                value: "A kártyákon lévő fegyverek meghatározzák, hogy milyen messzire tud lőni a játékos(ld.Lőtávolság). Az asztalra kell helyezni magunk elé. Legfeljebb egy fegyverkártya lehet előttünk, ha újat tesz le valaki, a régit el kell dobnia. Akinek nincs fegyvere, az egy távolságra tud lőni (az alapértelmezett pisztolyával). ");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Minden sebződéskor húzhat egy lapot a pakliból.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Második lapját felcsapva húzza, ha az piros (♥ (kör) vagy ♦ (káró)), húzhat egy harmadikat.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Használhat BANG! kártyát Missed!-ként (Elvétve!) és fordítva.Ettől még nem lőhet kettőt).");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Sebződésenként húzhat egy lapot a sebző kezéből. Ha Dynamite (Dinamit) sebzi, akkor senkitől sem húz.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "A saját laphúzás fázisában (1.fázis) mindig eldöntheti, hogy az első lapot a húzópakliból, vagy egy másik játékos kezéből húzza.A második lapot mindig a húzópakliból húzza.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Úgy kell rá tekinteni, mintha lenne egy Barrel (Hordó) kártya előtte (= „beépített hordó”), vagyis minden ellene kijátszott BANG! ellen felcsaphatja a húzópakli legfelső lapját, és ha az ♥ (kör), akkor elkerülte a lövést. (Ha van még egy hordója, akkor mindkettőre húzhat.) ");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "A saját laphúzás fázisában (1.fázis) 3 lapot húz, amiből egyet visszatesz a pakli tetejére(tehát nem eldobja!). ");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Mindig, amikor fel kell csapnia egy lapot valamelyik kártya hatása miatt(Dynamite (Dinamit), Barrel (Hordó), Jail (Börtön)),akkor két lapot csaphat fel és a számára kedvezőbbet választhatja.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Minden játékos eggyel nagyobb távolságra levőnek látja őt (= „beépített MUSTANG”). (Ha van még egy MUSTANG - ja, akkor + 2 távra van.)");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "A saját laphúzás fázisában (1.fázis) az első lapját húzhatja az eldobott lapok tetejéről.A második lapot mindenképpen a húzópakliból húzza.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Minden játékost eggyel kisebb távolságra levőnek lát (= „beépített SCOPE”). (Ha van még egy SCOPE - ja, akkor - 2 távra van)");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Két kártyáért visszanyerhet egy életet.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "Az ő lövése csak két Missed!-el (Elvétve!) védhető. Az egyik lehet Barrel(Hordó), vagy Jourdonnais esetében akár mindkettő");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Ha elfogy a lapja a kezéből, azonnal húzhat egy újabbat.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "Mindig, amikor egy játékos kiesik a játékból, elveszi a kieső játékos kézben tartott es asztalon levő lapjait.");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "Akárhány lövést leadhat a meglévő fegyverének megfelelő távolságban.");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "A banditák nyernek, ha a seriff meghal.");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "A renegát nyer, ha mindenki mást megöl. (Tehát a renegát csak úgy nyerhet, ha egyedül marad életben és utoljára a seriffet ölte meg.)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "A seriff és helyettese(i) nyernek, ha csak ő(k) maradt(ak) életben, azaz akkor, ha minden banditát és a renegátot is megölik és a seriff még él.");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "A seriff és helyettese(i) nyernek, ha csak ő(k) maradt(ak) életben, azaz akkor, ha minden banditát és a renegátot is megölik és a seriff még él.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Bang!");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Sör");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Cat Balou");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Pánik!");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Párbaj");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "Szatócsbolt");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Indiánok!");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Postakocsi");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "Gatling");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Kocsma");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Wells Fargo");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "Börtön");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Musztáng");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "Hordó");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "Távcső");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 17,
                column: "Description",
                value: "Dinamit");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 18,
                column: "Description",
                value: "Gyorstüzelő");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "Schofield");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 20,
                column: "Description",
                value: "Remingtion");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 21,
                column: "Description",
                value: "Karabély");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 22,
                column: "Description",
                value: "Winchester");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "");
        }
    }
}
