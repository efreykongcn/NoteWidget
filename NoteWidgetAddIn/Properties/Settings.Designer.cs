﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoteWidgetAddIn.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.0.3.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("System")]
        public string Markdown_ColorScheme {
            get {
                return ((string)(this["Markdown_ColorScheme"]));
            }
            set {
                this["Markdown_ColorScheme"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Default")]
        public string Markdown_HighlightTheme {
            get {
                return ((string)(this["Markdown_HighlightTheme"]));
            }
            set {
                this["Markdown_HighlightTheme"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1200")]
        public double Markdown_Preview_Width {
            get {
                return ((double)(this["Markdown_Preview_Width"]));
            }
            set {
                this["Markdown_Preview_Width"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public double Markdown_Preview_Height {
            get {
                return ((double)(this["Markdown_Preview_Height"]));
            }
            set {
                this["Markdown_Preview_Height"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool Markdown_Preview_Singleton {
            get {
                return ((bool)(this["Markdown_Preview_Singleton"]));
            }
            set {
                this["Markdown_Preview_Singleton"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1500")]
        public double Markdown_CheatSheet_Width {
            get {
                return ((double)(this["Markdown_CheatSheet_Width"]));
            }
            set {
                this["Markdown_CheatSheet_Width"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public double Markdown_CheatSheet_Height {
            get {
                return ((double)(this["Markdown_CheatSheet_Height"]));
            }
            set {
                this["Markdown_CheatSheet_Height"] = value;
            }
        }
    }
}
