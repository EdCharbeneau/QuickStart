using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickStart.Models
{
    public class PlaceholderModel
    {
        public DateTime Column1 { get; set; }
        public string Column2 { get; set;}
        public int Column3 { get; set; }

        public static List<PlaceholderModel> GetPlaceholderData()
        {
            var dataCollection = new List<PlaceholderModel>();
            dataCollection.Add(new PlaceholderModel { Column1 = DateTime.Now, Column2 = "Lorem Ipsum", Column3 = 500 });
            dataCollection.Add(new PlaceholderModel { Column1 = DateTime.Now, Column2 = "Lorem Ipsum", Column3 = 1500 });
            dataCollection.Add(new PlaceholderModel { Column1 = DateTime.Now, Column2 = "Lorem Ipsum", Column3 = 2500 });
            return dataCollection;
        }
    }
    public class Name
    {
        public string FullName { get; set; }

        public static List<Name> GetListView()
        {
            var names = new List<Name>();
            names.Add(new Name { FullName = "Lorem Ipsum" });
            names.Add(new Name { FullName = "Lorem Ipsum" });
            names.Add(new Name { FullName = "Lorem Ipsum" });
            return names;
        }
    }
}