using System;


namespace Duomo.Common.Lib.Dates
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

        /// <remarks>Includes US holidays. NOTE: does not include US weekends. Use USDWKN.</remarks>
        USD,
    }
}
