using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace SVG_from_Script
{
    public partial class frmHelp : Form
    {
        #region DLL Import
        //http://social.msdn.microsoft.com/Forums/vstudio/en-US/1940f513-8b72-4a29-9a57-85427d8fa88a/how-to-disable-click-sound-in-webbrowser-control?forum=csharpgeneral
        const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        const int SET_FEATURE_ON_THREAD = 0x00000001;
        const int SET_FEATURE_ON_PROCESS = 0x00000002;
        const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);

        static void DisableClickSounds()
        {
            CoInternetSetFeatureEnabled(
                FEATURE_DISABLE_NAVIGATION_SOUNDS,
                SET_FEATURE_ON_PROCESS,
                true);
        }
        #endregion DLL Import

        #region Fields
        private string helpFolder;
        private string helpHome = "index.html";
        private string helpURI = "";
        private FormWindowState lastWindowState;
        #endregion Fields

        #region Constuctor
        public frmHelp()
        {
            InitializeComponent();

            helpFolder = Path.Combine(Application.StartupPath, "Help");
        }
        #endregion Constructor

        #region Public Properties
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        public FormWindowState LastWindowState
        {
            get { return lastWindowState; }
        }
        #endregion Public Properties

        #region Public Methods
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods

        #region Event Handlers
        private void frmHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void FrmHelp_Load(object sender, EventArgs e)
        {
            Text = String.Format("Help - {0}", AssemblyTitle);
            helpHome = Path.Combine(helpFolder, helpHome);
            helpURI = "file://" + helpHome.Replace('\\', '/');

            if (File.Exists(helpHome))
            {
                DisableClickSounds();
                webBrowser1.Navigate(helpURI);
            }
            else
            {
                MessageBox.Show(this, "Cannot find Help Contents. Unable to display Help.", "Error loading Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void frmHelp_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                lastWindowState = this.WindowState;
        }
        #endregion Event Handlers
    }
}
