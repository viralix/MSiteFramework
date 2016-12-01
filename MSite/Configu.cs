using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MSite
{
    public partial class Configu : Form
    {
        Dictionary<string, string> props;
        public Configu()
        {
            InitializeComponent();
            apply.Enabled = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            props = new Dictionary<string, string>();
            loadFile();
            loadProps();
        }

        private void value_TextChanged(object sender, EventArgs e)
        {
            apply.Enabled = true;
            props[pr.Items[pr.SelectedIndex].ToString()] = value.Text;
        }

        private void apply_Click(object sender, EventArgs e)
        {
            apply.Enabled = false;
            save();
        }

        private void loadFile()
        {
            props.Clear();
            string[] file = File.ReadAllLines("config.m");
            foreach(string line in file)
            {
                try
                {
                    string[] x = line.Split('~');
                    props.Add(x[0], x[1]);
                } catch (Exception) { }
            }
        }

        private void loadProps()
        {
            pr.Items.Clear();
            foreach (string key in props.Keys)
            {
                pr.Items.Add(key);
            }
        }
        private void save()
        {
            string[] file = new string[props.Keys.Count];
            int i = 0;
            foreach(string key in props.Keys)
            {
                string val = props[key];
                file[i] = key + "~" + val;
                i++;
            }
            File.WriteAllLines("config.m", file);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            save();
            cancel_Click(sender,e);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pr_SelectedIndexChanged(object sender, EventArgs e)
        {
            value.Text = props[pr.Items[pr.SelectedIndex].ToString()];
            value.Enabled = true;
        }

        private void Configu_Load(object sender, EventArgs e)
        {
            value.Enabled = false;
        }
    }
}
