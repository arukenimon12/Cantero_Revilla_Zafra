using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cantero_Revilla_Zafra
{
    public class Person
    {
        public string name { get; set; }
        public int index { get; set; }

        public string gender { get; set; }

        public List<Person> Parents { get; set; } = new List<Person>();
        public List<Person> Children { get; set; } = new List<Person>();


        public Person(List<Person> _Parent = null, List<Person> _Children = null)
        {
            if(_Parent != null)
                Parents = _Parent;
            if(_Children != null)
                Children = _Children;
        }
        public void AddParent_Child(List<Person> parent)
        {

            for (int i = 0; i < parent.Count; i++)
            {
                parent[i].Children.Add(this);
            }
            Parents = parent;
        }
        public bool isSiblings(Person person)
        {
            bool valid = true;
            Parents.ForEach((name) =>
            {
                if(!person.Parents.Any(p => p.name == name.name))
                {
                    valid = false;
                }
            });
            return valid;
        }

        public bool isParent() => Children.Count > 0;
        public bool isChild () => Parents.Count > 0;
    }
}
