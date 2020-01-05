using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using SvgNet.SvgGdi;
using System.Drawing.Imaging;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Drawing.Drawing2D;

namespace SVG_from_Script
{
    public partial class frmMain : Form
    {
        #region Fields
        private const string SCRIPTFILEFILTER = "SVG Image Editor Script (*.ses)|*.ses|All files (*.*)|*.*";
        private const string LAYOUTSTOREFILE = @"layout.xml";
        private const string NEWSCRIPTFORMATSTRINGPREFIX = "new ";

        private readonly DeserializeDockContent m_deserializeDockContent;

        private readonly List<frmDocument> documents = new List<frmDocument>();
        private readonly frmImagePreview imagePreview = null;
        private readonly frmReadOnlyText hintWindow = null;
        private readonly frmReadOnlyText outputWindow = null;
        private readonly frmReadOnlyText svgContents = null;

        private frmHelp helpForm = null;

        private float dpmmX = 0;
        private float dpmmY = 0;
        #endregion Fields

        #region Constructor
        public frmMain()
        {
            InitializeComponent();

            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            imagePreview = new frmImagePreview();
            hintWindow = new frmReadOnlyText(Properties.Resources.IconInformation, "Hint", frmReadOnlyText.TextControlTypes.TextBox);
            outputWindow = new frmReadOnlyText(Properties.Resources.IconOutput, "Output", frmReadOnlyText.TextControlTypes.TextBox);
            svgContents = new frmReadOnlyText(Properties.Resources.IconSVGLogo, "SVG File", frmReadOnlyText.TextControlTypes.FastColoredTextBox);
        }
        #endregion Constructor

        #region Private Methods
        private void AddNewScriptIfNone()
        {
            if (documents.Count < 1)
            {
                frmDocument docNew = new frmDocument(string.Format(NEWSCRIPTFORMATSTRINGPREFIX + "{0}", 1), "");
                docNew.ToolTipShown += Document_ToolTipShown;
                docNew.Show(dockPanel1, DockState.Document);
                documents.Add(docNew);
            }
        }

        private bool ContinueIfChanges(frmDocument docToCheck)
        {
            if (docToCheck == null)
                return true;

            if (docToCheck.FileModified && !string.IsNullOrEmpty(docToCheck.ScriptText))
            {
                DialogResult dlgResult = SaveChangesQuestion(docToCheck.FileName);

                if (dlgResult == DialogResult.Cancel)
                    return false;
                else if (dlgResult == DialogResult.Yes)
                {
                    SaveScript(docToCheck);

                    if (docToCheck.FileModified)
                        return false;
                }
            }

            return true;
        }

        private frmDocument GetActiveDocument()
        {
            if (documents.Count < 1)
                return null;

            return (frmDocument)documents[0].Pane.ActiveContent;
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            string[] persistStringParts = persistString.Split(new char[] { '\t' });

            switch (persistStringParts[0].ToLower())
            {
                case "document":
                    // persistStringParts[1] is FileName
                    frmDocument doc = new frmDocument(persistStringParts[1], null);
                    doc.ToolTipShown += Document_ToolTipShown;

                    documents.Add(doc);
                    return doc;
                case "imagepreview":
                    // persistStringParts[1] is empty
                    return imagePreview;
                case "hint":
                    // persistStringParts[1] is TextControlType
                    return hintWindow;
                case "output":
                    // persistStringParts[1] is TextControlType
                    return outputWindow;
                case "svg file":
                    // persistStringParts[1] is TextControlType
                    return svgContents;
                default:
                    return null;
            }
        }

        private bool IsFileLoaded(string FileName)
        {
            return IsFileLoaded(FileName, false);
        }

        private bool IsFileLoaded(string FileName, bool ExcludeActiveDocument)
        {
            frmDocument activeDoc = GetActiveDocument();

            foreach (frmDocument doc in documents)
            {
                if (ExcludeActiveDocument && doc == activeDoc)
                    continue;

                if (doc.FileName == FileName)
                {
                    MessageBox.Show(this, "File is opened in another window.", "Duplicate File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }

            return false;
        }

        private void RefreshViewMenuItemsCheckedState()
        {
            previewToolStripMenuItem.Checked = !imagePreview.IsHidden;
            sVGFileToolStripMenuItem.Checked = !svgContents.IsHidden;
            hintsToolStripMenuItem.Checked = !hintWindow.IsHidden;
            outputToolStripMenuItem.Checked = !outputWindow.IsHidden;
        }

        private void ResetLayout()
        {
            AddNewScriptIfNone();

            imagePreview.Show(dockPanel1, DockState.DockRight);
            svgContents.Show(imagePreview.Pane, null);

            hintWindow.Show(dockPanel1, DockState.DockBottom);
            outputWindow.Show(hintWindow.Pane, null);

            imagePreview.Clear();
            svgContents.Clear();
            hintWindow.Clear();
            outputWindow.Clear();

            imagePreview.Activate();
            hintWindow.Activate();

            dockPanel1.SaveAsXml(LAYOUTSTOREFILE);
        }

        public void RunScript(IGraphics ig, string ScriptText)
        {
            /*** Begin Part 1 of 2: Flip Y axis ***/
            // Flip graphic area so Y axis positive is up rather than down
            // https://stackoverflow.com/questions/39897413/how-to-change-the-origin-in-windows-forms-bitmap
            // Begin graphics container
            GraphicsContainer containerState = ig.BeginContainer();
            // Flip the Y-Axis
            ig.ScaleTransform(1.0F, -1.0F);
            // Translate the drawing area accordingly
            ig.TranslateTransform(0.0F, -(float)imagePreview.Height);
            // Whatever you draw now (using this graphics context) will appear as
            // though (0,0) were at the bottom left corner
            /*** End Part 1 of 2: Flip Y axis ***/

            // Scale from px to mm
            if (dpmmX > 0 && dpmmY > 0)
                ig.ScaleTransform(dpmmX, dpmmY);

            var scriptOptions = ScriptOptions.Default.WithEmitDebugInformation(true).AddReferences(
                typeof(IGraphics).Assembly).AddImports("System.Drawing",
                                                        "System.Drawing.Drawing2D",
                                                        "System");

            string outFile = Path.Combine(Application.StartupPath, "output.txt");



            try
            {
                using (StreamWriter writer = new StreamWriter(outFile))
                {
                    Console.SetOut(writer);
                    System.Threading.Tasks.Task<object> emitResult = CSharpScript.EvaluateAsync(ScriptText, scriptOptions, new ScriptHost { G = ig });

                    /*** Begin Part 2 of 2: Flip Y axis ***/
                    // End graphics container
                    ig.EndContainer(containerState);
                    /*** End Part 2 of 2: Flip Y axis ***/
                }
            }
            catch (Exception)
            {
                //  CS8055: Cannot emit debug information for a source text without encoding.
                // e.Diagnostics.Verify(Diagnostic(ErrorCode.ERR_EncodinglessSyntaxTree, code).WithLocation(1, 1));
                throw;
            }

            outputWindow.ReadOnlyText = File.ReadAllText(outFile);
        }

        private DialogResult SaveChangesQuestion(string FileName)
        {
            string fName = Path.GetFileName(FileName);
            return MessageBox.Show(this, string.Format("Save changes to script, {0}, before closing?", fName), 
                "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        private void SaveScript(frmDocument docToSave)
        {
            if (docToSave == null)
            {
                MessageBox.Show(this, "Failed to get active document", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(docToSave.FileName) || docToSave.FileName.StartsWith(NEWSCRIPTFORMATSTRINGPREFIX))
            {
                SaveScriptAs(docToSave);
                return;
            }

            docToSave.SaveScriptFile();
        }

        private void SaveScriptAs(frmDocument docToSave)
        {
            if (docToSave == null)
            {
                MessageBox.Show(this, "Failed to get active document", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(docToSave.ScriptText))
            {
                MessageBox.Show(this, "No script to save", "Cannot Save Script", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.Filter = SCRIPTFILEFILTER;
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.Title = "Save Script File";

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                if (IsFileLoaded(saveFileDialog1.FileName, true))
                    return;

                docToSave.SaveScriptFile(saveFileDialog1.FileName);
            }
        }
        private void SaveTextFile(string FileContents, string FileName)
        {
            using (StreamWriter outputfile = new StreamWriter(FileName))
            {
                outputfile.Write(FileContents);
            }
        }
        #endregion Private Methods

        #region Event Handlers
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();

            about.ShowDialog(this);
        }

        private void CloseAllScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (frmDocument docToClose in documents)
            {
                if (!ContinueIfChanges(docToClose))
                    return;
            }

            foreach (frmDocument docToClose in documents)
            {
                docToClose.Close();
            }

            documents.Clear();

            AddNewScriptIfNone();
        }

        private void CloseScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDocument docToClose = GetActiveDocument();

            if (ContinueIfChanges(docToClose))
            {
                documents.Remove(docToClose);
                docToClose.Close();
            }

            AddNewScriptIfNone();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveDocument().Edit_Copy();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveDocument().Edit_Cut();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveDocument().Edit_Delete();
        }

        private void DockWindow_VisibleChanged(object sender, EventArgs e)
        {
            RefreshViewMenuItemsCheckedState();
        }

        private void Document_ToolTipShown(object sender, AutoCompleteHintEventArgs e)
        {
            hintWindow.ReadOnlyText = e.ToolTipText;
        }

        private void EditToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            cutToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;

            if (GetActiveDocument().IsTextSelected)
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            }

            pasteToolStripMenuItem.Enabled = false;

            if(Clipboard.ContainsText())
                pasteToolStripMenuItem.Enabled = true;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (helpForm != null)
                helpForm.Dispose();

            Application.Exit();
        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveDocument().FindDialogShow();
        }

        private void FindReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveDocument().FindReplaceDialogShow();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (frmDocument fDoc in documents)
            {
                e.Cancel = !ContinueIfChanges(fDoc);

                if (e.Cancel)
                    break;
            }

            dockPanel1.SaveAsXml(LAYOUTSTOREFILE);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(LAYOUTSTOREFILE))
            {
                dockPanel1.LoadFromXml(LAYOUTSTOREFILE, m_deserializeDockContent);

                for(int i = documents.Count; i > 0; i--)
                {
                    frmDocument doc = documents[i-1];

                    if (!doc.FileExists)
                    {
                        documents.Remove(doc);
                        doc.Close();
                    }
                }
            }
            else
            {
                ResetLayout();
            }

            imagePreview.VisibleChanged += DockWindow_VisibleChanged;
            hintWindow.VisibleChanged += DockWindow_VisibleChanged;
            outputWindow.VisibleChanged += DockWindow_VisibleChanged;
            svgContents.VisibleChanged += DockWindow_VisibleChanged;

            AddNewScriptIfNone();

            RefreshViewMenuItemsCheckedState();
        }

        private void NewScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int maxNew = 0;

            foreach(frmDocument fDoc in documents)
            {
                if(fDoc.FileName.StartsWith(NEWSCRIPTFORMATSTRINGPREFIX))
                {
                    if (int.TryParse(fDoc.FileName.Substring(4), out int i))
                    {
                        if (i > maxNew)
                            maxNew = i;
                    }
                }
            }

            frmDocument docNew = new frmDocument(string.Format(NEWSCRIPTFORMATSTRINGPREFIX + "{0}", maxNew + 1), "");
            docNew.ToolTipShown += Document_ToolTipShown;

            if (documents.Count < 1)
                docNew.Show(dockPanel1, DockState.Document);
            else
                docNew.Show(documents[0].Pane, null);

            documents.Add(docNew);
        }

        private void OpenScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = SCRIPTFILEFILTER;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.Title = "Open Script File";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                if (IsFileLoaded(openFileDialog1.FileName))
                    return;

                frmDocument docNew = new frmDocument(openFileDialog1.FileName, null);
                docNew.ToolTipShown += Document_ToolTipShown;

                if (documents.Count < 1)
                    docNew.Show(dockPanel1, DockState.Document);
                else
                    docNew.Show(documents[0].Pane, null);

                documents.Add(docNew);
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveDocument().Edit_Paste();
        }

        private void ResetLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetLayout();
            RefreshViewMenuItemsCheckedState();
        }

        private void RunScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox picBox = imagePreview.PicBox;
            frmDocument doc = GetActiveDocument();

            svgContents.Clear();
            imagePreview.Clear();
            outputWindow.Clear();

            saveImageToolStripMenuItem.Enabled = false;

            Cursor = Cursors.WaitCursor;

            SvgGraphics ig = new SvgGraphics();
            Bitmap bitmap = new Bitmap(picBox.Width, picBox.Height, PixelFormat.Format32bppArgb);
            GdiGraphics graphics = new GdiGraphics(Graphics.FromImage(bitmap));

            if (dpmmX <= 0 || dpmmY <= 0)
            {
                dpmmX = graphics.DpiX / 25.4F;
                dpmmY = graphics.DpiY / 25.4F;
            }

            try
            {
                RunScript(ig, doc.ScriptText);
                RunScript(graphics, doc.ScriptText);

                if (bitmap == null)
                    return;

                svgContents.ReadOnlyText = ig.WriteSVGString();
                picBox.Image = bitmap;

                saveImageToolStripMenuItem.Enabled = true;

                Cursor = Cursors.Default;
            }
            catch (Exception exp)
            {
                Cursor = Cursors.Default;
                outputWindow.ReadOnlyText = exp.Message;
                outputWindow.Activate();
                MessageBox.Show(this, "There were script errors. Unable to draw image.", "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAllScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(frmDocument doc in documents)
            {
                SaveScript(doc);
            }
        }

        private void SaveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(svgContents.ReadOnlyText))
            {
                MessageBox.Show(this, "No image to save", "Cannot Save Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.Filter = ImageFileTypes.FileDialogFilterAllImages + "|All Files|*.*";
            saveFileDialog1.FilterIndex = 6;
            saveFileDialog1.Title = "Save Image File";
            saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(GetActiveDocument().FileName);

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                ImageFileTypes.ImageFileType fileType = ImageFileTypes.GetImageFileTypeFromFileName(saveFileDialog1.FileName);

                if (string.IsNullOrEmpty(fileType.Name) || (!fileType.IsVectorFormat && fileType.ImgFormat == null))
                {
                    MessageBox.Show(this, "Unable to save file due to unsupported file format selected. (File Extension)", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(fileType.IsVectorFormat)
                    SaveTextFile(svgContents.ReadOnlyText, saveFileDialog1.FileName);
                else
                    imagePreview.PicBox.Image.Save(saveFileDialog1.FileName, fileType.ImgFormat);
            }
        }

        private void SaveScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScript(GetActiveDocument());
        }

        private void SaveScriptAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScriptAs(GetActiveDocument());
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveDocument().SelectAll();
        }

        private void ViewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(helpForm == null)
                helpForm = new frmHelp();

            helpForm.Show(this);
        }

        private void WindowVisibilityMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mnuItm = (ToolStripMenuItem)sender;
            DockContent dockContent;

            switch (mnuItm.Tag.ToString().ToLower())
            {
                case "imagepreview":
                    dockContent = imagePreview;
                    break;
                case "svgcontents":
                    dockContent = svgContents;
                    break;
                case "hintwindow":
                    dockContent = hintWindow;
                    break;
                case "outputwindow":
                    dockContent = outputWindow;
                    break;
                default:
                    return;
            }

            dockContent.IsHidden = !dockContent.IsHidden;
            mnuItm.Checked = !dockContent.IsHidden;
        }
        #endregion Event Handlers
    }

    public class ScriptHost
    {
        public IGraphics G { get; set; }
    }
}
