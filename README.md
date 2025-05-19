# DeviceManagement
APBD Task 9 - Time for Update 

To make application work, you have to generate appsettings.json file and put your connection string, at first
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DeviceDatabase" : ">>> YOUR CONNECTION STRING MUST BE HERE <<<"
  }

}

```

JSON bodies for easy insertion:
[POST]/[PUT]
```
{
  "deviceTypeName": "Monitor",
  "isEnabled": true,
  "additionalProperties": {"color" : "red", "size" : "5inchs", "status" : "it works!!:)"}
}
```
Available Device types: Monitor, Printer, Embedded etc.

The main decision to split a code was done by me personally. Personally, I'm used to, and was taught to, keep each layer as a separate project. In my opinion, this gives me absolute flexibility in customising the project and its packages. Thanks to Kostya, I can understand that choice very well.
