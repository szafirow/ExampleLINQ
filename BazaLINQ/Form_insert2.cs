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
    public partial class Form_insert2 : Form
    {
        public Form_insert2()
        {
            InitializeComponent();
        }

        DataClasses1DataContext db;

        private void Form_insert2_Load(object sender, EventArgs e)
        {
            using (db = new DataClasses1DataContext()){
                comboBox1.DisplayMember = "model";
                comboBox1.ValueMember = "id";
                comboBox1.DataSource = db.Cars.ToList<Car>().Where(c => c.status == 0).ToList();

                
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new DataClasses1DataContext())
                {

                    //zliczanie klientow - uzyte count
                    string dataNameModel = textBox1.Text;
                    string dataSurnameModel = textBox2.Text;

                    var count = (from c in db.Clients
                                 where c.name.Contains(dataNameModel) & c.surname.Contains(dataSurnameModel)
                                 select c).Count();
                    
               
                    if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(maskedTextBox1.Text))
                    {
                        MessageBox.Show("Nie wypelniles pol formularza!");
                    }
                    //sprawdzic czy takiej osoby nie ma w bazie - pozniej
                    else if(count >= 1 ){
                        MessageBox.Show("Taki klient już jest w bazie!");
                    }
                    else
                    {
                        if (MessageBox.Show("Czy na pewno chcesz wykonać tę czynność?", "Pytanie", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Client newClient = new Client();
                            newClient.name = textBox1.Text;
                            newClient.surname = textBox2.Text;
                            newClient.phoneNum = maskedTextBox1.Text;
                            db.Clients.InsertOnSubmit(newClient);
                            db.SubmitChanges();

                            var lastID = db.Clients.ToArray().LastOrDefault().id; //ostatni klient
                            //MessageBox.Show(lastID.ToString());

                            Rent newRent = new Rent();
                            newRent.cars_ID = Int32.Parse((comboBox1.SelectedValue.ToString()));
                            newRent.clients_ID = Int32.Parse((lastID.ToString()));
                            newRent.dateRent = Convert.ToDateTime(dateTimePicker1.Text);
                            newRent.dateReturn = Convert.ToDateTime(dateTimePicker2.Text);
                            db.Rents.InsertOnSubmit(newRent);
                            db.SubmitChanges();


                            Car car = db.Cars.Single(rx => rx.id == Int32.Parse((comboBox1.SelectedValue.ToString())));
                            car.status = 1;
                            db.SubmitChanges();
                            //  MessageBox.Show("Update");                        


                            MessageBox.Show("Dodano do bazy");
                            this.Close();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
            
        }


       



    }
}
  

     