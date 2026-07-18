-- Supabase database schema for AnimalZ game

-- Users table (managed by Supabase Auth, but we can add metadata)
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    email TEXT UNIQUE NOT NULL,
    display_name TEXT,
    avatar_url TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Animals table - available animals in the zoo/pet store
CREATE TABLE animals (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name TEXT NOT NULL,
    species TEXT NOT NULL,
    description TEXT,
    image_url TEXT,
    is_zoo_animal BOOLEAN DEFAULT true,
    default_colors JSONB DEFAULT '{}',
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- User animals table - animals owned by users
CREATE TABLE user_animals (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    animal_id UUID REFERENCES animals(id),
    nickname TEXT,
    customization JSONB,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Parks table - game areas where users can meet
CREATE TABLE parks (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name TEXT NOT NULL,
    description TEXT,
    max_capacity INTEGER DEFAULT 100,
    image_url TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Park users table - tracks which users are in which parks
CREATE TABLE park_users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    park_id UUID REFERENCES parks(id) ON DELETE CASCADE,
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    joined_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Insert sample zoo animals
INSERT INTO animals (name, species, description, image_url, is_zoo_animal, default_colors) VALUES
('Lion', 'Panthera leo', 'King of the jungle!', '/images/lion.png', true, '{"primary": "#FFD700", "secondary": "#8B4513"}'),
('Elephant', 'Loxodonta africana', 'The largest land animal.', '/images/elephant.png', true, '{"primary": "#F5F5DC", "secondary": "#696969"}'),
('Zebra', 'Equus quagga', 'Striped and proud!', '/images/zebra.png', true, '{"primary": "#FFFFFF", "secondary": "#000000"}'),
('Giraffe', 'Giraffa camelopardalis', 'Tallest animal on Earth.', '/images/giraffe.png', true, '{"primary": "#F5DEB3", "secondary": "#8B4513"}'),
('Penguin', 'Aptenodytes forsteri', 'Cool and waddling!', '/images/penguin.png', true, '{"primary": "#000000", "secondary": "#FFFFFF"}'),
('Monkey', 'Macaca fascicularis', 'Playful and clever!', '/images/monkey.png', true, '{"primary": "#C0C0C0", "secondary": "#8B4513"}');

-- Enable Row Level Security (RLS) for Supabase
ALTER TABLE users ENABLE ROW LEVEL SECURITY;
ALTER TABLE animals ENABLE ROW LEVEL SECURITY;
ALTER TABLE user_animals ENABLE ROW LEVEL SECURITY;
ALTER TABLE parks ENABLE ROW LEVEL SECURITY;
ALTER TABLE park_users ENABLE ROW LEVEL SECURITY;

-- RLS Policies for users table
CREATE POLICY "Users can read their own data" ON users
    FOR SELECT USING (auth.uid() = id);

CREATE POLICY "Users can update their own data" ON users
    FOR UPDATE USING (auth.uid() = id);

-- RLS Policies for animals table - everyone can read zoo animals
CREATE POLICY "Anyone can read zoo animals" ON animals
    FOR SELECT USING (is_zoo_animal = true);

-- RLS Policies for user_animals table
CREATE POLICY "Users can read their own animals" ON user_animals
    FOR SELECT USING (auth.uid() = user_id);

CREATE POLICY "Users can create their own animals" ON user_animals
    FOR INSERT WITH CHECK (auth.uid() = user_id);

-- RLS Policies for parks table - everyone can read parks
CREATE POLICY "Anyone can read parks" ON parks
    FOR SELECT USING (true);

CREATE POLICY "Authenticated users can create parks" ON parks
    FOR INSERT WITH CHECK (auth.role() = 'authenticated');

-- RLS Policies for park_users table
CREATE POLICY "Users can manage their own park memberships" ON park_users
    FOR ALL USING (auth.uid() = user_id);