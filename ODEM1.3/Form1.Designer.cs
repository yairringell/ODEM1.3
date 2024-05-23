namespace ODEM1._3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.grpJog = new System.Windows.Forms.GroupBox();
            this.btnJogLeft = new System.Windows.Forms.Button();
            this.btnJogDown = new System.Windows.Forms.Button();
            this.btnJogRight = new System.Windows.Forms.Button();
            this.btnJogUp = new System.Windows.Forms.Button();
            this.btnZaxis = new System.Windows.Forms.Button();
            this.btnXaxis = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.btnProg = new System.Windows.Forms.Button();
            this.grpProg = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblPosX = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPosZ = new System.Windows.Forms.Label();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnBath1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.grpJog.SuspendLayout();
            this.grpProg.SuspendLayout();
            this.grpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(130, 23);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(111, 34);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(6, 29);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(118, 24);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "192.168.0.115";
            // 
            // grpMain
            // 
            this.grpMain.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpMain.Controls.Add(this.btnBath1);
            this.grpMain.Controls.Add(this.grpJog);
            this.grpMain.Controls.Add(this.btnZaxis);
            this.grpMain.Controls.Add(this.btnXaxis);
            this.grpMain.Location = new System.Drawing.Point(1, 90);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(1526, 807);
            this.grpMain.TabIndex = 1;
            this.grpMain.TabStop = false;
            // 
            // grpJog
            // 
            this.grpJog.Controls.Add(this.btnJogLeft);
            this.grpJog.Controls.Add(this.btnJogDown);
            this.grpJog.Controls.Add(this.btnJogRight);
            this.grpJog.Controls.Add(this.btnJogUp);
            this.grpJog.Location = new System.Drawing.Point(7, 11);
            this.grpJog.Name = "grpJog";
            this.grpJog.Size = new System.Drawing.Size(199, 108);
            this.grpJog.TabIndex = 6;
            this.grpJog.TabStop = false;
            // 
            // btnJogLeft
            // 
            this.btnJogLeft.Location = new System.Drawing.Point(6, 46);
            this.btnJogLeft.Name = "btnJogLeft";
            this.btnJogLeft.Size = new System.Drawing.Size(91, 23);
            this.btnJogLeft.TabIndex = 3;
            this.btnJogLeft.Text = "JOG LEFT";
            this.btnJogLeft.UseVisualStyleBackColor = true;
            this.btnJogLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogXneg_MouseDown);
            this.btnJogLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogXneg_MouseUp);
            // 
            // btnJogDown
            // 
            this.btnJogDown.Location = new System.Drawing.Point(56, 75);
            this.btnJogDown.Name = "btnJogDown";
            this.btnJogDown.Size = new System.Drawing.Size(101, 23);
            this.btnJogDown.TabIndex = 5;
            this.btnJogDown.Text = "JOG DOWN";
            this.btnJogDown.UseVisualStyleBackColor = true;
            this.btnJogDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogXdown_MouseDown);
            this.btnJogDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogXdown_MouseUp);
            // 
            // btnJogRight
            // 
            this.btnJogRight.Location = new System.Drawing.Point(103, 46);
            this.btnJogRight.Name = "btnJogRight";
            this.btnJogRight.Size = new System.Drawing.Size(89, 23);
            this.btnJogRight.TabIndex = 2;
            this.btnJogRight.Text = "JOG RIGHT";
            this.btnJogRight.UseVisualStyleBackColor = true;
            this.btnJogRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogXpos_MouseDown);
            this.btnJogRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogXpos_MouseUp);
            // 
            // btnJogUp
            // 
            this.btnJogUp.Location = new System.Drawing.Point(56, 17);
            this.btnJogUp.Name = "btnJogUp";
            this.btnJogUp.Size = new System.Drawing.Size(101, 23);
            this.btnJogUp.TabIndex = 4;
            this.btnJogUp.Text = "JOG UP";
            this.btnJogUp.UseVisualStyleBackColor = true;
            this.btnJogUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogXup_MouseDown);
            this.btnJogUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogXup_MouseUp);
            // 
            // btnZaxis
            // 
            this.btnZaxis.Location = new System.Drawing.Point(566, 96);
            this.btnZaxis.Name = "btnZaxis";
            this.btnZaxis.Size = new System.Drawing.Size(32, 222);
            this.btnZaxis.TabIndex = 1;
            this.btnZaxis.UseVisualStyleBackColor = true;
            this.btnZaxis.Click += new System.EventHandler(this.btnZaxis_Click);
            // 
            // btnXaxis
            // 
            this.btnXaxis.Location = new System.Drawing.Point(212, 113);
            this.btnXaxis.Name = "btnXaxis";
            this.btnXaxis.Size = new System.Drawing.Size(1052, 30);
            this.btnXaxis.TabIndex = 0;
            this.btnXaxis.UseVisualStyleBackColor = true;
            this.btnXaxis.Click += new System.EventHandler(this.btnXaxis_Click);
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(6, 18);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(75, 40);
            this.btnMain.TabIndex = 2;
            this.btnMain.Text = "MAIN";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnProg
            // 
            this.btnProg.Location = new System.Drawing.Point(87, 18);
            this.btnProg.Name = "btnProg";
            this.btnProg.Size = new System.Drawing.Size(117, 40);
            this.btnProg.TabIndex = 3;
            this.btnProg.Text = "PROGRAMS";
            this.btnProg.UseVisualStyleBackColor = true;
            this.btnProg.Click += new System.EventHandler(this.btnProg_Click);
            // 
            // grpProg
            // 
            this.grpProg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grpProg.Controls.Add(this.button1);
            this.grpProg.Location = new System.Drawing.Point(1, 91);
            this.grpProg.Name = "grpProg";
            this.grpProg.Size = new System.Drawing.Size(1528, 810);
            this.grpProg.TabIndex = 2;
            this.grpProg.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1247, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "postion X: ";
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosX.Location = new System.Drawing.Point(1332, 39);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(16, 18);
            this.lblPosX.TabIndex = 5;
            this.lblPosX.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1410, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "postion Z: ";
            // 
            // lblPosZ
            // 
            this.lblPosZ.AutoSize = true;
            this.lblPosZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosZ.Location = new System.Drawing.Point(1494, 39);
            this.lblPosZ.Name = "lblPosZ";
            this.lblPosZ.Size = new System.Drawing.Size(16, 18);
            this.lblPosZ.TabIndex = 7;
            this.lblPosZ.Text = "0";
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.btnMain);
            this.grpButtons.Controls.Add(this.btnProg);
            this.grpButtons.Location = new System.Drawing.Point(438, 10);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(564, 73);
            this.grpButtons.TabIndex = 8;
            this.grpButtons.TabStop = false;
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(285, 24);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(131, 50);
            this.btnHome.TabIndex = 9;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(1131, 26);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(100, 50);
            this.btnManual.TabIndex = 10;
            this.btnManual.Text = "Manual";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // btnBath1
            // 
            this.btnBath1.Location = new System.Drawing.Point(212, 439);
            this.btnBath1.Name = "btnBath1";
            this.btnBath1.Size = new System.Drawing.Size(231, 146);
            this.btnBath1.TabIndex = 7;
            this.btnBath1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1541, 900);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.grpButtons);
            this.Controls.Add(this.lblPosZ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPosX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.grpProg);
            this.Name = "Form1";
            this.Text = "ODEM1.3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpJog.ResumeLayout(false);
            this.grpProg.ResumeLayout(false);
            this.grpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.GroupBox grpProg;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Button btnProg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPosZ;
        private System.Windows.Forms.Button btnZaxis;
        private System.Windows.Forms.Button btnXaxis;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnJogRight;
        private System.Windows.Forms.GroupBox grpJog;
        private System.Windows.Forms.Button btnJogLeft;
        private System.Windows.Forms.Button btnJogDown;
        private System.Windows.Forms.Button btnJogUp;
        private System.Windows.Forms.Button btnBath1;
    }
}

