# Traitement_De_Commande

## Description du projet

Ce projet est une application de traitement des commandes, développée dans le cadre du module DSED_M07 de la formation continue. L'application utilise RabbitMQ pour gérer les échanges de messages entre différents modules, permettant ainsi une architecture modulaire et scalable.

## Fonctionnalités principales

- **Producteur de commandes** : Génération et envoi de commandes via RabbitMQ.
- **Facturation** : Calcul des factures avec ou sans taxes, gestion des commandes premium.
- **Expédition** : Préparation des commandes pour l'expédition, avec distinction entre commandes normales et premium.
- **Journalisation** : Enregistrement des commandes reçues dans des fichiers JSON.
- **Courriels Premium** : Gestion des notifications pour les commandes premium.

## Structure du projet

Le projet est organisé en plusieurs modules :

- **DSED_M07_TraitementCommande_producteur** : Génère et publie des commandes.
- **DSED_M07_TraitementCommande_facturation** : Gère la facturation des commandes.
- **DSED_M07_TraitementCommande_Expedition** : Prépare les commandes pour l'expédition.
- **DSED_M07_TraitementCommande_CourrielsPremium** : Gère les courriels pour les commandes premium.
- **DSED_M07_TraitementCommande_journal** : Journalise les commandes reçues.

## Prérequis

- **Langage** : C# (.NET 8.0)
- **Dépendances** :
  - RabbitMQ.Client (>= 6.8.1)
  - System.Text.Json (>= 9.0.0)
- **Système d'exploitation** : Windows ou tout système compatible avec .NET 8.0
- **RabbitMQ** : Serveur RabbitMQ installé et configuré.

## Installation

1. Clonez le dépôt :
   ```bash
   git clone [URL_DU_DEPOT]
   ```
2. Accédez au répertoire du projet :
   ```bash
   cd DSED_M07_TraitementCommande
   ```
3. Assurez-vous que RabbitMQ est en cours d'exécution sur `localhost`.

4. Compilez les projets :
   ```bash
   dotnet build
   ```

## Utilisation

### Producteur de commandes
1. Lancez le producteur :
   ```bash
   dotnet run --project DSED_M07_TraitementCommande_producteur
   ```
2. Suivez les instructions pour générer des commandes.

### Facturation
1. Lancez le module de facturation :
   ```bash
   dotnet run --project DSED_M07_TraitementCommande_facturation
   ```

### Expédition
1. Lancez le module d'expédition :
   ```bash
   dotnet run --project DSED_M07_TraitementCommande_Expedition
   ```

### Journalisation
1. Lancez le module de journalisation :
   ```bash
   dotnet run --project DSED_M07_TraitementCommande_journal
   ```

### Courriels Premium
1. Lancez le module de courriels premium :
   ```bash
   dotnet run --project DSED_M07_TraitementCommande_CourrielsPremium
   ```

## Tests

Pour exécuter les tests unitaires et fonctionnels, utilisez la commande suivante :
```bash
dotnet test
```

## Contribution

Les contributions sont les bienvenues ! Veuillez suivre les étapes suivantes :

1. Forkez le projet.
2. Créez une branche pour vos modifications :
   ```bash
   git checkout -b feature/nom_de_la_fonctionnalite
   ```
3. Soumettez une pull request.

## Auteurs

- Don Nick Munezero 
- Contact : munezerodonnick@gmail.com

].
