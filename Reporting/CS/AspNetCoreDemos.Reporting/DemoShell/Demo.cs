using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace AspNetCoreDemos.DemoShell {
    public class Demo {

        // From menuMeta.json

        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<string> MvcAdditionalFiles { get; set; }
        public bool Hidden { get; set; }

        [JsonPropertyName("Widget")]
        public string Compat_Widget { set { Controller = value; } }

        [JsonPropertyName("Name")]
        public string Compat_Name { set { Action = value; } }


        // Assigned

        public string DescriptionHtml { get; set; }
        public List<DemoSourceFile> Files { get; } = new List<DemoSourceFile>();


        public string AdditionalCssUrl => $"~/css/DemosStyles/{Controller}/{Action}.css";

        public string ViewPath => Path.Combine("Views", Controller, Action + ".cshtml");

#pragma warning disable DX0025 // doesn't use user input
        public string ControllerPath => Path.Combine("Controllers", Controller + "Controller.cs");
#pragma warning restore DX0025
    }

}
