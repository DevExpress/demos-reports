# DevExpress Reports Demos

[DevExpress Reports](https://www.devexpress.com/subscriptions/reporting/) — our award-winning .NET reporting platform — allows you and your end users to build, view, print, and export fully formatted documents with ease. It includes designers and viewers for WinForms, WPF, Blazor (Server, WebAssembly), ASP.NET (Web Forms, MVC) and ASP.NET Core (Razor Pages, MVC), Angular, React, alongside a broad range of report controls/elements such as charts, cross tabs, bar codes, and more.

Visit our website to learn more about product features, capabilities, and pricing options. To evaluate DevExpress controls and libraries in your projects, download our fully-functional trial version today: [Free 30-day Trial Version](https://go.devexpress.com/devexpressdownload_universaltrial.aspx).

## What's Inside

Interactive DevExpress demos with source code for supported platforms:

* **Sample reports** — Invoices, catalogs, financial statements, and inspection forms in master-detail, hierarchical, multi-column, table, and cross-band layouts. 
* **Report controls** — Charts, sparklines, bar codes, Swiss QR bills, and conditional formatting. 
* **Data binding** — SQL, Entity Framework, Object, JSON, Excel, and XML data sources with data federation to combine multiple sources, calculated fields, report parameters, and master-detail relationships. 
* **Interactivity** — Drill-down, sorting, and fillable e-forms in Print Preview. 
* **Document operations** — Report merging, embedded PDF content, mail merge, and PDF signatures. 
* **AI integration** — AI-powered features in the Document Viewer and Report Designer across WinForms, WPF, Blazor, and ASP.NET Core. 

## Prerequisites 

Refer to the following DevExpress Reports help topic for information on system requirements (v26.1): [Prerequisites](https://docs.devexpress.com/XtraReports/400127/product-information/devexpress-reporting-prerequisites). 

Demo projects targeting .NET Framework 4.6.2+ require installation via the DevExpress Unified Installer on a local machine.

> Note: AI services used for this demo have been rate limited. As such, you may experience performance-related delays when exploring the capabilities of DevExpress AI-powered Extensions.
> When connected to your own AI model/service without rate limits, DevExpress AI-powered Extensions will perform seamlessly, without artificial delays.

## Run Demos Locally 

### WinForms

1. Clone the current repository: 

    ```cli 
    # HTTPS
    git clone https://github.com/DevExpress/demos-reports.git 
    ```
    
    ```cli
    # SSH
    git clone git@github.com:DevExpress/demos-reports.git 
    ``` 

2. Open one of the following solutions in your IDE: 

    * [Reporting/CS/ReportMainDemo/ReportMainDemo.NetCore.Desktop.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/CS/ReportMainDemo) (.NET)
    * [Reporting/CS/ReportMainDemo/ReportMainDemo.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/CS/ReportMainDemo) (.NET Framework)
    * [Reporting/VB/ReportMainDemo/ReportMainDemo.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/VB/ReportMainDemo) (.NET Framework)

3. Run the application.

### WPF

1. Clone the current repository: 

    ```cli 
    # HTTPS
    git clone https://github.com/DevExpress/demos-reports.git 
    ```
    
    ```cli
    # SSH
    git clone git@github.com:DevExpress/demos-reports.git 
    ``` 

2. Open one of the following solutions in your IDE: 

    * [Reporting/CS/ReportDemo.Wpf/ReportDemo.Wpf.NetCore.Desktop.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/CS/ReportDemo.Wpf) (.NET)
    * [Reporting/CS/ReportDemo.Wpf/ReportDemo.Wpf.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/CS/ReportDemo.Wpf) (.NET Framework)
    * [Reporting/VB/ReportDemo.Wpf/ReportDemo.Wpf.NetCore.Desktop.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/VB/ReportDemo.Wpf) (.NET)

3. Run the application.

### Blazor

1. Clone the current repository: 

    ```cli 
    # HTTPS
    git clone https://github.com/DevExpress/demos-reports.git 
    ```
    
    ```cli
    # SSH
    git clone git@github.com:DevExpress/demos-reports.git 
    ``` 

2. Open one of the following solutions in your IDE: 

    * [Reporting/BlazorDemo.ServerSide/CS/BlazorDemo.ServerSide.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/BlazorDemo.ServerSide/CS)
    * [Reporting/BlazorDemo.Wasm/CS/BlazorDemo.Wasm.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/BlazorDemo.Wasm/CS)

3. For our _BlazorDemo.Wasm_ demo, ensure that the _BlazorDemo.ServerSide_ project is set as the solution’s startup project. 
4. Run the application in a [supported browser](https://docs.devexpress.com/Blazor/401588/supported-browsers).

Note that Blazor WebAssembly includes fewer demos since it only supports JSON and Object data sources.

### ASP.NET Core

1. Clone the current repository: 

    ```cli 
    # HTTPS
    git clone https://github.com/DevExpress/demos-reports.git 
    ```
    
    ```cli
    # SSH
    git clone git@github.com:DevExpress/demos-reports.git 
    ``` 

2. Open the following solution in your IDE: 

    * [Reporting/CS/AspNetCoreDemos.Reporting/AspNetCoreDemos.Reporting.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/CS/AspNetCoreDemos.Reporting)

3. Run the application in a [supported browser](https://docs.devexpress.com/AspNetCore/403323/supported-browsers).

### ASP.NET MVC 5

1. Clone the current repository: 

    ```cli 
    # HTTPS
    git clone https://github.com/DevExpress/demos-reports.git 
    ```
    
    ```cli
    # SSH
    git clone git@github.com:DevExpress/demos-reports.git 
    ``` 

2.  Open the following solution in your IDE: 

    * [Reporting/CS/MVCxReportDemos/MVCxReportDemos.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/CS/MVCxReportDemos)

3. Run the application in a [supported browser](https://docs.devexpress.com/AspNetMvc/404393/supported-browsers).

### ASP.NET Web Forms

1. Clone the current repository: 

    ```cli 
    # HTTPS
    git clone https://github.com/DevExpress/demos-reports.git 
    ```
    
    ```cli
    # SSH
    git clone git@github.com:DevExpress/demos-reports.git 
    ``` 

2. Open one of the following solutions in your IDE: 

    * [Reporting/CS/ASPxReportDemos/ASPxReportDemos.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/CS/ASPxReportDemos)
    * [Reporting/VB/ASPxReportDemos/ASPxReportDemos.sln](https://github.com/DevExpress/demos-reports/tree/main/Reporting/VB/ASPxReportDemos)

3. Run the application in a [supported browser](https://docs.devexpress.com/AspNet/404392/supported-browsers).

## Online Demos  

* [DevExpress Reports Demos for Blazor](https://demos.devexpress.com/blazor/ReportViewer/IndustryReports/Finance/InvoiceReport)
* [DevExpress Reports Demos for ASP.NET Core](https://demos.devexpress.com/ASPNetCore/Demo/Reporting/ProductListReport/) 
* [DevExpress Reports Demos for ASP.NET MVC](https://demos.devexpress.com/MVCxReportDemos/Interaction/VehicleInspection) 
* [DevExpress Reports Demos for ASP.NET Web Forms](https://demos.devexpress.com/XtraReportsDemos/ReportTypes/ReportMergingWithPdf.aspx) 

## Helpful Resources 

* [Documentation](https://docs.devexpress.com/XtraReports/2162/reporting)
* [Get Started](https://docs.devexpress.com/XtraReports/14651/get-started-with-devexpress-reporting) 
* [Examples](https://github.com/DevExpress-Examples?q=reporting&type=all&language=&sort=) 
* [Videos](https://www.youtube.com/watch?v=oaXK-XhwF8g&list=PL8h4jt35t1wixmi-yv3Wj6FSJMEa08npR) 
* [Support Center](https://supportcenter.devexpress.com/ask)
 
