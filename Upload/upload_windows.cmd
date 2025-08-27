:: Ez a feltöltő script feltételezi, hogy telepítve van a butler (https://itch.io/docs/butler/)
:: és van egy Builds mappa a projekt mappájával egy szinten, a következő struktúrával:
:: 	Builds/
::	  - WebGL.zip
:: 	  - Windows/
::	      - Windows.zip

@echo off

set target="vdor01/bgj-2025-2"

butler push ..\..\Builds\Windows\Windows.zip %target%:windows --userversion-file version.txt