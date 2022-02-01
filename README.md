<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T907025)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Blazor - How to reuse a component and configure its state via a dictionary

In your application you have multiple components. For instance, let it be DataGrid. Some of the grids have one set of settings-other grids - other settings:

```razor
Group 1
<DxDataGrid PageSize="5" ShowFilterRow="false"  ShowPager="false" ShowGroupPanel="true" ...>
<DxDataGrid PageSize="5" ShowFilterRow="false"  ShowPager="false" ShowGroupPanel="true" ...>
<DxDataGrid PageSize="5" ShowFilterRow="false"  ShowPager="false" ShowGroupPanel="true" ...>
...
Group M
<DxDataGrid PageSize="15" ShowFilterRow="false"  AutoCollapseDetailRow="true" ShowDetailRow="true" ShowFilterRow="false" SelectionMode="DataGridSelectionMode.None"  ...>
```

To prevent writing a such routine N times for every grid, you may want to have a minimum set of dictionaries with a grid's settings. To support this scenario, create a [ComponentBase](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.componentbase?view=aspnetcore-3.1)'s class descendant and configure **DxDataGrid** at runtime in the following manner there:

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
        builder.OpenComponent<DxDataGrid<T>>(0);
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

This way, your grid accepts three parameters: Data, ChildContent (**for columns**), and Settings. So now you can configure any grid with all the required settings by passing them as a parameter:

```razor
<MyGrid Data="Forecasts" Settings="InputAttributes" >
    <DxDataGridCommandColumn Width="150px" />
    <DxDataGridColumn Field="@nameof(WeatherForecast.Date)"></DxDataGridColumn>
    <DxDataGridColumn Field="@nameof(WeatherForecast.TemperatureC)"></DxDataGridColumn>
    <DxDataGridColumn Field="@nameof(WeatherForecast.TemperatureF)"></DxDataGridColumn>
    <DxDataGridComboBoxColumn Data="@WeatherForecastService.Summaries" Field="@nameof(WeatherForecast.Summary)"></DxDataGridComboBoxColumn>
</MyGrid>

@code {
    public List<WeatherForecast> Forecasts { get; set; }

    public Dictionary<string, object> InputAttributes { get; set; } =
     new Dictionary<string, object>()
     {
         { "ShowFilterRow", false },
         { "ShowGroupPanel", true }
     };

    protected override async Task OnInitializedAsync() {
        base.OnInitialized();
        WeatherForecast[] data = await ForecastService.GetForecastAsync(DateTime.Now);
        Forecasts = data.ToList();
    }

}
```
