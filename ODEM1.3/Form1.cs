﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        double[,] single = new double[10,10];
        double[,] multi = new double[10, 50];

        int machineLength, machineHeight, bathLength, bathHeight, bathGap,Height1,Height2,Height3,Height4;
        int bathOn1, bathOn2, bathOn3, bathOn4, pistonOn1, pistonOn2, pistonOn3, pistonOn4;
        double xRatio, zRatio, xPos, zPos, single_prog_num, multi_prog_num;
        int screenX, screenZ;
        object resault,resault_multi;
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
            xRatio = Convert.ToDouble(machineLength) / 3825000; //3295000;
            //lblRatioX.Text= Convert.ToString(xRatio);
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
            connection();

        }
        private void connection()
        {
            while (btnConnect.BackColor != Color.LightGreen)
            {
                try
                {
                    _ACS.OpenCommEthernetTCP("192.168.120.17",
                               // txtIP.Text,                             // IP Address (Default : 10.0.0.100)
                                701   // TCP/IP Port nubmer (default : 701)
                                );
                    btnConnect.BackColor = Color.LightGreen;
                    btnHome.Enabled = true;
                    read_single_prog();
                    read_multi_prog();


                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        private void btnPiston1_Click(object sender, EventArgs e)
        {
            if (pistonOn1 == 0)
            {
                pistonOn1 = 1;
                btnPiston1.BackColor = Color.LightBlue;
                _ACS.WriteVariable(1, "PISTONS1_CMD");
            }
            else
            {
                pistonOn1 = 0;
                btnPiston1.BackColor = Color.LightGray;
                _ACS.WriteVariable(0, "PISTONS1_CMD");
            }
        }
        private void btnPiston2_Click(object sender, EventArgs e)
        {
            if (pistonOn2 == 0)
            {
                pistonOn2 = 1;
                btnPiston2.BackColor = Color.LightBlue;
                _ACS.WriteVariable(1, "PISTONS2_CMD");
            }
            else
            {
                pistonOn2 = 0;
                btnPiston2.BackColor = Color.LightGray;
                _ACS.WriteVariable(0, "PISTONS2_CMD");
            }
        }
        private void btnPiston3_Click(object sender, EventArgs e)
        {
            if (pistonOn3 == 0)
            {
                pistonOn3 = 1;
                btnPiston3.BackColor = Color.LightBlue;
                _ACS.WriteVariable(1, "PISTONS3_CMD");
            }
            else
            {
                pistonOn3 = 0;
                btnPiston3.BackColor = Color.LightGray;
                _ACS.WriteVariable(0, "PISTONS3_CMD");
            }
        }
        private void btnProg1_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "SINGLE_PROG_NUM");
            read_single_prog();
        }
       
        private void btnProg2_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(2, "SINGLE_PROG_NUM");
            read_single_prog();
        }
        private void btnProg3_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(3, "SINGLE_PROG_NUM");
            read_single_prog();
        }
        private void btnProg4_Click_1(object sender, EventArgs e)
        {
            _ACS.WriteVariable(4, "SINGLE_PROG_NUM");
            read_single_prog();
        }
        private void btnProg5_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(5, "SINGLE_PROG_NUM");
            read_single_prog();
        }

        private void btnProg6_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(6, "SINGLE_PROG_NUM");
            read_single_prog();
        }
        private void btnPiston4_Click(object sender, EventArgs e)
        {
            if (pistonOn4 == 0)
            {
                pistonOn4 = 1;
                btnPiston4.BackColor = Color.LightBlue;
                _ACS.WriteVariable(1, "PISTONS4_CMD");
            }
            else
            {
                pistonOn4 = 0;
                btnPiston4.BackColor = Color.LightGray;
                _ACS.WriteVariable(0, "PISTONS4_CMD");
            }
        }
        private void btnProg21_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "MULTI_PROG_NUM");
            read_multi_prog();
        }
        private void btnProg22_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(2, "MULTI_PROG_NUM");
            read_multi_prog();
        }
        private void btnProg23_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(3, "MULTI_PROG_NUM");
            read_multi_prog();
        }
        private void bntProg41_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(4, "MULTI_PROG_NUM");
            read_multi_prog();
        }

        private void btnProg51_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(5, "MULTI_PROG_NUM");
            read_multi_prog();
        }

        private void btnProg61_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(6, "MULTI_PROG_NUM");
            read_multi_prog();
        }
        private void btnStart_multi_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "RUN_MULTI");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "STOP_PROG");
        }
        private void btnTap1_Click(object sender, EventArgs e)
        {
            if (btnTap1.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP1");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP1");
            }
        }
        private void btnTap2_Click(object sender, EventArgs e)
        {
            if (btnTap2.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP2");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP2");
                MessageBox.Show("TURN OFF HEATER 1 !!!!!!!!!!!!!  כבה מחמם אמבטיה 1");
            }
        }
        private void btnTap3_Click(object sender, EventArgs e)
        {
            if (btnTap3.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP3");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP3");
                MessageBox.Show("TURN OFF HEATER 2 !!!!!!!!!!!!!   כבה מחמם אמבטיה 2");
            }
        }
        private void btnTap4_Click(object sender, EventArgs e)
        {
            if (btnTap4.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP4");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP4");
            }
        }
        private void btnTap5_Click(object sender, EventArgs e)
        {
            if (btnTap5.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP5");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP5");
                MessageBox.Show("TURN OFF HEATER 3 !!!!!!!!!!!!!   כבה מחמם אמבטיה 3");
            }
        }

        private void btnUS1_Click(object sender, EventArgs e)
        {
            if (btnUS1.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "US1");
            }
            else
            {
                _ACS.WriteVariable(1, "US1 ");
            }
        }

        private void US2_Click(object sender, EventArgs e)
        {
            if (btnUS2.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "US2");
            }
            else
            {
                _ACS.WriteVariable(1, "US2");
            }
        }

        private void btnJet_Click(object sender, EventArgs e)
        {

            if (btnJet.BackColor == Color.Lime)
            {
                _ACS.WriteVariable(0, "START_JET");
            }
            else
            {
                _ACS.WriteVariable(1, "START_JET");
            }
        }

        private void btnFoam_Click(object sender, EventArgs e)
        {
            if (btnFoam.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "FOAM");
            }
            else
            {
                _ACS.WriteVariable(1, "FOAM");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnAir.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "bAIR1");
            }
            else
            {
                _ACS.WriteVariable(1, "bAIR1");
            }
        }

        private void btnTap6_Click(object sender, EventArgs e)
        {
            if (btnTap6.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP6");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP6");
            }
        }

       

        private void btnTap7_Click(object sender, EventArgs e)
        {
            if (btnTap7.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP7");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP7");
            }
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "SINGLE_PROG_NUM");
            read_single_prog();
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(2, "SINGLE_PROG_NUM");
            read_single_prog();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(3, "SINGLE_PROG_NUM");
            read_single_prog();
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(4, "SINGLE_PROG_NUM");
            read_single_prog();
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(5, "SINGLE_PROG_NUM");
            read_single_prog();
        }

        private void btnF_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(6, "SINGLE_PROG_NUM");
            read_single_prog();
        }

        private void btnA1_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(1, "MULTI_PROG_NUM");
            read_multi_prog();
        }

        private void btnB1_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(2, "MULTI_PROG_NUM");
            read_multi_prog();
        }

        private void btnC1_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(3, "MULTI_PROG_NUM");
            read_multi_prog();
        }

        private void btnD1_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(4, "MULTI_PROG_NUM");
            read_multi_prog();
        }

        private void btnE1_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(5, "MULTI_PROG_NUM");
            read_multi_prog();
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            _ACS.WriteVariable(6, "MULTI_PROG_NUM");
            read_multi_prog();
        }

        private void btnTap8_Click(object sender, EventArgs e)
        {
            if (btnTap8.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP8");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP8");
                MessageBox.Show("TURN OFF HEATER 4 !!!!!!!!!!!!!   כבה מחמם אמבטיה 4");
            }
        }
        private void btnTap9_Click(object sender, EventArgs e)
        {
            if (btnTap9.BackColor == Color.Gold)
            {
                _ACS.WriteVariable(0, "TAP9");
            }
            else
            {
                _ACS.WriteVariable(1, "TAP9");
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
            _ACS.WriteVariable(1, "RUN_SINGLE");
        }
        private void btnSetMulti_Click(object sender, EventArgs e)
        {
            set_multi();
        }
        private void set_multi()
        {
            multi[((int)multi_prog_num), 1] = Convert.ToInt16(txtBathT11.Text);
            multi[((int)multi_prog_num), 2] = Convert.ToInt16(txtBathT21.Text);
            multi[((int)multi_prog_num), 3] = Convert.ToInt16(txtBathT31.Text);
            multi[((int)multi_prog_num), 4] = Convert.ToInt16(txtBathT41.Text);
            if (chk21Air.Checked) { multi[((int)multi_prog_num), 5] = 1; } else { multi[((int)multi_prog_num), 5] = 0; }
            if (chkPist11.Checked) { multi[((int)multi_prog_num), 6] = 1; } else { multi[((int)multi_prog_num), 6] = 0; }
            if (chkPist21.Checked) { multi[((int)multi_prog_num), 7] = 1; } else { multi[((int)multi_prog_num), 7] = 0; }
            if (chkPist31.Checked) { multi[((int)multi_prog_num), 8] = 1; } else { multi[((int)multi_prog_num), 8] = 0; }
            if (chkPist41.Checked) { multi[((int)multi_prog_num), 9] = 1; } else { multi[((int)multi_prog_num), 9] = 0; }
            if (chckJ1.Checked) { multi[((int)multi_prog_num), 0] = 1; } else { multi[((int)multi_prog_num), 0] = 0; }

            multi[((int)multi_prog_num), 11] = Convert.ToInt16(txtBathT12.Text);
            multi[((int)multi_prog_num), 12] = Convert.ToInt16(txtBathT22.Text);
            multi[((int)multi_prog_num), 13] = Convert.ToInt16(txtBathT32.Text);
            multi[((int)multi_prog_num), 14] = Convert.ToInt16(txtBathT42.Text);
            if (chk22Air.Checked) { multi[((int)multi_prog_num), 15] = 1; } else { multi[((int)multi_prog_num), 15] = 0; }
            if (chkPist12.Checked) { multi[((int)multi_prog_num), 16] = 1; } else { multi[((int)multi_prog_num), 16] = 0; }
            if (chkPist22.Checked) { multi[((int)multi_prog_num), 17] = 1; } else { multi[((int)multi_prog_num), 17] = 0; }
            if (chkPist32.Checked) { multi[((int)multi_prog_num), 18] = 1; } else { multi[((int)multi_prog_num), 18] = 0; }
            if (chkPist42.Checked) { multi[((int)multi_prog_num), 19] = 1; } else { multi[((int)multi_prog_num), 19] = 0; }
            if (chckJ2.Checked) { multi[((int)multi_prog_num), 10] = 1; } else { multi[((int)multi_prog_num), 10] = 0; }

            multi[((int)multi_prog_num), 21] = Convert.ToInt16(txtBathT13.Text);
            multi[((int)multi_prog_num), 22] = Convert.ToInt16(txtBathT23.Text);
            multi[((int)multi_prog_num), 23] = Convert.ToInt16(txtBathT33.Text);
            multi[((int)multi_prog_num), 24] = Convert.ToInt16(txtBathT43.Text);
            if (chk23Air.Checked) { multi[((int)multi_prog_num), 25] = 1; } else { multi[((int)multi_prog_num), 25] = 0; }
            if (chkPist13.Checked) { multi[((int)multi_prog_num), 26] = 1; } else { multi[((int)multi_prog_num), 26] = 0; }
            if (chkPist23.Checked) { multi[((int)multi_prog_num), 27] = 1; } else { multi[((int)multi_prog_num), 27] = 0; }
            if (chkPist33.Checked) { multi[((int)multi_prog_num), 28] = 1; } else { multi[((int)multi_prog_num), 28] = 0; }
            if (chkPist43.Checked) { multi[((int)multi_prog_num), 29] = 1; } else { multi[((int)multi_prog_num), 29] = 0; }
            if (chckJ3.Checked) { multi[((int)multi_prog_num), 20] = 1; } else { multi[((int)multi_prog_num), 20] = 0; }

            multi[((int)multi_prog_num), 31] = Convert.ToInt16(txtBathT14.Text);
            multi[((int)multi_prog_num), 32] = Convert.ToInt16(txtBathT24.Text);
            multi[((int)multi_prog_num), 33] = Convert.ToInt16(txtBathT34.Text);
            multi[((int)multi_prog_num), 34] = Convert.ToInt16(txtBathT44.Text);
            if (chk24Air.Checked) { multi[((int)multi_prog_num), 35] = 1; } else { multi[((int)multi_prog_num), 35] = 0; }
            if (chkPist14.Checked) { multi[((int)multi_prog_num), 36] = 1; } else { multi[((int)multi_prog_num), 36] = 0; }
            if (chkPist24.Checked) { multi[((int)multi_prog_num), 37] = 1; } else { multi[((int)multi_prog_num), 37] = 0; }
            if (chkPist34.Checked) { multi[((int)multi_prog_num), 38] = 1; } else { multi[((int)multi_prog_num), 38] = 0; }
            if (chkPist44.Checked) { multi[((int)multi_prog_num), 39] = 1; } else { multi[((int)multi_prog_num), 39] = 0; }
            if (chckJ4.Checked) { multi[((int)multi_prog_num), 30] = 1; } else { multi[((int)multi_prog_num), 30] = 0; }

            _ACS.WriteVariable(multi_prog_num, "MULTI_PROG_NUM");
            _ACS.WriteVariable(multi, "MULTI");
            _ACS.WriteVariable(1, "SET_MULTI");
            read_multi_prog();
        }
        private void btnSetSingle_Click(object sender, EventArgs e)
        {
             single[((int)single_prog_num), 1] = Convert.ToInt16(txtBathT1.Text);
            single[((int)single_prog_num), 2] = Convert.ToInt16(txtBathT2.Text);
            single[((int)single_prog_num), 3] = Convert.ToInt16(txtBathT3.Text);
            single[((int)single_prog_num), 4] = Convert.ToInt16(txtBathT4.Text);
            if (chk1Air.Checked) { single[((int)single_prog_num), 5] = 1; } else { single[((int)single_prog_num), 5] = 0; }
            if (chkPist1.Checked) { single[((int)single_prog_num), 6] = 1; } else { single[((int)single_prog_num), 6] = 0; }
            if (chkPist2.Checked) { single[((int)single_prog_num), 7] = 1; } else { single[((int)single_prog_num), 7] = 0; }
            if (chkPist3.Checked) { single[((int)single_prog_num), 8] = 1; } else { single[((int)single_prog_num), 8] = 0; }
            if (chkPist4.Checked) { single[((int)single_prog_num), 9] = 1; } else { single[((int)single_prog_num), 9] = 0; }
            if (chckJ.Checked) { single[((int)single_prog_num), 0] = 1; } else { single[((int)single_prog_num),0] = 0; }
             
            _ACS.WriteVariable(single_prog_num, "SINGLE_PROG_NUM");
            _ACS.WriteVariable(single, "SINGLE");
            _ACS.WriteVariable(1, "SET_SINGLE");
            read_single_prog();

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
                while (btnConnect.BackColor != Color.LightGreen)
                { 
                    try
                    {
                        _ACS.OpenCommEthernetTCP(
                                    txtIP.Text,                             // IP Address (Default : 10.0.0.100)
                                    701   // TCP/IP Port nubmer (default : 701)
                                    );
                        btnConnect.BackColor = Color.LightGreen;
                        btnHome.Enabled = true;
                        read_single_prog();
                        read_multi_prog();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
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
                //lblPosX.Text = String.Format("{0:0.0}", Axis1_FPOS);
                Axis2_FPOS = (double)_ACS.ReadVariable("FPOS", ProgramBuffer.ACSC_NONE, Convert.ToInt32(Axis2), Convert.ToInt32(Axis2));
                //lblPosZ.Text = String.Format("{0:0.0}", Axis2_FPOS);

                xPos = Convert.ToDouble(Axis1_FPOS);
                zPos = Convert.ToDouble(Axis2_FPOS);
                screenX =Convert.ToInt32( xPos * xRatio) + bathLength / 2;
                screenZ = -Convert.ToInt32(zPos * xRatio);
                //lblScreenX.Text = Convert.ToString(screenX);
                if (Convert.ToDouble(_ACS.ReadVariable("BUSY"))==1)
                {
                    grpActions.Enabled = false;
                }
                else
                {
                    grpActions.Enabled = true;
                }
                TimeSpan time = TimeSpan.FromSeconds(Convert.ToDouble(_ACS.ReadVariable("T1"))*60);
                lblT1.Text = "Time Left:   " + string.Format("{0:D2}:{1:D2}", (int)time.TotalMinutes, time.Seconds)  ;
                time = TimeSpan.FromSeconds(Convert.ToDouble(_ACS.ReadVariable("T2")) * 60);
                lblT2.Text = "Time Left:   " + string.Format("{0:D2}:{1:D2}", (int)time.TotalMinutes, time.Seconds);
                time = TimeSpan.FromSeconds(Convert.ToDouble(_ACS.ReadVariable("T3")) * 60);
                lblT3.Text = "Time Left:   " + string.Format("{0:D2}:{1:D2}", (int)time.TotalMinutes, time.Seconds);
                time = TimeSpan.FromSeconds(Convert.ToDouble(_ACS.ReadVariable("T4")) * 60);
                lblT4.Text = "Time Left:   " + string.Format("{0:D2}:{1:D2}", (int)time.TotalMinutes, time.Seconds);

                if (Convert.ToDouble(_ACS.ReadVariable("PISTONS1"))==1) { btnPiston1.BackColor = Color.LightBlue; } else { btnPiston1.BackColor = Color.LightGray; }
                if (Convert.ToDouble(_ACS.ReadVariable("PISTONS2")) == 1) { btnPiston2.BackColor = Color.LightBlue; } else { btnPiston2.BackColor = Color.LightGray; }
                if (Convert.ToDouble(_ACS.ReadVariable("PISTONS3")) == 1) { btnPiston3.BackColor = Color.LightBlue; } else { btnPiston3.BackColor = Color.LightGray; }
                if (Convert.ToDouble(_ACS.ReadVariable("PISTONS4")) == 1) { btnPiston4.BackColor = Color.LightBlue; } else { btnPiston4.BackColor = Color.LightGray; }
                if (true)
                {
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN1")) == 1) { btnSensor1.BackColor = Color.LightBlue; } else { btnSensor1.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN2")) == 1) { btnSensor2.BackColor = Color.LightBlue; } else { btnSensor2.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN3")) == 1) { btnSensor3.BackColor = Color.LightBlue; } else { btnSensor3.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN4")) == 1) { btnSensor4.BackColor = Color.LightBlue; } else { btnSensor4.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN_IN")) == 1) { btnSensorIn.BackColor = Color.LightBlue; } else { btnSensorIn.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN_OUT")) == 1) { btnSensorOut.BackColor = Color.LightBlue; } else { btnSensorOut.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN_HOME_X")) == 1) { btnHomeX.BackColor = Color.LightBlue; } else { btnHomeX.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN_HOME_Z")) == 1) { btnHomeZ.BackColor = Color.LightBlue; } else { btnHomeZ.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN_STATION")) == 1) { btnSensorZ.BackColor = Color.LightBlue; } else { btnSensorZ.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("SEN_BSKT")) == 1) { btnZbasket.BackColor = Color.LightBlue; } else { btnZbasket.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("sCAGE_IN_Z")) == 1) { btnZbasket.BackColor = Color.LightBlue; } else { btnZbasket.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("sFORK_S")) == 1) { btnSensorZ.BackColor = Color.LightBlue; } else { btnSensorZ.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("STATE")) < 9000) { grpMain.BackColor = Color.AliceBlue; } else { grpMain.BackColor = Color.Pink; }
                    if (Convert.ToDouble(_ACS.ReadVariable("sCAGE_IN1")) == 1) { btnSensor1.BackColor = Color.LightBlue; } else { btnSensor1.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("sCAGE_IN2")) == 1) { btnSensor2.BackColor = Color.LightBlue; } else { btnSensor2.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("sCAGE_IN3")) == 1) { btnSensor3.BackColor = Color.LightBlue; } else { btnSensor3.BackColor = Color.LightGray; }
                    if (Convert.ToDouble(_ACS.ReadVariable("sCAGE_IN4")) == 1) { btnSensor4.BackColor = Color.LightBlue; } else { btnSensor4.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP1")) == 1) { btnTap1.BackColor = Color.Gold; } else { btnTap1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP1")) == 1) { btnTap1W1.BackColor = Color.Blue; } else { btnTap1W1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP1")) == 1) { btnTap1W2.BackColor = Color.Blue; } else { btnTap1W2.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP2")) == 1) { btnTap2.BackColor = Color.Gold; } else { btnTap2.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP2")) == 1) { btnTap2W1.BackColor = Color.Blue; } else { btnTap2W1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP3")) == 1) { btnTap3.BackColor = Color.Gold; } else { btnTap3.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP3")) == 1) { btnTap3W1.BackColor = Color.Blue; } else { btnTap3W1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP4")) == 1) { btnTap4.BackColor = Color.Gold; } else { btnTap4.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP4")) == 1) { btnTap4W1.BackColor = Color.Blue; } else { btnTap4W1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP4")) == 1) { btnTap4W2.BackColor = Color.Blue; } else { btnTap4W2.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP5")) == 1) { btnTap5.BackColor = Color.Gold; } else { btnTap5.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP5")) == 1) { btnTap5W1.BackColor = Color.Blue; } else { btnTap5W1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP6")) == 1) { btnTap6.BackColor = Color.Gold; } else { btnTap6.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP7")) == 1) { btnTap7.BackColor = Color.Gold; } else { btnTap7.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP7")) == 1) { btnTap7W1.BackColor = Color.Blue; } else { btnTap7W1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP7")) == 1) { btnTap7W2.BackColor = Color.Blue; } else { btnTap7W2.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP8")) == 1) { btnTap8.BackColor = Color.Gold; } else { btnTap8.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP8")) == 1) { btnTap8W1.BackColor = Color.Blue; } else { btnTap8W1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP9")) == 1) { btnTap9.BackColor = Color.Gold; } else { btnTap9.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP9")) == 1) { btnTap9W1.BackColor = Color.Blue; } else { btnTap9W1.BackColor = Color.LightGray; }
                    if (Convert.ToInt16(_ACS.ReadVariable("TAP9")) == 1) { btnTap9W2.BackColor = Color.Blue; } else { btnTap9W2.BackColor = Color.LightGray; }
                    }// TAPS
                if (Convert.ToInt16(_ACS.ReadVariable("US1")) == 1) { btnUS1.BackColor = Color.Gold; } else { btnUS1.BackColor = Color.LightGray; }
                if (Convert.ToInt16(_ACS.ReadVariable("US2")) == 1) { btnUS2.BackColor = Color.Gold; } else { btnUS2.BackColor = Color.LightGray; }
                if (Convert.ToInt16(_ACS.ReadVariable("FOAM")) == 1) { btnFoam.BackColor = Color.Gold; } else { btnFoam.BackColor = Color.LightGray; }
                if (Convert.ToInt16(_ACS.ReadVariable("bAIR1")) == 1) { btnAir.BackColor = Color.Gold; } else { btnAir.BackColor = Color.LightGray; }

                if (Convert.ToInt16(_ACS.ReadVariable("START_JET")) == 1) { btnJet.BackColor = Color.Lime; } else { btnJet.BackColor = Color.LightGray; }

                single_prog_num = Convert.ToDouble(_ACS.ReadVariable("SINGLE_PROG_NUM"));
                if (single_prog_num == 1) { btnProg1.BackColor = Color.LightBlue; }else { btnProg1.BackColor = Color.LightGray; }
                if (single_prog_num == 2) { btnProg2.BackColor = Color.LightBlue; } else { btnProg2.BackColor = Color.LightGray; }
                if (single_prog_num == 3) { btnProg3.BackColor = Color.LightBlue; } else { btnProg3.BackColor = Color.LightGray; }
                if (single_prog_num == 4) { btnProg4.BackColor = Color.LightBlue; } else { btnProg4.BackColor = Color.LightGray; }
                if (single_prog_num == 5) { btnProg5.BackColor = Color.LightBlue; } else { btnProg5.BackColor = Color.LightGray; }
                if (single_prog_num == 6) { btnProg6.BackColor = Color.LightBlue; } else { btnProg6.BackColor = Color.LightGray; }
                if (single_prog_num == 1) { btnA.BackColor = Color.LightBlue; } else { btnA.BackColor = Color.LightGray; }
                if (single_prog_num == 2) { btnB.BackColor = Color.LightBlue; } else { btnB.BackColor = Color.LightGray; }
                if (single_prog_num == 3) { btnC.BackColor = Color.LightBlue; } else { btnC.BackColor = Color.LightGray; }
                if (single_prog_num == 4) { btnD.BackColor = Color.LightBlue; } else { btnD.BackColor = Color.LightGray; }
                if (single_prog_num == 5) { btnE.BackColor = Color.LightBlue; } else { btnE.BackColor = Color.LightGray; }
                if (single_prog_num == 6) { btnF.BackColor = Color.LightBlue; } else { btnF.BackColor = Color.LightGray; }
                multi_prog_num = Convert.ToDouble(_ACS.ReadVariable("MULTI_PROG_NUM"));
                if (multi_prog_num == 1) { btnProg21.BackColor = Color.LightBlue; } else { btnProg21.BackColor = Color.LightGray; }
                if (multi_prog_num == 2) { btnProg22.BackColor = Color.LightBlue; } else { btnProg22.BackColor = Color.LightGray; }
                if (multi_prog_num == 3) { btnProg23.BackColor = Color.LightBlue; } else { btnProg23.BackColor = Color.LightGray; }
                if (multi_prog_num == 4) { btnProg24.BackColor = Color.LightBlue; } else { btnProg24.BackColor = Color.LightGray; }
                if (multi_prog_num == 5) { btnProg25.BackColor = Color.LightBlue; } else { btnProg25.BackColor = Color.LightGray; }
                if (multi_prog_num == 6) { btnProg26.BackColor = Color.LightBlue; } else { btnProg26.BackColor = Color.LightGray; }
                if (multi_prog_num == 1) { btnA1.BackColor = Color.LightBlue; } else { btnA1.BackColor = Color.LightGray; }
                if (multi_prog_num == 2) { btnB1.BackColor = Color.LightBlue; } else { btnB1.BackColor = Color.LightGray; }
                if (multi_prog_num == 3) { btnC1.BackColor = Color.LightBlue; } else { btnC1.BackColor = Color.LightGray; }
                if (multi_prog_num == 4) { btnD1.BackColor = Color.LightBlue; } else { btnD1.BackColor = Color.LightGray; }
                if (multi_prog_num == 5) { btnE1.BackColor = Color.LightBlue; } else { btnE1.BackColor = Color.LightGray; }
                if (multi_prog_num == 6) { btnF1.BackColor = Color.LightBlue; } else { btnF1.BackColor = Color.LightGray; }

                lblStation.Text = Convert.ToString(Convert.ToDouble(_ACS.ReadVariable("STATION_NUM")));
                lblError.Text = Convert.ToString(Convert.ToDouble(_ACS.ReadVariable("STATE")));

                set_error();

                Height1 = Convert.ToInt32(_ACS.ReadVariable("HEIGHT1"));
                Height2 = Convert.ToInt32(_ACS.ReadVariable("HEIGHT2"));
                Height3 = Convert.ToInt32(_ACS.ReadVariable("HEIGHT3"));
                Height4 = Convert.ToInt32(_ACS.ReadVariable("HEIGHT4"));
                btnHeight1.Size = new Size(bathLength, Height1);
                btnHeight2.Size = new Size(bathLength, Height2);
                btnHeight3.Size = new Size(bathLength, Height3);
                btnHeight4.Size = new Size(bathLength, Height4);
                btnHeight1.Location = new Point(cornerX + 1 * bathLength + 1 * bathGap, cornerZ + machineHeight + (100-Height1));
                btnHeight2.Location = new Point(cornerX + 2 * bathLength + 2 * bathGap, cornerZ + machineHeight + (100 - Height2));
                btnHeight3.Location = new Point(cornerX + 3 * bathLength + 3 * bathGap, cornerZ + machineHeight + (100 - Height3));
                btnHeight4.Location = new Point(cornerX + 4 * bathLength + 4 * bathGap, cornerZ + machineHeight + (100 - Height4));
                if (Homed)
                {
                    btnZaxis.Location = new Point(cornerX + screenX - 15, cornerZ + screenZ - machineHeight + 100);// Convert.ToInt32(Axis2_FPOS)-machineHeight +30);
                    btnSensorZ.Location = new Point(cornerX + screenX-15, cornerZ );
                    btnHomeZ.Location = new Point(cornerX + screenX-15, cornerZ-(machineHeight/5));
                    btnZbasket.Location = new Point(cornerX + screenX - 15, cornerZ + screenZ - machineHeight/6 + 100);//Convert.ToInt32(Axis2_FPOS) +30);
                }


            }
        }
        private void set_error()
        {
            switch ((Convert.ToDouble(_ACS.ReadVariable("STATE"))))
            {
                case 9301:  lblErrorState.Text= "NO BASKET IN STATION 1 FAILED GRAB";return;
                case 9302: lblErrorState.Text= "NO BASKET IN STATION 2 FAILED GRAB"; return;
                case 9303: lblErrorState.Text= "NO BASKET IN STATION 3 FAILED GRAB"; return;
                case 9304: lblErrorState.Text= "NO BASKET IN STATION 4 FAILED GRAB"; return;
                case 9101: lblErrorState.Text= "PISTONS CLOSED CANT GRAB"; return;
                   
                case 9311: lblErrorState.Text= "BASKET IN STATION 1 FAILED RELEASE"; return;
                case 9312: lblErrorState.Text= "BASKET IN STATION 2 FAILED RELEASE"; return;
                case 9313: lblErrorState.Text= "BASKET IN STATION 3 FAILED RELEASE"; return;
                case 9314: lblErrorState.Text= "BASKET IN STATION 4 FAILED RELEASE"; return;
                case 9102: lblErrorState.Text= "PISTONS CLOSED CANT RELEASE"; return;
                   
                case 9011: lblErrorState.Text= "- ERROR PIST 1 FWD UP"; return;
                case 9012: lblErrorState.Text= "- ERROR PIST 1 FWD DOWN"; return;
                case 9013: lblErrorState.Text= "- ERROR PIST 1 BWD UP"; return;
                case 9014: lblErrorState.Text= "- ERROR PIST 1 BWD DOWN"; return;
                   
                case 9015: lblErrorState.Text= "- ERROR PIST 2 FWD UP"; return;
                case 9016: lblErrorState.Text= "- ERROR PIST 2 FWD DOWN"; return;
                case 9017: lblErrorState.Text= "- ERROR PIST 2 BWD UP"; return;
                case 9018: lblErrorState.Text = "- ERROR PIST 2 BWD DOWN"; return;

                case 9019: lblErrorState.Text= "- ERROR PIST 3 FWD UP"; return;
                case 9020: lblErrorState.Text= "- ERROR PIST 3 FWD DOWN"; return;
                case 9021: lblErrorState.Text= "- ERROR PIST 3 BWD UP"; return;
                case 9022: lblErrorState.Text= "- ERROR PIST 3 BWD DOWN"; return;
                  
                case 9023: lblErrorState.Text= "- ERROR PIST 4 FWD UP"; return;
                case 9024: lblErrorState.Text= "- ERROR PIST 4 FWD DOWN"; return;
                case 9025: lblErrorState.Text= "- ERROR PIST 4 BWD UP"; return;
                case 9026: lblErrorState.Text = "- ERROR PIST 4 BWD DOWN"; return;

                default: lblErrorState.Text = ""; return;
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
            btnHeight1.Size = new Size(bathLength, 100);
            btnHeight2.Size = new Size(bathLength, 100);
            btnHeight3.Size = new Size(bathLength, 100);
            btnHeight4.Size = new Size(bathLength, 100);
            btnInput.Location = new Point(cornerX, cornerZ + machineHeight);
            btnBath1.Location = new Point(cornerX+ 1*bathLength+1* bathGap, cornerZ + machineHeight);
            btnBath2.Location = new Point(cornerX + 2 * bathLength + 2 * bathGap, cornerZ + machineHeight);
            btnBath3.Location = new Point(cornerX + 3 * bathLength + 3 * bathGap, cornerZ + machineHeight);
            btnBath4.Location = new Point(cornerX + 4 * bathLength + 4 * bathGap, cornerZ + machineHeight);
            btnOutput.Location = new Point(cornerX + 5 * bathLength + 5 * bathGap, cornerZ + machineHeight);
            btnHeight1.Location = new Point(cornerX + 1 * bathLength + 1 * bathGap, cornerZ + machineHeight);
            btnHeight2.Location = new Point(cornerX + 2 * bathLength + 2 * bathGap, cornerZ + machineHeight);
            btnHeight3.Location = new Point(cornerX + 3 * bathLength + 3 * bathGap, cornerZ + machineHeight);
            btnHeight4.Location = new Point(cornerX + 4 * bathLength + 4 * bathGap, cornerZ + machineHeight);
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
           
            lblT1.Location= new Point(cornerX + 1 * bathLength + 1 * bathGap+5, cornerZ + machineHeight+100);
            lblT2.Location = new Point(cornerX + 2 * bathLength + 2 * bathGap+5, cornerZ + machineHeight + 100);
            lblT3.Location = new Point(cornerX + 3 * bathLength + 3 * bathGap+5, cornerZ + machineHeight + 100);
            lblT4.Location = new Point(cornerX + 4 * bathLength + 4 * bathGap + 5, cornerZ + machineHeight + 100);

            btnPiston1.BackColor = Color.LightGray;
            btnPiston2.BackColor = Color.LightGray; 
            btnPiston3.BackColor = Color.LightGray;
            btnPiston4.BackColor = Color.LightGray;

            
        }

        private void read_single_prog()
        {
            resault = _ACS.ReadVariable("SINGLE");
            single = resault as double[,];
            single_prog_num = Convert.ToDouble (_ACS.ReadVariable("SINGLE_PROG_NUM"));
            
            
            txtBathT1.Text = Convert.ToString(single[((int)single_prog_num), 1]);
            txtBathT2.Text = Convert.ToString(single[((int)single_prog_num), 2]);
            txtBathT3.Text = Convert.ToString(single[((int)single_prog_num), 3]);
            txtBathT4.Text = Convert.ToString(single[((int)single_prog_num), 4]);

            if (single[((int)single_prog_num), 5] == 0) { chk1Air.Checked = false; } else { chk1Air.Checked = true; }
            if (single[((int)single_prog_num), 6]==0) { chkPist1.Checked = false; } else { chkPist1.Checked = true;   }
            if (single[((int)single_prog_num), 7] == 0) { chkPist2.Checked = false; } else { chkPist2.Checked = true; }
            if (single[((int)single_prog_num), 8] == 0) { chkPist3.Checked = false; } else { chkPist3.Checked = true; }
            if (single[((int)single_prog_num), 9] == 0) { chkPist4.Checked = false; } else { chkPist4.Checked = true; }
            if (single[((int)single_prog_num), 0] == 0) { chckJ.Checked = false; } else { chckJ.Checked = true; }

        }
        private void read_multi_prog()
        {
            resault_multi = _ACS.ReadVariable("MULTI");
            multi = resault_multi as double[,];
            multi_prog_num = Convert.ToDouble(_ACS.ReadVariable("MULTI_PROG_NUM"));

            

            txtBathT11.Text = Convert.ToString(multi[((int)multi_prog_num), 1]);
            txtBathT21.Text = Convert.ToString(multi[((int)multi_prog_num), 2]);
            txtBathT31.Text = Convert.ToString(multi[((int)multi_prog_num), 3]);
            txtBathT41.Text = Convert.ToString(multi[((int)multi_prog_num), 4]);

            if (multi[((int)multi_prog_num), 0] == 0) { chckJ1.Checked = false; } else { chckJ1.Checked = true; }
            if (multi[((int)multi_prog_num), 5] == 0) {  chk21Air.Checked = false; } else { chk21Air.Checked = true; }
            if (multi[((int)multi_prog_num), 6] == 0) { chkPist11.Checked = false; } else { chkPist11.Checked = true; }
            if (multi[((int)multi_prog_num), 7] == 0) { chkPist21.Checked = false; } else { chkPist21.Checked = true; }
            if (multi[((int)multi_prog_num), 8] == 0) { chkPist31.Checked = false; } else { chkPist31.Checked = true; }
            if (multi[((int)multi_prog_num), 9] == 0) { chkPist41.Checked = false; } else { chkPist41.Checked = true; }

            txtBathT12.Text = Convert.ToString(multi[((int)multi_prog_num), 11]);
            txtBathT22.Text = Convert.ToString(multi[((int)multi_prog_num), 12]);
            txtBathT32.Text = Convert.ToString(multi[((int)multi_prog_num), 13]);
            txtBathT42.Text = Convert.ToString(multi[((int)multi_prog_num), 14]);

            if (multi[((int)multi_prog_num), 10] == 0) { chckJ2.Checked = false; } else { chckJ2.Checked = true; }
            if (multi[((int)multi_prog_num), 15] == 0) { chk22Air.Checked = false; } else { chk22Air.Checked = true; }
            if (multi[((int)multi_prog_num), 16] == 0) { chkPist12.Checked = false; } else { chkPist12.Checked = true; }
            if (multi[((int)multi_prog_num), 17] == 0) { chkPist22.Checked = false; } else { chkPist22.Checked = true; }
            if (multi[((int)multi_prog_num), 18] == 0) { chkPist32.Checked = false; } else { chkPist32.Checked = true; }
            if (multi[((int)multi_prog_num), 19] == 0) { chkPist42.Checked = false; } else { chkPist42.Checked = true; }

            txtBathT13.Text = Convert.ToString(multi[((int)multi_prog_num), 21]);
            txtBathT23.Text = Convert.ToString(multi[((int)multi_prog_num), 22]);
            txtBathT33.Text = Convert.ToString(multi[((int)multi_prog_num), 23]);
            txtBathT43.Text = Convert.ToString(multi[((int)multi_prog_num), 24]);

            if (multi[((int)multi_prog_num), 20] == 0) { chckJ3.Checked = false; } else { chckJ3.Checked = true; }
            if (multi[((int)multi_prog_num), 25] == 0) { chk23Air.Checked = false; } else { chk23Air.Checked = true; }
            if (multi[((int)multi_prog_num), 26] == 0) { chkPist13.Checked = false; } else { chkPist13.Checked = true; }
            if (multi[((int)multi_prog_num), 27] == 0) { chkPist23.Checked = false; } else { chkPist23.Checked = true; }
            if (multi[((int)multi_prog_num), 28] == 0) { chkPist33.Checked = false; } else { chkPist33.Checked = true; }
            if (multi[((int)multi_prog_num), 29] == 0) { chkPist43.Checked = false; } else { chkPist43.Checked = true; }

            txtBathT14.Text = Convert.ToString(multi[((int)multi_prog_num), 31]);
            txtBathT24.Text = Convert.ToString(multi[((int)multi_prog_num), 32]);
            txtBathT34.Text = Convert.ToString(multi[((int)multi_prog_num), 33]);
            txtBathT44.Text = Convert.ToString(multi[((int)multi_prog_num), 34]);

            if (multi[((int)multi_prog_num), 30] == 0) { chckJ4.Checked = false; } else { chckJ4.Checked = true; }
            if (multi[((int)multi_prog_num), 35] == 0) { chk24Air.Checked = false; } else { chk24Air.Checked = true; }
            if (multi[((int)multi_prog_num), 36] == 0) { chkPist14.Checked = false; } else { chkPist14.Checked = true; }
            if (multi[((int)multi_prog_num), 37] == 0) { chkPist24.Checked = false; } else { chkPist24.Checked = true; }
            if (multi[((int)multi_prog_num), 38] == 0) { chkPist34.Checked = false; } else { chkPist34.Checked = true; }
            if (multi[((int)multi_prog_num), 39] == 0) { chkPist44.Checked = false; } else { chkPist44.Checked = true; }


        }
        
        
    }
}
