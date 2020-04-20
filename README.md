# WSPay.NET
[![NuGet](https://img.shields.io/nuget/v/WSPay.NET.svg)](https://www.nuget.org/packages/WSPay.NET/)
![](https://github.com/Dobartek/wspay-dotnet/workflows/dotnetcore/badge.svg)

WSPay.NET is an async/sync client for WSPay API.
## Installation

Using the `.NET Core command-line interface (CLI)`:

```sh
dotnet add package WSPay.NET
```

Using the `NuGet Command Line Interface (CLI)`:

```sh
nuget install WSPay.NET
```

Using the `Package Manager Console`:

```powershell
Install-Package WSPay.NET
```

## Configuration
WSPay can be configured to use `Mode.Test` (default) or `Mode.Prod` mode. 

Using the app settings:
```xml
<appSettings>
  <add key="WSPayMode" value="Test" />
  <add key="WSPayRegularShopId" value="myShopId" />
  <add key="WSPayRegularShopSecret" value="myShopSecret" />
  <add key="WSPayTokenShopId" value="tokenShopId" />
  <add key="WSPayTokenShopSecret" value="tokenShopSecret" />
</appSettings>
```

Using the code configuration:
```csharp
WSPayConfiguration.Mode = Mode.Test;            
WSPayConfiguration.RegularShop = new Shop("regularShopId", "regularShopSecret");
WSPayConfiguration.TokenShop = new Shop("tokenShopId", "tokenShopSecret");
```

## Usage

### With dependency injection
```csharp
// Autofac
public void RegisterWsPayServices(ContainerBuilder builder)
{
    builder.RegisterType<SignatureFactory>()
        .As<ISignatureFactory>()
        .SingleInstance();

    builder.RegisterType<RequestFactory>()
        .As<IRequestFactory>()
        .SingleInstance();

    builder.RegisterType<WSPayApiClient>()
        .As<IWSPayClient>()
        .SingleInstance();

    builder.RegisterType<WSPayService>()
        .As<IWSPayService>()
        .SingleInstance();

    builder.RegisterType<FormSuccessResponse>();
    builder.RegisterType<FormErrorResponse>();
}
