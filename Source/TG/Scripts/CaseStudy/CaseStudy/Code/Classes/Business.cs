using System;
using System.Collections.Generic;


namespace TG.CaseStudy
{
    public class Business
    {
        public const string TRADER_JOES = @"TRADER JOE'S";
        public const string SPROUTS = @"SPROUTS FARMERS";
        public const string WHOLE_FOODS = @"WHOLE FOODS MARKET";
        public const int NUMBER_OF_BUSINESSES = 3;


        #region Static

        public static List<string> GetAllBusinessNames()
        {
            List<string> retValue = new List<string>();

            retValue.Add(Business.SPROUTS);
            retValue.Add(Business.TRADER_JOES);
            retValue.Add(Business.WHOLE_FOODS);

            return retValue;
        }

        #endregion


        public string Name { get; set; }


        public Business() { }

        public Business(string name)
        {
            this.Name = name;
        }
    }
}
