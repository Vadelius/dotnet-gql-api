![.NET Core](https://github.com/Vadelius/dotnet-gql-api/workflows/.NET%20Core/badge.svg)

.NET GraphQL thing.

Run migrations:
```
 dotnet build GraphQL
 dotnet ef migrations add Initial --project GraphQL
 dotnet ef database update --project GraphQL
```

Then ```dotnet run --project GraphQL``` & browse to ```http://localhost:5000/graphql/```

