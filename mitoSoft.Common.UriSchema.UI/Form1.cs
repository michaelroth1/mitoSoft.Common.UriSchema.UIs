namespace mitoSoft.Common.UriSchema.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //select file
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = this.openFileDialog.FileName;
            }
        }

        //Register
        private void button2_Click(object sender, EventArgs e)
        {
            var schema = this.textBox1.Text;
            var path = this.textBox2.Text;

            if (string.IsNullOrWhiteSpace(schema))
            {
                MessageBox.Show("Please choose a valid 'Url-Schema'.", "mitoSoft", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("Please choose a valid excecutable file.", "mitoSoft", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(RegistryHelper.IsRegistered(schema))
                && MessageBox.Show($"'{schema}' already existing. Do you want to update it?",
                                   "mitoSoft",
                                   MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            RegistryHelper.RegisterScheme(schema, path, this.checkBox1.Checked);

            MessageBox.Show("Successfully registered", "mitoSoft");
        }
    }
}