# ComponentCraft

A comprehensive Blazor Server + Razor Class Library (RCL) project showcasing reusable UI components.

## Project Structure

- **ComponentCraft.Components** - Razor Class Library containing reusable UI components
- **ComponentCraft.Web** - Blazor Server host application with component demos

## Components Library

### Buttons
- **PrimaryButton** - Primary action buttons with icons
- **SecondaryButton** - Secondary action buttons  
- **IconButton** - Icon-only buttons with tooltips

### Cards
- **InfoCard** - Information cards with header, body, and footer
- **StatCard** - Statistics display cards with icons and values
- **ProfileCard** - User profile cards with avatars

### Forms
- **TextInput** - Text input fields with labels and help text
- **SelectInput** - Dropdown select inputs
- **DatePicker** - Date selection inputs

### Layout
- **PageHeader** - Page headers with title, subtitle, and action buttons
- **Sidebar** - Collapsible sidebar navigation
- **Footer** - Page footers

### Feedback
- **Alert** - Alert messages (info, success, warning, error)
- **Toast** - Toast notifications
- **LoadingSpinner** - Loading spinners with messages
- **ProgressBar** - Progress indicators

## Features

- 🎨 **Customizable Themes** - CSS variables for easy theming
- 🔥 **Hot Reload Ready** - All components designed for hot reload testing
- 📱 **Responsive** - Mobile-friendly components
- ⚡ **Reusable** - Clean, parameterized components

## Demo Pages

- `/` - Home page with component gallery overview
- `/components/buttons` - Button component demos
- `/components/cards` - Card component demos
- `/components/forms` - Form component demos
- `/components/feedback` - Feedback component demos
- `/playground` - Interactive component playground
- `/themes` - Theme customization page

## Running the Application

```bash
cd ComponentCraft.Web
dotnet run
```

The application will be available at `http://localhost:5122`

## Building

```bash
# Build the RCL
dotnet build ComponentCraft.Components/ComponentCraft.Components.csproj

# Build the Web app (includes RCL)
dotnet build ComponentCraft.Web/ComponentCraft.Web.csproj
```

## Hot Reload Testing

This project is specifically designed for Hot Reload testing in .NET. You can:

1. Run the application with hot reload: `dotnet watch --project ComponentCraft.Web`
2. Modify any component in the RCL
3. Modify any page in the Web project
4. See changes reflected immediately without restart

## Theme Customization

Override CSS variables in your app to customize the theme:

```css
:root {
    --cc-primary: #your-color;
    --cc-primary-hover: #your-hover-color;
    --cc-secondary: #your-color;
    --cc-success: #your-color;
    --cc-warning: #your-color;
    --cc-error: #your-color;
}
```
