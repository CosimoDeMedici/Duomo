using System;


namespace TG.CaseStudy
{
    public class Customer
    {
        #region Static

        public static int GenerateIDFromIndex(int index)
        {
            int retValue = (index + 1000000) * 7 + 4;
            return retValue;
        }

        public static int GenerateIndexFromID(int ID)
        {
            int retValue = (ID - 4) / 7 - 1000000;
            return retValue;
        }

        #endregion


        public int ID { get; set; }


        public Customer() { }

        public Customer(int ID)
        {
            this.ID = ID;
        }
    }
}
