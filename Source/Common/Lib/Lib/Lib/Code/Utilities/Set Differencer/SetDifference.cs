using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class SetDifference<T>
    {
        public string Set1Name { get; protected set; }
        public string Set2Name { get; protected set; }
        public List<T> Set1Only { get; protected set; }
        public List<T> Set2Only { get; protected set; }


        public SetDifference()
        {
            this.Setup();
        }

        public SetDifference(string set1Name, string set2Name)
        {
            this.Set1Name = set1Name;
            this.Set2Name = set2Name;

            this.Setup();
        }

        private void Setup()
        {
            this.Set1Only = new List<T>();
            this.Set2Only = new List<T>();
        }
    }
}
