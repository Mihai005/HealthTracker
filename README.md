# 🏥 **HealthTracker** - Full Stack Application

Welcome to the **HealthTracker** project! This application is designed to help users track their health and fitness, including **meal tracking**, **water intake**, **steps** taken, and **personal AI coach**. It consists of a **WPF front-end (MVVM architecture)** and a **Java Spring Boot back-end**.

---

## 🔧 **Technologies Used**

### **Back-End (Java Spring Boot)**

- **Java 21**
- **Spring Boot** (v3.4.4)
- **Spring Data JPA** (for database interaction)
- **Lombok** (for boilerplate code reduction)
- **Jakarta Persistence (JPA)** (for ORM-based database interactions)
- **SQL Server** (for database management, specified schema `dbo`)
- **Spring Security** (for user authentication and authorization)

### **Front-End (WPF - MVVM Architecture)**

- **C#** with **WPF**
- **MVVM Pattern** (Model-View-ViewModel)
- **XAML** (for designing the user interface)
- **RelayCommand** (for command binding)
- **JSON API integration** (for communication with the Spring Boot back-end)

---

## 🏁 **Getting Started**

These instructions will help you set up the project locally and run both the back-end API and the front-end.

### Prerequisites

Before running this project, ensure you have the following installed on your machine:

#### **Back-End Requirements**

- **JDK 21**
- **Maven** (for managing dependencies)
- **IDE** like IntelliJ IDEA or Eclipse
- **Postman** (or any other API testing tool)
- **SQL Server** (or your preferred database system)

#### **Front-End Requirements**

- **Visual Studio** (or your preferred C# development environment)
- **.NET Core 8.0** (for running WPF applications)

---

### Clone the repository

Clone the entire project to your local machine:

```bash
git clone https://github.com/yourusername/HealthTracker.git
```


### Setup Instructions

To get the HealthTracker project up and running, follow these steps:

---

#### 1. **Backend Setup**

1. **Navigate to the Backend Directory**  
   Open your terminal or command prompt and run:
   ```bash
   cd HealthTracker/backend
   ```

2. **Install Project Dependencies**  
   Install the required dependencies using Maven:
   ```bash
   mvn install
   ```

3. **Set Up the Database**  
   - **Install SQL Server**: If you haven't already, install SQL Server.
   - **Create the Database**: Set up a new database for the project.
   - **Configure Database Connection**: Open the `application.properties` file and update the connection settings with your database credentials.
   
4. **Run the Backend Application**  
   Start the Spring Boot application with:
   ```bash
   mvn spring-boot:run
   ```

---

#### 2. **Frontend Setup**

1. **Open the Project in Visual Studio**  
   Launch Visual Studio and open the project folder.

3. **Set the API URL**  
   Open the `App.config` file and set the correct API URL to point to your running backend application.
   
