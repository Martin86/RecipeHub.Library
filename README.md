# RecipeHub

RecipeHub ist eine Rezeptverwaltungs-Anwendung mit Benutzerregistrierung, Favoriten, Kategorien, Zutaten und Filterfunktionen.  
Das Projekt besteht aus einer C# Bibliothek (RecipeHub.Library) und einem Beispiel-Frontend (RecipeHub.WinForms). Datenpersitenz wird über SQLite und Entity Framework Core realisiert.

## Installations- & Startanleitung

### Voraussetzungen
- .NET 8 SDK
- Visual Studio 2022 
- SQLite

###  Technologien

C# / .NET 8
Entity Framework Core (SQLite)
Windows Forms
Repository-Pattern & Service-Layer
Asynchrone Datenzugriffe (async/await)

### Installation

# Repository klonen oder herunterladen
git clone https://github.com/Martin86/RecipeHub.Library.git

# Projekt öffnen
Öffnen der Datei RecipeHub.sln mit Visual Studio 2022.

# Startprojekt festlegen
Im Projektmappen-Explorer Rechtsklick auf RecipeHub.WinForms, dann als Startprojekt festlegen.

# Anwendung starten
Mit F5 (Debuggen) oder Strg + F5 (ohne Debuggen) starten.
Beim ersten Start wird:
die SQLite-Datenbank (recipehub.db) automatisch erstellt,
die Zutatenliste.txt eingelesen und neue Zutaten in die Datenbank übernommen.

# Login / Registrierung
Benutzername und Passwort eingeben → Falls der Benutzer noch nicht existiert, wird er automatisch registriert.
Danach stehen alle Tabs und Funktionen zur Verfügung.

### Projektstruktur / Architekturübersicht

RecipeHub/
│
├── Projektmappenelemente/
│	└── README.md         # Diese Datei
│
├── RecipeHub.Library/
│   ├── Models/           # Enthält alle Datenmodelle mit EF Core-Annotationen (User, Recipe, Category, Ingredient)
│   ├── Data/             # EF Core DbContext & Migrations
│   ├── Repositories/     # Repository Architektur für generische CRUD-Operationen 
│   ├── Services/         # Geschäftslogik 
│   └── Zutatenliste.txt  # Beispiel-Zutatenliste
│
└── RecipeHub.WinForms/
    ├── MainForm.cs       # Hauptoberfläche
    └── Program.cs        # Anwendungseintrittspunkt
   

### Hauptfunktionen

-> Benutzerregistrierung / Login
-> Rezepte anlegen, bearbeiten und löschen
-> Zutaten und Kategorieverwaltung
-> Favoritenfunktion für Rezepte
-> Filter nach Kategorien und Zutaten
-> Persistente Speicherung in SQLite
-> Vollständig asynchron (async/await)

### Benutzeroberfläche

Die WinForms-Oberfläche besteht aus mehreren Tabs:

-> Alle Rezepte:	                Übersicht aller Rezepte (inkl. Favoritenstern, Filterung nach Kategorie & Zutat) Sichtbar für Registrierte User und nicht registrierte User
-> Meine Rezepte:	                Zeigt und verwaltet alle Rezepte des aktuell eingeloggten Benutzers
-> Favoriten:	                    Zeigt die markierten Lieblingsrezepte
-> Rezept hinzufügen:	            Eingabemaske mit Zutaten- und Kategorienauswahl
-> Kategorie bearbeiten	            Ermöglicht das Umbenennen von Kategorien
-> Versteckter Tab, Edit-Ansicht:	Öffnet sich beim Doppelklick auf ein eigenes Rezept und erlaubt Änderungen

### Zutatenliste-Import

Beim Start wird die Datei Zutatenliste.txt automatisch eingelesen und fehlende Zutaten in die Datenbank übernommen.

Format: "Mehl,Eier,Milch,Zucker,Salz,Butter"
Neue Zutaten einfach in die Datei schreiben, beim nächsten Start werden sie automatisch hinzugefügt.


### Benutzer / Rezepte

-> Benutzer können einloggen. Existiert der User nicht, wird er automatisch mit dem eingegeben Passwort registriert 
-> Nur angemeldete Benutzer dürfen Rezepte anlegen, bearbeiten oder löschen. 
-> Rezepte benötigen mindestens:
    - Einen Namen (global eindeutig)
    - Eine Kategorie oder mehrere Kategorien
    - Eine oder mehrere Zutaten
-> Rezepte anderer Benutzer können als Favoriten markiert werden
