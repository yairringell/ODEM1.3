﻿using System;
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
        int[,] times;
        int machineLength, machineHeight, bathLength, bathHeight, bathGap;
        int bathOn1, bathOn2, bathOn3, bathOn4, pistonOn1, pistonOn2, pistonOn3, pistonOn4;
        double xRatio, zRatio, xPos, zPos;
        int screenX, screenZ;
        bool Homed = false, Manual = false;

        

        public Form1()
        {
            InitializeComponent();
            _ACS = new Api();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            machineLength = this.Size.Width - 300;
            xRatio = Convert.ToDouble(machineLength) /3000;
            lblRatioX.Text= Convert.ToString(xRatio);
            machineHeight = machineLength / 5;
            bathGap = 10;//machineLength / 20;
            bathLength = machineLength / 6 - (bathGap );
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
            //grpMain.Visible = false;
            btnHome.Enabled = false;
            grpJog.Enabled = false;
            

            buttons();
        }
        private void btnPiston1_Click(object sender, EventArgs e)
        {
            if (pistonOn1 == 0)
            {
                pistonOn1 = 1;
                btnPiston1.BackColor = Color.LightBlue;
            }
            else
            {
                pistonOn1 = 0;
                btnPiston1.BackColor = Color.LightGray;
            }
        }
        private void btnPiston2_Click(object sender, EventArgs e)
        {
            if (pistonOn2 == 0)
            {
                pistonOn2 = 1;
                btnPiston2.BackColor = Color.LightBlue;
            }
            else
            {
                pistonOn2 = 0;
                btnPiston2.BackColor = Color.LightGray;
            }
        }

       

        private void btnPiston3_Click(object sender, EventArgs e)
        {
            if (pistonOn3 == 0)
            {
                pistonOn3 = 1;
                btnPiston3.BackColor = Color.LightBlue;
            }
            else
            {
                pistonOn3 = 0;
                btnPiston3.BackColor = Color.LightGray;
            }
        }

        

        private void btnPiston4_Click(object sender, EventArgs e)
        {
            if (pistonOn4 == 0)
            {
                pistonOn4 = 1;
                btnPiston4.BackColor = Color.LightBlue;
            }
            else
            {
                pistonOn4 = 0;
                btnPiston4.BackColor = Color.LightGray;
            }
        }
        private void btnGrab_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "GRAB_CMD");
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "NEXT_CMD");
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "PREV_CMD");
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "REL_CMD");
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "RUN_PROG");
        }
        private void btnSetSingle_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(Convert.ToInt16(txtBathT1.Text), "TIMER1");
            _ACS.WriteVariable(Convert.ToInt16(txtBathT2.Text), "TIMER2");
            _ACS.WriteVariable(Convert.ToInt16(txtBathT3.Text), "TIMER3");
            _ACS.WriteVariable(Convert.ToInt16(txtBathT4.Text), "TIMER4");
            txtBathT1.Text = Convert.ToString(_ACS.ReadVariable("TIMER1"));
            txtBathT2.Text = Convert.ToString(_ACS.ReadVariable("TIMER2"));
            txtBathT3.Text = Convert.ToString(_ACS.ReadVariable("TIMER3"));
            txtBathT4.Text = Convert.ToString(_ACS.ReadVariable("TIMER4"));
            _ACS.WriteVariable(1, "SET_SINGLE");
        }
        private void btnManual_Click(object sender, EventArgs e)
        {
            if (Manual) { Manual = false; grpJog.Enabled = false; btnManual.BackColor = Color.Gray; btnXaxis.Enabled = false; btnZaxis.Enabled = false; }
            else { Manual = true; grpJog.Enabled = true; btnManual.BackColor = Color.LightGreen; btnXaxis.Enabled = true; btnZaxis.Enabled = true; };
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
            _ACS.WriteVariable(1, "HOME_ALL_CMD");
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

                Axis1_FPOS = (double)_ACS.ReadVariable("FPOS", ProgramBuffer.ACSC_NONE, Convert.ToInt32(Axis1), Convert.ToInt32(Axis1));
                lblPosX.Text = String.Format("{0:0.0}", Axis1_FPOS);
                Axis2_FPOS = (double)_ACS.ReadVariable("FPOS", ProgramBuffer.ACSC_NONE, Convert.ToInt32(Axis2), Convert.ToInt32(Axis2));
                lblPosZ.Text = String.Format("{0:0.0}", Axis2_FPOS);

                xPos = Convert.ToDouble(Axis1_FPOS);
                zPos = Convert.ToDouble(Axis2_FPOS);
                screenX =Convert.ToInt32( xPos * xRatio);
                screenZ = Convert.ToInt32(zPos * xRatio);
                lblScreenX.Text = Convert.ToString(screenX);

                lblT1.Text = "Time Left:   " + Convert.ToString(Convert.ToInt32(_ACS.ReadVariable("T1"))) + "  [min]";
                lblT2.Text = "Time Left:   " +Convert.ToString(Convert.ToInt32(_ACS.ReadVariable("T2"))) + "  [min]";
                lblT3.Text = "Time Left:   " +Convert.ToString(Convert.ToInt32(_ACS.ReadVariable("T3"))) + "  [min]";
                lblT4.Text = "Time Left:   " + Convert.ToString(Convert.ToInt32(_ACS.ReadVariable("T4"))) + "  [min]";

                if (Homed)
                {
                    btnZaxis.Location = new Point(cornerX+ screenX-15, cornerZ + Convert.ToInt32(Axis2_FPOS)-machineHeight +30);
                    btnSensorZ.Location = new Point(cornerX + screenX-15, cornerZ );
                    btnHomeZ.Location = new Point(cornerX + screenX-15, cornerZ-(machineHeight/5));
                    btnZbasket.Location = new Point(cornerX + screenX-15, cornerZ + Convert.ToInt32(Axis2_FPOS) +30);
                }
            }
        }

        private void buttons()
        {

            btnXaxis.Location = new Point(cornerX, cornerZ);
            btnXaxis.Size = new Size(machineLength, 30);
            btnZaxis.Size = new Size(30, machineHeight);
            btnHomeZ.Size = new Size(30, 30);
            btnHomeX.Size = new Size(30, 30);
            btnHomeX.Location = new Point(cornerX, cornerZ+40);
            btnSensorZ.Location = new Point(cornerX, cornerZ);
            btnSensorZ.Size = new Size(30,30);
            btnZbasket.Size = new Size(30, 30);


            btnInput.Size = new Size(bathLength, machineHeight / 2);
            btnOutput.Size = new Size(bathLength, machineHeight / 2);
            btnBath1.Size = new Size(bathLength, machineHeight / 2);
            btnBath2.Size = new Size(bathLength, machineHeight / 2);
            btnBath3.Size = new Size(bathLength, machineHeight / 2);
            btnBath4.Size = new Size(bathLength, machineHeight / 2);
            btnInput.Location = new Point(cornerX, cornerZ + machineHeight);
            btnBath1.Location = new Point(cornerX+ 1*bathLength+1* bathGap, cornerZ + machineHeight);
            btnBath2.Location = new Point(cornerX + 2 * bathLength + 2 * bathGap, cornerZ + machineHeight);
            btnBath3.Location = new Point(cornerX + 3 * bathLength + 3 * bathGap, cornerZ + machineHeight);
            btnBath4.Location = new Point(cornerX + 4 * bathLength + 4 * bathGap, cornerZ + machineHeight);
            btnOutput.Location = new Point(cornerX + 5 * bathLength + 5 * bathGap, cornerZ + machineHeight);

            btnPiston1.Size = new Size(bathLength, machineHeight / 6 );
            btnPiston2.Size = new Size(bathLength, machineHeight / 6);
            btnPiston3.Size = new Size(bathLength, machineHeight / 6);
            btnPiston4.Size = new Size(bathLength, machineHeight / 6);

            btnPiston1.Location = new Point(cornerX + 1 * bathLength + 1 * bathGap, cornerZ + machineHeight+ (machineHeight / 2));
            btnPiston2.Location = new Point(cornerX + 2 * bathLength + 2 * bathGap, cornerZ + machineHeight + (machineHeight / 2));
            btnPiston3.Location = new Point(cornerX + 3 * bathLength + 3 * bathGap, cornerZ + machineHeight + (machineHeight / 2));
            btnPiston4.Location = new Point(cornerX + 4 * bathLength + 4 * bathGap, cornerZ + machineHeight + (machineHeight / 2));

            btnSensorIn.Size = new Size(bathLength/   7, machineHeight / 12);
            btnSensor1.Size = new   Size(bathLength / 7, machineHeight / 12);
            btnSensor2.Size = new   Size(bathLength / 7, machineHeight / 12);
            btnSensor3.Size = new   Size(bathLength / 7, machineHeight / 12);
            btnSensor4.Size = new   Size(bathLength / 7, machineHeight / 12);
            btnSensorOut.Size = new Size(bathLength / 7, machineHeight / 12);

            btnSensorIn.Location = new Point(cornerX, cornerZ + machineHeight);
            btnSensor1.Location = new Point(cornerX + 1 * bathLength + 1 * bathGap, cornerZ + machineHeight);
            btnSensor2.Location = new Point(cornerX + 2 * bathLength + 2 * bathGap, cornerZ + machineHeight);
            btnSensor3.Location = new Point(cornerX + 3 * bathLength + 3 * bathGap, cornerZ + machineHeight);
            btnSensor4.Location = new Point(cornerX + 4 * bathLength + 4 * bathGap, cornerZ + machineHeight);
            btnSensorOut.Location = new Point(cornerX + 5 * bathLength + 5 * bathGap, cornerZ + machineHeight);
           
            lblT1.Location= new Point(cornerX + 1 * bathLength + 1 * bathGap+5, cornerZ + machineHeight+90);
            lblT2.Location = new Point(cornerX + 2 * bathLength + 2 * bathGap+5, cornerZ + machineHeight + 90);
            lblT3.Location = new Point(cornerX + 3 * bathLength + 3 * bathGap+5, cornerZ + machineHeight + 90);
            lblT4.Location = new Point(cornerX + 4 * bathLength + 4 * bathGap + 5, cornerZ + machineHeight + 90);

            btnPiston1.BackColor = Color.LightGray;
            btnPiston2.BackColor = Color.LightGray; 
            btnPiston3.BackColor = Color.LightGray;
            btnPiston4.BackColor = Color.LightGray;
        }
    }
}
