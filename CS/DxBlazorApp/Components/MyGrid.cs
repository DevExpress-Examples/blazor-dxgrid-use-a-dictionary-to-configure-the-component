using DevExpress.Blazor;
using DxBlazorApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DxBlazorApp.Components {
    public class MyGrid<T> : ComponentBase {
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
}

