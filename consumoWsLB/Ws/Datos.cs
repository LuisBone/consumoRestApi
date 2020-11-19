using System;
using System.Collections.Generic;
using System.Text;

namespace consumoWsLB.Ws
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Item
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int stock { get; set; }
        public int activo { get; set; }
        public double precio { get; set; }
    }

    public class Info
    {
        public List<Item> items { get; set; }
        public int total { get; set; }
    }

    public class Root
    {
        public string mensaje { get; set; }
        public Info info { get; set; }
    }


}
