using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevExpress.Web.Demos {
    public partial class UserControls_ServerEventMonitor : UserControl {
        string _eventNames;
        int _eventLogWidth;

        public string EventNames {
            get { return _eventNames; }
            set { _eventNames = value; }
        }
        public int EventLogWidth {
            get { return _eventLogWidth; }
            set { _eventLogWidth = value; }
        }

        protected List<KeyValuePair<string, string>[]> Items {
            get {
                ViewState["Items"] = ViewState["Items"] ?? new List<KeyValuePair<string, string>[]>();
                return (List<KeyValuePair<string, string>[]>)ViewState["Items"];
            }
            set {
                ViewState["Items"] = value;
            }
        }

        public void TraceEvent(object sender, EventArgs e, string eventName) {
            if(!IsTraceEnabled(eventName))
                return;
            var rows = new List<KeyValuePair<string, string>>();
            rows.Add(new KeyValuePair<string, string>("Sender", (sender as Control).ID));
            rows.Add(new KeyValuePair<string, string>("EventType", eventName));

            int count = 0;
            foreach(System.Reflection.PropertyInfo prop in e.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)) {
                try {
                    object value = prop.PropertyType.IsValueType ? prop.GetValue(e, null) : "[" + prop.PropertyType.Name + "]";
                    rows.Add(new KeyValuePair<string, string>(count > 0 ? "" : "Arguments", prop.Name + " = " + value));
                    if(!prop.PropertyType.IsValueType)
                        TryTraceCollectionItemArgument(prop, e, rows);
                    count++;
                }
                catch {
                }
            }
            Items.Insert(0, rows.ToArray());
            RenderTable();
        }

        void RenderTable() {
            EventLog.Controls.Clear();
            foreach(var rows in Items) {
                var table = new Table() { CssClass = "event-table" };
                foreach(var row in rows) {
                    var tRow = new TableRow();
                    tRow.Cells.Add(new TableCell() {
                        CssClass = "event-header",
                        Text = HttpUtility.HtmlEncode(row.Key)
                    });
                    tRow.Cells.Add(new TableCell() {
                        CssClass = "event-content",
                        Text = HttpUtility.HtmlEncode(row.Value)
                    });
                    table.Rows.Add(tRow);
                }
                EventLog.Controls.Add(table);
            }
        }

        void TryTraceCollectionItemArgument(System.Reflection.PropertyInfo prop, EventArgs e, List<KeyValuePair<string, string>> rows) {
            try {
                CollectionItem item = prop.GetValue(e, null) as CollectionItem;
                if(item == null)
                    return;
                string[] itemPropNames = new string[] { "Name", "Text" };
                foreach(string itemPropName in itemPropNames) {
                    System.Reflection.PropertyInfo itemProp = item.GetType().GetProperty(itemPropName);
                    if(itemProp == null)
                        continue;
                    string itemPropValue = itemProp.GetValue(item, null) as string;
                    if(!string.IsNullOrEmpty(itemPropValue)) {
                        rows.Add(new KeyValuePair<string, string>("", prop.Name + "." + itemPropName + " = " + itemPropValue));
                        return;
                    }
                }
            }
            catch { }
        }

        bool IsTraceEnabled(string eventName) {
            foreach(RepeaterItem item in Repeater.Items) {
                foreach(Control control in item.Controls) {
                    ASPxCheckBox box = control as ASPxCheckBox;
                    if(box != null && box.Text == eventName)
                        return box.Checked;
                }
            }
            return false;
        }

        protected IEnumerable<string> GetEventNameList() {
            string[] eventNameList = Regex.Split(EventNames, "[^A-Za-z_]+");
            for(int index = 0; index < eventNameList.Length; index++) {
                eventNameList.SetValue(eventNameList[index].Replace("_", " "), index);
            }
            return eventNameList;
        }

        protected void Page_Init(object sender, EventArgs e) {
            Repeater.DataSource = GetEventNameList();
            Repeater.DataBind();

            if(EventLogWidth > 0)
                EventLog.Style.Add("width", EventLogWidth + "px");
        }

        protected void Page_Load(object sender, EventArgs e) {
            ClearButton.Theme = DemoHelper.Instance.Theme;
            foreach(RepeaterItem item in Repeater.Items) {
                foreach(Control control in item.Controls) {
                    var checkbox = control as ASPxCheckBox;
                    if(checkbox != null)
                        checkbox.Theme = DemoHelper.Instance.Theme;
                }
            }
        }

        protected void ClearButton_Click(object sender, EventArgs e) {
            Items.Clear();
            EventLog.Controls.Clear();
        }
    }
}
