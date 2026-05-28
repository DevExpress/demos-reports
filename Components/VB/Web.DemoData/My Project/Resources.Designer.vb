Imports System

'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Namespace My.Resources
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
    Global.Microsoft.VisualBasic.HideModuleNameAttribute()>
    Friend Module Resources
        Private resourceMan As Global.System.Resources.ResourceManager
        Private resourceCulture As Global.System.Globalization.CultureInfo
'        internal Resources()
'        {
'        }
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As New Global.System.Resources.ResourceManager("Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set(ByVal value As System.Globalization.CultureInfo)
                resourceCulture = value
            End Set
        End Property
        Friend ReadOnly Property Code_DirectoryManagmentUtils_cs() As String
            Get
                Return ResourceManager.GetString("Code_DirectoryManagmentUtils_cs", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property Code_RichEditDemoUtils_cs() As String
            Get
                Return ResourceManager.GetString("Code_RichEditDemoUtils_cs", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property DataProvider_Arts_cs() As String
            Get
                Return ResourceManager.GetString("DataProvider_Arts_cs", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property DataProvider_HeadphonesModel_cs() As String
            Get
                Return ResourceManager.GetString("DataProvider_HeadphonesModel_cs", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property DataProvider_Homes_cs() As String
            Get
                Return ResourceManager.GetString("DataProvider_Homes_cs", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property DataProvider_LargeDatabase_cs() As String
            Get
                Return ResourceManager.GetString("DataProvider_LargeDatabase_cs", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property DataProvider_Northwind_cs() As String
            Get
                Return ResourceManager.GetString("DataProvider_Northwind_cs", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property DataProvider_WorldCities_cs() As String
            Get
                Return ResourceManager.GetString("DataProvider_WorldCities_cs", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property IconDataUri1200x800() As String
            Get
                Return ResourceManager.GetString("IconDataUri1200x800", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property IconDataUri576x768() As String
            Get
                Return ResourceManager.GetString("IconDataUri576x768", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property IconDataUri768x576() As String
            Get
                Return ResourceManager.GetString("IconDataUri768x576", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property IconDataUri800x1200() As String
            Get
                Return ResourceManager.GetString("IconDataUri800x1200", resourceCulture)
            End Get
        End Property
        Friend ReadOnly Property IconDataUriFullscreen() As String
            Get
                Return ResourceManager.GetString("IconDataUriFullscreen", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
