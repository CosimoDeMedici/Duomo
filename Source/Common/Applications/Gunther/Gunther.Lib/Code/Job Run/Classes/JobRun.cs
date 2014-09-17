using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duomo.Common.Gunther.Lib
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:JobRunTypes")]
    [System.Xml.Serialization.XmlRootAttribute("JobRunFileRoot", Namespace = "urn:JobRunTypes", IsNullable = false)]
    public partial class JobRun
    {

        private int idField;

        private JobBase itemField;

        private System.DateTime startDateTimeField;

        private System.DateTime endDateTimeField;

        /// <remarks/>
        public int ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ExecutorTask", typeof(ExecutorTask))]
        [System.Xml.Serialization.XmlElementAttribute("SystemProcessCall", typeof(SystemProcessCall))]
        public JobBase Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        public System.DateTime StartDateTime
        {
            get
            {
                return this.startDateTimeField;
            }
            set
            {
                this.startDateTimeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime EndDateTime
        {
            get
            {
                return this.endDateTimeField;
            }
            set
            {
                this.endDateTimeField = value;
            }
        }
    }
}
