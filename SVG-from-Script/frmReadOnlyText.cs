using System.Windows.Forms;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace SVG_from_Script
{
    public partial class frmReadOnlyText : DockContent
    {
        public enum TextControlTypes
        {
            TextBox,
            RichTextBox,
            FastColoredTextBox
        };

        #region Fields
        private TextControlTypes mTextControlType = TextControlTypes.TextBox;
        #endregion Fields

        #region Constuctor
        public frmReadOnlyText(Icon Icon, string Title, TextControlTypes TextControlType)
        {
            InitializeComponent();

            this.Icon = Icon;
            this.Title = Title;
            this.TextControlType = TextControlType;
            Clear();
        }
        #endregion Constructor

        #region Public Properties
        public string ReadOnlyText
        {
            get { return TextControl.Text; }
            set { TextControl.Text = value; }
        }

        public Control TextControl
        {
            get
            {
                switch (TextControlType)
                {
                    case TextControlTypes.RichTextBox:
                        return richTextBox1;
                    case TextControlTypes.FastColoredTextBox:
                        return fastColoredTextBox1;
                    case TextControlTypes.TextBox:
                    default:
                        return textBox1;
                }
            }
        }

        public TextControlTypes TextControlType
        {
            get
            {
                return mTextControlType;
            }
            set
            {
                mTextControlType = value;

                textBox1.Visible = false;
                richTextBox1.Visible = false;
                fastColoredTextBox1.Visible = false;
                textBox1.Dock = DockStyle.Fill;
                richTextBox1.Dock = DockStyle.Fill;
                fastColoredTextBox1.Dock = DockStyle.Fill;

                switch (TextControlType)
                {
                    case TextControlTypes.RichTextBox:
                        richTextBox1.Visible = true;
                        break;
                    case TextControlTypes.FastColoredTextBox:
                        fastColoredTextBox1.Visible = true;
                        break;
                    case TextControlTypes.TextBox:
                    default:
                        textBox1.Visible = true;
                        break;
                }
            }
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
            switch (TextControlType)
            {
                case TextControlTypes.RichTextBox:
                    ((RichTextBox)TextControl).Clear();
                    break;
                case TextControlTypes.FastColoredTextBox:
                    ((FastColoredTextBoxNS.FastColoredTextBox)TextControl).Clear();
                    break;
                case TextControlTypes.TextBox:
                    ((TextBox)TextControl).Clear();
                    break;
                default:
                    TextControl.Text = null;
                    break;
            }
        }

        protected override string GetPersistString()
        {
            return string.Format("{0}\t{1}", Title, TextControlType.ToString());
        }
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods

        #region Event Handlers
        #endregion Event Handlers
    }
}
