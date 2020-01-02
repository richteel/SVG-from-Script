using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SVG_from_Script
{
    public static class NameDescriptions
    {
        private const string MEMBERDESCFILENAME = "Members.txt";

        private static Dictionary<string, string> members = null;

        public static string GetMemberDescription(string MemberName, string MemberTypeName)
        {
            if (members == null)
            {
                members = new Dictionary<string, string>();
                LoadFile(MEMBERDESCFILENAME, members);
            }

            string description = string.Empty;

            if (members.ContainsKey(MemberName))
                description = members[MemberName];
            else
            {
                description = "Description of " + MemberTypeName.ToLower() + " " + MemberName + " goes here.";
                AddItem(MEMBERDESCFILENAME, members, MemberName, description);
            }

            return description;
        }

        public static string GetMethodDescription(string MethodName)
        {
            return GetMemberDescription(MethodName, "Method");
        }

        public static string GetPropertyDescription(string PropertyName)
        {
            return GetMemberDescription(PropertyName, "Property");
        }

        private static void AddItem(string FileName, Dictionary<string, string> dict, string name, string description)
        {
            string filename = Path.Combine(Application.StartupPath, FileName);

            File.AppendAllText(filename, string.Format("{0}\t{1}\r\n", name, description));

            dict.Add(name, description);
        }

        private static void LoadFile(string FileName, Dictionary<string, string> dict)
        {
            string filename = Path.Combine(Application.StartupPath, FileName);

            if (!File.Exists(filename))
            {
                return;
            }

            string line;

            using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] lineColumns = line.Split(new char[] { '\t' });
                    if (lineColumns.Length == 2)
                    {
                        dict.Add(lineColumns[0], lineColumns[1].Replace(@"{\r\n}", "\r\n").Replace(@"{\t}", "\t"));
                    }
                }
            }
        }
    }
}
