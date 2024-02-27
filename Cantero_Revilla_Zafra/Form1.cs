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

            Person Prnt1 = new Person() //parent
            {
                name = "Alice",
                index = 0,
                gender = "F"
            };
            family.Add(Prnt1);
            Person Prnt2 = new Person() //parent
            {
                name = "Manuel",
                index = 0,
                gender = "M"
            };
            family.Add(Prnt2);

            Person Chld1 = new Person() // 
            {
                name = "Rex",
                index = 1,
                gender = "M"
            };
            Chld1.AddParent_Child(new List<Person> { Prnt1, Prnt2 });
            family.Add(Chld1);


            Person Chld_2 = new Person()
            {
                name = "ArAr",
                index = 2,
                gender = "M"
            };
            Chld_2.AddParent_Child(new List<Person> { Chld1 });
            family.Add(Chld_2);


            //^v siblings
            Person Chld2 = new Person()
            {
                name = "Ella",
                index = 1,
                gender = "F"
            };
            Chld2.AddParent_Child(new List<Person>{ Prnt1, Prnt2 });
            family.Add(Chld2);

            Person Chld2_1 = new Person()
            {
                name = "Xavier",
                index = 2,
                gender = "M"
            };
            Chld2_1.AddParent_Child(new List<Person> { Chld2 });
            family.Add(Chld2_1);

            //child1.Parents.ForEach((ee) => { MessageBox.Show($"{ee.name}"); }) ;


            Person Chld1_1 = new Person()
            {
                name = "Roy",
                index = 2,
                gender = "M"
            };
            Chld1_1.AddParent_Child(new List<Person> { Chld1 });
            family.Add(Chld1_1);

            Person Chld2_2 = new Person()
            {
                name = "Jean",
                index = 2,
                gender = "F"
            };
            Chld2_2.AddParent_Child(new List<Person> { Chld2 });
            family.Add(Chld2_2);
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
            Grandchildren
            Sibling*/
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


            var Sibling = family.Where(f => f.Parents.Count > 0 &&
            f.index == Selected.First().index &&
            f.isSiblings(Selected.First()));

            if (Sibling.Count() > 0)
            {
                label1.Text += "Sibling(s):\n";
                foreach (var item in Sibling)
                {
                    label1.Text += $"{item.name}\n";
                }
                label1.Text += "\n";
            }

        }
    }
}
