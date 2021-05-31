# TwitchChatLog
### Specifikace požadavků - Verze 2 
Jan Bezouška  
bezouska.jan.2018@skola.ssps.cz  
16.5.2021  

- Úvod
  - Konvence dokumentu
    - Důležitost - 1 největší
  - Kontakty
    - Jan Bezouška, bezouska.jan.2018@skola.ssps.cz
- Celkový popis
  - Program bude napojený na databázi, do které bude jiný program (https://github.com/janbezouska/TwitchChatLogger) zapisovat zprávy
  - Uživatel zadá username a vybere kanál, a vypíšou se mu všechny zprávy od daného uživatele na daném kanále
  - Aplikace je určená pro lidi, kteří se chtějí podívat na (nejen) jejich historii
  - Uživatelské prostředí bude jednoduché:
    - pole na zadání uživatelského jména
    - listview na výběr kanálu
    - zlačítko na vypsání
    - pole do kterého se zprávy vypíšou
- Vlastnosti systému  
  1. Hledání v databázi
    - Důležitost: 1
    - Vstup: Vybraný kanál a username
    - Akce: Získání listu zpráv od daného uživatele na daném kanále z databáze
    - Výstup: Vypsání zpráv
    - Výstup: Vypsání chyby pokud se žádné zprávy nenajdou nebo pokud se nelze připojit na databázi
- Nefunkční požadavky
  - Odezva
    - Hledání v databázi by mělo trvat max. 2 vteřiny
  - Spolehlivost
    - Snad velká :|

### Funkční specifikace - verze 1
Jan Bezouška  
bezouska.jan.2018@skola.ssps.cz  
16.5.2021  

- Úvod
    - Dokument je určený pro mě
- Scénáře
    - Způsoby použití
        - Zadání jména, vybrání chatu a vyhledání
    - Typy uživatelů
        - Člověk, který se chce podívat na něčí historii chatu
- Architektura
    - Pracovní tok
        - Uživatel zadá username
        - Uživatel vybere kanál
        - Uživatel klikne na "vyhledat"
            - Možný error: program není spojený s databází >> "Pokud jste připojeni na internet, tento problém nevyřešíte. Pokud ne, připojte se."
        - Program se pokusí vyhledat zprávy od zadaného jména
            - Nenalezne žádné zprávy >> chyba - "Nenalezeny žádn zprávy od {username} v {kanál}."
            - Nalezne zprávy >> vypíše je
    - Uživatelské rozhraní
        ![UI](https://user-images.githubusercontent.com/79062512/120157343-3a0bd600-c1f3-11eb-82ee-3db4cb0127af.png)









