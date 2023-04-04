// Pure Battery - System Tray Add-On, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// PureBatteryAddOn.Settings
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using PureBatteryAddOn;

public class Settings : Form
{
	private IContainer components;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private TextBox textBox1;

	private PictureBox pictureBox1;

	private PictureBox pictureBox2;

	private PictureBox pictureBox3;

	private TextBox textBox3;

	private Label label5;

	private TextBox textBox4;

	private Label label6;

	private Label label8;

	private CheckBox checkBox1;

	private Label label7;

	private Label label9;

	private TextBox textBox5;

	private Label label10;

	private CheckBox checkBox2;

	private TextBox textBox2;

	private Label label11;

	private Label label12;

	private Label label13;

	private CheckBox checkBox3;

	private Label label14;

	private Label label15;

	private Label label16;

	private Button button1;

	private Label label17;

	public Settings()
	{
		PTConfig pTConfig = new PTConfig();
		pTConfig.Load();
		string fontSize = pTConfig.FontSize;
		string xOffset = pTConfig.XOffset;
		string yOffset = pTConfig.YOffset;
		string normalColor = pTConfig.NormalColor;
		string chargingColor = pTConfig.ChargingColor;
		string lowColor = pTConfig.LowColor;
		string chargeNotifyValue = pTConfig.ChargeNotifyValue;
		string chargeNotify = pTConfig.ChargeNotify;
		string repeatNotification = pTConfig.RepeatNotification;
		string autoStart = pTConfig.AutoStart;
		InitializeComponent();
		textBox1.Text = fontSize;
		textBox3.Text = xOffset;
		textBox4.Text = yOffset;
		pictureBox1.BackColor = (Color)new ColorConverter().ConvertFromString(normalColor);
		pictureBox2.BackColor = (Color)new ColorConverter().ConvertFromString(chargingColor);
		pictureBox3.BackColor = (Color)new ColorConverter().ConvertFromString(lowColor);
		textBox2.Visible = false;
		textBox2.Enabled = false;
		base.ActiveControl = textBox2;
		textBox5.Text = chargeNotifyValue;
		checkBox1.Checked = autoStart == "true";
		checkBox2.Checked = chargeNotify == "true";
		checkBox3.Checked = repeatNotification == "true";
		if (!checkBox2.Checked)
		{
			textBox5.Enabled = false;
			checkBox3.Enabled = false;
		}
	}

	private void label1_Click(object sender, EventArgs e)
	{
	}

	private void label2_Click(object sender, EventArgs e)
	{
	}

	private void label4_Click(object sender, EventArgs e)
	{
	}

	private void label3_Click(object sender, EventArgs e)
	{
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
		try
		{
			TrayIcon.iconFontSize = Convert.ToInt32(textBox1.Text);
			PTConfig pTConfig = new PTConfig();
			pTConfig.Load();
			pTConfig.FontSize = textBox1.Text;
			pTConfig.Update();
		}
		catch
		{
		}
	}

	private void textBox3_TextChanged(object sender, EventArgs e)
	{
		try
		{
			TrayIcon.xoffset = Convert.ToInt32(textBox3.Text);
			PTConfig pTConfig = new PTConfig();
			pTConfig.Load();
			pTConfig.XOffset = textBox3.Text;
			pTConfig.Update();
		}
		catch
		{
		}
	}

	private void textBox4_TextChanged(object sender, EventArgs e)
	{
		try
		{
			TrayIcon.yoffset = Convert.ToInt32(textBox4.Text);
			PTConfig pTConfig = new PTConfig();
			pTConfig.Load();
			pTConfig.YOffset = textBox4.Text;
			pTConfig.Update();
		}
		catch
		{
		}
	}

	private void pictureBox1_Click_1(object sender, EventArgs e)
	{
		ColorDialog colorDialog = new ColorDialog();
		if (colorDialog.ShowDialog() == DialogResult.OK)
		{
			pictureBox1.BackColor = colorDialog.Color;
			TrayIcon.normalColor = colorDialog.Color;
			PTConfig pTConfig = new PTConfig();
			pTConfig.Load();
			try
			{
				pTConfig.NormalColor = new ColorConverter().ConvertToString(TrayIcon.normalColor);
				pTConfig.Update();
			}
			catch (Exception ex)
			{
				MessageBox.Show("无法更新您的颜色偏好。" + ex.ToString());
			}
		}
	}

	private void pictureBox2_Click(object sender, EventArgs e)
	{
		ColorDialog colorDialog = new ColorDialog();
		if (colorDialog.ShowDialog() == DialogResult.OK)
		{
			pictureBox2.BackColor = colorDialog.Color;
			TrayIcon.chargingColor = colorDialog.Color;
			PTConfig pTConfig = new PTConfig();
			pTConfig.Load();
			try
			{
				pTConfig.ChargingColor = new ColorConverter().ConvertToString(TrayIcon.chargingColor);
				pTConfig.Update();
			}
			catch (Exception ex)
			{
				MessageBox.Show("无法更新您的颜色偏好。" + ex.ToString());
			}
		}
	}

	private void pictureBox3_Click(object sender, EventArgs e)
	{
		ColorDialog colorDialog = new ColorDialog();
		if (colorDialog.ShowDialog() == DialogResult.OK)
		{
			pictureBox3.BackColor = colorDialog.Color;
			TrayIcon.lowColor = colorDialog.Color;
			PTConfig pTConfig = new PTConfig();
			pTConfig.Load();
			try
			{
				pTConfig.LowColor = new ColorConverter().ConvertToString(TrayIcon.lowColor);
				pTConfig.Update();
			}
			catch (Exception ex)
			{
				MessageBox.Show("无法更新您的颜色偏好。" + ex.ToString());
			}
		}
	}

	private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '-')
		{
			e.Handled = true;
		}
	}

	private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '-')
		{
			e.Handled = true;
		}
	}

	private void pictureBox4_Click(object sender, EventArgs e)
	{
	}

	private void label7_Click(object sender, EventArgs e)
	{
		string input = Registry.ClassesRoot.OpenSubKey("http\\shell\\open\\command\\").GetValue("").ToString();
		MatchCollection matchCollection = new Regex("\"([^\"]+)\"").Matches(input);
		if (matchCollection.Count > 0)
		{
			Process.Start(matchCollection[0].Groups[1].Value, "https://www.microsoft.com/en-us/p/pure-battery-analytics/9nblggh4x4k3?activetab=pivot:overviewtab");
		}
	}

	private void label8_Click(object sender, EventArgs e)
	{
	}

	private void label9_Click(object sender, EventArgs e)
	{
	}

	private void Settings_Load(object sender, EventArgs e)
	{
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		PTConfig pTConfig = new PTConfig();
		pTConfig.Load();
		RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
		if (checkBox1.Checked)
		{
			pTConfig.AutoStart = "true";
			registryKey.SetValue(Assembly.GetEntryAssembly().GetName().Name, Application.ExecutablePath);
		}
		else
		{
			pTConfig.AutoStart = "false";
			registryKey.DeleteValue(Assembly.GetEntryAssembly().GetName().Name, throwOnMissingValue: false);
		}
		pTConfig.Update();
	}

	private void label9_Click_1(object sender, EventArgs e)
	{
	}

	private void textBox5_TextChanged(object sender, EventArgs e)
	{
		try
		{
			Regex regex = new Regex("^\\d+(,\\d+)*$");
			if (textBox5.Text == "")
			{
				label16.Text = "不能是空值";
				button1.Enabled = false;
			}
			else if (regex.IsMatch(textBox5.Text))
			{
				label16.Text = "使用“保存”按钮保留更改。";
				button1.Enabled = true;
			}
			else
			{
				label16.Text = "无效条目";
				button1.Enabled = false;
			}
		}
		catch (Exception)
		{
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			if (new Regex("^\\d+(,\\d+)*$").IsMatch(textBox5.Text))
			{
				PTConfig pTConfig = new PTConfig();
				pTConfig.Load();
				pTConfig.ChargeNotifyValue = textBox5.Text;
				pTConfig.Update();
				label16.Text = "保存成功";
			}
		}
		catch (Exception)
		{
		}
	}

	private void label10_Click(object sender, EventArgs e)
	{
	}

	private void checkBox2_CheckedChanged(object sender, EventArgs e)
	{
		PTConfig pTConfig = new PTConfig();
		pTConfig.Load();
		if (checkBox2.Checked)
		{
			pTConfig.ChargeNotify = "true";
			label9.Visible = true;
			textBox5.Enabled = true;
			checkBox3.Enabled = true;
			button1.Enabled = true;
		}
		else
		{
			pTConfig.ChargeNotify = "false";
			label9.Visible = true;
			textBox5.Enabled = false;
			checkBox3.Enabled = false;
			button1.Enabled = false;
		}
		pTConfig.Update();
	}

	private void label11_Click(object sender, EventArgs e)
	{
	}

	private void checkBox3_CheckedChanged(object sender, EventArgs e)
	{
		PTConfig pTConfig = new PTConfig();
		pTConfig.Load();
		if (checkBox3.Checked)
		{
			pTConfig.RepeatNotification = "true";
		}
		else
		{
			pTConfig.RepeatNotification = "false";
		}
		pTConfig.Update();
	}

	private void label15_Click(object sender, EventArgs e)
	{
		string input = Registry.ClassesRoot.OpenSubKey("http\\shell\\open\\command\\").GetValue("").ToString();
		MatchCollection matchCollection = new Regex("\"([^\"]+)\"").Matches(input);
		if (matchCollection.Count > 0)
		{
			Process.Start(matchCollection[0].Groups[1].Value, "https://www.paypal.me/medhakarri");
		}
	}

	private void label16_Click(object sender, EventArgs e)
	{
	}

	private void label17_Click(object sender, EventArgs e)
	{
		string input = Registry.ClassesRoot.OpenSubKey("http\\shell\\open\\command\\").GetValue("").ToString();
		MatchCollection matchCollection = new Regex("\"([^\"]+)\"").Matches(input);
		if (matchCollection.Count > 0)
		{
			Process.Start(matchCollection[0].Groups[1].Value, "https://github.com/medhachaitanya/PureBatteryAddOnSetup/tree/master/Latest");
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PureBatteryAddOn.Settings));
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.label8 = new System.Windows.Forms.Label();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.textBox5 = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.label17 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label1.Location = new System.Drawing.Point(203, 187);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(70, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "字体大小";
		this.label1.Click += new System.EventHandler(label1_Click);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label2.Location = new System.Drawing.Point(10, 178);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(69, 19);
		this.label2.TabIndex = 1;
		this.label2.Text = "正在充电";
		this.label2.Click += new System.EventHandler(label2_Click);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label3.Location = new System.Drawing.Point(10, 234);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(91, 19);
		this.label3.TabIndex = 2;
		this.label3.Text = "电量不足";
		this.label3.Click += new System.EventHandler(label3_Click);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label4.Location = new System.Drawing.Point(10, 123);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(77, 19);
		this.label4.TabIndex = 3;
		this.label4.Text = "正常放电";
		this.label4.Click += new System.EventHandler(label4_Click);
		this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
		this.textBox1.Location = new System.Drawing.Point(360, 180);
		this.textBox1.Margin = new System.Windows.Forms.Padding(2);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(36, 26);
		this.textBox1.TabIndex = 6;
		this.textBox1.Text = "28";
		this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox1.WordWrap = false;
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox1_KeyPress);
		this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
		this.textBox3.Location = new System.Drawing.Point(360, 217);
		this.textBox3.Margin = new System.Windows.Forms.Padding(2);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(36, 26);
		this.textBox3.TabIndex = 12;
		this.textBox3.Text = "0";
		this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox3.WordWrap = false;
		this.textBox3.TextChanged += new System.EventHandler(textBox3_TextChanged);
		this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox3_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label5.Location = new System.Drawing.Point(203, 221);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(153, 19);
		this.label5.TabIndex = 11;
		this.label5.Text = "水平偏移(+/-)";
		this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
		this.textBox4.Location = new System.Drawing.Point(360, 251);
		this.textBox4.Margin = new System.Windows.Forms.Padding(2);
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(36, 26);
		this.textBox4.TabIndex = 14;
		this.textBox4.Text = "0";
		this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox4.WordWrap = false;
		this.textBox4.TextChanged += new System.EventHandler(textBox4_TextChanged);
		this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox4_KeyPress);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label6.Location = new System.Drawing.Point(203, 255);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(132, 19);
		this.label6.TabIndex = 13;
		this.label6.Text = "垂直偏移(+/-)";
		this.pictureBox3.BackColor = System.Drawing.SystemColors.Control;
		this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pictureBox3.Location = new System.Drawing.Point(14, 256);
		this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Size = new System.Drawing.Size(76, 20);
		this.pictureBox3.TabIndex = 9;
		this.pictureBox3.TabStop = false;
		this.pictureBox3.Click += new System.EventHandler(pictureBox3_Click);
		this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
		this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pictureBox2.Location = new System.Drawing.Point(14, 200);
		this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(76, 20);
		this.pictureBox2.TabIndex = 8;
		this.pictureBox2.TabStop = false;
		this.pictureBox2.Click += new System.EventHandler(pictureBox2_Click);
		this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
		this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pictureBox1.Location = new System.Drawing.Point(14, 145);
		this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(76, 20);
		this.pictureBox1.TabIndex = 7;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Click += new System.EventHandler(pictureBox1_Click_1);
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label8.Location = new System.Drawing.Point(10, 288);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(149, 19);
		this.label8.TabIndex = 17;
		this.label8.Text = "开机启动";
		this.label8.Click += new System.EventHandler(label8_Click);
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(14, 310);
		this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(15, 14);
		this.checkBox1.TabIndex = 18;
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.label7.AutoSize = true;
		this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.label7.Font = new System.Drawing.Font("Consolas", 7.8f);
		this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
		this.label7.Location = new System.Drawing.Point(357, 288);
		this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(133, 13);
		this.label7.TabIndex = 16;
		this.label7.Text = "下载插件";
		this.label7.Click += new System.EventHandler(label7_Click);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label9.Location = new System.Drawing.Point(199, 34);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(85, 19);
		this.label9.TabIndex = 19;
		this.label9.Text = "通知时间 %";
		this.label9.Click += new System.EventHandler(label9_Click_1);
		this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
		this.textBox5.Location = new System.Drawing.Point(286, 31);
		this.textBox5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.textBox5.Name = "textBox5";
		this.textBox5.Size = new System.Drawing.Size(110, 26);
		this.textBox5.TabIndex = 20;
		this.textBox5.Text = "30,90";
		this.textBox5.WordWrap = false;
		this.textBox5.TextChanged += new System.EventHandler(textBox5_TextChanged);
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label10.Location = new System.Drawing.Point(36, 12);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(206, 19);
		this.label10.TabIndex = 21;
		this.label10.Text = "启用电池通知？";
		this.label10.Click += new System.EventHandler(label10_Click);
		this.checkBox2.AutoSize = true;
		this.checkBox2.Location = new System.Drawing.Point(14, 18);
		this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(15, 14);
		this.checkBox2.TabIndex = 22;
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox2.CheckedChanged += new System.EventHandler(checkBox2_CheckedChanged);
		this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
		this.textBox2.Location = new System.Drawing.Point(360, 145);
		this.textBox2.Margin = new System.Windows.Forms.Padding(2);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(36, 26);
		this.textBox2.TabIndex = 23;
		this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox2.WordWrap = false;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(8, 79);
		this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(220, 13);
		this.label11.TabIndex = 24;
		this.label11.Text = "-----------------------------------------------------------------------";
		this.label11.Click += new System.EventHandler(label11_Click);
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Bold);
		this.label12.ForeColor = System.Drawing.SystemColors.MenuHighlight;
		this.label12.Location = new System.Drawing.Point(10, 93);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(276, 19);
		this.label12.TabIndex = 25;
		this.label12.Text = "个性化配置";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(8, 112);
		this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(220, 13);
		this.label13.TabIndex = 26;
		this.label13.Text = "-----------------------------------------------------------------------";
		this.checkBox3.AutoSize = true;
		this.checkBox3.Location = new System.Drawing.Point(14, 68);
		this.checkBox3.Margin = new System.Windows.Forms.Padding(2);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(15, 14);
		this.checkBox3.TabIndex = 27;
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox3.Visible = false;
		this.checkBox3.CheckedChanged += new System.EventHandler(checkBox3_CheckedChanged);
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Calibri", 12f);
		this.label14.Location = new System.Drawing.Point(36, 63);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(228, 19);
		this.label14.TabIndex = 28;
		this.label14.Text = "重复通知直到拔出插头？";
		this.label14.Visible = false;
		this.label15.AutoSize = true;
		this.label15.Cursor = System.Windows.Forms.Cursors.Hand;
		this.label15.Font = new System.Drawing.Font("Consolas", 7.8f);
		this.label15.ForeColor = System.Drawing.Color.Teal;
		this.label15.Location = new System.Drawing.Point(37, 327);
		this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(307, 13);
		this.label15.TabIndex = 29;
		this.label15.Text = "帮助和支持这个免费项目-请捐助";
		this.label15.Click += new System.EventHandler(label15_Click);
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Calibri Light", 8.25f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(314, 57);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(103, 13);
		this.label16.TabIndex = 30;
		this.label16.Text = "Separate by Commas";
		this.label16.Click += new System.EventHandler(label16_Click);
		this.button1.Location = new System.Drawing.Point(402, 34);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 31;
		this.button1.Text = "保存";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label17.AutoSize = true;
		this.label17.Cursor = System.Windows.Forms.Cursors.Hand;
		this.label17.Font = new System.Drawing.Font("Consolas", 7.8f);
		this.label17.ForeColor = System.Drawing.Color.Blue;
		this.label17.Location = new System.Drawing.Point(400, 327);
		this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(109, 13);
		this.label17.TabIndex = 32;
		this.label17.Text = "检测更新";
		this.label17.Click += new System.EventHandler(label17_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
		this.BackColor = System.Drawing.Color.Linen;
		base.ClientSize = new System.Drawing.Size(520, 349);
		base.Controls.Add(this.label17);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.checkBox3);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.checkBox2);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.textBox5);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.textBox4);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.textBox3);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.pictureBox3);
		base.Controls.Add(this.pictureBox2);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(2);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "设置";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "插件设置";
		base.Load += new System.EventHandler(Settings_Load);
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
