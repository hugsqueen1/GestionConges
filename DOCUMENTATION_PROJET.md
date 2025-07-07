# 📋 Documentation Complète - Projet GestionConges

## 🎯 Vue d'Ensemble du Projet

**GestionConges** est une application web de gestion des congés développée avec ASP.NET Core 9.0. Elle permet aux employés de soumettre des demandes de congés et aux administrateurs de les approuver ou les refuser.

### 🏗️ Architecture Technique
- **Framework** : ASP.NET Core 9.0 avec Razor Pages
- **Base de données** : SQLite avec Entity Framework Core
- **Authentification** : ASP.NET Core Identity
- **Interface** : Bootstrap 5 + Font Awesome
- **Validation** : Client et serveur

---

## 📁 Structure du Projet

```
GestionConges/
├── Pages/                          # Pages Razor
│   ├── Index.cshtml               # Page d'accueil (soumission + liste)
│   ├── Index.cshtml.cs            # Logique de la page d'accueil
│   ├── Suivi.cshtml               # Page de suivi des demandes
│   ├── Suivi.cshtml.cs            # Logique de suivi
│   ├── DashboardAdmin.cshtml      # Dashboard administrateur
│   ├── DashboardAdmin.cshtml.cs   # Logique du dashboard
│   ├── Privacy.cshtml             # Page de politique de confidentialité
│   ├── Error.cshtml               # Page d'erreur
│   └── Shared/                    # Composants partagés
│       ├── _Layout.cshtml         # Layout principal
│       ├── _LoginPartial.cshtml   # Menu de connexion
│       └── _ValidationScriptsPartial.cshtml
├── Conge.cs                       # Modèle de données
├── GestionCongesContext.cs        # Contexte de base de données
├── Program.cs                     # Point d'entrée de l'application
└── appsettings.json               # Configuration
```

---

## 🏠 Page d'Accueil (Index)

### 📍 URL : `/` ou `/Index`

### 🎯 Fonctionnalités Principales

#### 1. **Formulaire de Soumission de Demande**
- **Champs requis** :
  - Nom de l'employé
  - Poste de l'employé
  - Date de début (validation : ≥ date actuelle)
  - Date de fin (validation : ≥ date de début)

- **Validation** :
  - Dates ne peuvent pas être dans le passé
  - Date de fin ≥ date de début
  - Tous les champs obligatoires

- **Génération automatique** :
  - Code de suivi unique (8 caractères alphanumériques)
  - Statut initial : "En attente"

#### 2. **Liste Publique des Demandes**
- **Affichage limité** (confidentialité) :
  - ✅ Date de début (format dd/MM/yyyy)
  - ✅ Date de fin (format dd/MM/yyyy)
  - ✅ Statut avec badges colorés
  - ❌ Nom de l'employé (masqué)
  - ❌ Poste de l'employé (masqué)

- **Statuts visibles** :
  - 🟡 **En attente** (badge jaune)
  - 🟢 **Validé** (badge vert)
  - 🔴 **Refusé** (badge rouge)
  - ⚫ **Annulé** (badge gris)

#### 3. **Navigation**
- Lien vers la page de suivi
- Message d'information sur les droits d'accès

### 🔧 Code Clé

```csharp
// Validation des dates
if (DateDebut.Value.Date < DateTime.Today)
{
    ModelState.AddModelError("DateDebut", "La date de début ne peut pas être antérieure à la date actuelle.");
}

// Génération du code de suivi
private string GenererCodeSuivi()
{
    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    var random = new Random();
    return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
}
```

---

## 🔍 Page de Suivi (Suivi)

### 📍 URL : `/Suivi`

### 🎯 Fonctionnalités

#### 1. **Recherche par Code de Suivi**
- Champ de saisie pour le code de suivi
- Validation du code (8 caractères)
- Message d'erreur si code invalide

#### 2. **Affichage des Détails**
- **Informations complètes** :
  - Nom de l'employé
  - Poste de l'employé
  - Dates de début et fin (format dd/MM/yyyy)
  - Statut avec badge coloré
  - Code de suivi

#### 3. **Actions Disponibles** (si statut "En attente")
- **Modification des dates** :
  - Validation : dates ≥ date actuelle
  - Validation : date fin ≥ date début
  - Message de confirmation

- **Annulation de la demande** :
  - Changement de statut vers "Annulé"
  - Message de confirmation

### 🔧 Code Clé

```csharp
// Recherche par code
CongeTrouve = await _context.Conges.FirstOrDefaultAsync(c => c.CodeSuivi == CodeSuivi);

// Validation des modifications
if (dateDebut.Date < DateTime.Today)
{
    Message = "Erreur : La date de début ne peut pas être antérieure à la date actuelle.";
}
```

---

## 👨‍💼 Dashboard Administrateur (DashboardAdmin)

### 📍 URL : `/DashboardAdmin`

### 🔐 Accès Restreint
- **Authentification requise** : Seuls les utilisateurs connectés
- **Utilisateur par défaut** : `admin@demo.com` / `MotDePasse123!`

### 🎯 Fonctionnalités

#### 1. **Tableau de Bord Statistique**
- **Cartes d'information** :
  - 📊 Total des demandes
  - 🟡 Demandes en attente
  - 🟢 Demandes validées
  - 🔴 Demandes refusées/annulées

#### 2. **Liste Complète des Demandes**
- **Informations détaillées** :
  - Nom de l'employé
  - Poste de l'employé
  - Dates de début et fin
  - Statut avec badges
  - Code de suivi

#### 3. **Actions de Gestion**
- **Pour les demandes "En attente"** :
  - ✅ Bouton "Valider" (change statut vers "Validée")
  - ❌ Bouton "Refuser" (change statut vers "Refusée")

- **Pour toutes les demandes** :
  - 🗑️ Bouton "Supprimer" (supprime définitivement)

#### 4. **Export de Données**
- **Filtrage par statut** :
  - Tous les statuts
  - En attente uniquement
  - Validées uniquement
  - Refusées uniquement
  - Annulées uniquement

- **Format CSV** :
  - Séparateur : point-virgule (;)
  - Colonnes : Nom, Poste, DateDebut, DateFin, Statut, CodeSuivi
  - Nom de fichier : `demandes_conges.csv`

### 🔧 Code Clé

```csharp
// Calcul des statistiques
TotalDemandes = Conges.Count;
EnAttente = Conges.Count(c => c.Statut == StatutConge.EnAttente);
Validees = Conges.Count(c => c.Statut == StatutConge.Validee);

// Export CSV
var csv = "Nom;Poste;DateDebut;DateFin;Statut;CodeSuivi\n" +
    string.Join("\n", conges.Select(c => $"{c.NomEmploye};{c.PosteEmploye};{c.DateDebut:yyyy-MM-dd};{c.DateFin:yyyy-MM-dd};{c.Statut};{c.CodeSuivi}"));
```

---

## 🏗️ Composants Partagés

### 📄 Layout Principal (_Layout.cshtml)

#### 🎨 Design
- **Framework CSS** : Bootstrap 5
- **Icônes** : Font Awesome 6.0
- **Responsive** : Compatible mobile/desktop

#### 🧭 Navigation
- **Logo** : "GestionConges"
- **Menu principal** :
  - Accueil (toujours visible)
  - Privacy (toujours visible)
  - Dashboard RH (si connecté)

#### 🔐 Menu de Connexion (_LoginPartial.cshtml)
- **Non connecté** :
  - Lien "Register" (inscription)
  - Lien "Login" (connexion)

- **Connecté** :
  - Message "Hello [nom]"
  - Bouton "Logout" (déconnexion)

---

## 📊 Modèle de Données

### 🗃️ Entité Conge

```csharp
public class Conge
{
    public int Id { get; set; }                    // Clé primaire
    public string NomEmploye { get; set; }         // Nom de l'employé
    public string PosteEmploye { get; set; }       // Poste de l'employé
    public DateTime DateDebut { get; set; }        // Date de début
    public DateTime DateFin { get; set; }          // Date de fin
    public string CodeSuivi { get; set; }          // Code de suivi unique
    public StatutConge Statut { get; set; }        // Statut de la demande
    public bool Annule { get; set; }               // Flag d'annulation
}

public enum StatutConge
{
    EnAttente,    // En attente de validation
    Validee,      // Demande approuvée
    Refusee,      // Demande refusée
    Annulee       // Demande annulée
}
```

### 🗄️ Contexte de Base de Données

```csharp
public class GestionCongesContext : IdentityDbContext
{
    public DbSet<Conge> Conges { get; set; }       // Table des congés
    // Tables Identity automatiques pour l'authentification
}
```

---

## 🔐 Système d'Authentification

### 👤 Utilisateur par Défaut
- **Email** : `admin@demo.com`
- **Mot de passe** : `MotDePasse123!`
- **Création automatique** : Au démarrage de l'application

### 🛡️ Sécurité
- **Hachage des mots de passe** : ASP.NET Core Identity
- **Sessions sécurisées** : Cookies HTTPS
- **Validation des entrées** : Client et serveur

---

## ⚙️ Configuration

### 📄 appsettings.json
```json
{
  "ConnectionStrings": {
    "GestionCongesContext": "Data Source=GestionConges.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 🔧 Program.cs
- Configuration des services
- Configuration de la base de données
- Configuration de l'authentification
- Création de l'utilisateur admin
- Configuration du pipeline HTTP

---

## 🚀 Démarrage et Utilisation

### 📋 Prérequis
- .NET 9.0 SDK
- Navigateur web moderne

### ▶️ Lancement
```bash
dotnet run
```

### 🌐 Accès
- **URL** : `https://localhost:5001` ou `http://localhost:5000`
- **Admin** : Connectez-vous avec `admin@demo.com` / `MotDePasse123!`

### 📱 Utilisation

#### 👥 Employés
1. **Soumettre une demande** : Page d'accueil
2. **Suivre sa demande** : Page Suivi avec le code reçu
3. **Modifier/Annuler** : Si statut "En attente"

#### 👨‍💼 Administrateurs
1. **Se connecter** : Menu Login
2. **Accéder au dashboard** : Menu "Dashboard RH"
3. **Gérer les demandes** : Valider/Refuser/Supprimer
4. **Exporter les données** : Télécharger CSV

---

## 🔧 Fonctionnalités Techniques

### ✅ Validation des Données
- **Client-side** : HTML5 + JavaScript
- **Server-side** : ASP.NET Core ModelState
- **Base de données** : Contraintes Entity Framework

### 📱 Interface Responsive
- **Desktop** : Layout complet
- **Tablet** : Adaptation automatique
- **Mobile** : Menu hamburger

### 🎨 Design Moderne
- **Bootstrap 5** : Composants modernes
- **Font Awesome** : Icônes professionnelles
- **Badges colorés** : Statuts visuels
- **Alertes informatives** : Messages utilisateur

### 🔄 Workflow des Demandes
```
Soumission → En Attente → [Validation/Refus] → Terminé
     ↓           ↓              ↓
  Code de    Modifiable    Non modifiable
  suivi      par l'emp.    par l'emp.
```

---

## 📈 Points Forts du Projet

### 🎯 Fonctionnalités
- ✅ Gestion complète des congés
- ✅ Interface intuitive
- ✅ Sécurité et confidentialité
- ✅ Export de données
- ✅ Validation robuste

### 🏗️ Architecture
- ✅ Code propre et maintenable
- ✅ Séparation des responsabilités
- ✅ Utilisation des bonnes pratiques
- ✅ Documentation complète

### 🔒 Sécurité
- ✅ Authentification sécurisée
- ✅ Validation des entrées
- ✅ Protection des données personnelles
- ✅ Gestion des erreurs

---

## 🎓 Valeur Éducative

Ce projet démontre la maîtrise de :
- **ASP.NET Core** : Framework web moderne
- **Entity Framework** : ORM et gestion de base de données
- **Razor Pages** : Architecture page-based
- **Bootstrap** : Design responsive
- **C#** : Programmation orientée objet
- **Sécurité web** : Authentification et validation
- **Architecture logicielle** : Patterns et bonnes pratiques

---

*Documentation générée le : @DateTime.Now.ToString("dd/MM/yyyy HH:mm")* 