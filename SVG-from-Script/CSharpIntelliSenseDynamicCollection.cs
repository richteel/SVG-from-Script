using System.Reflection;
using FastColoredTextBoxNS;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SVG_from_Script
{
    public class CSharpIntelliSenseDynamicCollection : IEnumerable<AutocompleteItem>
    {
        private AutocompleteMenu menu;
        private readonly FastColoredTextBox tb;

        public CSharpIntelliSenseDynamicCollection(AutocompleteMenu menu, FastColoredTextBox tb)
        {
            this.menu = menu;
            this.tb = tb;
        }

        public IEnumerator<AutocompleteItem> GetEnumerator()
        {
            //get current fragment of the text
            var text = menu.Fragment.Text;

            //extract class name (part before dot)
            var parts = text.Split('.');
            if (parts.Length < 2)
                yield break;
            var className = parts[parts.Length - 2];

            if (className == "G")
                className = "Graphics";

            //find type for given className
            var type = FindTypeByName(className);

            if (type == null)
                yield break;

            List<string> t = new List<string>();
            foreach (var methodName in type.GetMethods().AsEnumerable().Select(mi => mi.Name).Distinct())
            {
                t.Add(className + "." + methodName + "()");
            }

            foreach (var pi in type.GetProperties())
            {
                t.Add(className + "." + pi.Name);
            }

            t.Sort();

            foreach (string s in t)
            {
                string[] classMember = s.Split(new char[] { '.' });

                if (classMember.Length != 2)
                    continue;

                string autoCompleteText = classMember[1];
                string mName = classMember[1].Replace("()", "");

                yield return new MethodAutocompleteItem(autoCompleteText)
                {
                    ToolTipTitle = mName,

                    ToolTipText = NameDescriptions.GetMemberDescription(className + "." + mName, autoCompleteText == mName ? "Property" : "Method"),
                };
            }
        }

        Type FindTypeByName(string name)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in assemblies)
            {
                foreach (var t in a.GetTypes())
                    if (t.Name == name)
                    {
                        return t;
                    }
            }

            return null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
