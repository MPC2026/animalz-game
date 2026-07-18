# AnimalZ - Database Setup Guide

## Prerequisites

1. Create a Supabase project at https://supabase.com
2. Get your project URL and anon key from Project Settings > API

## Setup Instructions

### 1. Configure Environment Variables

Create `AnimalZ.Client/wwwroot/appsettings.json`:

```json
{
  "Supabase": {
    "Url": "YOUR_SUPABASE_URL",
    "Key": "YOUR_SUPABASE_ANON_KEY"
  }
}
```

### 2. Run the Database Schema

In Supabase SQL Editor, run the contents of `supabase/schema.sql`

### 3. Enable Authentication

1. Go to Authentication > Settings
2. Enable "Email confirmations" (optional for development)
3. Configure email provider or use magic links

### 4. Set Up Storage (Optional)

For animal images, you can:
- Use public URLs in the animals table
- Or set up a storage bucket and upload images

## Database Tables

| Table | Description |
|-------|-------------|
| `users` | User accounts (managed by Supabase Auth) |
| `animals` | Available zoo animals for adoption |
| `user_animals` | Animals owned by users with customizations |
| `parks` | Game areas where users meet |
| `park_users` | Tracks user presence in parks |

## Development

```bash
cd AnimalZ.Client
dotnet watch run
```