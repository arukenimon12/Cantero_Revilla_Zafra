using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cantero_Revilla_Zafra
{
    public partial class Form1 : Form
    {
        List<Person> family = new List<Person>();
        public Form1()
        {
            InitializeComponent();

            Person parent1 = new Person()
            {
                name = "Alice",
                index = 0,
            };
            family.Add(parent1);
            Person parent2 = new Person()
            {
                name = "Manuel",
                index = 0
            };
            family.Add(parent2);

            Person child1 = new Person()
            {
                name = "Rex",
                index = 1
            };
            child1.AddParent_Child(new List<Person> { parent1, parent2 });
            family.Add(child1);
            //^v siblings
            Person child2 = new Person()
            {
                name = "Ella",
                index = 1
            };
            child2.AddParent_Child(new List<Person>{ parent1, parent2 });
            family.Add(child2);

            Person Xavier = new Person()
            {
                name = "Xavier",
                index = 2
            };
            Xavier.AddParent_Child(new List<Person> { child2 });
            family.Add(Xavier);

            //child1.Parents.ForEach((ee) => { MessageBox.Show($"{ee.name}"); }) ;


            Person generation3 = new Person()
            {
                name = "Roy",
                index = 2
            };
            generation3.AddParent_Child(new List<Person> { child1 });
            family.Add(generation3);

            Person generation3_1 = new Person()
            {
                name = "Jean",
                index = 2
            };
            generation3_1.AddParent_Child(new List<Person> { child2 });
            family.Add(generation3_1);

            //MessageBox.Show($"{family.OrderByDescending(f => f.name).ToArray()[0].name}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            label1.Text = string.Empty;
            if (cbSelection.SelectedIndex == 0)
            {
                var Family = family.Where(f => f.index == 0);
                //MessageBox.Show($"{Family.Count()}");

                foreach (var person in Family)
                    if(!listBox1.Items.Contains(person.name))
                        listBox1.Items.Add(person.name);
            }

            if (cbSelection.SelectedIndex == 1)
            {
                var Family = family.Where(f => f.isParent());

                foreach (var person in Family)
                    if (!listBox1.Items.Contains(person.name))
                        listBox1.Items.Add(person.name);
            }

            if (cbSelection.SelectedIndex == 2)
            {
                var Family = family.Where(f=>f.isChild());
                //MessageBox.Show($"{Family.Count()}");
                
                foreach(var person in Family)
                    if (!listBox1.Items.Contains(person.name))
                        listBox1.Items.Add(person.name);
            }

            if (cbSelection.SelectedIndex == 3)
            {
                var Family = family.Where(f => f.index == family.Max(ff => ff.index));
                //MessageBox.Show($"{Family.Count()}");

                foreach (var person in Family)
                    if (!listBox1.Items.Contains(person.name))
                        listBox1.Items.Add(person.name);
            }

            /*
             Grandparents
            Parents
            Children
            Grandchildren*/
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox1.SelectedItem == null) return;

            var Selected = family.Where(f => f.name == listBox1.SelectedItem.ToString());
            label1.Text = "";
            if (Selected.Any(f => f.Parents.Count > 0))
            {
                label1.Text = "Parents:\n";
                foreach (var item in Selected)
                {
                    if (item.Parents.Count > 0)
                    {
                        item.Parents.ForEach((ee) =>
                        {
                            label1.Text += ee.name + "\n";
                        });
                    }
                }
                label1.Text += "\n";
            }
            

            //var Children = family.Where(f => f.name == listBox1.SelectedItem.ToString());

            if(Selected.Any(c => c.Children.Count > 0))
            {
                label1.Text += "Children:\n";
                foreach (var item in Selected)
                {
                    if (item.Children.Count > 0)
                    {
                        item.Children.ForEach((ee) =>
                        {
                            label1.Text += $"{ee.name}\n";
                        });
                    }
                }
                label1.Text += "\n";
            }


            var Cousins = family.Where(
                f => f.index == Selected.First().index && 
                !f.isSiblings(Selected.First()));

            if (Cousins.Count() > 0)
            {
                label1.Text += "Cousin(s):\n";
                foreach (var item in Cousins)
                {
                    label1.Text += $"{item.name}\n";
                }
                label1.Text += "\n";
            }

        }
    }
}
