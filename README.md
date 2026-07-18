# AnimalZ - A Fortnite-style Animal Game

A multiplayer game where users adopt animals from a pet store, customize them, and play together in parks.

## Prerequisites

- .NET 8 SDK (install from https://dotnet.microsoft.com/download)
- Supabase account (https://supabase.com)

## Setup Instructions

### 1. Create Solution File
```bash
cd "/Users/michael/Documents/Michael/My Apps/Projects/Websites/Animalz"
dotnet new sln -n AnimalZ --force
dotnet sln add "AnimalZ.Client/AnimalZ.Client.csproj"
```

### 2. Restore Packages & Build
```bash
cd AnimalZ.Client
dotnet restore
dotnet build
```

### 3. Copy Assets (Logo & Icon)
The logo (`Animalz_logo.png`) and icon (`Animalz_icon.png`) are in the `assets/` folder at the root level. They need to be copied to:
`AnimalZ.Client/wwwroot/assets/`

Or update the image paths in Login.razor to point to `/assets/Animalz_logo.png` directly.

### 4. Configure Supabase
Your credentials are already configured in `wwwroot/appsettings.json`:
- URL: https://jpjbclvdyduxhufmiouz.supabase.co
- Key: sb_publishable_FwqP90UCDweI8tiaTY3Vbg_FTVk92eO

### 5. Deploy Database Schema
Run `supabase/schema.sql` in your Supabase SQL editor to create the tables.

## Run the Game
```bash
dotnet watch run
```
Visit: http://localhost:5173

## Project Structure

- **AnimalZ.Client/** - Blazor WebAssembly frontend
  - Components/ - Reusable UI components
  - Pages/ - Main pages (Login, PetStore, Park)
  - Services/ - API services for Supabase
  - Models/ - Data models and types
  
- **AnimalZ.Server/** - Server-side project

## Game Features

1. User authentication (email/password)
2. Pet store with zoo animals to adopt
3. Animal customization system
4. Park lobby for meeting other players
5. Real-time multiplayer features