using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVG_from_Script
{
    public partial class frmFindReplace : Form
    {
        #region Fields
        #endregion Fields

        #region Constuctor
        public frmFindReplace()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Public Properties
        public string MessageText
        {
            get { return txtInfo.Text; }
            set { txtInfo.Text = value; }
        }
        #endregion Public Properties

        #region Public Methods
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods

        #region Event Handlers
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cmdCountAll_Click(object sender, EventArgs e)
        {
            FindEventArgs args = new FindEventArgs
            {
                FindText = txtFind.Text,
                ReplaceText = null
            };
            OnCountAll(args);
        }

        private void cmdFindNext_Click(object sender, EventArgs e)
        {
            FindEventArgs args = new FindEventArgs
            {
                FindText = txtFind.Text,
                ReplaceText = null
            };
            OnFindNext(args);
        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {
            FindEventArgs args = new FindEventArgs
            {
                FindText = txtFind.Text,
                ReplaceText = txtReplace.Text
            };
            OnReplace(args);
        }

        private void cmdReplaceAll_Click(object sender, EventArgs e)
        {
            FindEventArgs args = new FindEventArgs
            {
                FindText = txtFind.Text,
                ReplaceText = txtReplace.Text
            };
            OnReplaceAll(args);
        }
        #endregion Event Handlers

        #region Events
        public event EventHandler<FindEventArgs> CountAll;

        public event EventHandler<FindEventArgs> FindNext;

        public event EventHandler<FindEventArgs> Replace;

        public event EventHandler<FindEventArgs> ReplaceAll;

        protected virtual void OnCountAll(FindEventArgs e)
        {
            CountAll?.Invoke(this, e);
        }

        protected virtual void OnFindNext(FindEventArgs e)
        {
            FindNext?.Invoke(this, e);
        }

        protected virtual void OnReplace(FindEventArgs e)
        {
            Replace?.Invoke(this, e);
        }

        protected virtual void OnReplaceAll(FindEventArgs e)
        {
            ReplaceAll?.Invoke(this, e);
        }
        #endregion Events
    }

    public class FindEventArgs : EventArgs
    {
        public string FindText { get; set; }

        public string ReplaceText { get; set; }
    }
}
