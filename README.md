# 📋 GestionConges - Leave Management System

A modern web application for managing employee leave requests built with ASP.NET Core 9.0.

## 🎯 Features

- **Employee Leave Requests**: Submit and track leave requests
- **Admin Dashboard**: Approve, reject, and manage all requests
- **Tracking System**: Unique codes for request tracking
- **Responsive Design**: Works on desktop, tablet, and mobile
- **Secure Authentication**: ASP.NET Core Identity integration
- **Data Export**: CSV export functionality for administrators

## 🖼️ Screenshots

### Home Page - Leave Request Form
![Home Page](https://via.placeholder.com/800x400/007bff/ffffff?text=Home+Page+-+Leave+Request+Form)

### Admin Dashboard
![Admin Dashboard](https://via.placeholder.com/800x400/28a745/ffffff?text=Admin+Dashboard)

### Request Tracking Page
![Tracking Page](https://via.placeholder.com/800x400/ffc107/000000?text=Request+Tracking+Page)

### Login Page
![Login Page](https://via.placeholder.com/800x400/6c757d/ffffff?text=Login+Page)

## 🚀 Quick Start

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/hugsqueen1/GestionConges.git
   cd GestionConges
   ```

2. **Run the application**
   ```bash
   dotnet run
   ```

3. **Access the application**
   - URL: `https://localhost:5001` or `http://localhost:5000`
   - Admin login: `admin@demo.com` / `MotDePasse123!`

## 📱 How to Use

### For Employees
1. **Submit a request**: Fill out the form on the home page
2. **Get tracking code**: Save the unique code provided
3. **Track your request**: Use the code on the tracking page
4. **Modify if needed**: Edit dates or cancel if still pending

### For Administrators
1. **Login**: Use admin credentials
2. **Access dashboard**: View all leave requests
3. **Manage requests**: Approve, reject, or delete requests
4. **Export data**: Download CSV reports

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core 9.0
- **Frontend**: Razor Pages, Bootstrap 5
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Icons**: Font Awesome 6.0

## 📊 Database Schema

The application uses a simple but effective schema:
- **Conge**: Leave requests with employee info, dates, and status
- **Identity**: User authentication and authorization

## 🔒 Security Features

- Password hashing with ASP.NET Core Identity
- Input validation (client and server-side)
- SQL injection protection via Entity Framework
- XSS protection with Razor Pages

## 📈 Project Structure

```
GestionConges/
├── Pages/                 # Razor Pages
│   ├── Index.cshtml      # Home page (submit requests)
│   ├── Suivi.cshtml      # Request tracking
│   └── DashboardAdmin.cshtml # Admin dashboard
├── Areas/Identity/       # Authentication pages
├── wwwroot/             # Static files (CSS, JS, images)
├── Migrations/          # Database migrations
└── DOCUMENTATION_PROJET.md # Detailed documentation
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

If you have any questions or need help, please open an issue on GitHub.

---

**Built with ❤️ using ASP.NET Core** 