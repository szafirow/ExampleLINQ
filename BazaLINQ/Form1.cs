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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataClasses1DataContext db;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            db = new DataClasses1DataContext();
            dataGridView1.DataSource = db.viewRents;
            button2.Visible = false;
            button3.Visible = false;

            label1.Visible = false;
            comboBox1.Visible = false;
            label2.Visible = false;
            textBox1.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
            label4.Visible = false;
            maskedTextBox1.Visible = false;
            label5.Visible = false;
            dateTimePicker1.Visible = false;
            label6.Visible = false;
            dateTimePicker2.Visible = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_insert form_insert = new Form_insert();
            form_insert.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_insert2 form_insert2 = new Form_insert2();
            form_insert2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db = new DataClasses1DataContext();
            dataGridView1.DataSource = db.viewRents;
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            try
            {
                string dataCarModel = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string dataNameModel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string dataSurnameModel = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string dataDateRentModel = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                string dataDateReturnModel = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                using (db = new DataClasses1DataContext())
                {
                    if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(maskedTextBox1.Text))
                    {
                         MessageBox.Show("Nie wypelniles pol formularza!");
                    }
                    else
                    {
                        if (MessageBox.Show("Czy na pewno chcesz wykonać tę czynność?", "Pytanie", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //UPDATE CAR

                            var result1 = (
                                from c in db.Cars
                                join r in db.Rents
                                  on c.id equals r.cars_ID
                                join cl in db.Clients
                                  on r.clients_ID equals cl.id
                                where c.model.Contains(dataCarModel) & cl.name.Contains(dataNameModel) & cl.surname.Contains(dataSurnameModel)
                                select r);

                            foreach (var r in result1)
                            {
                                if (dataGridView1.SelectedRows[0].Cells[0].Value.ToString() != comboBox1.Text)
                                {
                                    Car carPresent = db.Cars.Single(cp => cp.id == r.cars_ID);
                                    carPresent.status = 0;
                                    db.SubmitChanges();
                                
                                    Car carRented = db.Cars.Single(cr => cr.id == Convert.ToInt32(comboBox1.SelectedValue));
                                    carRented.status = 1;
                                    db.SubmitChanges();
                               
                                    Rent rent = db.Rents.Single(rx => rx.cars_ID == r.cars_ID); 
                                    rent.cars_ID = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                                    db.SubmitChanges();
                                }
                               

                            }

                            //UPDATE NAME && SURNAME & PHONENUM

                            var result2 = (
                                from c in db.Clients
                                join r in db.Rents
                                  on c.id equals r.clients_ID
                                where c.name.Contains(dataNameModel) & c.surname.Contains(dataSurnameModel)
                                select c).Take(1);

                            foreach (var c in result2)
                            {
                                Client client = db.Clients.Single(cp => cp.id == c.id);
                                client.name = textBox1.Text;
                                client.surname = textBox2.Text;
                                client.phoneNum = maskedTextBox1.Text;
                                db.SubmitChanges();
                            }
                            //UPDATE DATERENT && DATERETURN
 
                            var result3 = (
                                    from r in db.Rents
                                    join c in db.Clients
                                        on r.clients_ID equals c.id
                                    join ca in db.Cars
                                        on r.cars_ID equals ca.id
                                    where ca.model.Contains(dataCarModel) & r.dateRent.Equals(dataDateRentModel) & r.dateReturn.Equals(dataDateReturnModel) & c.name.Contains(dataNameModel) & c.surname.Contains(dataSurnameModel)
                                    select r).Take(1);

                            foreach (var r in result3)
                            {
                                Rent rent = db.Rents.Single(rp => rp.id == r.id);
                                rent.dateRent = Convert.ToDateTime(dateTimePicker1.Text);
                                rent.dateReturn = Convert.ToDateTime(dateTimePicker2.Text);
                                db.SubmitChanges();
                            }


                            dataGridView1.DataSource = db.viewRents;
                            MessageBox.Show("Zmodyfikowano rekordy!"); 
                        }
                        
                     }


       
                    
                    
                }
            }
            catch(Exception ex){
                MessageBox.Show("Error" + ex);
            }
            
            
            
           
            //SETTINGS
            button2.Visible = false;
            button3.Visible = false;

            label1.Visible = false;
            comboBox1.Visible = false;
            label2.Visible = false;
            textBox1.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
            label4.Visible = false;
            maskedTextBox1.Visible = false;
            label5.Visible = false;
            dateTimePicker1.Visible = false;
            label6.Visible = false;
            dateTimePicker2.Visible = false;
       

         
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                using (db = new DataClasses1DataContext())
                {
                    comboBox1.DisplayMember = "model";
                    comboBox1.ValueMember = "id";
                    //comboBox1.DataSource = db.Cars.ToList<Car>();
                    comboBox1.DataSource = db.Cars.ToList<Car>().Where(c => c.status == 0).ToList();
                }


                comboBox1.SelectedItem = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
               
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                maskedTextBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                dateTimePicker2.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                button2.Visible = true;
                button3.Visible = true;

                label1.Visible = true;
                comboBox1.Visible = true;
                label2.Visible = true;
                textBox1.Visible = true;
                label3.Visible = true;
                textBox2.Visible = true;
                label4.Visible = true;
                maskedTextBox1.Visible = true;
                label5.Visible = true;
                dateTimePicker1.Visible = true;
                label6.Visible = true;
                dateTimePicker2.Visible = true;
               
            }
            catch (Exception ex)
            {
                
            }
                

               
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new DataClasses1DataContext())
                {
                    if (MessageBox.Show("Czy na pewno chcesz wykonać tę czynność?", "Pytanie", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string dataNameModel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                        string dataSurnameModel = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                        string dataDateRentModel = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                        string dataDateReturnModel = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
    
            
            //DEL RENT
                        var result4 = (
                                    from r in db.Rents
                                    join c in db.Clients
                                        on r.clients_ID equals c.id
                                    where c.name.Contains(dataNameModel) & c.surname.Contains(dataSurnameModel)
                                    select r).Take(1);

                        foreach (var r in result4)
                        {
                            db.Rents.DeleteOnSubmit(r);
                            db.SubmitChanges();

            //CAR STATUS
                            Car carPresent = db.Cars.Single(cp => cp.id == r.cars_ID);
                            carPresent.status = 0;
                            db.SubmitChanges();

                        }
            //DEL CLIENT

                        var result6 = (
                                    from c in db.Clients
                                    where c.name.Contains(dataNameModel) & c.surname.Contains(dataSurnameModel)
                                    select c).Take(1);

                        foreach (var c in result6)
                        {
                            db.Clients.DeleteOnSubmit(c);
                            db.SubmitChanges();
                        }

           


                        dataGridView1.DataSource = db.viewRents;
                        MessageBox.Show("Usunieto zapis!");
                    }

                    
                }

            }
            catch(Exception ex){
                MessageBox.Show("Error" + ex);
            }
            
        }
    }
}
