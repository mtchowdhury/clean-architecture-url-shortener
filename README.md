# clean-architecture-url-shortener
A demo url shortener focusing on clean architecture.

## External Packages/Libraries used:

* FastEndPoints
* MediatR
* EntityFrameworkCore
* Automapper
* FluentValidation
* Hashids.net
* Microsoft.AspNetCore.Authentication.JwtBearer

## Authentication

`POST /api/login` issues a JWT, and creating a short URL requires it. The creating user is recorded on the URL.
Credentials are checked by `ICredentialValidator`. The included implementation validates a single
user from configuration and stands in for a real user store.

Nothing sensitive is committed. Set the values through user-secrets or environment variables:

```bash
cd src/Api
dotnet user-secrets set "Auth:Secret"   "<at least 32 characters>"
dotnet user-secrets set "Auth:Username" "<username>"
dotnet user-secrets set "Auth:Password" "<password>"
```

The API refuses to start without `Auth:Secret`.
