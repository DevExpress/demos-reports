using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace AspNetCoreDemos.DemoShell {
    public class DemoTreeNode {
        public string Text { get; set; }

        public string Url { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<DemoTreeNode> Items { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Active { get; set; }
    }
}
