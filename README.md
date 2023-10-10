# Password Manager

Password manager full stack application composed of a C# .NET Framework backend and a React frontend.
Passwords are encrypted and saved in a database with a display name used to
identify the them. Display names have any sensitive information redacted before saving.
For example, a key with name "username@email.com password" will be saved with name
"&lt;EMAIL> password".
Backend utilizes the Pangea Cloud C# SDK to access
the following security services:

- <b>Vault:</b> Used to encrypt/decrypt passwords saved in the database.
- <b>Redact:</b> Removes sensitive information from displayed password names.

## Run Backend

Create a file at `backend/PasswordManager/PasswordManager/appsettings.Development.json`
and copy the contents of `backend/PasswordManager/PasswordManager/appsettings.json`
into it. Edit the values in `appsettings.Development.json` to match your configuration.
Then run the service with:

```
cd backend/PasswordManager/PasswordManager
dotnet run
```

Backend service will run at `localhost:5000`.
The service contains the following endpoints:
| endpoint | method| description |
| -------- | ---- |------------|
| /passwords | GET | Get the name and ID of all saved passwords.|
| /passwords | POST | Save a new password by providing a name and value in JSON. |
| /passwords/{id} | GET | Get the decrypted value of a password.|

## Run Frontend

```
cd frontend/password-manager
npm install
npm run dev
```

Password manager UI will run at `localhost:5173`
![password manager](/assets/password-manager.png)

Save a new password by clicking the "ADD" button in the top right.

![password manager](/assets/new-password.png)

## OpenAPI

OpenAPI API spec can be viewed at http://localhost:5000/swagger/index.html.

![openapi](/assets/openapi.png)
