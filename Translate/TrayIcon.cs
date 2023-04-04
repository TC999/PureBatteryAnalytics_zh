// Pure Battery - System Tray Add-On, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// PureBatteryAddOn.TrayIcon
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using PureBatteryAddOn;

internal class TrayIcon
{
	private const string iconFont = "Microsoft Yahei";

	public static int iconFontSize;

	public static int xoffset;

	public static int yoffset;

	public static Color normalColor;

	public static Color chargingColor;

	public static Color lowColor;

	public static int isHideIcon;

	public static int isShowPercent;

	public static string autoStart;

	public static string chargeNotify;

	public static string chargeNotifyValue;

	public static string repeatNotification;

	public static bool showOnce;

	public static string notifiedAtBattery;

	private string batteryPercentage;

	private NotifyIcon notifyIcon;

	private Color batteryColor = Color.Green;

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern bool DestroyIcon(IntPtr handle);

	public TrayIcon()
	{
		PTConfig pTConfig = new PTConfig();
		pTConfig.Load();
		string fontSize = pTConfig.FontSize;
		string xOffset = pTConfig.XOffset;
		string yOffset = pTConfig.YOffset;
		string text = pTConfig.NormalColor;
		string text2 = pTConfig.ChargingColor;
		string text3 = pTConfig.LowColor;
		autoStart = pTConfig.AutoStart;
		try
		{
			iconFontSize = Convert.ToInt32(fontSize);
		}
		catch
		{
			iconFontSize = 15;
		}
		try
		{
			xoffset = Convert.ToInt32(xOffset);
		}
		catch
		{
			xoffset = 0;
		}
		try
		{
			yoffset = Convert.ToInt32(yOffset);
		}
		catch
		{
			yoffset = 0;
		}
		normalColor = (Color)new ColorConverter().ConvertFromString(text);
		chargingColor = (Color)new ColorConverter().ConvertFromString(text2);
		lowColor = (Color)new ColorConverter().ConvertFromString(text3);
		ContextMenu contextMenu = new ContextMenu();
		MenuItem menuItem = new MenuItem();
		MenuItem menuItem2 = new MenuItem();
		MenuItem menuItem3 = new MenuItem();
		notifyIcon = new NotifyIcon();
		contextMenu.MenuItems.AddRange(new MenuItem[3] { menuItem, menuItem2, menuItem3 });
		menuItem.Index = 0;
		menuItem.Text = "插件设置";
		menuItem.Click += settingButton_Click;
		menuItem2.Index = 1;
		menuItem2.Text = "退出";
		menuItem2.Click += exitButton_Click;
		menuItem3.Index = 2;
		menuItem3.Text = "关于";
		menuItem3.Click += aboutButton_Click;
		notifyIcon.ContextMenu = contextMenu;
		batteryPercentage = "?";
		notifyIcon.Visible = true;
		Timer timer = new Timer();
		timer.Tick += timer_Tick;
		timer.Interval = 1000;
		timer.Start();
	}

	private void timer_Tick(object sender, EventArgs e)
	{
		PowerStatus powerStatus = SystemInformation.PowerStatus;
		batteryPercentage = (powerStatus.BatteryLifePercent * 100f).ToString();
		PTConfig pTConfig = new PTConfig();
		pTConfig.Load();
		iconFontSize = Convert.ToInt32(pTConfig.FontSize);
		xoffset = Convert.ToInt32(pTConfig.XOffset);
		yoffset = Convert.ToInt32(pTConfig.YOffset);
		chargeNotify = Convert.ToString(pTConfig.ChargeNotify);
		chargeNotifyValue = Convert.ToString(pTConfig.ChargeNotifyValue);
		repeatNotification = Convert.ToString(pTConfig.RepeatNotification);
		bool flag = ((!chargeNotifyValue.Contains(",")) ? (batteryPercentage == chargeNotifyValue) : chargeNotifyValue.Split(',').Contains(batteryPercentage));
		if (showOnce && flag && chargeNotify == "true")
		{
			notifyIcon.ShowBalloonTip(500, "电池电量" + batteryPercentage + "%", " ", ToolTipIcon.Info);
			showOnce = false;
			notifiedAtBattery = batteryPercentage;
		}
		if (notifiedAtBattery != batteryPercentage)
		{
			showOnce = true;
		}
		if (batteryPercentage == "100")
		{
			iconFontSize = 23;
			xoffset = -10;
			yoffset = 5;
			batteryPercentage = "100";
		}
		notifyIcon.Visible = true;
		if (powerStatus.BatteryChargeStatus.ToString().Contains(BatteryChargeStatus.Charging.ToString()))
		{
			batteryColor = chargingColor;
		}
		else if (powerStatus.BatteryChargeStatus.ToString().Contains(BatteryChargeStatus.Low.ToString()))
		{
			batteryColor = lowColor;
		}
		else
		{
			batteryColor = normalColor;
		}
		using Bitmap bitmap = new Bitmap(DrawText(batteryPercentage, new Font("Microsoft Yahei", iconFontSize), batteryColor, Color.Transparent));
		IntPtr hicon = bitmap.GetHicon();
		try
		{
			using Icon icon = Icon.FromHandle(hicon);
			notifyIcon.Icon = icon;
			notifyIcon.Text = "电池电量" + batteryPercentage + "% - ";
			if (powerStatus.PowerLineStatus == PowerLineStatus.Online)
			{
				notifyIcon.Text += "电源已接通";
			}
			else
			{
				notifyIcon.Text += "正在放电";
			}
		}
		finally
		{
			DestroyIcon(hicon);
		}
	}

	private void settingButton_Click(object sender, EventArgs e)
	{
		new Settings().ShowDialog();
	}

	private void exitButton_Click(object sender, EventArgs e)
	{
		notifyIcon.Visible = false;
		notifyIcon.Dispose();
		Application.Exit();
	}

	private void aboutButton_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("这是纯电池分析应用程序的附加应用程序。", "关于", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
		{
			string input = Registry.ClassesRoot.OpenSubKey("http\\shell\\open\\command\\").GetValue("").ToString();
			MatchCollection matchCollection = new Regex("\"([^\"]+)\"").Matches(input);
			if (matchCollection.Count > 0)
			{
				Process.Start(matchCollection[0].Groups[1].Value, "https://www.microsoft.com/en-us/p/pure-battery-analytics/9nblggh4x4k3?activetab=pivot:overviewtab");
			}
		}
	}

	private Image DrawText(string text, Font font, Color textColor, Color backColor)
	{
		GetImageSize(text, font);
		Image image = new Bitmap(48, 45);
		using Graphics graphics = Graphics.FromImage(image);
		graphics.Clear(backColor);
		using Brush brush = new SolidBrush(textColor);
		graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
		graphics.DrawString(text, font, brush, xoffset, yoffset);
		graphics.Save();
		return image;
	}

	private static SizeF GetImageSize(string text, Font font)
	{
		using Image image = new Bitmap(32, 32);
		using Graphics graphics = Graphics.FromImage(image);
		return graphics.MeasureString(text, font);
	}

	static TrayIcon()
	{
		iconFontSize = 28;
		xoffset = 0;
		yoffset = 0;
		normalColor = Color.FromArgb(255, 255, 255);
		chargingColor = Color.FromArgb(254, 190, 4);
		lowColor = Color.FromArgb(254, 97, 82);
		isHideIcon = 1;
		isShowPercent = 0;
		chargeNotifyValue = "100";
		repeatNotification = "false";
		showOnce = true;
		notifiedAtBattery = "0";
	}
}
