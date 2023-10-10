# Password Manager

Password manager full stack application composed of a C# .NET Framework backend and a React frontend.
Backend utilizes the Pangea Cloud C# SDK to access
the following security services:

- Vault: Used to encrypt/decrypt passwords saved in the database.
- Redact: Removes sensitive information from publicly displayed password names

## Run Backend

```
cd backend/PasswordManager
dotnet run
```

Backend service will run at `localhost:5000`.
Swagger API spec can be viewed at http://localhost:5000/swagger/index.html.

## Run Frontend

```
cd frontend/password-manager
npm run dev
```
