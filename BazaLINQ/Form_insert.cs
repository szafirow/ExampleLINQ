using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BazaLINQ
{
    public partial class Form_insert : Form
    {
        public Form_insert()
        {
            InitializeComponent();
        }

        DataClasses1DataContext db;

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                using (db = new DataClasses1DataContext()){
                    if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
                    {
                        MessageBox.Show("Nie wypelniles pol formularza!");
                    }
                    else
                    {
                        if (MessageBox.Show("Czy na pewno chcesz wykonać tę czynność?", "Pytanie", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Car newCar = new Car();
                            newCar.model = textBox1.Text;
                            newCar.status = 0;
                            newCar.regNum = textBox2.Text;
                            db.Cars.InsertOnSubmit(newCar);
                            db.SubmitChanges();
                            MessageBox.Show("Dodano do bazy" );
                            this.Close();
                        }
                        
                    }
                    
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ex);
            }
            

            
        }
    }
}
