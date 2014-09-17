using System;


namespace Duomo.Common.Gunther.Lib
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ScheduleSpecificationTypes")]
    public enum HolidayCalendarEnumeration
    {
        /// <remarks>Use for running on all days, weekends included.</remarks>
        NONE,

        /// <remarks>Use for running on all (US) weekend days.</remarks>
        WKN,

        /// <remarks>Includes (US) weekends and US holidays.</remarks>
        USD,
    }
}
