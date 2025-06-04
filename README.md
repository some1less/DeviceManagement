# DeviceManagement 2.0
### Instance of DeviceManagement 1.0
Previously it was "APBD Task 9 - Time for Update", but for now that is UPGRADED version!
Now, the project related to APBD Task 11 and config changed a bit :>

Upgraded project is focused on better security and customer performance.
 
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
    "DeviceDatabase" :  // **YOUR CONNECTION STRING MUST BE HERE**
  },
  "Jwt": {
    "Issuer": "http://localhost:0000",  // **instead of 0000 you can define your own port**
    "Audience": "http://localhost:0000",  // **same here**
    "Key": "eXaMpLeOfKeYfOrGeNeRaTiNGVALIDTOKEN4444",   //   instead of "eXaMpLe..." put your own key.
    "ValidInMinutes" : 10    // set up estimated time for token
  }

}

```
 **IMPORTANT NOTE**: key has to have length >32
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
