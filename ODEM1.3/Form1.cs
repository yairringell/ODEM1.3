using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACS.SPiiPlusNET;

namespace ODEM1._3
{
    public partial class Form1 : Form
    {
        private Api _ACS;
        private MotorStates Axis1_MST; private MotorStates Axis2_MST;
        private double Axis1_FVEL, Axis1_FPOS, Axis2_FVEL, Axis2_FPOS;
        int Axis1 = 0, Axis2 = 1, Axis3 = 2, cornerX = 160, cornerZ = 150;
        int machineLength,machineHeight,bathLength,bathHeight,bathGap ;

        private void btnManual_Click(object sender, EventArgs e)
        {
           if (Manual) { Manual = false;grpJog.Enabled = false; btnManual.BackColor = Color.Gray; btnXaxis.Enabled = false; btnZaxis.Enabled = false; } 
            else { Manual = true; grpJog.Enabled = true; btnManual.BackColor = Color.LightGreen; btnXaxis.Enabled = true; btnZaxis.Enabled = true; };
        }

        

        double EFAC = 0.006;

       

        bool Homed = false,Manual=false;
        public Form1()
        {
            InitializeComponent();
            _ACS = new Api();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            machineLength = this.Size.Width - 300;
            machineHeight = machineLength / 5;
            bathGap = machineLength / 15;
            bathLength = machineLength / 6;// - bathGap * 2;
            grpMain.Size = new Size(this.Size.Width, this.Size.Height-100);
            grpMain.Location = new Point(1, 90);
            grpProg.Size = new Size(this.Size.Width, this.Size.Height - 100);
            grpProg.Location = new Point(1, 90);
            btnXaxis.BackColor = Color.Gray;
            btnZaxis.BackColor = Color.Gray;
            btnXaxis.Enabled=false;
            btnZaxis.Enabled=false;
            btnConnect.BackColor = Color.Gray;
            grpProg.Visible = false;
            grpMain.Visible = false;
            btnHome.Enabled = false;
            grpJog.Enabled = false;
            btnXaxis.Location = new Point(cornerX, cornerZ);
            btnXaxis.Size = new Size(machineLength, 30);
            btnZaxis.Size = new Size(30, machineHeight);

            btnBath1.Size = new Size(bathLength, machineHeight/2);
            btnBath1.Location= new Point(cornerX, cornerZ+machineHeight);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (btnConnect.BackColor == Color.Gray)
            {
                try
                {
                    _ACS.OpenCommEthernetTCP(
                                txtIP.Text,                             // IP Address (Default : 10.0.0.100)
                                701   // TCP/IP Port nubmer (default : 701)
                                );
                    btnConnect.BackColor = Color.LightGreen;
                    btnHome.Enabled = true;



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        private void btnZaxis_Click(object sender, EventArgs e)
        {
            if (btnZaxis.BackColor == Color.Gray)
            {
                try
                {
                    _ACS.Enable((Axis)Convert.ToInt32(Axis2));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                _ACS.Disable((Axis)Convert.ToInt32(Axis2));
            }
        }
        private void btnXaxis_Click(object sender, EventArgs e)
        {
            if (btnXaxis.BackColor == Color.Gray)
            {
                try
                {
                    _ACS.Enable((Axis)Convert.ToInt32(Axis1));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                _ACS.Disable((Axis)Convert.ToInt32(Axis1));
            }
        }
        private void btnMain_Click(object sender, EventArgs e)
        {
            grpProg.Visible = false;
            grpMain.Visible = true;
        }

        private void btnProg_Click(object sender, EventArgs e)
        {
            grpProg.Visible = true;
            grpMain.Visible = false;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.LightGreen;
            Homed = true;
            grpMain.Visible = true;
        }
        //==================================================================================================
        //============================================   JOG   =============================================
        //==================================================================================================
        private void btnJogXneg_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                _ACS.Jog(
                        MotionFlags.ACSC_AMF_VELOCITY,      // Velocity flag
                        (Axis)Convert.ToInt32(Axis1),  // Axis number
                        -1 * _ACS.GetVelocity((Axis)Convert.ToInt32(Axis1))                  // Velocity
                        );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void btnJogXneg_MouseUp(object sender, MouseEventArgs e)
        {

            _ACS.Halt((Axis)Convert.ToInt32(Axis1));
        }
        private void btnJogXpos_MouseDown(object sender, MouseEventArgs e)
        {


            try
            {
                _ACS.Jog(
                       MotionFlags.ACSC_AMF_VELOCITY,      // Velocity flag
                       (Axis)Convert.ToInt32(Axis1),  // Axis number
                       _ACS.GetVelocity((Axis)Convert.ToInt32(Axis1))                  // Velocity
                       );

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        // Stop JOG motion
        private void btnJogXpos_MouseUp(object sender, MouseEventArgs e)
        {
            _ACS.Halt((Axis)Convert.ToInt32(Axis1));
        }
        //===================================================== JOG Z ======================================

        private void btnJogXup_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                _ACS.Jog(
                        MotionFlags.ACSC_AMF_VELOCITY,      // Velocity flag
                        (Axis)Convert.ToInt32(Axis2),  // Axis number
                        -1 * _ACS.GetVelocity((Axis)Convert.ToInt32(Axis2))                  // Velocity
                        );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void btnJogXup_MouseUp(object sender, MouseEventArgs e)
        {

            _ACS.Halt((Axis)Convert.ToInt32(Axis2));
        }
        private void btnJogXdown_MouseDown(object sender, MouseEventArgs e)
        {


            try
            {
                _ACS.Jog(
                       MotionFlags.ACSC_AMF_VELOCITY,      // Velocity flag
                       (Axis)Convert.ToInt32(Axis2),  // Axis number
                       _ACS.GetVelocity((Axis)Convert.ToInt32(Axis2))                  // Velocity
                       );

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        // Stop JOG motion
        private void btnJogXdown_MouseUp(object sender, MouseEventArgs e)
        {
            _ACS.Halt((Axis)Convert.ToInt32(Axis2));
        }

        //==================================================================================================
        // ======================================   TIMER   =================================================
        //===================================================================================================
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (btnConnect.BackColor == Color.LightGreen)
            {
                Axis1_MST = _ACS.GetMotorState((Axis)Convert.ToInt32(Axis1));
                if ((Axis1_MST & MotorStates.ACSC_MST_ENABLE) != 0) { btnXaxis.BackColor = Color.LightGreen; } else { btnXaxis.BackColor = Color.Gray;  }
                Axis2_MST = _ACS.GetMotorState((Axis)Convert.ToInt32(Axis2));
                if ((Axis2_MST & MotorStates.ACSC_MST_ENABLE) != 0) { btnZaxis.BackColor = Color.LightGreen; } else { btnZaxis.BackColor = Color.Gray; }

                Axis1_FVEL = EFAC * (double)_ACS.ReadVariable("FVEL", ProgramBuffer.ACSC_NONE, Convert.ToInt32(Axis1), Convert.ToInt32(Axis1));
                Axis1_FPOS = (double)_ACS.ReadVariable("FPOS", ProgramBuffer.ACSC_NONE, Convert.ToInt32(Axis1), Convert.ToInt32(Axis1));
                lblPosX.Text = String.Format("{0:0.0}", Axis1_FPOS);
                Axis2_FVEL = EFAC * (double)_ACS.ReadVariable("FVEL", ProgramBuffer.ACSC_NONE, Convert.ToInt32(Axis2), Convert.ToInt32(Axis2));
                Axis2_FPOS = (double)_ACS.ReadVariable("FPOS", ProgramBuffer.ACSC_NONE, Convert.ToInt32(Axis2), Convert.ToInt32(Axis2));
                lblPosZ.Text = String.Format("{0:0.0}", Axis2_FPOS);
                


                if (Homed)
                {
                    btnZaxis.Location = new Point(cornerX+ Convert.ToInt32( Axis1_FPOS), cornerZ + Convert.ToInt32(Axis2_FPOS)-machineHeight +30);  
                }
            }
        }


    }
}
