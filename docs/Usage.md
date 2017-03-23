# Usage

For the most up to date information on adding additional grid row & column configuration checkout the [Our Umbraco documentation](https://our.umbraco.org/documentation/getting-started/backoffice/property-editors/built-in-property-editors/grid-layout/settings-and-styles).

This documentation will assume you will know how to add settings to a grid row or column.

**NOTE**: Capabilities for core grid settings has changed from version to version (especially v7.5.4+). Please be sure to check compatability will your version of Umbraco.

## Sample Usage

Here are sample usages of the currently supported property editors. You are not limited to simply these usages though. Be creative! :)

### Background Position

 - **Typical CSS Property**: background-position
 - **Supported values**:
   - **Vertical**: top, center & bottom
   - **Horizontal**: left, center & right
 - **Editor**: 3 &times; 3 grid

```json
...
  {
    "label": "Set a background position",
    "description": "Set a background position",
    "key": "background-position",
    "view": "/App_Plugins/Our.Umbraco.GridSettings/editors/BackgroundPosition/view.html"
  }
...
```

### Background Repeat

 - **Typical CSS Property**: background-repeat
 - **Supported values**:
   - no-repeat
   - repeat
   - repeat-x
   - repeat-y
 - **Editor**: Check Radio List

```json
...
  {
    "label": "Set background repetition",
    "description": "Set background repetition",
    "key": "background-repeat",
    "view": "/App_Plugins/Our.Umbraco.GridSettings/editors/BackgroundRepeat/view.html"
  }
...
```

### Background Size

 - **Typical CSS Property**: background-size
 - **Supported values**:
   - auto
   - cover
   - contain
 - **Editor**: Check Radio List

```json
...
  {
    "label": "Set background repetition",
    "description": "Set background repetition",
    "key": "background-size",
    "view": "/App_Plugins/Our.Umbraco.GridSettings/editors/BackgroundSize/view.html"
  }
...
```