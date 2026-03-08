# AnimeVerse

AnimeVerse är en webbapplikation utvecklad i ASP.NET Core MVC med C# som huvudspråk.  
Syftet är att ge användaren möjlighet att snabbt söka efter japanska animer och få information om titel, betyg, årtal och handling.  
Applikationen hämtar data från det öppna Jikan API, som ger information om hundratals animer.

Teknik och struktur:

(Språk och ramverk):

- C# (.NET 6)
- ASP.NET Core MVC
- HTML, CSS och Razor
- xUnit (enhetstester)

(Arkitektur):

- MVC-struktur (Model–View–Controller)
- Factory-mönster för objekt­skapande
- Dependency Injection för att koppla samman tjänster
- Asynkrona metoder (async / await) för API-anrop

Funktioner:

- Sök efter anime med titel via ett sökfält
- Visa detaljer (titel, betyg, år, bild, beskrivning)
- Hämtar populära animer automatiskt vid start
- Felhantering vid tomma eller ogiltiga sökningar
- Responsiv design med HTML och CSS

Avancerade C#-koncept (projektet använder):

- Asynkron programmering för att hämta data utan att låsa användargränssnittet
- Interfaces (IAnimeService) för lös koppling
- Factory Pattern (AnimeFactory) för att skapa objekt från JSON
- Dependency Injection för effektiv resurshantering

Testning:

Enhetstesterna har skapats med xUnit och är uppdelade i tydligt namngivna testklasser, där varje testklass ansvarar för att testa en specifik del av applikationen.

- AnimeFactoryTests testar att AnimeFactory korrekt skapar Anime-objekt från JSON-data samt hanterar ofullständig data på ett säkert sätt.
- AnimeServiceTests testar att AnimeService returnerar korrekt data vid lyckade API-anrop och hanterar fel (404) korrekt genom användning av en fejkad HttpMessageHandler.

Totalt innehåller projektet fyra enhetstester.


Minneshantering:

- HttpClient hanteras via Dependency Injection för att undvika onödiga instanser
- using används vid JSON-hantering för att frigöra resurser
- Kod skrivs så att garbage collectorn arbetar effektivt (inga statiska listor som ligger kvar i minnet)

Såhär kör du projektet:

1. Klona eller ladda ner projektet.
2. Öppna mappen i Rider eller Visual Studio.
3. Kör projektet:

