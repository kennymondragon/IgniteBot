﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IgniteBot.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.6.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool autoRestart {
            get {
                return ((bool)(this["autoRestart"]));
            }
            set {
                this["autoRestart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool showDatabaseLog {
            get {
                return ((bool)(this["showDatabaseLog"]));
            }
            set {
                this["showDatabaseLog"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool logToServer {
            get {
                return ((bool)(this["logToServer"]));
            }
            set {
                this["logToServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool startOnBoot {
            get {
                return ((bool)(this["startOnBoot"]));
            }
            set {
                this["startOnBoot"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int targetDeltaTimeIndexStats {
            get {
                return ((int)(this["targetDeltaTimeIndexStats"]));
            }
            set {
                this["targetDeltaTimeIndexStats"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool useCompression {
            get {
                return ((bool)(this["useCompression"]));
            }
            set {
                this["useCompression"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool batchWrites {
            get {
                return ((bool)(this["batchWrites"]));
            }
            set {
                this["batchWrites"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("none")]
        public string saveFolder {
            get {
                return ((string)(this["saveFolder"]));
            }
            set {
                this["saveFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool enableFullLogging {
            get {
                return ((bool)(this["enableFullLogging"]));
            }
            set {
                this["enableFullLogging"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool enableStatsLogging {
            get {
                return ((bool)(this["enableStatsLogging"]));
            }
            set {
                this["enableStatsLogging"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int targetDeltaTimeIndexFull {
            get {
                return ((int)(this["targetDeltaTimeIndexFull"]));
            }
            set {
                this["targetDeltaTimeIndexFull"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool showConsoleOnStart {
            get {
                return ((bool)(this["showConsoleOnStart"]));
            }
            set {
                this["showConsoleOnStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool outputGameStateEvents {
            get {
                return ((bool)(this["outputGameStateEvents"]));
            }
            set {
                this["outputGameStateEvents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool outputScoreEvents {
            get {
                return ((bool)(this["outputScoreEvents"]));
            }
            set {
                this["outputScoreEvents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool outputStunEvents {
            get {
                return ((bool)(this["outputStunEvents"]));
            }
            set {
                this["outputStunEvents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool outputDiscThrownEvents {
            get {
                return ((bool)(this["outputDiscThrownEvents"]));
            }
            set {
                this["outputDiscThrownEvents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool outputDiscCaughtEvents {
            get {
                return ((bool)(this["outputDiscCaughtEvents"]));
            }
            set {
                this["outputDiscCaughtEvents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool outputDiscStolenEvents {
            get {
                return ((bool)(this["outputDiscStolenEvents"]));
            }
            set {
                this["outputDiscStolenEvents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool outputSaveEvents {
            get {
                return ((bool)(this["outputSaveEvents"]));
            }
            set {
                this["outputSaveEvents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string echoVRPath {
            get {
                return ((string)(this["echoVRPath"]));
            }
            set {
                this["echoVRPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string accessCode {
            get {
                return ((string)(this["accessCode"]));
            }
            set {
                this["accessCode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool outputOther {
            get {
                return ((bool)(this["outputOther"]));
            }
            set {
                this["outputOther"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int whenToUploadLogs {
            get {
                return ((int)(this["whenToUploadLogs"]));
            }
            set {
                this["whenToUploadLogs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool uploadToFirestore {
            get {
                return ((bool)(this["uploadToFirestore"]));
            }
            set {
                this["uploadToFirestore"] = value;
            }
        }
    }
}
