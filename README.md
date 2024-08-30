<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/260013606/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T907025)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Grid for Blazor - How to use a dictionary to configure the component state

This example demonstrates how you can use a dictionary to configure the [Blazor Grid](https://docs.devexpress.com/Blazor/403143/components/grid) component. This technique allows you to configure multiple components based on one options set.

![Use a Dictionary to Configure the Component State](/image.png)

## Overview

Create a [ComponentBase](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.componentbase?view=aspnetcore-3.1) class descendant that accepts a data source, grid columns, and grid settings stored in a dictionary. In the created class, configure the Grid component at runtime as follows:

```cs
public class MyGrid<T> : ComponentBase
{
    [Parameter]
    public IEnumerable<T> Data { get; set; }
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    [Parameter]
    public Dictionary<string, object> Settings { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder) {
        builder.OpenComponent<DxGrid>(0);
        builder.AddAttribute(1, "Data", (object)Data);
        builder.AddAttribute(2, "Columns", ChildContent);
        if (Settings != null) {
            builder.AddMultipleAttributes(3, Settings);
            //OR
            //int seq = 3;
            //foreach (var item in Settings) {
            //    builder.AddAttribute(seq++, item.Key, item.Value);
            //}
        }
        builder.CloseComponent();
    }
}
```

Create a dictionary that stores setting names and values. Assign this dictionary to the corresponding component parameter to create and configure the Grid:

```razor
<MyGrid Data="Forecasts" Settings="InputAttributes" >
	<DxGridCommandColumn Width="150px" />
	<DxGridDataColumn FieldName="@nameof(WeatherForecast.Date)" />
	<DxGridDataColumn FieldName="@nameof(WeatherForecast.TemperatureC)" />
	<DxGridDataColumn FieldName="@nameof(WeatherForecast.TemperatureF)" />
	<DxGridDataColumn FieldName="@nameof(WeatherForecast.Summary)" />
</MyGrid>

@code {
    public List<WeatherForecast> Forecasts { get; set; }

    public Dictionary<string, object> InputAttributes { get; set; } =
        new Dictionary<string, object>() {
            { "EditMode", GridEditMode.EditRow},
            { "PageSize", 5 },
            { "ShowFilterRow", false },
            { "PagerVisible" , false },
            { "ShowGroupPanel", true }
	};
    protected override async Task OnInitializedAsync() {
        base.OnInitialized();
        WeatherForecast[] data = await ForecastService.GetForecastAsync(DateTime.Now);
        Forecasts = data.ToList();
    }
}
```
 
## Files to Review

- [MyGrid](./CS/DxBlazorApp/Components/MyGrid.cs)
- [Index.razor](./CS/DxBlazorApp/Pages/Index.razor)

## Documentation

* [Reuse and Customize Components](https://docs.devexpress.com/Blazor/401753/common-concepts/customize-and-reuse-components)

## More Examples

* [DevExpress Blazor Components - Set predefined settings for a specific component](https://github.com/DevExpress-Examples/blazor-default-settings)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-dxgrid-use-a-dictionary-to-configure-the-component&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-dxgrid-use-a-dictionary-to-configure-the-component&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
