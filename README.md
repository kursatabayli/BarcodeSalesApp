# Barcode Sales App

This project is a desktop barcode-based sales and inventory management application developed with .NET MAUI Blazor, designed for small to medium-sized businesses. The application aims to simplify sales processes by quickly scanning products with a barcode reader, tracking inventory, and generating daily cash reports.

## ✨ Key Features

* **Barcode-Based Sales Screen:** Instantly add products to the sales cart by scanning them with a barcode reader.

* **Product Management:** Add new products to the system, edit the information (price, name, etc.) of existing products, and delete them.

* **Inventory Management:** Manage stock entries for products, handle inventory quantities on a case/package basis, and reset existing stock.

* **Daily Cash-Out Report:** View total sales, total profit, and a list of sold products for a specific date.

* **Multi-language Support:** Switch between Turkish and English languages.

* **Customizable UI:** Support for both dark and light mode themes.

* **Keyboard Shortcuts:** Assign customizable keyboard shortcuts for completing sales and clearing the cart.

* **Search and Filtering:** Perform instant searches in product and sales lists.

* **Local Database:** All data is stored in a local SQLite database, requiring no internet connection.

## 🛠️ Tech Stack & Libraries
* **Framework:** .NET MAUI, Blazor Hybrid

* **Programming Language:** C#

* **UI Library:** MudBlazor

* **Database:** SQLite

* **Barcode Scanning:** ZXing.Net.Maui

* **Architectural Patterns:** Repository Pattern, Dependency Injection

## 📂 Project Structure
The project is organized using a layered architecture to ensure the code is sustainable and manageable.
```
├── BarcodeSalesApp
│   ├── Components/         # Blazor components (UI parts)
│   │   ├── Layout/         # Core layout components like MainLayout, NavMenu, and AppBar
│   │   └── Pages/          # The application's pages (Home, ProductList, Settings, etc.)
│   │
│   ├── Constants/          # Constants for Language, Theme, and Shortcut options
│   ├── Extensions/         # Extension methods for service registration
│   ├── Helpers/            # Helper classes for Cart, Focus, and Search management
│   ├── Interfaces/         # Interfaces used for abstraction (IRepository, INavigationService, etc.)
│   ├── Models/             # Entity classes representing database tables (Product, SalesRecord)
│   ├── Platforms/          # Platform-specific code (Windows, Android, etc.)
│   ├── Repositories/       # Classes that handle database operations (ProductRepository)
│   ├── Resources/          # Application resources
│   │   └── Locales/        # .resx files for multi-language support
│   │
│   ├── Services/           # Service classes containing the business logic (SalesService, ProductService)
│   └── wwwroot/            # Static web assets (CSS, HTML, fonts)
```

## 🚀 Setup and Installation
1.  **Clone the Project:**
    ```bash
    git clone https://github.com/kursatabayli/barcodesalesapp.git
    ```

2. **Open the Project:**
   * Open the BarcodeSalesApp.sln file with Visual Studio 2022 or a later version.

3. **Restore NuGet Packages:**
   * Visual Studio should automatically restore the dependencies when you open the project. If not, right-click on the solution in the Solution Explorer and select "Restore NuGet Packages".

4. **Run the Application:**
   * In Visual Studio, set the project under Platforms/Windows as the startup project and press F5 to launch the application. The database file (BarkodDB.db3) will be created automatically on the first run.

## 📝 Usage
* **Making a Sale:** On the main page, scan products with a barcode reader to add them to the cart. Complete the sale using your assigned shortcut key.

* **Adding a Product:** Navigate to the "Add Product" page from the left menu to save new products to the system.

* **Updating Stock:** Select a product from the "Product List", go to its details, and use the "Update Stock" button to add new inventory.

* **Viewing Reports:** Check the sales reports for any desired date from the "Cash Out" page.

* **Settings:** Customize the theme, language, and keyboard shortcuts on the "Settings" page.
