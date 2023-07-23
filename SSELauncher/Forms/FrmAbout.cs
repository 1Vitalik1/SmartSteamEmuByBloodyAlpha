using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SSELauncher
{
    public class FrmAbout : Form
	{
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private Label lblVisit;
        private PictureBox pictureBox2;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private Panel panel_move;



        private PictureBox pbLogo;

		private Label label1;

		private Label label2;

		private Label lblVersionx86;

		private Label lblVersionx64;

		private Label label3;

		private Panel panel1;
        private Panel panel2;
        private PictureBox Btn_WinMinimize;
        private PictureBox Btn_close;
        private PictureBox pictureBox1;
        private Panel panel_spc;
        private Label lblVisitComfySource;
        private Panel panel3;
        private Panel panel7;
        private PictureBox btn_exit;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;

		public FrmAbout()
		{
            InitializeComponent();
		}

        private void FrmAbout_Load(object sender, EventArgs e)
		{
			try
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SmartSteamEmu\\SmartSteamEmu.dll"));
                lblVersionx86.Text = "SmartSteamEmu.dll version " + string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					versionInfo.FileMajorPart,
					versionInfo.FileMinorPart,
					versionInfo.FileBuildPart,
					versionInfo.FilePrivatePart
				});
			}
			catch
			{
                lblVersionx86.Text = "Unable to retrieve SmartSteamEmu.dll version";
			}
			try
			{
				FileVersionInfo versionInfo2 = FileVersionInfo.GetVersionInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SmartSteamEmu\\SmartSteamEmu64.dll"));
                lblVersionx64.Text = "SmartSteamEmu64.dll version " + string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					versionInfo2.FileMajorPart,
					versionInfo2.FileMinorPart,
					versionInfo2.FileBuildPart,
					versionInfo2.FilePrivatePart
				});
			}
			catch
			{
                lblVersionx64.Text = "Unable to retrieve SmartSteamEmu64.dll version";
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void lblVisit_Click(object sender, EventArgs e)
		{
			Process.Start("https://discord.gg/PfxujSxDYA");
		}

        private void lblVisitComfySource_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitgud.io/softashell/SSELauncher-comfy-edition");
        }

        private void InitializeComponent()
		{
            this.panel_move = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVersionx86 = new System.Windows.Forms.Label();
            this.lblVersionx64 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btn_exit = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_WinMinimize = new System.Windows.Forms.PictureBox();
            this.Btn_close = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_spc = new System.Windows.Forms.Panel();
            this.lblVisitComfySource = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblVisit = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_exit)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_WinMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_move
            // 
            this.panel_move.Location = new System.Drawing.Point(0, 0);
            this.panel_move.Name = "panel_move";
            this.panel_move.Size = new System.Drawing.Size(200, 100);
            this.panel_move.TabIndex = 0;
            this.panel_move.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(222, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "SmartSteam Emu && Launcher";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(2, 113);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "visit";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersionx86
            // 
            this.lblVersionx86.ForeColor = System.Drawing.Color.White;
            this.lblVersionx86.Location = new System.Drawing.Point(222, 81);
            this.lblVersionx86.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersionx86.Name = "lblVersionx86";
            this.lblVersionx86.Size = new System.Drawing.Size(254, 19);
            this.lblVersionx86.TabIndex = 2;
            this.lblVersionx86.Text = "VERSION x86";
            this.lblVersionx86.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersionx64
            // 
            this.lblVersionx64.ForeColor = System.Drawing.Color.White;
            this.lblVersionx64.Location = new System.Drawing.Point(222, 100);
            this.lblVersionx64.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersionx64.Name = "lblVersionx64";
            this.lblVersionx64.Size = new System.Drawing.Size(254, 19);
            this.lblVersionx64.TabIndex = 2;
            this.lblVersionx64.Text = "VERSION x64";
            this.lblVersionx64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(222, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Version Info";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 277);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panel1.Size = new System.Drawing.Size(502, 50);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btn_exit);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(148, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(190, 44);
            this.panel7.TabIndex = 4;
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.btn_exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_exit.Image = global::Properties.Resources.Btn_Exit_Menu;
            this.btn_exit.ImageLocation = "";
            this.btn_exit.Location = new System.Drawing.Point(0, 0);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(190, 44);
            this.btn_exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_exit.TabIndex = 0;
            this.btn_exit.TabStop = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            this.btn_exit.MouseEnter += new System.EventHandler(this.btn_exit_MouseEnter);
            this.btn_exit.MouseLeave += new System.EventHandler(this.btn_exit_MouseLeave);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(148, 44);
            this.panel6.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(338, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(164, 44);
            this.panel5.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(36)))));
            this.panel2.Controls.Add(this.Btn_WinMinimize);
            this.panel2.Controls.Add(this.Btn_close);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.panel_spc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(502, 40);
            this.panel2.TabIndex = 5;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // Btn_WinMinimize
            // 
            this.Btn_WinMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_WinMinimize.Image = global::Properties.Resources.Btn_Minimize;
            this.Btn_WinMinimize.Location = new System.Drawing.Point(416, 0);
            this.Btn_WinMinimize.Name = "Btn_WinMinimize";
            this.Btn_WinMinimize.Size = new System.Drawing.Size(43, 40);
            this.Btn_WinMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Btn_WinMinimize.TabIndex = 4;
            this.Btn_WinMinimize.TabStop = false;
            this.Btn_WinMinimize.Click += new System.EventHandler(this.Btn_WinMinimize_Click);
            this.Btn_WinMinimize.MouseEnter += new System.EventHandler(this.Btn_WinMinimize_MouseEnter);
            this.Btn_WinMinimize.MouseLeave += new System.EventHandler(this.Btn_WinMinimize_MouseLeave);
            // 
            // Btn_close
            // 
            this.Btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_close.Image = global::Properties.Resources.Btn_Exit;
            this.Btn_close.Location = new System.Drawing.Point(459, 0);
            this.Btn_close.Name = "Btn_close";
            this.Btn_close.Size = new System.Drawing.Size(43, 40);
            this.Btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Btn_close.TabIndex = 2;
            this.Btn_close.TabStop = false;
            this.Btn_close.Click += new System.EventHandler(this.Btn_close_Click);
            this.Btn_close.MouseEnter += new System.EventHandler(this.Btn_close_MouseEnter);
            this.Btn_close.MouseLeave += new System.EventHandler(this.Btn_close_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::Properties.Resources.SmartSteamEmu;
            this.pictureBox1.Location = new System.Drawing.Point(17, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // panel_spc
            // 
            this.panel_spc.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_spc.Location = new System.Drawing.Point(0, 0);
            this.panel_spc.Name = "panel_spc";
            this.panel_spc.Size = new System.Drawing.Size(17, 40);
            this.panel_spc.TabIndex = 5;
            // 
            // lblVisitComfySource
            // 
            this.lblVisitComfySource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVisitComfySource.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblVisitComfySource.Location = new System.Drawing.Point(-3, 151);
            this.lblVisitComfySource.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVisitComfySource.Name = "lblVisitComfySource";
            this.lblVisitComfySource.Size = new System.Drawing.Size(201, 19);
            this.lblVisitComfySource.TabIndex = 4;
            this.lblVisitComfySource.Text = "Comfy edition source @ gitgud.io";
            this.lblVisitComfySource.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVisitComfySource.Click += new System.EventHandler(this.lblVisitComfySource_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lblVisitComfySource);
            this.panel3.Controls.Add(this.lblVisit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 237);
            this.panel3.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pbLogo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 113);
            this.panel4.TabIndex = 0;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLogo.Image = global::Properties.Resources.launcher_big;
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(2);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(200, 113);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // lblVisit
            // 
            this.lblVisit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVisit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblVisit.Location = new System.Drawing.Point(2, 132);
            this.lblVisit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVisit.Name = "lblVisit";
            this.lblVisit.Size = new System.Drawing.Size(196, 19);
            this.lblVisit.TabIndex = 2;
            this.lblVisit.Text = "Project forum @ Discord";
            this.lblVisit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVisit.Click += new System.EventHandler(this.lblVisit_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox2.Image = global::Properties.Resources.ProjectByBloodyAlpha1;
            this.pictureBox2.Location = new System.Drawing.Point(200, 235);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(302, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(35)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(502, 327);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblVersionx64);
            this.Controls.Add(this.lblVersionx86);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_exit)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_WinMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void btn_exit_MouseLeave(object sender, EventArgs e)
        {
            btn_exit.Image = Properties.Resources.Btn_Exit_Menu;
        }

        private void btn_exit_MouseEnter(object sender, EventArgs e)
        {
            btn_exit.Image = Properties.Resources.Exit_OnEnter;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_WinMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Btn_close_MouseEnter(object sender, EventArgs e)
        {
            Btn_close.Image = Properties.Resources.Btn_Exit_OnEnter;
        }

        private void Btn_WinMinimize_MouseEnter(object sender, EventArgs e)
        {
            Btn_WinMinimize.Image = Properties.Resources.Btn_Minimize_OnEnter;
        }

        private void Btn_close_MouseLeave(object sender, EventArgs e)
        {
            Btn_close.Image = Properties.Resources.Btn_Exit;
        }

        private void Btn_WinMinimize_MouseLeave(object sender, EventArgs e)
        {
            Btn_WinMinimize.Image = Properties.Resources.Btn_Minimize;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // При захвате мышью за панель отправляем сообщение о перемещении окна
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // При захвате мышью за панель отправляем сообщение о перемещении окна
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
