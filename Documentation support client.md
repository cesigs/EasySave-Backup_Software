# [FR] MANUEL SUPPORT CLIENT

## Configuration minimale requise
- Processeur : Processeur de 1 GHz ou plus rapide
- RAM : 1 gigaoctet (Go)
- Espace sur le disque dur : 1 Go pour le programme et l'enregistrement des informations de sauvegardes et des logs
- Carte graphique :	DirectX 9 ou version ultérieure avec pilote WDDM 1.0
- Ecran : 800x600 

## Arborescence du projet
```
Projet Programmation Système
│   Documentation support client.md    
│
└───ClientWPF
│ 	│	ClientWPF.sln
│ 	│
│	└───ClientWPF
│	│	│	App.xaml
│	│	│	App.xaml.cs
│	│	│	AssemblyInfo.cs
│	│	│	ClientWPF.csproj
│	│	│	MainWindow.xaml
│	│	│	MainWindow.xaml.cs
│	│	│	pause-button.png
│	│	│	play-button.png
│	│	│	stop-button.png
│	│	│
│	│	ClientWPF.core
│	│	│	ClientWPF.core.csproj
│	│	│	Repository.cs
│	│	│	SaveWork.cs
│	│	│	ServiceDB.cs
│	└───└────────────────────────────────
│	│
└───ConsoleProject
│   │	ConsoleProject.csproj
│   │   ConsoleProject.sln
│   │	Program.cs
│   │
│   └───Controller
│   │   │   InterfaceLanguage.cs
│   │   │   InterfaceSaveType.cs
│   │   │   LaunchProgram.cs
│   │   │   SelectLanguage.cs
│   │   │
│   │	Model
│   │   │   Context.cs
│   │   │   DBjson.cs
│   │   │   Logs.cs
│   │   │   SaveWork.cs
│   │   │   States.cs
│   │   │   WriteLogs.cs
│   │   │   WriteStates.cs
│   │   │
│   │   └───Strategies
│   │   │   │   AllTheSavesStrategy.cs
│   │   │   │   EnglishStrategy.cs
│   │   │   │   LogsExecuteSaveOnCreation.cs
│   │   │   │   FrenchStrategy.cs
│   │   │   │   SpecificSaveStrategy.cs
│   │   │
│   │   View
│   │   │   View.cs
│   └───└────────────────────────────────
│	│
└───CryptoSoft
│	│	CryptoSoft.csproj
│	│	CryptoSoft.sln
│	│	Program.cs
│   └────────────────────────────────────
│	│
└───GuiProject
│ 	│	GuiProject.sln
│ 	│
│	└───GuiProject
│	│	│	App.xaml
│	│	│	App.xaml.cs
│	│	│	AssemblyInfo.cs
│	│	│	GuiProject.csproj
│	│	│	iconeSave.ico
│	│	│	Language.cs
│	│	│	MainWindow.xaml
│	│	│	MainWindow.xaml.cs
│	│	│
│	│	└───Images
│	│	│	│	france-flag-round-small.png
│	│	│	│	iconeSave.ico
│	│	│	│	iconeSave.png
│	│	│	│	pause-button.png
│	│	│	│	play-button.png
│	│	│	│	stop-button.png
│	│	│	│	united-kingdom-flag-round-small.png
│	│	│	│
│	│	│	Language
│	│	│	│	Resource.Designer.cs
│	│	│	│	Resource.fr.resx
│	│	│	│	Resource.resx
│	│	│	│
│	│	│	Pages
│	│	│	│	FunctionalPage.xaml
│	│	│	│	FunctionalPage.xaml.cs
│	│	│	│
│	│	GuiProject.core
│	│	│	GUIProject.core.csproj
│	│	│
│	│	└───Business
│	│	│	│	Logs.cs
│	│	│	│	SaveWork.cs
│	│	│	│	States.cs
│	│	│	│
│	│	│	Data
│	│	│	│	Repository.cs
│	│	│	│
│	│	│	Services
│	│	│	│	ActionPPS.cs
│	│	│	│	EncryptFile.cs
│	│	│	│	ExecuteAllTheSave.cs
│	│	│	│	ExecuteOneSave.cs
│	│	│	│	ExecuteSaveOnCreation.cs
│	│	│	│	Server.cs
│	│	│	│	ServiceDB.cs
│	│	│	│	WriteLogs.cs
│	│	│	│	WriteStates.cs
│	│	│	│
│	│	Server
│	│	│	Console_Server.csproj
│	│	│	Program.cs
│	│	│	Server.cs
└───└───└───────────────────────────────────     

```
## Emplacement des fichiers de configuration
Fichier de configuration de sauvegarde 'bdd.json' : C:\\bdd.json  
Fichier logs au format JSON 'logs.json' : C:\\logs.json  
Fichier logs au format XML 'logs.xml' : C:\\logs.xml  
Fichier statut 'states.json' : C:\\states.json  
Les fichiers JSON sont placés dans un premier temps à cet endroit afin de faciliter leur accès pour les premières versions de l'application
________________________________________________________________

# [EN] CLIENT SUPPORT DOCUMENTATION

## Minimum system requirements
- Processor : 1 gigahertz (GHz) or faster compatible processor
- RAM : 1 gigabyte (GB)
- Hard drive size : 1 Gb pour le programme et l'enregistrement des informations de sauvegardes et des logs
- Graphics card :	DirectX 9 or later with WDDM 1.0 driver
- Display: 	800x600 

## Project spanning tree
```
Projet Programmation Système
│   Documentation support client.md    
│
└───ClientWPF
│ 	│	ClientWPF.sln
│ 	│
│	└───ClientWPF
│	│	│	App.xaml
│	│	│	App.xaml.cs
│	│	│	AssemblyInfo.cs
│	│	│	ClientWPF.csproj
│	│	│	MainWindow.xaml
│	│	│	MainWindow.xaml.cs
│	│	│	pause-button.png
│	│	│	play-button.png
│	│	│	stop-button.png
│	│	│
│	│	ClientWPF.core
│	│	│	ClientWPF.core.csproj
│	│	│	Repository.cs
│	│	│	SaveWork.cs
│	│	│	ServiceDB.cs
│	└───└────────────────────────────────
│	│
└───ConsoleProject
│   │	ConsoleProject.csproj
│   │   ConsoleProject.sln
│   │	Program.cs
│   │
│   └───Controller
│   │   │   InterfaceLanguage.cs
│   │   │   InterfaceSaveType.cs
│   │   │   LaunchProgram.cs
│   │   │   SelectLanguage.cs
│   │   │
│   │	Model
│   │   │   Context.cs
│   │   │   DBjson.cs
│   │   │   Logs.cs
│   │   │   SaveWork.cs
│   │   │   States.cs
│   │   │   WriteLogs.cs
│   │   │   WriteStates.cs
│   │   │
│   │   └───Strategies
│   │   │   │   AllTheSavesStrategy.cs
│   │   │   │   EnglishStrategy.cs
│   │   │   │   LogsExecuteSaveOnCreation.cs
│   │   │   │   FrenchStrategy.cs
│   │   │   │   SpecificSaveStrategy.cs
│   │   │
│   │   View
│   │   │   View.cs
│   └───└────────────────────────────────
│	│
└───CryptoSoft
│	│	CryptoSoft.csproj
│	│	CryptoSoft.sln
│	│	Program.cs
│   └────────────────────────────────────
│	│
└───GuiProject
│ 	│	GuiProject.sln
│ 	│
│	└───GuiProject
│	│	│	App.xaml
│	│	│	App.xaml.cs
│	│	│	AssemblyInfo.cs
│	│	│	GuiProject.csproj
│	│	│	iconeSave.ico
│	│	│	Language.cs
│	│	│	MainWindow.xaml
│	│	│	MainWindow.xaml.cs
│	│	│
│	│	└───Images
│	│	│	│	france-flag-round-small.png
│	│	│	│	iconeSave.ico
│	│	│	│	iconeSave.png
│	│	│	│	pause-button.png
│	│	│	│	play-button.png
│	│	│	│	stop-button.png
│	│	│	│	united-kingdom-flag-round-small.png
│	│	│	│
│	│	│	Language
│	│	│	│	Resource.Designer.cs
│	│	│	│	Resource.fr.resx
│	│	│	│	Resource.resx
│	│	│	│
│	│	│	Pages
│	│	│	│	FunctionalPage.xaml
│	│	│	│	FunctionalPage.xaml.cs
│	│	│	│
│	│	GuiProject.core
│	│	│	GUIProject.core.csproj
│	│	│
│	│	└───Business
│	│	│	│	Logs.cs
│	│	│	│	SaveWork.cs
│	│	│	│	States.cs
│	│	│	│
│	│	│	Data
│	│	│	│	Repository.cs
│	│	│	│
│	│	│	Services
│	│	│	│	ActionPPS.cs
│	│	│	│	EncryptFile.cs
│	│	│	│	ExecuteAllTheSave.cs
│	│	│	│	ExecuteOneSave.cs
│	│	│	│	ExecuteSaveOnCreation.cs
│	│	│	│	Server.cs
│	│	│	│	ServiceDB.cs
│	│	│	│	WriteLogs.cs
│	│	│	│	WriteStates.cs
│	│	│	│
│	│	Server
│	│	│	Console_Server.csproj
│	│	│	Program.cs
│	│	│	Server.cs
└───└───└───────────────────────────────────     

```
## Configuration files location
Backup configuration file 'bdd.json' : C:\\bdd.json  
Logs file JSON format 'logs.json' : C:\\logs.json  
Logs file XML format 'logs.xml' : C:\\logs.xml  
State file 'states.json' : C:\\states.json  
JSON files are first placed here for an easy acces on the first versions of the application