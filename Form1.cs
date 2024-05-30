using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace API
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            employeeDataLoad();
            departmentDataLoad();
        }
        // load to datagridview
        public class Employee
        {
            public string emp_id { get; set; }
            public string emp_name { get; set; }
            public string emp_contact_number { get; set; }
            public string emp_address   { get; set; }
            public string dep_name { get; set; }
            public string username { get; set; }
            public string email { get; set; }
        }

        public class Department
        {
            public string dep_id { get; set; }
            public string dep_name { get; set; }
        }

        public async void departmentDataLoad()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost/myapi/phpapi/api.php?department");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var departments = JsonConvert.DeserializeObject<List<Department>>(responseBody);
                dataGridView2.DataSource = departments;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        async void employeeDataLoad()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost/myapi/phpapi/api.php?employee");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<Employee>>(responseBody);
                dataGridView1.DataSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGet_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            var employeeData = new { emp_name = txtName.Text, emp_address = txtAddress.Text, emp_contact_number = txtContact.Text, dep_id = txtDept.Text };
            string json = JsonConvert.SerializeObject(employeeData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost/myapi/phpapi/api.php", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var employee = JsonConvert.DeserializeObject<Employee>(responseBody);

                // add success message
                MessageBox.Show("Employee added successfully!");


                // refresh data
                employeeDataLoad();

                // clear textboxes
                txtName.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
                txtDept.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
