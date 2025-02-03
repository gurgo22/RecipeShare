# RecipeShare Web Application

RecipeShare is a recipe-sharing web application built with **ASP.NET Core** and **Entity Framework**. This app allows users to explore, rate, and comment on a variety of recipes, with features that showcase technicalities in web application development, such as caching, role-based access control, and user activity tracking.

---

## Features

### Recipe Listings
- View a list of recipes with filter and search options based on the recipe name and country of origin.

### Detailed Recipe View
- Each recipe page displays ingredients, ratings, and user comments.

### Create, Edit, and Delete Recipes
- Admin and moderator roles can manage recipes by creating, editing, and deleting entries.

### Commenting
- Logged-in users can add comments on recipe pages.

### Rating
- Users can rate recipes, with checks to ensure that a recipe is only rated once per user.

### Recipe Caching
- Popular recipes are cached for faster access using in-memory caching. Cache settings ensure that data remains current.

### User Activity Logging and Analytics
- Admins can view logs of user activities to monitor usage patterns within the application.

### Accessibility Toolbar
- A custom toolbar allows users to adjust text size, contrast, and other display settings for an enhanced user experience.

---

## Security and Role-Based Access Control

### Authorization
- Specific actions are restricted to roles (Admin and Moderator) to ensure secure access control.

### User Authentication
- Built-in authentication restricts certain features, like commenting and rating, to logged-in users.

---

## Technology Stack

- **ASP.NET Core** – Web framework for building the application.
- **Entity Framework Core** – ORM for database interaction.
- **Microsoft SQL Server** – Database management system.
- **Identity Framework** – User authentication and role-based access control.
- **IMemoryCache** – Caching service for frequently accessed data.

