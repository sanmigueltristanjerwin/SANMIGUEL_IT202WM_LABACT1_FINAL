using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SANMIGUEL_IT202WM_LABACT1_FINAL
{
    public partial class Form1 : Form
    {
        private ParkingRecord activerecord;
        public Form1()
        {
            InitializeComponent();
        }
        public class ParkingRecord
        {
            public string PlateNumber { get; set; }
            public string VehicleType { get; set; }
            public int HoursParked { get; set; }
            public string AssignedSlot { get; set; }

            private const double CarRate = 50.0;
            private const double MotorcycleRate = 30.0;
            private const double VanRate = 70.0;
            private const double ServiceCharge = 20.0;
            private const double OvertimeRate = 30.0;

            public ParkingRecord(string plate, string type, int hours, string slot)
            {
                PlateNumber = plate;
                VehicleType = type;
                HoursParked = hours;
                AssignedSlot = slot;
            }

            public double GetStandardFee()
            {
                double rate = 0;
                if (VehicleType == "Car") rate = CarRate;
                else if (VehicleType == "Motorcycle") rate = MotorcycleRate;
                else if (VehicleType == "Van") rate = VanRate;

                return rate * HoursParked;
            }

            public double GetOvertimeFee()
            {
                return (HoursParked > 8) ? (HoursParked - 8) * OvertimeRate : 0;
            }

            public double GetTotalAmount(double discountRate)
            {
                double subtotal = GetStandardFee() + GetOvertimeFee() + ServiceCharge;
                return subtotal * (1 - discountRate);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void registervehiclebutton_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    activerecord = new ParkingRecord(
                        platenumbertxt.Text,
                        vehicletypecombo.SelectedItem.ToString(),
                        int.Parse(houseparkedtxt.Text),
                        assignedslottxt.Text
                    );

                    platenumlabel.Text = activerecord.PlateNumber;
                    vehicleinfolabel.Text = activerecord.VehicleType;
                    durationlabel.Text = activerecord.HoursParked + " hrs";
                    assignedslottxt.Text = activerecord.AssignedSlot;
                    overtimefeelabel.Text = "P" + activerecord.GetOvertimeFee().ToString("N2");
                        
                    standardfeelabel.Text = "P" + activerecord.GetStandardFee().ToString("N2");
                    servicechargelabel.Text = "P20.00";
                    totallabel.Text = "P" + activerecord.GetTotalAmount(0).ToString("N2");
                }
                catch { MessageBox.Show("Please ensure all fields are filled correctly."); }
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void updatestatusbutton_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in parkingstatusgroupbox.Controls)
            {
                if (ctrl is Button btn && btn.Text == assignedslottxt.Text)
                {
                    btn.BackColor = Color.Red;
                    btn.ForeColor = Color.White;
                    btn.Text = platenumbertxt.Text;
                }
            }
        }

        private void processpaymentbutton_Click(object sender, EventArgs e)
        {
            if (activerecord == null) return;

            double discount = 0;
            if (discountcombo.Text == "Senior") discount = 0.20;
            else if (discountcombo.Text == "Employee") discount = 0.10;

            double finalTotal = activerecord.GetTotalAmount(discount);
            double payment = double.Parse(paymentamounttxt.Text);

            changelabel.Text = (payment - finalTotal).ToString("N2");
            totallabel.Text = "P" + finalTotal.ToString("N2");
        }
        private void generatereceiptbutton_Click(object sender, EventArgs e)
        {
            paymentandtransactionsrichtextbox.Clear();
            paymentandtransactionsrichtextbox.SelectionAlignment = HorizontalAlignment.Center;
            paymentandtransactionsrichtextbox.AppendText("Small Management Parking System\n");
            paymentandtransactionsrichtextbox.AppendText("\n");
            paymentandtransactionsrichtextbox.AppendText($"Plate: {activerecord.PlateNumber}\n");
            paymentandtransactionsrichtextbox.AppendText($"Type: {activerecord.VehicleType}\n");
            paymentandtransactionsrichtextbox.AppendText($"Slot: {activerecord.AssignedSlot}\n");
            paymentandtransactionsrichtextbox.AppendText($"Total Due: {totallabel.Text}\n");
            paymentandtransactionsrichtextbox.AppendText("\n");
            paymentandtransactionsrichtextbox.AppendText("Thank you!");
        }

        private void clearformbutton_Click(object sender, EventArgs e)
        {
            platenumbertxt.Clear();
            vehicletypecombo.Text = "";
            houseparkedtxt.Clear();
            assignedslottxt.Clear();

            platenumlabel.Text = "";
            vehicleinfolabel.Text = "";
            durationlabel.Text = "";
            assignedslottxt.Text = "";
            overtimefeelabel.Text = "";

            standardfeelabel.Text = "";
            servicechargelabel.Text = "";
            totallabel.Text = "";

            discountcombo.Text = "";
            paymentamounttxt.Text = "";
            changelabel.Text = "";
            paymentandtransactionsrichtextbox.Clear();
        }
    }
}
