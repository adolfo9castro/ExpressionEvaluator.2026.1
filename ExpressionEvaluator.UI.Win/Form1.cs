using System;
using System.Windows.Forms;
using ExpressionEvaluator.Core;

namespace ExpressionEvaluator.UI.Win
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGeneric_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            screenResult.Text += btn.Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            screenResult.Text = string.Empty;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (screenResult.Text.Length > 0)
            {
                screenResult.Text = screenResult.Text.Substring(0, screenResult.Text.Length - 1);
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                string expression = screenResult.Text;

                if (expression.Contains("="))
                {
                    expression = expression.Split('=')[0];
                }

                double result = Evaluator.Evaluate(expression);

                screenResult.Text = expression + "=" + result.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
