# TwitchChatLog
### Verze 1 
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
- Vlastnosti systému  
  1. Hledání v databázi
    - Důležitost: 1
    - Vstup: Vybraný kanál a username
    - Akce: Záskání listu zpráv od daného uživatele na daném kanále z databáze
    - Výstup: Vypsání zpráv
    - Výstup: Vypsání chyby pokud se žádné zprávy nenajdou
- Nefunkční požadavky
  - Odezva
    - Hledání v databázi by mělo trvat max. 2 vteřiny
  - Spolehlivost
    - Snad velká :|
