using System;
using System.Windows.Forms;

namespace RedBear.MpanGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var mpan = long.Parse(txtMpan.Text);
            var isValid = false;

            while (!isValid)
            {
                mpan += 1;
                isValid = MpanIsValid(mpan.ToString());
            }

            txtMpan.Text = mpan.ToString();
            Clipboard.SetText(txtMpan.Text);
        }

        private bool MpanIsValid(string mpan)
        {
            // Set initial conditions.
            var validationResult = false;

            if (mpan.Length > 12)
            {

                //Read the check digit into an Integer variable.
                int intCheckDigit;
                if (int.TryParse(mpan.Substring(mpan.Length - 1), out intCheckDigit))
                {
                    int[] intPrimes = { 3, 5, 7, 13, 17, 19, 23, 29, 31, 37, 41, 43 };
                    var productTotal = 0;
                    var blnError = false;

                    for (var i = 0; i <= 11; i++)
                    {
                        int intTestDigit;
                        if (int.TryParse(mpan.Substring(i, 1), out intTestDigit))
                        {
                            productTotal += (intTestDigit * intPrimes[i]);
                        }
                        else
                        {
                            blnError = true;
                            break;
                        }
                    }

                    if (!blnError)
                    {
                        validationResult = ((productTotal % 11 % 10) == intCheckDigit);
                    }
                }

            }

            return validationResult;

        }
    }
}
