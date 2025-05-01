# Check-If-Email-Exists-ConsoleApp

A simple C# console application that checks if an email address exists using the [Reacher email verification API](https://github.com/reacherhq/check-if-email-exists). This project runs the Reacher backend locally in Docker and uses `HttpClient` to send verification requests.

---

## 📦 Features

- Validates syntax, reachability, and SMTP deliverability
- Detects disposable, role-based, and B2C email accounts
- Runs locally without needing any external API key
- Interactive console interface

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [check-if-email-exists](https://github.com/reacherhq/check-if-email-exists)

## ⬇️ Download
Latest EmailVerifier-Release.zip built with github actions 😊
https://github.com/0xAnthonyW/Check-If-Email-Exists-ConsoleApp/releases


## Developer Build instructions 

### 1. Clone the Repository

```bash
git clone https://github.com/0xAnthonyW/Check-If-Email-Exists-ConsoleApp.git
cd Check-If-Email-Exists-ConsoleApp
```

### 2. Run the Reacher Email Verification Server

```
docker run -p 8080:8080 reacherhq/backend:latest
```

This will expose the verification API locally on http://localhost:8080

### 3. Build and Run the Console App
```
dotnet build
dotnet run
```

You’ll be prompted to enter an email address to verify.

Example Output
```
Enter email to check (or 'exit' to quit): firstname@google.com
Sending request to verify email...

Verification Result:
Input: firstname@google.com
Reachability: risky
Disposable: False
Role Account: False
B2C: False
Accepts Mail: True
Can Connect SMTP: True
Is Deliverable: True
Valid Syntax: True

Press any key to continue...
```
## 🛠️ Built With
C# / .NET 8

Reacher backend (Docker)

HttpClient + System.Text.Json

## 🔗 Related Projects
https://github.com/reacherhq/check-if-email-exists

