## MAUIZebraDataWedge POC Sample
Just of POC sample app to test MAUI framework with Datawedge Intents , bluetooth & printing functionality. 

### UseCases
[x] Read datawedge scan
[x] Enable/disable scanner
[x] Read bluetooth status & paired devices
[x] Find target printer by name
[x] Print ZPL code to zebra test printer

### Fameworks & packages used
- MAUI RC1
- dotnet6 (6.0.300-preview.22204.3)
- Zebra.Sdk.Printer 

### Datawedge intent
Platforms > Android > Services > ScannerService.cs has intent broadcast to enable / disable scanner api. 
When scanning make sure text field is selected.
