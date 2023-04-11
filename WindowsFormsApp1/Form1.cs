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
using System.Net;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
       
       

        private void writeAnythingTextBox_Enter(object sender, EventArgs e)
        {

            if (writeAnythingTextBox.Text == "Write anything")
            {
                writeAnythingTextBox.Text = "";
                writeAnythingTextBox.ForeColor = Color.Black;
            }

        }

        private void writeAnythingTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(writeAnythingTextBox.Text))
            {
                writeAnythingTextBox.ForeColor = Color.Gray;
                writeAnythingTextBox.Text = "Write anything";
            }
        }

       
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string query = writeAnythingTextBox.Text;
      
                string url = "https://www.google.com/search?q=" + query;
                //I used Google server instead of Yahoo because it is the reason for the exceptions


                try
                {
                    // Create a web request
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                    // Get the response
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    string result = streamReader.ReadToEnd();

                    // Save the result to a file
                    File.WriteAllText(filePath, result);

                    MessageBox.Show("Result saved to " + filePath);
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.TrustFailure)
                    {
                        // Handle SSL/TLS trust failure
                        MessageBox.Show("Could not establish trust relationship for the SSL/TLS secure channel. Please check your network connection and try again.");
                    }
                    else
                    {
                        // Handle other web exceptions
                        MessageBox.Show("An error occurred while trying to make the web request: " + ex.Message);
                    }
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
           string query = writeAnythingTextBox.Text;
            
            string url = "https://www.google.com/search?q=" + query;
            //I used Google server instead of Yahoo because it is the reason for the exceptions



            try
            {
        // Create a web request
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        // Get the response
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader streamReader = new StreamReader(response.GetResponseStream());
        string result = streamReader.ReadToEnd();

        // Display the result
        MessageBox.Show(result);
    }
    catch (WebException ex)
    {
        if (ex.Status == WebExceptionStatus.TrustFailure)
        {
            // Handle SSL/TLS trust failure
            MessageBox.Show("Could not establish trust relationship for the SSL/TLS secure channel. Please check your network connection and try again.");
        }
        else
        {
            // Handle other web exceptions
            MessageBox.Show("An error occurred while trying to make the web request: " + ex.Message);
        }
    }
        }
    }
}
