using System;
using System.Collections.Generic;

using Duomo.Common.Lib;


namespace Duomo.Common.Lib.IO.Paths
{
    [Serializable]
    public class PathInterpreter
    {
        private const string ExecutableFolderRootedPathName = @"EXECUTABLE_FOLDER_ROOTED_PATH";
        private const string DocumentsFolderRootedPathName = @"DOCUMENTS_FOLDER_ROOTED_PATH";


        #region Static

        public static Dictionary<string, string> GetDefaultSubstitutions()
        {
            Dictionary<string, string> retValue = new Dictionary<string, string>();

            retValue.Add(PathInterpreter.ExecutableFolderRootedPathName, Utilities.ExecutableFolderRootedPath);
            retValue.Add(PathInterpreter.DocumentsFolderRootedPathName, Utilities.DocumentsFolderRootedPath);

            return retValue;
        }

        public static string GetSearchKey(string substitutionKey)
        {
            string retValue = String.Format(@"[{0}]", substitutionKey);
            return retValue;
        }

        public static string Interpret(string interpretedPath, Dictionary<string, string> substitutions)
        {
            string retValue = interpretedPath;
            if (null != interpretedPath)
            {
                foreach (string substitutionKey in substitutions.Keys)
                {
                    string searchKey = PathInterpreter.GetSearchKey(substitutionKey);

                    if (interpretedPath.Contains(searchKey))
                    {
                        string substitution = substitutions[substitutionKey];

                        retValue = retValue.Replace(searchKey, substitution);
                    }
                }
            }

            return retValue;
        }

        public static string Interpret(string interpretedPath)
        {
            Dictionary<string, string> defaultSubstitutions = PathInterpreter.GetDefaultSubstitutions();

            string retValue = PathInterpreter.Interpret(interpretedPath, defaultSubstitutions);
            return retValue;
        }

        public static List<string> Interpret(List<string> interpretedPaths, Dictionary<string, string> substitutions)
        {
            List<string> retValue = new List<string>();
            foreach (string interpretedPath in interpretedPaths)
            {
                string path = PathInterpreter.Interpret(interpretedPath, substitutions);
                retValue.Add(path);
            }

            return retValue;
        }

        public static List<string> Interpret(List<string> interpretedPaths)
        {
            Dictionary<string, string> defaultSubstitutions = PathInterpreter.GetDefaultSubstitutions();

            List<string> retValue = PathInterpreter.Interpret(interpretedPaths, defaultSubstitutions);
            return retValue;
        }

        public static Dictionary<string, string> Interpret(Dictionary<string, string> interpretedPaths, Dictionary<string, string> substitutions)
        {
            Dictionary<string, string> retValue = new Dictionary<string, string>();
            foreach (string interpretedPathKey in interpretedPaths.Keys)
            {
                string interpretedPath = interpretedPaths[interpretedPathKey];

                string path = PathInterpreter.Interpret(interpretedPath, substitutions);
                retValue.Add(interpretedPathKey, path);
            }
            return retValue;
        }
        
        #endregion


        public Dictionary<string, string> Substitutions { get; protected set; }
        public string this[string interpretedPath]
        {
            get
            {
                string retValue = PathInterpreter.Interpret(interpretedPath, this.Substitutions);
                return retValue;
            }
        }
        public List<string> this[List<string> interpretedPaths]
        {
            get
            {
                List<string> retValue = PathInterpreter.Interpret(interpretedPaths, this.Substitutions);
                return retValue;
            }
        }
        public Dictionary<string, string> this[Dictionary<string, string> interpretedPathsByKey]
        {
            get
            {
                Dictionary<string, string> retValue = PathInterpreter.Interpret(interpretedPathsByKey, this.Substitutions);
                return retValue;
            }
        }


        public PathInterpreter()
        {
            this.Substitutions = PathInterpreter.GetDefaultSubstitutions();
        }

        public PathInterpreter(Dictionary<string, string> substitutions)
        {
            this.Substitutions = substitutions;
        }
    }
}
