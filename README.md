# EventHubsCustomExtension

## Case One (Provide EventHubsCustomExtension dll and user customer does not use extensionBundle):

  1. Stop the application.
  2. Upgrade eventhub extension to the latest version.
  3. Place the EventHubsCustomExtension.dll in the D:\home\site\wwwroot\bin.
  4. Add { "name": "EventHubsCustomExtension", "typeName":"EventHubsCustomExtension.EventHubsCustomExtensionStartup, EventHubsCustomExtension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"} as the last line at D:\home\site\wwwroot\bin\extensions.json 
  
Restart the Function App.

## Case Two (user uses extensionBundle):

We have two options.
### Option 1: Provide all extenstions zip folder, Disadvantage the user can not upgrade to new versions.
  1. User needs to remove the support for extensionBundle.
  2. We can build all extension along with our custome one and share it with the user, The user then need to point their app settings to the location for the extensions.  
  
### Option 2: Provide the EventHubsCustomExtension nuget, Disadvantage the user needs to have .net installed.

  1. User needs to remove the support for extensionBundle.
  2. User will need to install all extensions they need.
  3. func install custom nuget.  --> This will add the gnerated dll in the bin folder of the function app.
  4. Update { "name": "EventHubsCustomExtension", "typeName":"EventHubsCustomExtension.EventHubsCustomExtensionStartup, EventHubsCustomExtension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"} as the last line at .\bin\extensions.json.
  5. Deploy the function.
  
Restart the Function App.


