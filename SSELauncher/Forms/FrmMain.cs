using AppInfo;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using vbAccelerator.Components.Shell;
using System.Linq;
using System.Net.Sockets;
using System.Xml;


namespace SSELauncher
{
    public class FrmMain : Form
    {
        public string personaName;
        XmlDocument Config = new XmlDocument();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private PictureBox Btn_AddGame;
        private Panel ButtonPanel;
        private Panel panel10;
        private Panel panel11;
        private Panel panel21;
        private PictureBox Btn_Exit;
        private Panel panel22;
        private Panel panel23;
        private Panel panel18;
        private PictureBox Btn_About;
        private Panel panel19;
        private Panel panel20;
        private Panel panel15;
        private PictureBox Btn_SmartEmuSettings;
        private Panel panel16;
        private Panel panel17;
        private Panel panel12;
        private PictureBox Btn_GameSettings;
        private Panel panel13;
        private Panel panel14;
        private Label label_ipv4;
        private Panel panel25;
        private PictureBox Label_StudioName;
        private Panel panel27;
        private Panel panel26;
        private Panel panel24;
        private Panel panel28;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private Panel ReMovementPanel;

        private delegate void sort();

        public class ToolStripItemComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                ToolStripItem arg_0D_0 = (ToolStripItem)x;
                ToolStripItem toolStripItem = (ToolStripItem)y;
                return string.Compare(arg_0D_0.Text, toolStripItem.Text, true);
            }
        }

        private CAppList m_AppList;

        private CConfig Conf;

        private string AppPath;

        private List<string> AvailableCategories = new List<string>();

        private AppInfoVDF appinfoVDF;

        private string SteamInstallPath;

        private bool FormFullyLoaded;

        private FormWindowState LastWindowState;

        private ContextMenuStrip mnuTrayMenu = new ContextMenuStrip();

        private bool MenuFirstInit = true;

        private IContainer components;

        private ListView lstApps;

        private ImageList imgList;

        private MenuStrip menuStrip;

        private ToolStripMenuItem fileToolStripMenuItem;

        private ToolStripMenuItem addGamesToolStripMenuItem;

        private ToolStripMenuItem deleteGameToolStripMenuItem;

        private ToolStripMenuItem exitToolStripMenuItem;

        private ToolStripMenuItem settingsToolStripMenuItem;

        private ToolStripMenuItem editGameToolStripMenuItem;

        private ContextMenuStrip ctxMenuStrip;

        private ToolStripMenuItem editToolStripMenuItem;

        private ToolStripMenuItem deleteToolStripMenuItem;

        private ToolStripMenuItem launchToolStripMenuItem;

        private ToolStripSeparator toolStripMenuItem3;

        private ToolStripMenuItem helpToolStripMenuItem;

        private ToolStripMenuItem aboutToolStripMenuItem;

        private ToolStripMenuItem fileToolStripMenuItem1;

        private ToolStripMenuItem addGameToolStripMenuItem;

        private ToolStripMenuItem editGameToolStripMenuItem1;

        private ToolStripMenuItem deleteGameToolStripMenuItem1;

        private ToolStripSeparator toolStripMenuItem4;

        private ToolStripMenuItem settingToolStripMenuItem;

        private ToolStripSeparator toolStripMenuItem5;

        private ToolStripMenuItem exitToolStripMenuItem1;

        private ToolStripMenuItem helpToolStripMenuItem1;

        private ToolStripMenuItem aboutToolStripMenuItem1;

        private PictureBox pbDrop;

        private ToolStripMenuItem launchNormallywithoutEmuToolStripMenuItem;

        private ContextMenuStrip ctxMenuViewStrip;

        private ToolStripMenuItem viewToolStripMenuItem;

        private ToolStripMenuItem largeIconToolStripMenuItem;

        private ToolStripMenuItem smallIconToolStripMenuItem;

        private ToolStripMenuItem listToolStripMenuItem;

        private ToolStripMenuItem tileToolStripMenuItem;

        private ToolStripMenuItem addGameToolStripMenuItem1;

        private ToolStripSeparator toolStripMenuItem6;

        private ToolStripSeparator toolStripMenuItem7;

        private ToolStripMenuItem createDesktopShortcutToolStripMenuItem;

        private ToolStripMenuItem renameToolStripMenuItem;

        private ToolStripMenuItem sortByToolStripMenuItem;

        private ToolStripMenuItem nameToolStripMenuItem;

        private ToolStripMenuItem dateAddedToolStripMenuItem;

        private ToolStripMenuItem groupByToolStripMenuItem;

        private ToolStripMenuItem noneToolStripMenuItem;

        private ToolStripMenuItem typeToolStripMenuItem;

        private ToolStripMenuItem categoryToolStripMenuItem;

        private ToolStripMenuItem refreshToolStripMenuItem;

        private ToolStripSeparator toolStripMenuItem8;

        private ToolStripMenuItem hideMissingShortcutToolStripMenuItem;

        private NotifyIcon notifyIcon1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel AppList_panel;
        private PictureBox pictureBox1;
        private PictureBox Btn_close;
        private PictureBox Btn_WinMinimize;
        private PictureBox Btn_FormWin;
        private Panel panel_spc;
        private PictureBox pictureBox5;
        private Panel Logo_Dock;
        private Panel panel4;
        private Panel panel6;
        private Panel panel5;
        private Panel panel9;
        private Panel panel8;
        private Panel panel7;
        private ToolStripMenuItem openFileLocationToolStripMenuItem;

        public FrmMain()
        {

            AppPath = AppDomain.CurrentDomain.BaseDirectory;
            m_AppList = Program.AppList;
            Conf = m_AppList.GetConfig();
            InitializeComponent();
            base.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            notifyIcon1.Icon = base.Icon;
            notifyIcon1.Text = Text;
            m_AppList.EventAppClear += new EventHandler<AppModifiedEventArgs>(OnAppClear);
            m_AppList.EventAppAdded += new EventHandler<AppModifiedEventArgs>(OnAppAdded);
            m_AppList.EventAppDeleted += new EventHandler<AppModifiedEventArgs>(OnAppDeleted);
            BackgroundWorker expr_D1 = new BackgroundWorker();
            expr_D1.DoWork += new DoWorkEventHandler(bwWorker_DoWorkInitVDF);
            expr_D1.RunWorkerAsync();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                Visible = true;
                WindowState = LastWindowState;
                Activate();
            }

            base.WndProc(ref m);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {


            this.IsMdiContainer = true;


            string Host = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostEntry(Host).AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).ToArray();
            string IP = addresses[0].ToString();
            label_ipv4.Text = "IPv4: " + IP;

            //End BloodyAlpha
            editGameToolStripMenuItem.Enabled = false;
            deleteGameToolStripMenuItem.Enabled = false;
            Size = new Size((Conf.WindowSizeX == 0) ? Size.Width : Conf.WindowSizeX, (Conf.WindowSizeY == 0) ? Size.Height : Conf.WindowSizeY);
            Location = new Point((Conf.WindowPosX < 0) ? Location.X : Conf.WindowPosX, (Conf.WindowPosY < 0) ? Location.Y : Conf.WindowPosY);
            pbDrop.Visible = true;
            DragEnter += new DragEventHandler(LstApps_DragEnter);
            DragDrop += new DragEventHandler(lstApps_DragDrop);
            m_AppList.Refresh();
            OnSelectedAppChanged();
            MenuFirstInit = false;
            SortTrayMenu();
            FormFullyLoaded = true;

            AdjustSize();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (!FormFullyLoaded)
            {
                return;
            }

            if (WindowState != FormWindowState.Minimized)
            {
                LastWindowState = WindowState;
            }

            if (Conf.HideToTray)
            {
                Visible = (WindowState != FormWindowState.Minimized);
            }

            AdjustSize();
        }

        private void AdjustSize() => pbDrop.SetBounds(-10, -25, Width + 10, Height + 25);

        private void LstApps_DoubleClick(object sender, EventArgs e) => LaunchApp();

        private void LstApps_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnSelectedAppChanged();
            if (e.KeyChar == '\r')
            {
                LaunchApp();
            }
        }

        private void OnSelectedAppChanged()
        {
            if (lstApps.SelectedItems.Count == 0)
            {
                largeIconToolStripMenuItem.Checked = (lstApps.View == View.LargeIcon);
                smallIconToolStripMenuItem.Checked = (lstApps.View == View.SmallIcon);
                listToolStripMenuItem.Checked = (lstApps.View == View.List);
                tileToolStripMenuItem.Checked = (lstApps.View == View.Tile);
                nameToolStripMenuItem.Checked = (Conf.SortBy == CConfig.ESortBy.SortByName);
                dateAddedToolStripMenuItem.Checked = (Conf.SortBy == CConfig.ESortBy.SortByDateAdded);
                noneToolStripMenuItem.Checked = (Conf.GroupBy == CConfig.EGroupBy.GroupByNone);
                typeToolStripMenuItem.Checked = (Conf.GroupBy == CConfig.EGroupBy.GroupByType);
                categoryToolStripMenuItem.Checked = (Conf.GroupBy == CConfig.EGroupBy.GroupByCategory);
                hideMissingShortcutToolStripMenuItem.Checked = Conf.HideMissingShortcut;
                lstApps.ContextMenuStrip = ctxMenuViewStrip;
                editGameToolStripMenuItem.Enabled = false;
                deleteGameToolStripMenuItem.Enabled = false;
//                Btn_GameSettings.Enabled = false;
                return;
            }
            lstApps.ContextMenuStrip = ctxMenuStrip;
            editGameToolStripMenuItem.Enabled = true;
            deleteGameToolStripMenuItem.Enabled = true;
//            Btn_GameSettings.Enabled = true;
            ListViewItem tag = lstApps.SelectedItems[0];
            CApp app = m_AppList.GetApp(tag);
            if (app != null)
            {
                launchNormallywithoutEmuToolStripMenuItem.Visible = (app.AppId != -1);
            }
        }

        private void LstApps_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lstApps_DragDrop(object sender, DragEventArgs e)
        {
            string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                CApp cApp = new CApp
                {
                    Path = CApp.MakeRelativePath(text, true)
                };
                cApp.GameName = Path.GetFileNameWithoutExtension(cApp.Path);
                cApp.StartIn = CApp.MakeRelativePath(Path.GetDirectoryName(cApp.Path), true);
                if (text.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        using (ShellLink shellLink = new ShellLink(text))
                        {
                            cApp.Path = CApp.MakeRelativePath(shellLink.Target, true);
                            cApp.GameName = Path.GetFileNameWithoutExtension(cApp.Path);
                            cApp.StartIn = CApp.MakeRelativePath(shellLink.WorkingDirectory, true);
                            cApp.CommandLine = shellLink.Arguments;
                        }
                    }
                    catch
                    {
                    }
                }
                if (File.Exists(Path.Combine(CApp.GetAbsolutePath(cApp.StartIn), "bin\\launcher.dll")) || File.Exists(Path.Combine(CApp.GetAbsolutePath(cApp.StartIn), "hl.exe")) || File.Exists(Path.Combine(CApp.GetAbsolutePath(cApp.StartIn), "hlds.exe")) || File.Exists(Path.Combine(CApp.GetAbsolutePath(cApp.StartIn), "hltv.exe")))
                {
                    cApp.CommandLine = "-steam";
                }

                AutoAppConfig(text, cApp);
            }
        }

        private void LstApps_OnItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            OnSelectedAppChanged();
        }

        private void lstApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedAppChanged();
        }

        private void LaunchApp()
        {
            if (lstApps.SelectedItems.Count == 0)
            {
                return;
            }

            var tag = lstApps.SelectedItems[0];
            CApp app = m_AppList.GetApp(tag);

            if (app.AppId == 0)
            {
                if (MessageBox.Show("You need to set up game app id first. You can find your game app id on steam store url: http://store.steampowered.com/app/<AppId> \n\nSetup now?", "Invalid app id", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DoEditGame();
                }

                return;
            }

            if (app.AppId == -1)
            {
                LaunchWithoutEmu(app);

                return;
            }

            FrmMain.WriteIniAndLaunch(app, Conf, null);
        }

        private void OnAppClear(object sender, AppModifiedEventArgs e)
        {
            mnuTrayMenu.Items.Clear();
            PopulateTrayLaunchMenu();
            lstApps.Groups.Clear();
            lstApps.Clear();
            CConfig.ESortBy sortBy = Conf.SortBy;
            if (sortBy == CConfig.ESortBy.SortByName)
            {
                lstApps.Sorting = SortOrder.Ascending;
                return;
            }
            if (sortBy != CConfig.ESortBy.SortByDateAdded)
            {
                return;
            }
            lstApps.Sorting = SortOrder.None;
        }

        private void OnAppAdded(object sender, AppModifiedEventArgs e)
        {
            if (Conf.HideMissingShortcut && !new FileInfo(CApp.GetAbsolutePath(e.app.Path)).Exists)
            {
                return;
            }
            ListViewItem listViewItem = new ListViewItem(e.app.GameName);
            SetListViewItemGroup(e.app, listViewItem);
            DoRefreshCategories(e.app);
            try
            {
                listViewItem.ImageKey = e.app.GetIconHash();
                BackgroundWorker expr_6A = new BackgroundWorker();
                expr_6A.DoWork += new DoWorkEventHandler(bwWorker_DoWork);
                expr_6A.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwWorker_RunWorkerCompleted);
                expr_6A.RunWorkerAsync(new BgParam(listViewItem.GetHashCode().ToString(), e.app));
            }
            catch
            {
            }
            e.tag = lstApps.Items.Add(listViewItem);
            e.app.Tag = e.tag;
            AddTrayLaunchMenu(e.app);
            pbDrop.Visible = false;
            lstApps.Sort();
        }

        private void bwWorker_DoWorkInitVDF(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                    if (registryKey != null)
                    {
                        RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Valve\\Steam");
                        if (registryKey2 != null)
                        {
                            SteamInstallPath = registryKey2.GetValue("InstallPath").ToString();
                            appinfoVDF = new AppInfoVDF(Path.Combine(SteamInstallPath, "appcache\\appinfo.vdf"));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void bwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BgParam bgParam = (BgParam)e.Result;
            if (bgParam.AppIcon != null)
            {
                imgList.Images.Add(bgParam.App.GetIconHash(), bgParam.AppIcon);
            }
            EditTrayLaunchMenu(bgParam.App, true);
        }

        private void bwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BgParam bgParam = (BgParam)e.Argument;
            bgParam.AppIcon = CAppList.GetIcon(bgParam.App);
            e.Result = bgParam;
        }

        private void OnAppDeleted(object sender, AppModifiedEventArgs e)
        {
            DeleteTrayLaunchMenu(e.tag);
            lstApps.Items.Remove((ListViewItem)e.tag);
            if (lstApps.Items.Count > 0)
            {
                pbDrop.Visible = false;
                return;
            }
            pbDrop.Visible = true;
        }

        public static void WriteIniAndLaunch(CApp app, CConfig gconf, string extra_commandline = null)
        {
            var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SmartSteamEmu");
            var launcherSettings = Path.Combine(baseDirectory, "launcher.ini");
            var launcherExecutable = Path.Combine(baseDirectory, "SmartSteamLoader" + (app.Use64Launcher ? "_x64" : "") + ".exe");

            bool lowViolence = (app.LowViolence == -1) ? gconf.LowViolence : Convert.ToBoolean(app.LowViolence);
            bool storageOnAppdata = (app.StorageOnAppdata == -1) ? gconf.StorageOnAppdata : Convert.ToBoolean(app.StorageOnAppdata);
            bool separateStorageByName = (app.SeparateStorageByName == -1) ? gconf.SeparateStorageByName : Convert.ToBoolean(app.SeparateStorageByName);
            bool automaticallyJoinInvite = (app.AutomaticallyJoinInvite == -1) ? gconf.AutomaticallyJoinInvite : Convert.ToBoolean(app.AutomaticallyJoinInvite);
            bool enableHTTP = (app.EnableHTTP == -1) ? gconf.EnableHTTP : Convert.ToBoolean(app.EnableHTTP);
            bool enableInGameVoice = (app.EnableInGameVoice == -1) ? gconf.EnableInGameVoice : Convert.ToBoolean(app.EnableInGameVoice);
            bool enableLobbyFilter = (app.EnableLobbyFilter == -1) ? gconf.EnableLobbyFilter : Convert.ToBoolean(app.EnableLobbyFilter);
            bool disableFriendList = (app.DisableFriendList == -1) ? gconf.DisableFriendList : Convert.ToBoolean(app.DisableFriendList);
            bool disableLeaderboard = (app.DisableLeaderboard == -1) ? gconf.DisableLeaderboard : Convert.ToBoolean(app.DisableLeaderboard);
            bool securedServer = (app.SecuredServer == -1) ? gconf.SecuredServer : Convert.ToBoolean(app.SecuredServer);
            bool enableVR = (app.VR == -1) ? gconf.EnableVR : Convert.ToBoolean(app.VR);
            bool offline = (app.Offline == -1) ? gconf.Offline : Convert.ToBoolean(app.Offline);
            bool enableOverlay = (app.EnableOverlay == -1) ? gconf.EnableOverlay : Convert.ToBoolean(app.EnableOverlay);
            bool enableOnlinePlay = (app.EnableOnlinePlay == -1) ? gconf.EnableOnlinePlay : Convert.ToBoolean(app.EnableOnlinePlay);
            bool enableLog = ((app.EnableDebugLogging == -1) ? gconf.EnableLog : Convert.ToBoolean(app.EnableDebugLogging));

            long manualSteamID = (app.ManualSteamId == -1L) ? gconf.ManualSteamId : app.ManualSteamId;

            string avatarPath = string.IsNullOrEmpty(app.AvatarPath) ? ((gconf.AvatarPath == "avatar.png") ? gconf.AvatarPath : CApp.GetAbsolutePath(gconf.AvatarPath)) : CApp.GetAbsolutePath(app.AvatarPath);
            string personaName = string.IsNullOrEmpty(app.PersonaName) ? gconf.PersonaName : app.PersonaName;
            string steamIdGeneration = string.IsNullOrEmpty(app.SteamIdGeneration) ? gconf.SteamIdGeneration : app.SteamIdGeneration;
            string quickJoinHotkey = string.IsNullOrEmpty(app.QuickJoinHotkey) ? gconf.QuickJoinHotkey : app.QuickJoinHotkey;
            string language = string.IsNullOrEmpty(app.Language) ? gconf.Language : app.Language;
            string overlayLanguage = gconf.OverlayLanguage;

            string masterServerAddress = "";
            foreach (var server in gconf.MasterServerAddress)
            {
                masterServerAddress += server + " ";
            }


            if (!string.IsNullOrWhiteSpace(overlayLanguage))
            {
                if (language.Equals("Simplified Chinese", StringComparison.OrdinalIgnoreCase))
                {
                    language = "schinese";
                }
                else if (language.Equals("Traditional Chinese", StringComparison.OrdinalIgnoreCase))
                {
                    language = "tchinese";
                }
            }

            if (!string.IsNullOrWhiteSpace(overlayLanguage))
            {
                if (overlayLanguage.Equals("Simplified Chinese", StringComparison.OrdinalIgnoreCase))
                {
                    overlayLanguage = "schinese";
                }
                else if (overlayLanguage.Equals("Traditional Chinese", StringComparison.OrdinalIgnoreCase))
                {
                    overlayLanguage = "tchinese";
                }
            }


            try
            {
                // Empty log file
                if (enableLog && gconf.CleanLog)
                {
                    var logFile = Path.Combine(baseDirectory, "SmartSteamEmu.log");

                    if (File.Exists(logFile))
                    {
                        using (new FileStream(logFile, FileMode.Truncate)) { }
                    }
                }

                using (var sw = new StreamWriter(new FileStream(launcherSettings, FileMode.Create, FileAccess.ReadWrite), Encoding.Unicode))
                {
                    sw.WriteLine("# This file is generated by and will be overwritten by SSELauncher");
                    sw.WriteLine("#");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("[Launcher]");
                    sw.WriteLine("Target = " + CApp.GetAbsolutePath(app.Path));
                    sw.WriteLine("StartIn = " + CApp.GetAbsolutePath(app.StartIn));
                    sw.WriteLine("CommandLine = " + app.CommandLine + (string.IsNullOrEmpty(extra_commandline) ? "" : extra_commandline));
                    sw.WriteLine("SteamClientPath = " + Path.Combine(baseDirectory, "SmartSteamEmu.dll"));
                    sw.WriteLine("SteamClientPath64 = " + Path.Combine(baseDirectory, "SmartSteamEmu64.dll"));
                    sw.WriteLine("Persist = " + (app.Persist ? 1 : 0));
                    sw.WriteLine("ParanoidMode = " + (gconf.ParanoidMode ? 1 : 0));
                    sw.WriteLine("InjectDll = " + (app.InjectDll ? 1 : 0));
                    sw.WriteLine("");

                    sw.WriteLine("[SmartSteamEmu]");
                    sw.WriteLine("AvatarFilename = " + avatarPath);
                    sw.WriteLine("PersonaName = " + personaName);
                    sw.WriteLine("AppId = " + app.AppId);
                    sw.WriteLine("SteamIdGeneration = " + steamIdGeneration);
                    sw.WriteLine("ManualSteamId = " + manualSteamID);
                    sw.WriteLine("Language = " + language);
                    sw.WriteLine("LowViolence = " + lowViolence.ToString());
                    sw.WriteLine("StorageOnAppdata = " + storageOnAppdata.ToString());
                    sw.WriteLine("SeparateStorageByName = " + separateStorageByName.ToString());
                    if (!string.IsNullOrEmpty(app.RemoteStoragePath) && !string.IsNullOrWhiteSpace(app.RemoteStoragePath))
                    {
                        sw.WriteLine("RemoteStoragePath = " + CApp.GetAbsolutePath(app.RemoteStoragePath));
                    }
                    sw.WriteLine("AutomaticallyJoinInvite = " + automaticallyJoinInvite.ToString());
                    sw.WriteLine("EnableHTTP = " + enableHTTP.ToString());
                    sw.WriteLine("EnableInGameVoice = " + enableInGameVoice.ToString());
                    sw.WriteLine("EnableLobbyFilter = " + enableLobbyFilter.ToString());
                    sw.WriteLine("DisableFriendList = " + disableFriendList.ToString());
                    sw.WriteLine("DisableLeaderboard = " + disableLeaderboard.ToString());
                    sw.WriteLine("DisableGC = " + app.DisableGC.ToString());
                    sw.WriteLine("SecuredServer = " + securedServer.ToString());
                    sw.WriteLine("VR = " + enableVR.ToString());
                    sw.WriteLine("Offline = " + offline.ToString());
                    sw.WriteLine("QuickJoinHotkey = " + quickJoinHotkey);
                    sw.WriteLine("MasterServer = " + masterServerAddress);
                    sw.WriteLine("MasterServerGoldSrc = " + masterServerAddress);
                    sw.WriteLine(app.Extras);
                    sw.WriteLine("");

                    sw.WriteLine("[Achievements]");
                    sw.WriteLine("FailOnNonExistenceStats = " + Convert.ToBoolean(app.FailOnNonExistenceStats).ToString());
                    sw.WriteLine("");

                    sw.WriteLine("[SSEOverlay]");
                    sw.WriteLine("DisableOverlay = " + (!Convert.ToBoolean(enableOverlay)).ToString());
                    sw.WriteLine("OnlineMode = " + Convert.ToBoolean(enableOnlinePlay).ToString());
                    sw.WriteLine("Language = " + overlayLanguage);
                    sw.WriteLine("ScreenshotHotkey = " + gconf.OverlayScreenshotHotkey);
                    sw.WriteLine("HookRefCount = " + app.EnableHookRefCount.ToString());
                    sw.WriteLine("OnlineKey = " + (string.IsNullOrWhiteSpace(app.OnlineKey) ? gconf.OnlineKey : app.OnlineKey));
                    sw.WriteLine("");

                    sw.WriteLine("[DirectPatch]");
                    foreach (var patch in app.DirectPatchList)
                    {
                        sw.WriteLine(patch);
                    }
                    sw.WriteLine("");


                    sw.WriteLine("[Debug]");
                    sw.WriteLine("EnableLog = " + enableLog.ToString());
                    sw.WriteLine("MarkLogHotkey = " + gconf.MarkLogHotkey);
                    sw.WriteLine("LogFilter = " + gconf.LogFilter);
                    sw.WriteLine("Minidump = " + gconf.Minidump.ToString());
                    sw.WriteLine("");


                    sw.WriteLine("[DLC]");
                    sw.WriteLine("Default = " + app.DefaultDlcSubscribed.ToString());
                    foreach (var dlc in app.DlcList)
                    {
                        if (!dlc.Disabled)
                        {
                            sw.WriteLine(dlc.DlcId + " = " + dlc.DlcName);
                        }
                    }
                    sw.WriteLine("");
                    string broadcastAddress = "";
                    if (app.ListenPort == -1)
                    {
                        foreach (var address in gconf.BroadcastAddress)
                        {
                            broadcastAddress += address + " ";
                        }
                    }
                    else
                    {
                        foreach (var address in app.BroadcastAddress)
                        {
                            broadcastAddress += address + " ";
                        }
                    }
                    int num2 = (app.ListenPort == -1) ? gconf.ListenPort : app.ListenPort;
                    int num3 = (app.MaximumPort == -1) ? gconf.MaximumPort : app.MaximumPort;
                    int num4 = (app.DiscoveryInterval == -1) ? gconf.DiscoveryInterval : app.DiscoveryInterval;
                    int num5 = (app.MaximumConnection == -1) ? gconf.MaximumConnection : app.MaximumConnection;
                    sw.WriteLine("[Networking]");
                    sw.WriteLine("BroadcastAddress = " + broadcastAddress);
                    sw.WriteLine("ListenPort = " + num2);
                    sw.WriteLine("MaximumPort = " + num3);
                    sw.WriteLine("DiscoveryInterval = " + num4);
                    sw.WriteLine("MaximumConnection = " + num5);
                    sw.WriteLine("");
                    sw.WriteLine("[PlayerManagement]");
                    sw.WriteLine("AllowAnyoneConnect = " + gconf.AllowAnyoneConnect.ToString());
                    sw.WriteLine("AdminPassword = " + gconf.AdminPass);
                    foreach (string current6 in gconf.BanList)
                    {
                        sw.WriteLine(current6 + " = 0");
                    }
                    sw.WriteLine("");
                    using (Process.Start(new ProcessStartInfo
                    {
                        CreateNoWindow = false,
                        UseShellExecute = false,
                        FileName = launcherExecutable,
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = "\"" + launcherSettings + "\""
                    }))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat(new string[]
                {
                    ex.Message,
                    "\n\nPath search by launcher:\n\n",
                    launcherSettings,
                    "\n\n",
                    launcherExecutable
                }), "Unable to launch games");
            }
        }

        private void DoRefreshCategories(CApp app)
        {
            if (!string.IsNullOrEmpty(app.Category))
            {
                bool flag = false;
                using (List<string>.Enumerator enumerator = AvailableCategories.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Current == app.Category)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    AvailableCategories.Add(app.Category);
                }
            }
        }

        private void DoEditGame()
        {
            if (lstApps.SelectedItems.Count == 0)
            {
                return;
            }

            var listViewItem = lstApps.SelectedItems[0];

            CApp app = m_AppList.GetApp(listViewItem);

            if (app == null)
            {
                return;
            }

            var frmAppSetting = new FrmAppSetting
            {
                CategoryList = AvailableCategories
            };

            frmAppSetting.SetEditApp(app, Conf);

            DoRefreshCategories(app);

            if (frmAppSetting.ShowDialog() == DialogResult.OK)
            {
                listViewItem.Text = app.GameName;
                listViewItem.ImageKey = app.GetIconHash();
                if (imgList.Images[app.GetIconHash()] == null)
                {
                    try
                    {
                        var bw = new BackgroundWorker();

                        bw.DoWork += new DoWorkEventHandler(bwWorker_DoWork);
                        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwWorker_RunWorkerCompleted);
                        bw.RunWorkerAsync(new BgParam(listViewItem.ImageKey, app));
                    }
                    catch
                    {
                    }
                }
                SetListViewItemGroup(app, listViewItem);
                EditTrayLaunchMenu(app, false);

            }
            m_AppList.Save();
            lstApps.Sort();
        }

        private void DoDeleteGame()
        {

            if (lstApps.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this item(s)?", "Delete Game From List", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            foreach (ListViewItem tag in lstApps.SelectedItems)
            {
                m_AppList.DeleteApp(tag);
            }
        }

        private void DoSorting()
        {
            CConfig.ESortBy sortBy = Conf.SortBy;
            if (sortBy != CConfig.ESortBy.SortByName)
            {
                if (sortBy == CConfig.ESortBy.SortByDateAdded)
                {
                    lstApps.Sorting = SortOrder.None;
                }
            }
            else
            {
                lstApps.Sorting = SortOrder.Ascending;
            }
            lstApps.Sort();
        }

        private void SetListViewItemGroup(CApp app, ListViewItem lvi)
        {
            CConfig.EGroupBy groupBy = Conf.GroupBy;
            if (groupBy == CConfig.EGroupBy.GroupByType)
            {
                ListViewGroup listViewGroup = null;
                ListViewGroup listViewGroup2 = null;
                if (lstApps.Groups.Count == 0)
                {
                    listViewGroup = new ListViewGroup("Steam", HorizontalAlignment.Left);
                    listViewGroup2 = new ListViewGroup("Non-steam", HorizontalAlignment.Left);
                    lstApps.Groups.Add(listViewGroup);
                    lstApps.Groups.Add(listViewGroup2);
                }
                foreach (ListViewGroup listViewGroup3 in lstApps.Groups)
                {
                    if (listViewGroup3.Header == "Steam")
                    {
                        listViewGroup = listViewGroup3;
                    }
                    else
                    {
                        listViewGroup2 = listViewGroup3;
                    }
                }
                lvi.Group = ((app.AppId >= 0) ? listViewGroup : listViewGroup2);
                return;
            }
            if (groupBy != CConfig.EGroupBy.GroupByCategory)
            {
                return;
            }
            ListViewGroup listViewGroup4 = null;
            if (!string.IsNullOrEmpty(app.Category))
            {
                foreach (ListViewGroup listViewGroup5 in lstApps.Groups)
                {
                    if (listViewGroup5.Header == app.Category)
                    {
                        listViewGroup4 = listViewGroup5;
                    }
                }
                if (listViewGroup4 == null)
                {
                    listViewGroup4 = new ListViewGroup(app.Category, HorizontalAlignment.Left);
                    lstApps.Groups.Add(listViewGroup4);
                }
                lvi.Group = listViewGroup4;
            }
        }

        private void OnAddGame()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Game executables (*.exe)|*.exe;*.bat;*.cmd;*.lnk|All Files|*.*",
                FilterIndex = 1,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                CApp cApp = new CApp();

                if (openFileDialog.FileName.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        using (ShellLink shellLink = new ShellLink(openFileDialog.FileName))
                        {
                            cApp.Path = CApp.MakeRelativePath(shellLink.Target, true);
                            cApp.GameName = Path.GetFileNameWithoutExtension(cApp.Path);
                            cApp.StartIn = CApp.MakeRelativePath(shellLink.WorkingDirectory, true);
                            cApp.CommandLine = shellLink.Arguments;
                        }
                        goto IL_DC;
                    }
                    catch
                    {
                        goto IL_DC;
                    }
                }

                cApp.Path = CApp.MakeRelativePath(openFileDialog.FileName, true);
                cApp.GameName = Path.GetFileNameWithoutExtension(cApp.Path);
                cApp.StartIn = CApp.MakeRelativePath(Path.GetDirectoryName(openFileDialog.FileName), true);

            IL_DC:
                if (File.Exists(Path.Combine(CApp.GetAbsolutePath(cApp.StartIn), "bin\\launcher.dll")) || File.Exists(Path.Combine(CApp.GetAbsolutePath(cApp.StartIn), "hl.exe")) || File.Exists(Path.Combine(CApp.GetAbsolutePath(cApp.StartIn), "hlds.exe")) || File.Exists(Path.Combine(CApp.GetAbsolutePath(cApp.StartIn), "hltv.exe")))
                {
                    cApp.CommandLine = "-steam";
                }

                AutoAppConfig(CApp.GetAbsolutePath(cApp.Path), cApp);
            }
        }

        private void AutoAppConfig(string path, CApp app)
        {
            if (appinfoVDF == null)
            {
                var frmAppSetting = new FrmAppSetting
                {
                    CategoryList = AvailableCategories
                };

                frmAppSetting.SetEditApp(app, Conf);

                DoRefreshCategories(app);

                if (frmAppSetting.ShowDialog() == DialogResult.OK)
                {
                    m_AppList.AddApp(app);
                }

                m_AppList.Save();

                return;
            }

            List<CApp> list = new List<CApp>();
            List<AppInfoItem> list2 = new List<AppInfoItem>();
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                string text = fileInfo.DirectoryName;
                List<AppConfig> list3 = new List<AppConfig>();
                int num = text.LastIndexOf("\\");
                if (num > -1)
                {
                    AppConfig item = new AppConfig(text, text.Substring(num + 1), fileInfo.FullName.Substring(text.Length + 1));
                    list3.Add(item);
                    text = text.Substring(0, num);
                }
                num = text.LastIndexOf("\\");
                if (num > -1)
                {
                    AppConfig item2 = new AppConfig(text, text.Substring(num + 1), fileInfo.FullName.Substring(text.Length + 1));
                    list3.Add(item2);
                    text = text.Substring(0, num);
                }
                num = text.LastIndexOf("\\");
                if (num > -1)
                {
                    AppConfig item3 = new AppConfig(text, text.Substring(num + 1), fileInfo.FullName.Substring(text.Length + 1));
                    list3.Add(item3);
                    text = text.Substring(0, num);
                }
                foreach (AppConfig current in list3)
                {
                    List<AppInfoItem> appInfoItem = appinfoVDF.GetAppInfoItem(current.Folder, current.Exe);
                    if (appInfoItem.Count > 0)
                    {
                        current.Matched = true;
                        list2.AddRange(appInfoItem);
                    }
                }
                foreach (AppInfoItem current2 in list2)
                {
                    string keyValue = current2.AppInfoKey.GetKeyValue("gamedir");
                    foreach (AppConfig current3 in list3)
                    {
                        if (current3.Matched)
                        {
                            AppInfoItemKey key = current2.AppInfoKey.GetKey("launch", current2.AppInfoKey);
                            string keyValue2 = current2.AppInfoKey.GetKeyValue("name");
                            bool hasGameDir = Directory.Exists(Path.Combine(current3.Path, keyValue));
                            if (key != null)
                            {
                                foreach (AppInfoItemKey current4 in key.keys)
                                {
                                    string text2 = current4.GetKeyValue("oslist").ToLower();
                                    if (text2 == "" || text2.ToLower() == "windows")
                                    {
                                        string path2 = current3.Path;
                                        CApp cApp = new CApp(app)
                                        {
                                            AppId = (int)current2.AppID
                                        };
                                        string text3 = current4.GetKeyValue("workingdir");
                                        if (text3.Length > 0)
                                        {
                                            text3 = Path.Combine(path2, text3);
                                        }
                                        string keyValue3 = current4.GetKeyValue("description");
                                        string gameName = keyValue2;
                                        if (keyValue3.Length > 0)
                                        {
                                            gameName = keyValue3;
                                        }
                                        cApp.Path = Path.Combine(path2, current4.GetKeyValue("executable"));
                                        cApp.CommandLine = current4.GetKeyValue("arguments");
                                        cApp.GameName = gameName;
                                        cApp.StartIn = (string.IsNullOrEmpty(text3) ? path2 : text3);
                                        cApp.HasGameDir = hasGameDir;
                                        list.Add(cApp);
                                    }
                                }
                                if (list.Count == 1)
                                {
                                    list[0].GameName = keyValue2;
                                }
                            }
                        }
                    }
                }
            }
            if (list.Count <= 1)
            {
                try
                {
                    app.Copy(list[0]);
                }
                catch
                {
                }

                var frmAppSetting = new FrmAppSetting
                {
                    CategoryList = AvailableCategories
                };

                frmAppSetting.SetEditApp(app, Conf);

                DoRefreshCategories(app);

                if (frmAppSetting.ShowDialog() == DialogResult.OK)
                {
                    m_AppList.AddApp(app);
                }
            }
            else
            {
                var frmAppMulti = new FrmAppMulti
                {
                    Apps = list
                };

                var dialogResult3 = frmAppMulti.ShowDialog();

                if (dialogResult3 == DialogResult.Yes)
                {
                    foreach (var currentApp in frmAppMulti.SelectedApps)
                    {
                        m_AppList.AddApp(currentApp);
                    }
                }
                if (dialogResult3 == DialogResult.No)
                {
                    if (frmAppMulti.SelectedApp != null)
                    {
                        app.Copy(frmAppMulti.SelectedApp);
                    }

                    var frmAppSetting = new FrmAppSetting
                    {
                        CategoryList = AvailableCategories
                    };

                    frmAppSetting.SetEditApp(app, Conf);

                    dialogResult3 = frmAppSetting.ShowDialog();

                    DoRefreshCategories(app);

                    if (dialogResult3 == DialogResult.OK)
                    {
                        m_AppList.AddApp(app);
                    }
                }
            }
            m_AppList.Save();
        }

        private void addGamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddGame();
        }

        private void editGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoEditGame();
        }

        private void deleteGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteGame();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings expr_05 = new FrmSettings();
            expr_05.SetConfig(Conf);
            expr_05.ShowDialog();
            expr_05.Dispose();
            m_AppList.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoEditGame();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteGame();
        }

        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchApp();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout expr_05 = new FrmAbout();


            expr_05.ShowDialog();
            expr_05.Dispose();



        }

        private void addGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void launchNormallywithoutEmuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstApps.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem tag = lstApps.SelectedItems[0];
            CApp app = m_AppList.GetApp(tag);
            LaunchWithoutEmu(app);
        }

        private void LaunchWithoutEmu(CApp app)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = true,
                FileName = CApp.GetAbsolutePath(app.Path),
                WorkingDirectory = CApp.GetAbsolutePath(app.StartIn),
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = app.CommandLine
            };
            try
            {
                using (Process.Start(processStartInfo))
                {
                }
            }
            catch
            {
            }
        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstApps.View = View.LargeIcon;
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstApps.View = View.SmallIcon;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstApps.View = View.List;
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstApps.View = View.Tile;
        }

        private void lstApps_OnMouseUp(object sender, MouseEventArgs e)
        {
            OnSelectedAppChanged();
        }

        private void lstApps_KeyUp(object sender, KeyEventArgs e)
        {
            OnSelectedAppChanged();
        }

        private void addGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OnAddGame();
        }

        private void createDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem tag = lstApps.SelectedItems[0];
                CApp app = m_AppList.GetApp(tag);
                Regex arg_3C_0 = new Regex("[\\\\/?:*?\"<>|]");
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string str = arg_3C_0.Replace(app.GameName, "");
                string linkFile = Path.Combine(folderPath, str + ".lnk");
                using (ShellLink shellLink = new ShellLink())
                {
                    if (app.AppId != -1)
                    {
                        shellLink.Target = Application.ExecutablePath;
                        shellLink.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                        shellLink.Arguments = "-appid " + app.AppId;
                        shellLink.IconPath = CApp.GetAbsolutePath(string.IsNullOrEmpty(app.IconPath) ? app.Path : app.IconPath);
                        shellLink.Description = "Play " + app.GameName;
                        shellLink.Save(linkFile);
                    }
                    else
                    {
                        shellLink.Target = CApp.GetAbsolutePath(app.Path);
                        shellLink.WorkingDirectory = Path.GetDirectoryName(CApp.GetAbsolutePath(app.Path));
                        shellLink.Arguments = app.CommandLine;
                        shellLink.IconPath = CApp.GetAbsolutePath(string.IsNullOrEmpty(app.IconPath) ? app.Path : app.IconPath);
                        shellLink.Description = "Run " + app.GameName;
                        shellLink.Save(linkFile);
                    }
                }
            }
            catch (Exception arg_177_0)
            {
                MessageBox.Show(arg_177_0.Message, "Error creating shortcut");
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstApps.SelectedItems.Count > 0)
            {
                lstApps.SelectedItems[0].BeginEdit();
            }
        }

        private void lstApps_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            CApp app = m_AppList.GetApp(lstApps.Items[e.Item]);
            if (app == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(e.Label) || string.IsNullOrWhiteSpace(e.Label))
            {
                lstApps.Items[e.Item].Text = app.GameName;
                return;
            }
            app.GameName = e.Label;
            EditTrayLaunchMenu(app, false);
            lstApps.BeginInvoke(new FrmMain.sort(lstApps.Sort));
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_AppList.Refresh();
        }

        private void nameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conf.SortBy = CConfig.ESortBy.SortByName;
            DoSorting();
        }

        private void dateAddedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conf.SortBy = CConfig.ESortBy.SortByDateAdded;
            DoSorting();
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conf.GroupBy = CConfig.EGroupBy.GroupByNone;
            m_AppList.Refresh();
        }

        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conf.GroupBy = CConfig.EGroupBy.GroupByType;
            m_AppList.Refresh();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conf.GroupBy = CConfig.EGroupBy.GroupByCategory;
            m_AppList.Refresh();
        }

        private void FrmMain_Closing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Icon = null;
            if (WindowState == FormWindowState.Normal)
            {
                Conf.WindowSizeX = Size.Width;
                Conf.WindowSizeY = Size.Height;
                Conf.WindowPosX = Location.X;
                Conf.WindowPosY = Location.Y;
                return;
            }
            Conf.WindowSizeX = base.RestoreBounds.Size.Width;
            Conf.WindowSizeY = base.RestoreBounds.Size.Height;
            Conf.WindowPosX = base.RestoreBounds.Location.X;
            Conf.WindowPosY = base.RestoreBounds.Location.Y;
        }

        private void hideMissingShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conf.HideMissingShortcut = !Conf.HideMissingShortcut;
            m_AppList.Refresh();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = LastWindowState;
            Activate();
        }

        private void NotifyMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            string text = toolStripMenuItem.Text;
            if (text == "About")
            {
                aboutToolStripMenuItem_Click(sender, e);
                return;
            }
            if (text == "Exit")
            {
                Application.Exit();
                return;
            }
            if (text == "Settings")
            {
                settingsToolStripMenuItem_Click(sender, e);
                return;
            }
            if (!(text == "Open"))
            {
                TrayLaunchApp(toolStripMenuItem);
                return;
            }
            Visible = true;
            WindowState = LastWindowState;
            Activate();
        }

        private void TrayLaunchApp(ToolStripMenuItem menuObject)
        {
            CApp app = m_AppList.GetApp(menuObject.Tag);
            if (app != null)
            {
                if (app.AppId == -1)
                {
                    LaunchWithoutEmu(app);
                    return;
                }
                FrmMain.WriteIniAndLaunch(app, Conf, null);
            }
        }

        private void AddTrayLaunchMenu(CApp app)
        {
            if (!string.IsNullOrWhiteSpace(app.Category))
            {
                ToolStripItem[] array = mnuTrayMenu.Items.Find(app.Category, true);
                ToolStripMenuItem toolStripMenuItem;
                if (array.Length < 1)
                {
                    toolStripMenuItem = new ToolStripMenuItem
                    {
                        Name = app.Category,
                        Text = app.Category
                    };

                    mnuTrayMenu.Items.Add(toolStripMenuItem);
                }
                else
                {
                    toolStripMenuItem = (ToolStripMenuItem)array[0];
                }
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
                {
                    Name = app.GameName,
                    Text = app.GameName,
                    Tag = app.Tag,
                    Image = imgList.Images[app.GetIconHash()]
                };

                toolStripMenuItem2.Click += new EventHandler(NotifyMenu_Click);
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                ResortToolStripItemCollection(toolStripMenuItem.DropDownItems);
            }
            else if (app.AppId == -1)
            {
                ToolStripItem[] array2 = mnuTrayMenu.Items.Find("Non-Steam", true);
                ToolStripMenuItem toolStripMenuItem;
                if (array2.Length < 1)
                {
                    toolStripMenuItem = new ToolStripMenuItem();
                    toolStripMenuItem.Name = "Non-Steam";
                    toolStripMenuItem.Text = "Non-Steam";
                    mnuTrayMenu.Items.Add(toolStripMenuItem);
                    mnuTrayMenu.Refresh();
                }
                else
                {
                    toolStripMenuItem = (ToolStripMenuItem)array2[0];
                }
                ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                toolStripMenuItem3.Name = app.GameName;
                toolStripMenuItem3.Text = app.GameName;
                toolStripMenuItem3.Tag = app.Tag;
                toolStripMenuItem3.Image = imgList.Images[app.GetIconHash()];
                toolStripMenuItem3.Click += new EventHandler(NotifyMenu_Click);
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                ResortToolStripItemCollection(toolStripMenuItem.DropDownItems);
            }
            else
            {
                ToolStripItem[] array3 = mnuTrayMenu.Items.Find("Steam", true);
                ToolStripMenuItem toolStripMenuItem;
                if (array3.Length < 1)
                {
                    toolStripMenuItem = new ToolStripMenuItem();
                    toolStripMenuItem.Name = "Steam";
                    toolStripMenuItem.Text = "Steam";
                    mnuTrayMenu.Items.Add(toolStripMenuItem);
                }
                else
                {
                    toolStripMenuItem = (ToolStripMenuItem)array3[0];
                }
                ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
                toolStripMenuItem4.Name = app.GameName;
                toolStripMenuItem4.Text = app.GameName;
                toolStripMenuItem4.Tag = app.Tag;
                toolStripMenuItem4.Image = imgList.Images[app.GetIconHash()];
                toolStripMenuItem4.Click += new EventHandler(NotifyMenu_Click);
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem4);
                ResortToolStripItemCollection(toolStripMenuItem.DropDownItems);
            }
            if (!MenuFirstInit)
            {
                SortTrayMenu();
            }
        }

        private void ResortToolStripItemCollection(ToolStripItemCollection coll)
        {
            if (MenuFirstInit)
            {
                return;
            }
            ArrayList expr_0F = new ArrayList(coll);
            expr_0F.Sort(new FrmMain.ToolStripItemComparer());
            coll.Clear();
            foreach (ToolStripItem value in expr_0F)
            {
                coll.Add(value);
            }
        }

        private void SortTrayMenu()
        {
            ArrayList arrayList = new ArrayList();
            ToolStripItem toolStripItem = null;
            ToolStripItem toolStripItem2 = null;
            foreach (ToolStripItem toolStripItem3 in mnuTrayMenu.Items)
            {
                if (toolStripItem3.GetType() == typeof(ToolStripSeparator))
                {
                    arrayList.Add(toolStripItem3);
                }
                else
                {
                    string text = toolStripItem3.Text;
                    if (!(text == "Steam"))
                    {
                        if (!(text == "Non-Steam"))
                        {
                            if (text == "About" || text == "Exit" || text == "Settings" || text == "Open")
                            {
                                arrayList.Add(toolStripItem3);
                            }
                        }
                        else
                        {
                            toolStripItem2 = toolStripItem3;
                        }
                    }
                    else
                    {
                        toolStripItem = toolStripItem3;
                    }
                }
            }
            if (toolStripItem != null)
            {
                mnuTrayMenu.Items.Add(toolStripItem);
            }
            if (toolStripItem2 != null)
            {
                mnuTrayMenu.Items.Add(toolStripItem2);
            }
            foreach (ToolStripItem value in arrayList)
            {
                mnuTrayMenu.Items.Add(value);
            }
        }

        private void EditTrayLaunchMenu(CApp app, bool backgroundLoadingOnly)
        {
            ToolStripMenuItem toolStripMenuItem = FindMenuByTag(app.Tag, mnuTrayMenu.Items);
            if (toolStripMenuItem == null)
            {
                return;
            }
            if (imgList.Images[app.GetIconHash()] != null)
            {
                toolStripMenuItem.Image = imgList.Images[app.GetIconHash()];
            }
            if (backgroundLoadingOnly)
            {
                return;
            }
            toolStripMenuItem.Text = app.GameName;
            toolStripMenuItem.Name = app.GameName;
            if (toolStripMenuItem.OwnerItem != null)
            {
                ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)toolStripMenuItem.OwnerItem;
                toolStripMenuItem2.DropDownItems.Remove(toolStripMenuItem);
                if (toolStripMenuItem2.DropDownItems.Count < 1)
                {
                    mnuTrayMenu.Items.Remove(toolStripMenuItem2);
                }
            }
            ToolStripMenuItem toolStripMenuItem3;
            if (app.Category != null && app.Category != "")
            {
                ToolStripItem[] array = mnuTrayMenu.Items.Find(app.Category, true);
                if (array.Length < 1)
                {
                    toolStripMenuItem3 = new ToolStripMenuItem();
                    toolStripMenuItem3.Name = app.Category;
                    toolStripMenuItem3.Text = app.Category;
                    mnuTrayMenu.Items.Add(toolStripMenuItem3);
                }
                else
                {
                    toolStripMenuItem3 = (ToolStripMenuItem)array[0];
                }
            }
            else if (app.AppId == -1)
            {
                ToolStripItem[] array2 = mnuTrayMenu.Items.Find("Non-Steam", true);
                if (array2.Length < 1)
                {
                    toolStripMenuItem3 = new ToolStripMenuItem();
                    toolStripMenuItem3.Name = "Non-Steam";
                    toolStripMenuItem3.Text = "Non-Steam";
                    mnuTrayMenu.Items.Add(toolStripMenuItem3);
                    mnuTrayMenu.Refresh();
                }
                else
                {
                    toolStripMenuItem3 = (ToolStripMenuItem)array2[0];
                }
            }
            else
            {
                ToolStripItem[] array3 = mnuTrayMenu.Items.Find("Steam", true);
                if (array3.Length < 1)
                {
                    toolStripMenuItem3 = new ToolStripMenuItem();
                    toolStripMenuItem3.Name = "Steam";
                    toolStripMenuItem3.Text = "Steam";
                    mnuTrayMenu.Items.Add(toolStripMenuItem3);
                }
                else
                {
                    toolStripMenuItem3 = (ToolStripMenuItem)array3[0];
                }
            }
            if (toolStripMenuItem3 != null)
            {
                toolStripMenuItem3.DropDownItems.Add(toolStripMenuItem);
            }
            if (toolStripMenuItem.OwnerItem != null)
            {
                ResortToolStripItemCollection(((ToolStripMenuItem)toolStripMenuItem.OwnerItem).DropDownItems);
            }
            SortTrayMenu();
        }

        private void DeleteTrayLaunchMenu(object tag)
        {
            ToolStripItem toolStripItem = FindMenuByTag(tag, mnuTrayMenu.Items);
            if (toolStripItem == null)
            {
                return;
            }
            ToolStripItem ownerItem = toolStripItem.OwnerItem;
            if (ownerItem == null)
            {
                mnuTrayMenu.Items.Remove(toolStripItem);
                return;
            }
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)ownerItem;
            toolStripMenuItem.DropDownItems.Remove(toolStripItem);
            if (toolStripMenuItem.DropDownItems.Count < 1)
            {
                mnuTrayMenu.Items.Remove(toolStripMenuItem);
            }
        }

        private void PopulateTrayLaunchMenu()
        {
            mnuTrayMenu.Items.Add("-");
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "Open";
            toolStripMenuItem.Click += new EventHandler(NotifyMenu_Click);
            mnuTrayMenu.Items.Add(toolStripMenuItem);
            toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "Settings";
            toolStripMenuItem.Click += new EventHandler(NotifyMenu_Click);
            mnuTrayMenu.Items.Add(toolStripMenuItem);
            mnuTrayMenu.Items.Add("-");
            toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "About";
            toolStripMenuItem.Click += new EventHandler(NotifyMenu_Click);
            mnuTrayMenu.Items.Add(toolStripMenuItem);
            mnuTrayMenu.Items.Add("-");
            toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "Exit";
            toolStripMenuItem.Click += new EventHandler(NotifyMenu_Click);
            mnuTrayMenu.Items.Add(toolStripMenuItem);
            notifyIcon1.ContextMenuStrip = mnuTrayMenu;
        }

        private ToolStripMenuItem FindMenuByTag(object tag, ToolStripItemCollection menuItems)
        {
            foreach (ToolStripItem toolStripItem in menuItems)
            {
                if (toolStripItem.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)toolStripItem;
                    if (toolStripMenuItem.Tag != null && toolStripMenuItem.Tag == tag)
                    {
                        ToolStripMenuItem result = toolStripMenuItem;
                        return result;
                    }
                    if (toolStripMenuItem.DropDownItems.Count > 0)
                    {
                        ToolStripMenuItem toolStripMenuItem2 = FindMenuByTag(tag, toolStripMenuItem.DropDownItems);
                        if (toolStripMenuItem2 != null)
                        {
                            ToolStripMenuItem result = toolStripMenuItem2;
                            return result;
                        }
                    }
                }
            }
            return null;
        }

        private void lstApps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DoDeleteGame();
            }
        }

        private void openFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstApps.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem tag = lstApps.SelectedItems[0];
            Process.Start(Path.GetDirectoryName(m_AppList.GetApp(tag).Path));
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.ReMovementPanel = new System.Windows.Forms.Panel();
            this.lstApps = new System.Windows.Forms.ListView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchNormallywithoutEmuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDesktopShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuViewStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addGameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.hideMissingShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateAddedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.typeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.Label_StudioName = new System.Windows.Forms.PictureBox();
            this.panel27 = new System.Windows.Forms.Panel();
            this.panel26 = new System.Windows.Forms.Panel();
            this.panel24 = new System.Windows.Forms.Panel();
            this.panel21 = new System.Windows.Forms.Panel();
            this.Btn_Exit = new System.Windows.Forms.PictureBox();
            this.panel22 = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.Btn_About = new System.Windows.Forms.PictureBox();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.Btn_SmartEmuSettings = new System.Windows.Forms.PictureBox();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.Btn_GameSettings = new System.Windows.Forms.PictureBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.Btn_AddGame = new System.Windows.Forms.PictureBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Logo_Dock = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_WinMinimize = new System.Windows.Forms.PictureBox();
            this.Btn_FormWin = new System.Windows.Forms.PictureBox();
            this.Btn_close = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_spc = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel28 = new System.Windows.Forms.Panel();
            this.label_ipv4 = new System.Windows.Forms.Label();
            this.AppList_panel = new System.Windows.Forms.Panel();
            this.pbDrop = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            this.ctxMenuStrip.SuspendLayout();
            this.ctxMenuViewStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Label_StudioName)).BeginInit();
            this.panel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Exit)).BeginInit();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_About)).BeginInit();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_SmartEmuSettings)).BeginInit();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_GameSettings)).BeginInit();
            this.ButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_AddGame)).BeginInit();
            this.panel6.SuspendLayout();
            this.Logo_Dock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_WinMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_FormWin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.AppList_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrop)).BeginInit();
            this.SuspendLayout();
            // 
            // ReMovementPanel
            // 
            this.ReMovementPanel.Location = new System.Drawing.Point(0, 0);
            this.ReMovementPanel.Name = "ReMovementPanel";
            this.ReMovementPanel.Size = new System.Drawing.Size(200, 100);
            this.ReMovementPanel.TabIndex = 0;
            this.ReMovementPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // lstApps
            // 
            this.lstApps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lstApps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstApps.ForeColor = System.Drawing.Color.White;
            this.lstApps.HideSelection = false;
            this.lstApps.LabelEdit = true;
            this.lstApps.LargeImageList = this.imgList;
            this.lstApps.Location = new System.Drawing.Point(0, 0);
            this.lstApps.Margin = new System.Windows.Forms.Padding(2);
            this.lstApps.Name = "lstApps";
            this.lstApps.Size = new System.Drawing.Size(760, 384);
            this.lstApps.SmallImageList = this.imgList;
            this.lstApps.TabIndex = 0;
            this.lstApps.UseCompatibleStateImageBehavior = false;
            this.lstApps.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstApps_AfterLabelEdit);
            this.lstApps.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LstApps_OnItemSelectionChanged);
            this.lstApps.SelectedIndexChanged += new System.EventHandler(this.lstApps_SelectedIndexChanged);
            this.lstApps.DoubleClick += new System.EventHandler(this.LstApps_DoubleClick);
            this.lstApps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstApps_KeyDown);
            this.lstApps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LstApps_KeyPress);
            this.lstApps.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstApps_KeyUp);
            this.lstApps.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstApps_OnMouseUp);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgList.ImageSize = new System.Drawing.Size(32, 32);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(200, 40);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(760, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGamesToolStripMenuItem,
            this.editGameToolStripMenuItem,
            this.deleteGameToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // addGamesToolStripMenuItem
            // 
            this.addGamesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.addGamesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.addGamesToolStripMenuItem.Image = global::Properties.Resources.Img_AddGame;
            this.addGamesToolStripMenuItem.Name = "addGamesToolStripMenuItem";
            this.addGamesToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addGamesToolStripMenuItem.Text = "&Add Game";
            this.addGamesToolStripMenuItem.Click += new System.EventHandler(this.addGamesToolStripMenuItem_Click);
            // 
            // editGameToolStripMenuItem
            // 
            this.editGameToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.editGameToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.editGameToolStripMenuItem.Image = global::Properties.Resources.Img_edit;
            this.editGameToolStripMenuItem.Name = "editGameToolStripMenuItem";
            this.editGameToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.editGameToolStripMenuItem.Text = "&Edit Game";
            this.editGameToolStripMenuItem.Click += new System.EventHandler(this.editGameToolStripMenuItem_Click);
            // 
            // deleteGameToolStripMenuItem
            // 
            this.deleteGameToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.deleteGameToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.deleteGameToolStripMenuItem.Image = global::Properties.Resources.Img_DelGame;
            this.deleteGameToolStripMenuItem.Name = "deleteGameToolStripMenuItem";
            this.deleteGameToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.deleteGameToolStripMenuItem.Text = "&Delete Game";
            this.deleteGameToolStripMenuItem.Click += new System.EventHandler(this.deleteGameToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Info";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ctxMenuStrip
            // 
            this.ctxMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchToolStripMenuItem,
            this.launchNormallywithoutEmuToolStripMenuItem,
            this.createDesktopShortcutToolStripMenuItem,
            this.openFileLocationToolStripMenuItem,
            this.toolStripMenuItem3,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripMenuItem7,
            this.editToolStripMenuItem});
            this.ctxMenuStrip.Name = "ctxMenuStrip";
            this.ctxMenuStrip.Size = new System.Drawing.Size(245, 170);
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.launchToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.launchToolStripMenuItem.Text = "&Launch";
            this.launchToolStripMenuItem.Click += new System.EventHandler(this.launchToolStripMenuItem_Click);
            // 
            // launchNormallywithoutEmuToolStripMenuItem
            // 
            this.launchNormallywithoutEmuToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.launchNormallywithoutEmuToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.launchNormallywithoutEmuToolStripMenuItem.Name = "launchNormallywithoutEmuToolStripMenuItem";
            this.launchNormallywithoutEmuToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.launchNormallywithoutEmuToolStripMenuItem.Text = "Launch &Normally (without Emu)";
            this.launchNormallywithoutEmuToolStripMenuItem.Click += new System.EventHandler(this.launchNormallywithoutEmuToolStripMenuItem_Click);
            // 
            // createDesktopShortcutToolStripMenuItem
            // 
            this.createDesktopShortcutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.createDesktopShortcutToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.createDesktopShortcutToolStripMenuItem.Name = "createDesktopShortcutToolStripMenuItem";
            this.createDesktopShortcutToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.createDesktopShortcutToolStripMenuItem.Text = "&Create Desktop Shortcut";
            this.createDesktopShortcutToolStripMenuItem.Click += new System.EventHandler(this.createDesktopShortcutToolStripMenuItem_Click);
            // 
            // openFileLocationToolStripMenuItem
            // 
            this.openFileLocationToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.openFileLocationToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.openFileLocationToolStripMenuItem.Name = "openFileLocationToolStripMenuItem";
            this.openFileLocationToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.openFileLocationToolStripMenuItem.Text = "&Open File Location";
            this.openFileLocationToolStripMenuItem.Click += new System.EventHandler(this.openFileLocationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMenuItem3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(241, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.renameToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.renameToolStripMenuItem.Text = "&Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMenuItem7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(241, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.editToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.editToolStripMenuItem.Text = "&Properties";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // addGameToolStripMenuItem
            // 
            this.addGameToolStripMenuItem.Name = "addGameToolStripMenuItem";
            this.addGameToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.addGameToolStripMenuItem.Text = "&Add Game";
            this.addGameToolStripMenuItem.Click += new System.EventHandler(this.addGameToolStripMenuItem_Click);
            // 
            // editGameToolStripMenuItem1
            // 
            this.editGameToolStripMenuItem1.Name = "editGameToolStripMenuItem1";
            this.editGameToolStripMenuItem1.Size = new System.Drawing.Size(175, 24);
            this.editGameToolStripMenuItem1.Text = "&Edit Game";
            // 
            // deleteGameToolStripMenuItem1
            // 
            this.deleteGameToolStripMenuItem1.Name = "deleteGameToolStripMenuItem1";
            this.deleteGameToolStripMenuItem1.Size = new System.Drawing.Size(175, 24);
            this.deleteGameToolStripMenuItem1.Text = "&Delete Game";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(172, 6);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.settingToolStripMenuItem.Text = "&Setting";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(172, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(175, 24);
            this.exitToolStripMenuItem1.Text = "E&xit";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(175, 24);
            this.aboutToolStripMenuItem1.Text = "&About";
            // 
            // ctxMenuViewStrip
            // 
            this.ctxMenuViewStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.ctxMenuViewStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenuViewStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGameToolStripMenuItem1,
            this.toolStripMenuItem6,
            this.viewToolStripMenuItem,
            this.sortByToolStripMenuItem,
            this.groupByToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.ctxMenuViewStrip.Name = "ctxMenuViewStrip";
            this.ctxMenuViewStrip.ShowImageMargin = false;
            this.ctxMenuViewStrip.Size = new System.Drawing.Size(106, 120);
            // 
            // addGameToolStripMenuItem1
            // 
            this.addGameToolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addGameToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.addGameToolStripMenuItem1.Image = global::Properties.Resources.Img_AddGame_BF;
            this.addGameToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.addGameToolStripMenuItem1.Name = "addGameToolStripMenuItem1";
            this.addGameToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.addGameToolStripMenuItem1.Text = "Add Game";
            this.addGameToolStripMenuItem1.Click += new System.EventHandler(this.addGameToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(102, 6);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.largeIconToolStripMenuItem,
            this.smallIconToolStripMenuItem,
            this.listToolStripMenuItem,
            this.tileToolStripMenuItem,
            this.toolStripMenuItem8,
            this.hideMissingShortcutToolStripMenuItem});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // largeIconToolStripMenuItem
            // 
            this.largeIconToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.largeIconToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.largeIconToolStripMenuItem.Name = "largeIconToolStripMenuItem";
            this.largeIconToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.largeIconToolStripMenuItem.Text = "L&arge Icon";
            this.largeIconToolStripMenuItem.Click += new System.EventHandler(this.largeIconToolStripMenuItem_Click);
            // 
            // smallIconToolStripMenuItem
            // 
            this.smallIconToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.smallIconToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.smallIconToolStripMenuItem.Name = "smallIconToolStripMenuItem";
            this.smallIconToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.smallIconToolStripMenuItem.Text = "&Small Icon";
            this.smallIconToolStripMenuItem.Click += new System.EventHandler(this.smallIconToolStripMenuItem_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.listToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.listToolStripMenuItem.Text = "&List";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
            // 
            // tileToolStripMenuItem
            // 
            this.tileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.tileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
            this.tileToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.tileToolStripMenuItem.Text = "&Tile";
            this.tileToolStripMenuItem.Click += new System.EventHandler(this.tileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripMenuItem8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(187, 6);
            // 
            // hideMissingShortcutToolStripMenuItem
            // 
            this.hideMissingShortcutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.hideMissingShortcutToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.hideMissingShortcutToolStripMenuItem.Name = "hideMissingShortcutToolStripMenuItem";
            this.hideMissingShortcutToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.hideMissingShortcutToolStripMenuItem.Text = "Hide missing shortcut";
            this.hideMissingShortcutToolStripMenuItem.Click += new System.EventHandler(this.hideMissingShortcutToolStripMenuItem_Click);
            // 
            // sortByToolStripMenuItem
            // 
            this.sortByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameToolStripMenuItem,
            this.dateAddedToolStripMenuItem});
            this.sortByToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.sortByToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.sortByToolStripMenuItem.Name = "sortByToolStripMenuItem";
            this.sortByToolStripMenuItem.ShowShortcutKeys = false;
            this.sortByToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.sortByToolStripMenuItem.Text = "S&ort by";
            // 
            // nameToolStripMenuItem
            // 
            this.nameToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.nameToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.nameToolStripMenuItem.Name = "nameToolStripMenuItem";
            this.nameToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.nameToolStripMenuItem.Text = "Name";
            this.nameToolStripMenuItem.Click += new System.EventHandler(this.nameToolStripMenuItem_Click);
            // 
            // dateAddedToolStripMenuItem
            // 
            this.dateAddedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.dateAddedToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.dateAddedToolStripMenuItem.Name = "dateAddedToolStripMenuItem";
            this.dateAddedToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.dateAddedToolStripMenuItem.Text = "(None)";
            this.dateAddedToolStripMenuItem.Click += new System.EventHandler(this.dateAddedToolStripMenuItem_Click);
            // 
            // groupByToolStripMenuItem
            // 
            this.groupByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.typeToolStripMenuItem,
            this.categoryToolStripMenuItem});
            this.groupByToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.groupByToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.groupByToolStripMenuItem.Name = "groupByToolStripMenuItem";
            this.groupByToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.groupByToolStripMenuItem.Text = "Grou&p by";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.noneToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.noneToolStripMenuItem.Text = "(&None)";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // typeToolStripMenuItem
            // 
            this.typeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.typeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.typeToolStripMenuItem.Name = "typeToolStripMenuItem";
            this.typeToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.typeToolStripMenuItem.Text = "Type";
            this.typeToolStripMenuItem.Click += new System.EventHandler(this.typeToolStripMenuItem_Click);
            // 
            // categoryToolStripMenuItem
            // 
            this.categoryToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.categoryToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            this.categoryToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.categoryToolStripMenuItem.Text = "Category";
            this.categoryToolStripMenuItem.Click += new System.EventHandler(this.categoryToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.refreshToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(55)))));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.refreshToolStripMenuItem.Text = "R&efresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(36)))));
            this.panel1.Controls.Add(this.panel25);
            this.panel1.Controls.Add(this.panel24);
            this.panel1.Controls.Add(this.panel21);
            this.panel1.Controls.Add(this.panel18);
            this.panel1.Controls.Add(this.panel15);
            this.panel1.Controls.Add(this.panel12);
            this.panel1.Controls.Add(this.ButtonPanel);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.Logo_Dock);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 440);
            this.panel1.TabIndex = 3;
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.Label_StudioName);
            this.panel25.Controls.Add(this.panel27);
            this.panel25.Controls.Add(this.panel26);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel25.Location = new System.Drawing.Point(0, 320);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(200, 38);
            this.panel25.TabIndex = 12;
            // 
            // Label_StudioName
            // 
            this.Label_StudioName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_StudioName.Image = global::Properties.Resources.Version__preAlpha_2_1;
            this.Label_StudioName.Location = new System.Drawing.Point(10, 0);
            this.Label_StudioName.Name = "Label_StudioName";
            this.Label_StudioName.Size = new System.Drawing.Size(180, 38);
            this.Label_StudioName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Label_StudioName.TabIndex = 2;
            this.Label_StudioName.TabStop = false;
            this.Label_StudioName.Click += new System.EventHandler(this.Label_StudioName_Click);
            this.Label_StudioName.MouseEnter += new System.EventHandler(this.Label_StudioName_MouseEnter);
            this.Label_StudioName.MouseLeave += new System.EventHandler(this.Label_StudioName_MouseLeave);
            // 
            // panel27
            // 
            this.panel27.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel27.Location = new System.Drawing.Point(0, 0);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(10, 38);
            this.panel27.TabIndex = 1;
            // 
            // panel26
            // 
            this.panel26.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel26.Location = new System.Drawing.Point(190, 0);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(10, 38);
            this.panel26.TabIndex = 0;
            // 
            // panel24
            // 
            this.panel24.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel24.Location = new System.Drawing.Point(0, 310);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(200, 10);
            this.panel24.TabIndex = 11;
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.Btn_Exit);
            this.panel21.Controls.Add(this.panel22);
            this.panel21.Controls.Add(this.panel23);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel21.Location = new System.Drawing.Point(0, 270);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(200, 40);
            this.panel21.TabIndex = 10;
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Exit.Image = global::Properties.Resources.Btn_Exit_Menu;
            this.Btn_Exit.Location = new System.Drawing.Point(10, 0);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(180, 40);
            this.Btn_Exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Btn_Exit.TabIndex = 8;
            this.Btn_Exit.TabStop = false;
            this.Btn_Exit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            this.Btn_Exit.MouseEnter += new System.EventHandler(this.Btn_Exit_MouseEnter);
            this.Btn_Exit.MouseLeave += new System.EventHandler(this.Btn_Exit_MouseLeave);
            // 
            // panel22
            // 
            this.panel22.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel22.Location = new System.Drawing.Point(0, 0);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(10, 40);
            this.panel22.TabIndex = 0;
            // 
            // panel23
            // 
            this.panel23.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel23.Location = new System.Drawing.Point(190, 0);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(10, 40);
            this.panel23.TabIndex = 1;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.Btn_About);
            this.panel18.Controls.Add(this.panel19);
            this.panel18.Controls.Add(this.panel20);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel18.Location = new System.Drawing.Point(0, 230);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(200, 40);
            this.panel18.TabIndex = 10;
            // 
            // Btn_About
            // 
            this.Btn_About.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_About.Image = global::Properties.Resources.Btn_About;
            this.Btn_About.Location = new System.Drawing.Point(10, 0);
            this.Btn_About.Name = "Btn_About";
            this.Btn_About.Size = new System.Drawing.Size(180, 40);
            this.Btn_About.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Btn_About.TabIndex = 8;
            this.Btn_About.TabStop = false;
            this.Btn_About.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            this.Btn_About.MouseEnter += new System.EventHandler(this.Btn_About_MouseEnter);
            this.Btn_About.MouseLeave += new System.EventHandler(this.Btn_About_MouseLeave);
            // 
            // panel19
            // 
            this.panel19.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(10, 40);
            this.panel19.TabIndex = 0;
            // 
            // panel20
            // 
            this.panel20.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel20.Location = new System.Drawing.Point(190, 0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(10, 40);
            this.panel20.TabIndex = 1;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.Btn_SmartEmuSettings);
            this.panel15.Controls.Add(this.panel16);
            this.panel15.Controls.Add(this.panel17);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 190);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(200, 40);
            this.panel15.TabIndex = 10;
            // 
            // Btn_SmartEmuSettings
            // 
            this.Btn_SmartEmuSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_SmartEmuSettings.Image = global::Properties.Resources.Btn_SmartEmuSettings;
            this.Btn_SmartEmuSettings.Location = new System.Drawing.Point(10, 0);
            this.Btn_SmartEmuSettings.Name = "Btn_SmartEmuSettings";
            this.Btn_SmartEmuSettings.Size = new System.Drawing.Size(180, 40);
            this.Btn_SmartEmuSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Btn_SmartEmuSettings.TabIndex = 8;
            this.Btn_SmartEmuSettings.TabStop = false;
            this.Btn_SmartEmuSettings.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            this.Btn_SmartEmuSettings.MouseEnter += new System.EventHandler(this.Btn_SmartEmuSettings_MouseEnter);
            this.Btn_SmartEmuSettings.MouseLeave += new System.EventHandler(this.Btn_SmartEmuSettings_MouseLeave);
            // 
            // panel16
            // 
            this.panel16.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(10, 40);
            this.panel16.TabIndex = 0;
            // 
            // panel17
            // 
            this.panel17.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel17.Location = new System.Drawing.Point(190, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(10, 40);
            this.panel17.TabIndex = 1;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.Btn_GameSettings);
            this.panel12.Controls.Add(this.panel13);
            this.panel12.Controls.Add(this.panel14);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 150);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(200, 40);
            this.panel12.TabIndex = 10;
            // 
            // Btn_GameSettings
            // 
            this.Btn_GameSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_GameSettings.Image = global::Properties.Resources.Btn_GameSettings_Menu;
            this.Btn_GameSettings.Location = new System.Drawing.Point(10, 0);
            this.Btn_GameSettings.Name = "Btn_GameSettings";
            this.Btn_GameSettings.Size = new System.Drawing.Size(180, 40);
            this.Btn_GameSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Btn_GameSettings.TabIndex = 8;
            this.Btn_GameSettings.TabStop = false;
            this.Btn_GameSettings.Click += new System.EventHandler(this.Btn_GameSettings_Click);
            this.Btn_GameSettings.MouseEnter += new System.EventHandler(this.Btn_GameSettings_MouseEnter);
            this.Btn_GameSettings.MouseLeave += new System.EventHandler(this.Btn_GameSettings_MouseLeave);
            // 
            // panel13
            // 
            this.panel13.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(10, 40);
            this.panel13.TabIndex = 0;
            // 
            // panel14
            // 
            this.panel14.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel14.Location = new System.Drawing.Point(190, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(10, 40);
            this.panel14.TabIndex = 1;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Controls.Add(this.Btn_AddGame);
            this.ButtonPanel.Controls.Add(this.panel10);
            this.ButtonPanel.Controls.Add(this.panel11);
            this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonPanel.Location = new System.Drawing.Point(0, 110);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(200, 40);
            this.ButtonPanel.TabIndex = 9;
            // 
            // Btn_AddGame
            // 
            this.Btn_AddGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_AddGame.Image = global::Properties.Resources.Btn_AddGame;
            this.Btn_AddGame.Location = new System.Drawing.Point(10, 0);
            this.Btn_AddGame.Name = "Btn_AddGame";
            this.Btn_AddGame.Size = new System.Drawing.Size(180, 40);
            this.Btn_AddGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Btn_AddGame.TabIndex = 8;
            this.Btn_AddGame.TabStop = false;
            this.Btn_AddGame.Click += new System.EventHandler(this.Btn_AddGame_Click);
            this.Btn_AddGame.MouseEnter += new System.EventHandler(this.Btn_AddGame_MouseEnter);
            this.Btn_AddGame.MouseLeave += new System.EventHandler(this.Btn_AddGame_MouseLeave);
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(10, 40);
            this.panel10.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel11.Location = new System.Drawing.Point(190, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(10, 40);
            this.panel11.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 100);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(200, 10);
            this.panel9.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 97);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 3);
            this.panel6.TabIndex = 7;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(36)))));
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(167, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(33, 3);
            this.panel8.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(36)))));
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(32, 3);
            this.panel7.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 87);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 10);
            this.panel5.TabIndex = 3;
            // 
            // Logo_Dock
            // 
            this.Logo_Dock.Controls.Add(this.pictureBox5);
            this.Logo_Dock.Dock = System.Windows.Forms.DockStyle.Top;
            this.Logo_Dock.Location = new System.Drawing.Point(0, 12);
            this.Logo_Dock.Name = "Logo_Dock";
            this.Logo_Dock.Size = new System.Drawing.Size(200, 75);
            this.Logo_Dock.TabIndex = 1;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox5.Image = global::Properties.Resources.Logo;
            this.pictureBox5.Location = new System.Drawing.Point(0, 0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(200, 75);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 12);
            this.panel4.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(36)))));
            this.panel2.Controls.Add(this.Btn_WinMinimize);
            this.panel2.Controls.Add(this.Btn_FormWin);
            this.panel2.Controls.Add(this.Btn_close);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.panel_spc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(960, 40);
            this.panel2.TabIndex = 4;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            // 
            // Btn_WinMinimize
            // 
            this.Btn_WinMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_WinMinimize.Image = global::Properties.Resources.Btn_Minimize;
            this.Btn_WinMinimize.Location = new System.Drawing.Point(831, 0);
            this.Btn_WinMinimize.Name = "Btn_WinMinimize";
            this.Btn_WinMinimize.Size = new System.Drawing.Size(43, 40);
            this.Btn_WinMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Btn_WinMinimize.TabIndex = 4;
            this.Btn_WinMinimize.TabStop = false;
            this.Btn_WinMinimize.Click += new System.EventHandler(this.pictureBox4_Click);
            this.Btn_WinMinimize.MouseEnter += new System.EventHandler(this.Btn_WinMinimize_MouseEnter);
            this.Btn_WinMinimize.MouseLeave += new System.EventHandler(this.Btn_WinMinimize_MouseLeave);
            // 
            // Btn_FormWin
            // 
            this.Btn_FormWin.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_FormWin.Image = global::Properties.Resources.Btn_Screen;
            this.Btn_FormWin.Location = new System.Drawing.Point(874, 0);
            this.Btn_FormWin.Name = "Btn_FormWin";
            this.Btn_FormWin.Size = new System.Drawing.Size(43, 40);
            this.Btn_FormWin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Btn_FormWin.TabIndex = 3;
            this.Btn_FormWin.TabStop = false;
            this.Btn_FormWin.Click += new System.EventHandler(this.pictureBox3_Click);
            this.Btn_FormWin.MouseEnter += new System.EventHandler(this.Btn_FormWin_MouseEnter);
            this.Btn_FormWin.MouseLeave += new System.EventHandler(this.Btn_FormWin_MouseLeave);
            // 
            // Btn_close
            // 
            this.Btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_close.Image = global::Properties.Resources.Btn_Exit;
            this.Btn_close.Location = new System.Drawing.Point(917, 0);
            this.Btn_close.Name = "Btn_close";
            this.Btn_close.Size = new System.Drawing.Size(43, 40);
            this.Btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Btn_close.TabIndex = 2;
            this.Btn_close.TabStop = false;
            this.Btn_close.Click += new System.EventHandler(this.pictureBox2_Click);
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(36)))));
            this.panel3.Controls.Add(this.panel28);
            this.panel3.Controls.Add(this.label_ipv4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(200, 448);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(760, 32);
            this.panel3.TabIndex = 5;
            // 
            // panel28
            // 
            this.panel28.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel28.Location = new System.Drawing.Point(0, 0);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(292, 32);
            this.panel28.TabIndex = 2;
            // 
            // label_ipv4
            // 
            this.label_ipv4.AutoSize = true;
            this.label_ipv4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_ipv4.Font = new System.Drawing.Font("MuseoModerno Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ipv4.ForeColor = System.Drawing.Color.White;
            this.label_ipv4.Location = new System.Drawing.Point(555, 0);
            this.label_ipv4.Name = "label_ipv4";
            this.label_ipv4.Size = new System.Drawing.Size(205, 25);
            this.label_ipv4.TabIndex = 0;
            this.label_ipv4.Text = "IPv4:000.000.000.000";
            // 
            // AppList_panel
            // 
            this.AppList_panel.Controls.Add(this.pbDrop);
            this.AppList_panel.Controls.Add(this.lstApps);
            this.AppList_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AppList_panel.Location = new System.Drawing.Point(200, 64);
            this.AppList_panel.Name = "AppList_panel";
            this.AppList_panel.Size = new System.Drawing.Size(760, 384);
            this.AppList_panel.TabIndex = 6;
            // 
            // pbDrop
            // 
            this.pbDrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.pbDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDrop.Image = global::Properties.Resources.drop_here;
            this.pbDrop.InitialImage = null;
            this.pbDrop.Location = new System.Drawing.Point(0, 0);
            this.pbDrop.Margin = new System.Windows.Forms.Padding(2);
            this.pbDrop.Name = "pbDrop";
            this.pbDrop.Size = new System.Drawing.Size(760, 384);
            this.pbDrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDrop.TabIndex = 2;
            this.pbDrop.TabStop = false;
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 480);
            this.Controls.Add(this.AppList_panel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(960, 480);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartSteamEmu Launcher By BloodyAlpha";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_Closing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ctxMenuStrip.ResumeLayout(false);
            this.ctxMenuViewStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Label_StudioName)).EndInit();
            this.panel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Exit)).EndInit();
            this.panel18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_About)).EndInit();
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_SmartEmuSettings)).EndInit();
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_GameSettings)).EndInit();
            this.ButtonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_AddGame)).EndInit();
            this.panel6.ResumeLayout(false);
            this.Logo_Dock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_WinMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_FormWin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.AppList_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        //BloodyAlpha
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
            {
                panel1.Visible = false; 
            }
            else
            {
                panel1.Visible = true;
            }    
/*
            if (WindowState == FormWindowState.Normal) 
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
*/
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Btn_close_MouseEnter(object sender, EventArgs e)
        {
            Btn_close.Image = Properties.Resources.Btn_Exit_OnEnter;
        }

        private void Btn_close_MouseLeave(object sender, EventArgs e)
        {
            Btn_close.Image = Properties.Resources.Btn_Exit;
        }

        private void Btn_FormWin_MouseEnter(object sender, EventArgs e)
        {
            Btn_FormWin.Image = Properties.Resources.Btn_Screen_OnEnter;
        }

        private void Btn_FormWin_MouseLeave(object sender, EventArgs e)
        {
            Btn_FormWin.Image = Properties.Resources.Btn_Screen;
        }

        private void Btn_WinMinimize_MouseEnter(object sender, EventArgs e)
        {
            Btn_WinMinimize.Image = Properties.Resources.Btn_Minimize_OnEnter;
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

        private void Btn_AddGame_MouseLeave(object sender, EventArgs e)
        {
            Btn_AddGame.Image = Properties.Resources.Btn_AddGame;
        }

        private void Btn_GameSettings_MouseLeave(object sender, EventArgs e)
        {
            Btn_GameSettings.Image = Properties.Resources.Btn_GameSettings_Menu;
        }

        private void Btn_SmartEmuSettings_MouseLeave(object sender, EventArgs e)
        {
            Btn_SmartEmuSettings.Image = Properties.Resources.Btn_SmartEmuSettings;
        }

        private void Btn_About_MouseLeave(object sender, EventArgs e)
        {
            Btn_About.Image = Properties.Resources.Btn_About;
        }

        private void Btn_Exit_MouseLeave(object sender, EventArgs e)
        {
            Btn_Exit.Image = Properties.Resources.Btn_Exit_Menu;
        }

        private void Btn_AddGame_MouseEnter(object sender, EventArgs e)
        {
            Btn_AddGame.Image = Properties.Resources.AddGame_OnEnter;
        }

        private void Btn_GameSettings_MouseEnter(object sender, EventArgs e)
        {
            Btn_GameSettings.Image = Properties.Resources.GameSettings_OnEnter;
        }

        private void Btn_SmartEmuSettings_MouseEnter(object sender, EventArgs e)
        {
            Btn_SmartEmuSettings.Image = Properties.Resources.SmartEmuSettings_OnEnter;
        }

        private void Btn_About_MouseEnter(object sender, EventArgs e)
        {
            Btn_About.Image = Properties.Resources.About_OnEnter;
        }

        private void Btn_Exit_MouseEnter(object sender, EventArgs e)
        {
            Btn_Exit.Image = Properties.Resources.Exit_OnEnter;
        }

        private void Btn_AddGame_Click(object sender, EventArgs e)
        {
            OnAddGame();
        }

        private void Btn_GameSettings_Click(object sender, EventArgs e)
        {
            DoEditGame();
        }

        private void Btn_SmartEmuSettings_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
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

        private void Label_StudioName_MouseLeave(object sender, EventArgs e)
        {
            Label_StudioName.Image = Properties.Resources.Version__preAlpha_2_1;
        }

        private void Label_StudioName_MouseEnter(object sender, EventArgs e)
        {
            Label_StudioName.Image = Properties.Resources.ProjectByBloodyAlpha;
        }

        private void Label_StudioName_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/PfxujSxDYA");
        }
    }
}
