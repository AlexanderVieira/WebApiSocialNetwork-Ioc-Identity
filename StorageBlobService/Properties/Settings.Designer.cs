﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.StorageBlobService.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.6.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DefaultEndpointsProtocol=https;AccountName=lettucebrain;AccountKey=IiFjvKVMCEbnsp" +
            "zDYvrca67OPh3qDtgqVMefL4kdxNCRyd8QyDOQ0LBgbO3X3Sby76Ly2LtV88KxltbHLtMQ4Q==;Endpo" +
            "intSuffix=core.windows.net")]
        public string StorageConnectionString {
            get {
                return ((string)(this["StorageConnectionString"]));
            }
        }
    }
}
