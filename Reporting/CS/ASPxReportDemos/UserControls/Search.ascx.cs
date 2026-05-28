using DevExpress.Web;
using DevExpress.Web.Demos;
using System;
using System.Web;
using System.Web.UI;

namespace DevExpress.Web.Demos {
    public partial class Search : UserControl {
        public bool FocusOnLoad { get; set; }
        protected void Page_Load(object sender, EventArgs e) {
            if(FocusOnLoad)
                SearchEditor.Focus();
        }
        public string GetSearchItemUrl(object dataItem) {
            SearchResult sr = (SearchResult)dataItem;
            string url = Utils.GenerateDemoUrl(!object.Equals(sr.Demo, sr.Product.Intro) ? sr.Demo : sr.Product.Intro);
            return ResolveClientUrl(url);
        }
        public bool HasAdditionalInfo(object dataItem) {
            SearchResult sr = (SearchResult)dataItem;
            return sr.Group != null;
        }
        public string GetDemoGroupTittle(object dataItem) {
            SearchResult sr = (SearchResult)dataItem;
            return sr.Group != null ? sr.Group.Title : string.Empty;
        }
        public string GetAdditionalInfoMarker(object dataItem) {
            return HasAdditionalInfo(dataItem) ? "" : "no-ai";
        }
        protected void SearchResults_Callback(object sender, CallbackEventArgsBase e) {
            var text = e.Parameter;
            var results = SearchUtils.DoSearch(text);
            if(results.Count > 0) {
                resultsContainer.Visible = true;
                noResultsContainer.Visible = false;
                resultsRepeater.DataSource = results;
                resultsRepeater.DataBind();
            } else {
                resultsContainer.Visible = false;
                noResultsContainer.Visible = true;
                requestText.InnerHtml = HttpUtility.HtmlEncode(text);
            }
        }
    }
}
