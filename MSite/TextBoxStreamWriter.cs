using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MSite
{
    public class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;
        private Main main;

        public TextBoxStreamWriter(TextBox output, Main main)
        {
            this.main = main;
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            if (main.InvokeRequired)
            {
                main.Invoke(new MethodInvoker(delegate { _output.AppendText(value.ToString()); }));
            } else
            {
                try
                {
                    _output.AppendText(value.ToString());
                } catch (System.Exception) { };
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}