#!/bin/bash

# Build the release version
echo "Building release version..."
dotnet publish AnimalZ.Client -c Release -o ./publish --no-restore

# Deploy to GitHub Pages
echo "Deploying to GitHub Pages..."

if command -v gh &> /dev/null; then
    # Check if we're in a git repository
    if git rev-parse --git-dir > /dev/null 2>&1; then
        # Get the current branch
        CURRENT_BRANCH=$(git rev-parse --abbrev-ref HEAD)
        
        # Deploy to GitHub Pages from the publish/wwwroot folder
        gh pages push ./publish/wwwroot --branch gh-pages --message "Deploy to GitHub Pages"
        
        echo "Deployment complete! Your site will be available at:"
        echo "https://<your-username>.github.io/<repo-name>/"
    else
        echo "Not in a git repository. Please initialize git and authenticate with GitHub first."
        echo "Run: gh auth login"
    fi
else
    echo "GitHub CLI not installed. Installing..."
    
    # Install GitHub CLI
    if command -v brew &> /dev/null; then
        brew install gh
        echo "Please run 'gh auth login' to authenticate with GitHub"
    else
        echo "Please install GitHub CLI manually: https://cli.github.com/"
    fi
fi

echo ""
echo "Alternative manual method:"
echo "1. Create a public repo named '<your-username>.github.io' or '<project-name>'"
echo "2. Push the contents of ./publish/wwwroot to the 'gh-pages' branch"
