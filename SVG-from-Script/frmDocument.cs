using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using FastColoredTextBoxNS;

namespace SVG_from_Script
{
    public partial class frmDocument : DockContent
    {
        #region Fields
        bool fileModified = false;
        string mFileName = "";
        AutocompleteMenu popupMenu;

        #endregion Fields

        #region Constuctor
        public frmDocument(string FileName, string ScriptText)
        {
            InitializeComponent();

            //create autocomplete popup menu
            popupMenu = new AutocompleteMenu(fastColoredTextBox1)
            {
                ForeColor = Color.White,
                BackColor = Color.Gray,
                SelectedColor = Color.Purple,
                SearchPattern = @"[\w\.]",
                AllowTabKey = true,
                AlwaysShowTooltip = true
            };

            popupMenu.ToolTip.Popup += ToolTip_Popup;

            popupMenu.Items.SetAutocompleteItems(new CSharpIntelliSenseDynamicCollection(popupMenu, fastColoredTextBox1));

            Clear();

            this.FileName = FileName;
            this.ScriptText = ScriptText;

            if(string.IsNullOrEmpty(ScriptText) && File.Exists(FileName))
            {
                this.ScriptText = File.ReadAllText(FileName);
            }

            fileModified = false;
            UpdateTitle();
        }
        #endregion Constructor

        #region Public Properties
        public bool FileExists
        {
            get { return File.Exists(FileName); }
        }

        public string FileName
        {
            get { return mFileName; }
            set
            {
                mFileName = value;
                UpdateTitle();
            }
        }

        public bool FileModified
        {
            get { return fileModified; }
        }

        public bool FileSaved
        {
            set
            {
                fileModified = value ? false : fileModified;
                UpdateTitle();
            }
        }

        public string ScriptText
        {
            get { return fastColoredTextBox1.Text; }
            set { fastColoredTextBox1.Text = value; }
        }

        public string Title
        {
            get { return Text; }
        }
        #endregion Public Properties

        #region Public Methods
        public void Clear()
        {
            fastColoredTextBox1.Clear();
        }

        public string FindReplace_CountAll(string FindText)
        {
            int cnt = FindReplace.CountSubStrings(fastColoredTextBox1.Text, FindText);

            if (cnt < 1)
                return "Text not found";
            else
                return string.Format("Found {0} occurances", cnt);
        }

        public string FindReplace_FindNext(string FindText)
        {
            throw new NotImplementedException();
        }

        public string FindReplace_Replace(string FindText, string ReplaceText)
        {
            throw new NotImplementedException();
        }

        public string FindReplace_ReplaceAll(string FindText, string ReplaceText)
        {
            ReplaceResults results = FindReplace.ReplaceAll(fastColoredTextBox1.Text, FindText, ReplaceText);

            fastColoredTextBox1.Text = results.ResultingText;

            if (results.OccuranceCount < 1)
                return "Text was not found. No changes made.";
            else
                return string.Format("Found and replaced {0} occurances", results.OccuranceCount);
        }

        protected override string GetPersistString()
        {
            return string.Format("{0}\t{1}", "Document", FileName);
        }

        public void SaveScriptFile()
        {
            using (StreamWriter outputfile = new StreamWriter(FileName))
            {
                outputfile.Write(ScriptText);
            }

            FileSaved = true;
        }

        public void SaveScriptFile(string FileName)
        {
            this.FileName = FileName;
            SaveScriptFile();
        }
        #endregion Public Methods

        #region Private Methods
        private void UpdateTitle()
        {
            if (string.IsNullOrEmpty(FileName))
                mFileName = "new";

            Text = string.Format("{0}{1}", fileModified ? "* " : "", Path.GetFileName(mFileName));
        }
        #endregion Private Methods

        #region Event Handlers
        private void FastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!fileModified)
            {
                fileModified = true;
                UpdateTitle();
            }
        }

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            ToolTip toolTip = (ToolTip)sender;

            AutoCompleteHintEventArgs args = new AutoCompleteHintEventArgs
            {
                ToolTipText = toolTip.GetToolTip(e.AssociatedControl),
                MemberName = toolTip.ToolTipTitle
            };

            OnToolTipShown(args);
        }
        #endregion Event Handlers

        protected virtual void OnToolTipShown(AutoCompleteHintEventArgs e)
        {
            ToolTipShown?.Invoke(this, e);
        }

        public event EventHandler<AutoCompleteHintEventArgs> ToolTipShown;
    }

    public class AutoCompleteHintEventArgs : EventArgs
    {
        public string ToolTipText { get; set; }
        public string MemberName { get; set; }
    }
}
