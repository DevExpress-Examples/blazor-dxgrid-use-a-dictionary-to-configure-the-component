using System;
using System.Collections.Generic;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;

namespace DxBlazorApp.Shared
{
    public class MyGridBase<T> : DxDataGrid<T>
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<String, object> InputAttributes { get; set; }
    }
}
