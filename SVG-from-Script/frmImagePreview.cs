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

namespace SVG_from_Script
{
    public partial class frmImagePreview : DockContent
    {
        #region Fields
        #endregion Fields

        #region Constuctor
        public frmImagePreview()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Public Properties
        public PictureBox PicBox
        {
            get { return pictureBox1; }
        }

        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }
        #endregion Public Properties

        #region Public Methods
        public void Clear()
        {
            pictureBox1.Image = null;
        }

        protected override string GetPersistString()
        {
            return string.Format("{0}\t{1}", "ImagePreview", "");
        }
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods

        #region Event Handlers
        #endregion Event Handlers
    }
}
