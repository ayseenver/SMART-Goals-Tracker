﻿

#pragma checksum "C:\Users\Ayse\Documents\Visual Studio 2015\Projects\Goals\Goals\Finances.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4E0019FEFBC98B4035FCFD2992FC8984"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Goals
{
    partial class Finances : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 20 "..\..\Finances.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AddCategoryButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 23 "..\..\Finances.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 117 "..\..\Finances.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.newCategoryButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


