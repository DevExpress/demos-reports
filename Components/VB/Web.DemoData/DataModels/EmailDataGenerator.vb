'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports DevExpress.Data.Utils
Namespace DevExpress.Web.Demos
    Public NotInheritable Class EmailDataGenerator

        Private Sub New()
        End Sub


        Private Shared table_Renamed As DatabaseGenerator.TableData
        Private Shared ReadOnly subjects() As String = { "Integrating Developer Express MasterView control into an Accounting System.", "Web Edition: Data Entry Page. There is an issue with date validation.", "Payables Due Calculator is ready for testing.", "Web Edition: Search Page is ready for testing.", "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.", "Receivables Calculator. Where can I find the complete specs?", "Ledger: Inconsistency. Please fix it.", "Receivables Printing module is ready for testing.", "Screen Redraw. Somebody has to look at it.", "Email System. What library are we going to use?", "Cannot add new vendor. This module doesn't work!", "History. Will we track sales history in our system?", "Main Menu: Add a File menu. File menu item is missing.", "Currency Mask. The current currency mask in completely unusable.", "Drag & Drop operations are not available in the scheduler module.", "Data Import. What database types will we support?", "Reports. The list of incomplete reports.", "Data Archiving. We still don't have this features in our application.", "Email Attachments. Is it possible to add multiple attachments? I haven't found a way to do this.", "Check Register. We are using different paths for different modules.", "Data Export. Our customers asked us for export to Microsoft Excel" }
        Private Shared ReadOnly names() As String = { "Peter Dolan", "Ryan Fischer", "Richard Fisher", "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell" }
        Public Shared ReadOnly Property Table() As DatabaseGenerator.TableData
            Get
                Return table_Renamed
            End Get
        End Property
        Public Shared Sub Register()
            table_Renamed = New DatabaseGenerator.TableData()
            table_Renamed.Name = "Emails"
            table_Renamed.ConnectionStringName = "LargeDatabaseConnectionString"
            table_Renamed.Fields.Add(New DatabaseGenerator.FieldData("Subject", subjects))
            table_Renamed.Fields.Add(New DatabaseGenerator.FieldData("From", names))
            table_Renamed.Fields.Add(New DatabaseGenerator.FieldData("Sent", AddressOf GenerateSent))
            table_Renamed.Fields.Add(New DatabaseGenerator.FieldData("Size", AddressOf GenerateSize))
            table_Renamed.Fields.Add(New DatabaseGenerator.FieldData("HasAttachment", AddressOf GenerateHasAttachment))
            table_Renamed.RecordCount = 300000
            DatabaseGenerator.RegisterTable("GeneratedEmailTable", table_Renamed)
        End Sub
        Private Shared Function GenerateSent(ByVal rnd As NonCryptographicRandom) As Object
            Return Date.Now.Date.Subtract(TimeSpan.FromDays(rnd.Next(50)))
        End Function
        Private Shared Function GenerateSize(ByVal rnd As NonCryptographicRandom) As Object
            Return CLng((1000 + rnd.Next(300000)))
        End Function
        Private Shared Function GenerateHasAttachment(ByVal rnd As NonCryptographicRandom) As Object
            Return rnd.Next(10) > 5
        End Function
    End Class
End Namespace
