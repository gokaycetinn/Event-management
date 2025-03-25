using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Daily.Data
{
    public class DailyDefinition
    {
        int _id;
        string _title;
        string _content;
        DateTime _date;
        UserDefinition _user;

        public DailyDefinition()
        {

        }

        public DailyDefinition(int id, string title, string content, DateTime date, UserDefinition user)
        {
            Id = id;
            Title = title;
            Content = content;
            Date = date;
            User = user;
        }

        public int Id { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public string Content { get => _content; set => _content = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public UserDefinition User { get => _user; set => _user = value; }







    }
}

