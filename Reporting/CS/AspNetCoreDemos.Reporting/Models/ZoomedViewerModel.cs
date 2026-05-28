using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Reporting.Models {
    public class ZoomedViewerModel {
        public string ReportUrl { get; set; }
        public double ZoomFactor { get; set; } = 0.5;
        public bool UseMultiPage { get; set; } = true;
    }
}
