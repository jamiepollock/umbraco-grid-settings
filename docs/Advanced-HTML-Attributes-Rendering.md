# Advanced HTML Attributes Rendering Guide

This section is only for advanced use cases when custom logic is required to render Umbraco Grid settings. This is not required to use the property editors out of the box. Having said that, we hope you'll try this stuff out. It's pretty cool! :)

The primary purpose of this guide is to walk through the usage of the HTMLHelper extension methods which can be found in the `Our.Umbraco.GridSettings.Web` namespace.

**Note**: at the moment this documentation is mainly intended for Settings rather than Styles. Although Styles are also rendered by this code there is no advanced logic behind their rendering simply outputting the key value pairs from Umbraco.

## Basic usage: Replacing the Umbraco helper methods in a Grid template

This usage guide will walk you through the most basic of usages. Replacing the existing `RenderElementAttributes(dynamic contentItem)` calls in a Grid razor template. It should be noted that the outcome of this replace will be the same as the RenderElementAttributes.

 1. Locate your Grid Razor template. This should be in `~/Views/Partials/Grid/`.
 2. Add a `using` statement to `Our.Umbraco.GridSettings.Web` at the top of the chosen Razor file.
 3. Replace the following:
 

`@RenderElementAttributes(area)` to `@Html.RenderGridSettingsAttributes(area as JObject)`

and 

`@RenderElementAttributes(row)` to `@Html.RenderGridSettingsAttributes(area as JObject)`

 4. Remove the `@functions` block at the bottom of the page.

## Intermediate Usage: Adding in custom render logic

The `@Html.RenderGridSettingsAttributes(JObject)` comes with several optional overloads for tailoring attribute rendering.

 - *attributesResolver*: An `IGridSettingsAttributesResolver` which can group similarly named attributes together into a group so their values can rendered under one attribute. (eg. class)

 - *attributeValueResolvers*: An `IDictionary<string, IGridSettingsAttributeValueResolver>` collection of resolvers that provide logic on how a grouped attribute's value should be rendered. For example if one attribute needed to be rendered differently from other attributes simply specify its group attribute key and then provide a `IGridSettingsAttributeValueResolver` to handle the rendering.

 - *defaultAttributeValueResolver*: The default `IGridSettingsAttributeValueResolver` which is used if no key can be found for an attribute in the *attributeValueResolvers* dictionary.

### Using an IGridSettingsAttributesResolver

Out of the box, there is actually an `IGridSettingsAttributesResolver` which supports grouping of Settings. 

Given the following setup, let's say we have 3 properties we wish to combine together into the `class` attribute:

 1. class_background-color: "color-scheme-red"
 2. class_text-alignment: "align-center"
 3. class_padding: "padding-medium"

We've set up a convention of prefixing our settings with the intended HTML attribute name. Now let's bundle these together.

Firstly we'll need to add an `IGridSettingsAttributesResolver` to our `@Html.RenderGridSettingsAttributes(JObject)` call. Below our using statements add the following code block.

``` c#
@{
  var attributesResolver = new Our.Umbraco.GridSettings.Resolvers.GroupByPrefixTokenGridSettingsAttributesResolver("_");
}
```

This will look for the token `"_"` in our property keys and group similar keys together. In this case, "class".

**Note**: If no `"_"` is found the key is added to the collection as a group of one value.

Now add the `attributesResolver` to our HTML helper like so:

``` c#

`@Html.RenderGridSettingsAttributes(row as JObject, attributesResolver)`

`@Html.RenderGridSettingsAttributes(area as JObject, attributesResolver)`

```

**Note**: the *attributesResolver* will probably need to be passed as a parameter into the `renderRow()` helper as variables created at the top of the file will not be in scope to helper functions.

This should now group the values of these properties together into a single class attribute, then concatenate the values together broken up by whitespace.

``` html
<div class="color-scheme-red align-center padding-medium">
...
</div>
```

### Using IGridSettingsAttributeValueResolvers

If the default behaviour of separating grouped values by whitespace isn't the desired behaviour for one or all of your properties using the `attributeValueResolvers` or `defaultValueResolver` overloads could be the step you need.

For example creating a value resolver for a data attribute could be as simple as using the `StringConcatGridSettingValueResolver` with a different token to create a basic CSV attribute.

Extending the example above:

 1. class_background-color: "color-scheme-red"
 2. class_text-alignment: "align-center"
 3. class_padding: "padding-medium"
 4. data-csv_1: "first"
 5. data-csv_2: "second"
 6. data-csv_3: "third"

``` c#
@using Our.Umbraco.GridSettings.Resolvers;

@{
  var attributesResolver = new GroupByPrefixTokenGridSettingsAttributesResolver("_");

  var attributeValueResolvers = new Dictionary<string, IGridSettingsAttributeValueResolver>() {
    {"data-csv", new StringConcatGridSettingValueResolver(",") }
  };
}
```

``` c#

`@Html.RenderGridSettingsAttributes(row as JObject, attributesResolver, attributeValueResolvers)`

`@Html.RenderGridSettingsAttributes(area as JObject, attributesResolver, attributeValueResolvers)`

```

This should target the data-csv attribute while leaving the class attribute alone. Producing the following HTML.

``` html
<div class="color-scheme-red align-center padding-medium" data-csv="first,second,third">
...
</div>
```

**Note**: In theory far more advanced Value Resolvers could be made potentially rendering JSON in attributes if needed.

### Override the default IGridSettingsAttributeValueResolver

As mentioned above the default `IGridSettingsAttributeValueResolver` can also be overwritten using the *defaultAttributeValueResolver*. Such cases should be carefully considered as this could have implications on our attributes being resolved if no attribute value resolver is set.

### Recommended Usage

IF you're intending to provide multiple overloads to the `@Html.RenderGridSettingsAttributes` it might be a better option to create a `GridSettingsAttributesService` at the top of the razor file with the overloads and pass the service in instead.

For an example of recommended usage have a look at this [gist of Bootstrap3.cshtml](https://gist.github.com/jamiepollock/f2ed2be6744a8bd0f6b48cdd22fbed5a).

## Advanced 

Of course if the default implementation doesn't give you enough control or you're using a grid implementation that doesn't revolve around `JObject`. You can always implement your own `IGridSettingsAttributeService` class and pass that in like so:

``` c#
@using Our.Umbraco.GridSettings;

@{
  IGridSettingsAttributeService<JObject> gridSettingService = new My.Website.CustomGridSettingsAttributeService();
}
```

``` c#

`@Html.RenderGridSettingsAttributes(row as JObject, gridSettingService)`

`@Html.RenderGridSettingsAttributes(area as JObject, gridSettingService)`

```

The most important stipulation is that the service must implement the same type as the first parameter. In theory a IGridSettingsAttributeService could be created to work in conjunction with [Skybrud Umbraco GridData](https://our.umbraco.org/projects/developer-tools/skybrudumbracogriddata/)'s `GridElement`. 

Partial markup based on the [Skybrud Umbraco GridData demo project](https://github.com/abjerner/UmbracoGridDataDemo/).

``` c#
@using Our.Umbraco.GridSettings;

@{
  IGridSettingsAttributeService<GridElement> gridSettingService = new My.Website.SkybrudGridDataGridSettingsAttributeService();
}
```

``` c#

`@Html.RenderGridSettingsAttributes(Model, gridSettingService)`

`@Html.RenderGridSettingsAttributes(area, gridSettingService)`

```
