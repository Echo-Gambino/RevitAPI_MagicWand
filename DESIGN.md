# Design

### Functionality

* When the user clicks on an object (wall, floor, windows, etc) it selects it and keeps it selected like how you would normally multi-select in Revit (CTRL + RMB)
* When the user performs a right click, any and all selected elements will deselect, does nothing if nothing was selected
* The selection's midpoint is automatically calculated
* Can move the object group around like a single entity if dragged using the LMB

### Deployment

* Given as a MagicWand.addin and MagicWand.dll file.
  * Currently, the user must modify the addin file to select the path to the MagicWand.dll, as the path to where the dll will be placed couldn't be guaranteed as of this moment

