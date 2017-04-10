# Usage

For the most up to date information on adding additional grid row & column configuration checkout the [Our Umbraco documentation](https://our.umbraco.org/documentation/getting-started/backoffice/property-editors/built-in-property-editors/grid-layout/settings-and-styles).

This documentation will assume you will know how to add settings to a grid row or column.

**NOTE**: Capabilities for core grid settings has changed from version to version (especially v7.5.4+). Please be sure to check compatability will your version of Umbraco.

## Sample Usage

Here are sample usages of the currently supported property editors. You are not limited to simply these usages though. Be creative! :)

### Background Position

 - **Introduced in**: [v0.1.0-beta](https://github.com/jamiepollock/umbraco-grid-settings/releases/tag/v0.1.0-beta)
 - **Typical CSS Property**: background-position
 - **Supported values**:
   - **Vertical**: top, center & bottom
   - **Horizontal**: left, center & right
 - **Editor**: 3&times;3 grid

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

 - **Introduced in**: [v0.1.0-beta](https://github.com/jamiepollock/umbraco-grid-settings/releases/tag/v0.1.0-beta)
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

 - **Introduced in**: [v0.1.0-beta](https://github.com/jamiepollock/umbraco-grid-settings/releases/tag/v0.1.0-beta)
 - **Typical CSS Property**: background-size
 - **Supported values**:
   - auto
   - cover
   - contain
 - **Editor**: Check Radio List

```json
...
  {
    "label": "Set background size",
    "description": "Set background size",
    "key": "background-size",
    "view": "/App_Plugins/Our.Umbraco.GridSettings/editors/BackgroundSize/view.html"
  }
...
```

### Color Picker

 - **Introduced in**: [v0.1.0-beta](https://github.com/jamiepollock/umbraco-grid-settings/releases/tag/v0.1.0-beta)
 - **Typical CSS Property**: background-color, color (anything which expects a hexcode color)
 - **Supported values**: Developer provided prevalues (see below). The value or string must be a valid hexcode color value. Non-hexcode color values are not supported.
 - **Editor**: Simple color picker (reupurposing the Approved Color Picker editor)

**Note**: Prior to v7.5.4 grid prevalues only supported a string array. From v7.5.4+ prevalues support a mixture of label/value objects and strings.

#### Background Color Picker

```json
  {
    "label": "Set background color",
    "description": "Set the row background color",
    "key": "background-color",
    "view": "/App_Plugins/Our.Umbraco.GridSettings/editors/ColorPicker/view.html",
    "prevalues": [
      {
        "label": "Blue",
        "value": "#0000ff"
      },
      {
        "label": "Green",
        "value": "#00ff00"
      },
      "#ff0000"
    ]
  },
```

#### Foreground Color Picker

```json
  {
    "label": "Set text color",
    "description": "Set the row text color",
    "key": "color",
    "view": "/App_Plugins/Our.Umbraco.GridSettings/editors/ColorPicker/view.html",
    "prevalues": [
      {
        "label": "Black",
        "value": "#000000"
      },
      {
        "label": "White",
        "value": "#ffffff"
      },
      "#808080"
    ]
  }
```

### Text Align

 - **Introduced in**: [v0.1.0-beta](https://github.com/jamiepollock/umbraco-grid-settings/releases/tag/v0.1.0-beta)
 - **Typical CSS Property**: text-align
 - **Supported values**:
   - left
   - right
   - center
   - justify
 - **Editor**: Check Radio List

```json
...
  {
    "label": "Set text alignment",
    "description": "Set text alignment",
    "key": "text-align",
    "view": "/App_Plugins/Our.Umbraco.GridSettings/editors/TextAlign/view.html"
  }
...
```

### Class Color Picker

Class Color Picker is for cases where an developer will want to return a class value rather than a hexcode color.

This could be used in cases where the hexcode color is simply a representation of the idea of what that class looks like.

Basicially it will return "green" rather than "#00ff00" as its value. This means the label & value are switched from what is used in the Color Picker property editor.

 - **Introduced in**: [v0.2.0](https://github.com/jamiepollock/umbraco-grid-settings/releases/tag/v0.2.0)
 - **Typical CSS Property**: class
 - **Supported values**: Developer provided prevalues (see below).
   - label: A valid hexcode color value. Non-hexcode color values are not supported.
   - value: The class value to be rendered in Grid markup
 - **Editor**: Simple color picker (reupurposing the Approved Color Picker editor)

**Note**: This property editor only supports key value pair prevalues and there is only available for Umbraco v7.5.4. Providing string values will produce a handled error.

#### Background Color Picker

```json
  {
    "label": "Set background color",
    "description": "Set the row background color",
    "key": "background-color",
    "view": "/App_Plugins/Our.Umbraco.GridSettings/editors/ClassColorPicker/view.html",
    "prevalues": [
      {
        "label": "#0000ff",
        "value": "Blue"
      },
      {
        "label": "#00ff00",
        "value": "Green"
      },
      {
        "label": "#ff0000",
        "value": "Red"
      }
    ]
  },
```