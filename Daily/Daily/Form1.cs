using Daily.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Daily
{
    public partial class Form1 : Form

       
    {
        public UserDefinition _user;

        public Form1(UserDefinition user)
        {
           
            InitializeComponent();
            _user = user;

            this.Size = new Size(950, 600);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text ="Hello " + _user.FirstName;
            label3.Text = _user.FirstName + " " + _user.LastName +" " + _user.Username;

            string[] files = Directory.GetFiles(Application.StartupPath, "*.xml");

            foreach (var file in files)
            {
                
                XmlDocument xml = new XmlDocument();
                xml.Load(file);

                
                XmlNode date = xml.SelectSingleNode("//Date");
                XmlNode title = xml.SelectSingleNode("//Title");

                if (date != null && title != null)
                {
                    
                    string entry = $"{date.InnerText} - {title.InnerText}";
                    if (!ListBox1.Items.Contains(entry))
                    {
                     ListBox1.Items.Add(entry);
                    }
                }
            }

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string title = richTextBox1.Text;
            

            if (!string.IsNullOrEmpty(title))
            {

                string content = richTextBox2.Text;

                DateTime selectedDate = dateTimePicker1.Value;


                DailyDefinition daily = new DailyDefinition
                {
                    Date = selectedDate,
                    Title = title,
                    Content = content
                };  

                string fileName = $"{selectedDate:dd-MM-yyyy}_{title}.xml";
                string filePath = Path.Combine(Application.StartupPath, fileName);


                XmlDocument x = new XmlDocument();

                XmlElement root = x.CreateElement("DailyEntry");
                XmlElement dateElement = x.CreateElement("Date");
                dateElement.InnerText = daily.Date.ToString("dd-MM-yyyy");
                XmlElement titleElement = x.CreateElement("Title");
                titleElement.InnerText = daily.Title;
                XmlElement contentElement = x.CreateElement("Content");
                contentElement.InnerText = daily.Content;


                root.AppendChild(dateElement);
                root.AppendChild(titleElement);
                root.AppendChild(contentElement);
                x.AppendChild(root);


                x.Save(filePath);

                
                ListBox1.Items.Add($"{daily.Date:dd-MM-yyyy} - {daily.Title}");
                richTextBox1.Clear();
                richTextBox2.Clear();


            }
            else
            {
                MessageBox.Show("Please enter a title");
            }

        }


        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem != null)
            {
                
                string selectedItem = ListBox1.SelectedItem.ToString();

               
                string[] p = selectedItem.Split(new[] { " - " }, StringSplitOptions.None);

                if (p.Length == 2)
                {
                    TitleBox.Text = p[1];

                    string selectedDate = p[0];  
                    string selectedTitle = p[1]; 

                    
                    string fileName = $"{selectedDate}_{selectedTitle}.xml";
                    string filePath = Path.Combine(Application.StartupPath, fileName);

                    
                    if (File.Exists(filePath))
                    {
                        XmlDocument x = new XmlDocument();
                        x.Load(filePath);

                        
                        XmlNode _content = x.SelectSingleNode("//Content");
                        if (_content != null)
                        {
                            
                            listBox2.Items.Clear();
                            listBox2.Items.Add(_content.InnerText);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File not found");
                    }

                }
            }

            else
            {
                TitleBox.Text = " ";
            }

        }
        
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string selectedItem = ListBox1.SelectedItem.ToString();
            string[] p = selectedItem.Split(new[] { " - " }, StringSplitOptions.None);

            if (p.Length == 2)
            {
                string selectedDate = p[0];  
                string selectedTitle = p[1]; 

                
                string fileName = $"{selectedDate}_{selectedTitle}.xml";
                string filePath = Path.Combine(Application.StartupPath, fileName);

                
                if (File.Exists(filePath))
                {
                    ListBox1.Items.Remove(ListBox1.SelectedItem);
                    listBox2.Items.Clear();
                    File.Delete(filePath);   
                }

                else
                {
                    MessageBox.Show("File not found");
                }
            }
        }

        

        private void UpdateDaily_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem != null)
            {
                
                string selectedItem = ListBox1.SelectedItem.ToString();
                string[] p = selectedItem.Split(new[] { " - " }, StringSplitOptions.None);

                if (p.Length == 2)
                {
                    string selectedDate = p[0];
                    string selectedTitle = p[1];

                    string fileName = $"{selectedDate}_{selectedTitle}.xml";
                    string filePath = Path.Combine(Application.StartupPath, fileName);

                    
                    if (File.Exists(filePath))
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.Load(filePath);

                       
                        string newContent = richTextBox2.Text;

                      
                        XmlNode content = xml.SelectSingleNode("//Content");

                        if (content != null)
                        {
                            
                            content.InnerText = newContent;
                        }
                        else
                        {
                            
                            XmlElement content0 = xml.CreateElement("Content");
                            content0.InnerText = newContent;
                            xml.DocumentElement.AppendChild(content0);
                        }

                       
                        xml.Save(filePath);
                        MessageBox.Show("XML file has been updated");
                    }
                    else
                    {
                        MessageBox.Show("File not found.");
                    }
                }
                
            }
            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
    }

