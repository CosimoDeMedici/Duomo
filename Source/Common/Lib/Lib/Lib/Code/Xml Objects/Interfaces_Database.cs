﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 


namespace Duomo.Common.Lib.Xml
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:Duomo.Common.Interfaces.Database")]
    [System.Xml.Serialization.XmlRootAttribute("DatabaseInterfacesRoot", Namespace = "urn:Duomo.Common.Interfaces.Database", IsNullable = false)]
    public partial class DatabaseInterfacesListXmlType
    {

        private DatabaseInterfaceXmlType[] databaseInterfaceDefinitionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DatabaseInterface")]
        public DatabaseInterfaceXmlType[] DatabaseInterfaceDefinition
        {
            get
            {
                return this.databaseInterfaceDefinitionField;
            }
            set
            {
                this.databaseInterfaceDefinitionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:Duomo.Common.Interfaces.Database")]
    public partial class DatabaseInterfaceXmlType
    {

        private string serverAddressField;

        private int portField;

        private string databaseNameField;

        private string databaseTypeField;

        private string databaseIDField;

        /// <remarks/>
        public string ServerAddress
        {
            get
            {
                return this.serverAddressField;
            }
            set
            {
                this.serverAddressField = value;
            }
        }

        /// <remarks/>
        public int Port
        {
            get
            {
                return this.portField;
            }
            set
            {
                this.portField = value;
            }
        }

        /// <remarks/>
        public string DatabaseName
        {
            get
            {
                return this.databaseNameField;
            }
            set
            {
                this.databaseNameField = value;
            }
        }

        /// <remarks/>
        public string DatabaseType
        {
            get
            {
                return this.databaseTypeField;
            }
            set
            {
                this.databaseTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DatabaseID
        {
            get
            {
                return this.databaseIDField;
            }
            set
            {
                this.databaseIDField = value;
            }
        }
    }
}