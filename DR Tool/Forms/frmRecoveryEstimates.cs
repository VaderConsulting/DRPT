using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISA.Dependency;

namespace DRPlanningTool
{
    public partial class frmRecoveryEstimates : Form
    {
        private Form _ParentForm = null;
        private List<BusinessApplication> _Applications = null;

        public frmRecoveryEstimates(Form ParentForm, List<BusinessApplication> Applications)
        {
            InitializeComponent();

            _ParentForm = ParentForm;
            _Applications = Applications;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRecoveryEstimates_Load(object sender, EventArgs e)
        {
            TimeSpan CumulativeTime = new TimeSpan();
            ListViewItem Item = new ListViewItem();
            List<Server> ServersAdded = new List<Server>();
            List<RecoveryTask> TaskList = null;
            bool StreamAAdded = false;
            bool StreamBAdded = false;
            bool StreamCAdded = false;
            bool StreamDAdded = false;
            bool StreamEAdded = false;
            TimeSpan IntervalPeriod = TimeSpan.Zero;
            TimeSpan RecoveryTime = TimeSpan.Zero;

            lvwRunbook.Items.Clear();

            // Add the overall overhead time
            if (Properties.Settings.Default.Restore_OverallOverheadTime.TotalSeconds > 0)
            {
                Item = new ListViewItem();

                CumulativeTime += Properties.Settings.Default.Restore_OverallOverheadTime;

                Item.Text = "Disaster Recovery preparation steps";
                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(Properties.Settings.Default.Restore_OverallOverheadTime));
                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(CumulativeTime));

                Item.ImageIndex = 6;

                lvwRunbook.Items.Add(Item);
            }

            // Add each Service, with Stream prerequisites as appropriate
            foreach (BusinessApplication App in _Applications)
            {
                if (App.InScope)
                {
                    #region Add the Stream Prerequisite Service

                    switch (App.CalculatedStream)
                    {
                        case Global.RecoveryStream.A:
                            if (!StreamAAdded && Properties.Settings.Default.Restore_StreamAServicePrerequisite.ToUpper() != "[NONE]")
                            {
                                Item = new ListViewItem();

                                CumulativeTime += Properties.Settings.Default.Restore_StreamAOverheadTime;

                                Item.Text = Properties.Settings.Default.Restore_StreamAServicePrerequisite + " preparation steps";
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(Properties.Settings.Default.Restore_StreamAOverheadTime));
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(CumulativeTime));

                                Item.ImageIndex = 7;

                                lvwRunbook.Items.Add(Item);

                                StreamAAdded = true;
                            }
                            break;
                        case Global.RecoveryStream.B:
                            if (!StreamBAdded && Properties.Settings.Default.Restore_StreamBServicePrerequisite.ToUpper() != "[NONE]")
                            {
                                Item = new ListViewItem();

                                CumulativeTime += Properties.Settings.Default.Restore_StreamBOverheadTime;

                                Item.Text = Properties.Settings.Default.Restore_StreamBServicePrerequisite + " preparation steps";
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(Properties.Settings.Default.Restore_StreamBOverheadTime));
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(CumulativeTime));

                                Item.ImageIndex = 7;

                                lvwRunbook.Items.Add(Item);

                                StreamBAdded = true;
                            }
                            break;
                        case Global.RecoveryStream.C:
                            if (!StreamCAdded && Properties.Settings.Default.Restore_StreamCServicePrerequisite.ToUpper() != "[NONE]")
                            {
                                Item = new ListViewItem();

                                CumulativeTime += Properties.Settings.Default.Restore_StreamCOverheadTime;

                                Item.Text = Properties.Settings.Default.Restore_StreamCServicePrerequisite + " preparation steps";
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(Properties.Settings.Default.Restore_StreamCOverheadTime));
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(CumulativeTime));

                                Item.ImageIndex = 7;

                                lvwRunbook.Items.Add(Item);

                                StreamCAdded = true;
                            }
                            break;
                        case Global.RecoveryStream.D:
                            if (!StreamDAdded && Properties.Settings.Default.Restore_StreamDServicePrerequisite.ToUpper() != "[NONE]")
                            {
                                Item = new ListViewItem();

                                CumulativeTime += Properties.Settings.Default.Restore_StreamDOverheadTime;

                                Item.Text = Properties.Settings.Default.Restore_StreamDServicePrerequisite + " preparation steps";
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(Properties.Settings.Default.Restore_StreamDOverheadTime));
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(CumulativeTime));

                                Item.ImageIndex = 7;

                                lvwRunbook.Items.Add(Item);

                                StreamDAdded = true;
                            }
                            break;
                        case Global.RecoveryStream.E:
                            if (!StreamEAdded && Properties.Settings.Default.Restore_StreamEServicePrerequisite.ToUpper() != "[NONE]")
                            {
                                Item = new ListViewItem();

                                CumulativeTime += Properties.Settings.Default.Restore_StreamEOverheadTime;

                                Item.Text = Properties.Settings.Default.Restore_StreamEServicePrerequisite + " preparation steps";
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(Properties.Settings.Default.Restore_StreamEOverheadTime));
                                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(CumulativeTime));

                                Item.ImageIndex = 7;

                                lvwRunbook.Items.Add(Item);

                                StreamEAdded = true;
                            }
                            break;
                    }

                    #endregion

                    Item = new ListViewItem();  // <--- this is the entry for the Business Application

                    TaskList = new List<RecoveryTask>();

                    RecoveryTime = TimeSpan.Zero;

                    foreach (Server Server in App.ComponentServers)
                    {
                        if (!ServersAdded.Contains(Server)) // if this Server's recovery time hasn't already been added, do so now
                        {
                            ServersAdded.Add(Server);

                            foreach (RecoveryTask Task in Server.RecoveryTasks)
                            {
                                RecoveryTime += Task.Time;

                                Task.Completed = false;
                                TaskList.Add(Task);
                            }
                        }
                        else
                        {
                            foreach (RecoveryTask Task in Server.RecoveryTasks)
                            {
                                Task.Completed = true;
                                TaskList.Add(Task);
                            }
                        }
                    }

                    CumulativeTime += RecoveryTime;

                    Item.Text = App.Name;

                    Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(RecoveryTime));
                    Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(CumulativeTime));
                    Item.Tag = TaskList; // Service;

                    Item.ImageIndex = Global.GetBusinessApplicationStateImageIndex(App, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                    lvwRunbook.Items.Add(Item);

                    #region Add Stream Interval Period

                    // Is there a period of time between each Service (i.e. Stream x Interval Period)?
                    IntervalPeriod = TimeSpan.Zero;

                    switch (App.CalculatedStream)
                    {
                        case Global.RecoveryStream.A:
                            if (Properties.Settings.Default.Restore_StreamAIntervalPeriod.TotalSeconds > 0)
                            {
                                IntervalPeriod = Properties.Settings.Default.Restore_StreamAIntervalPeriod;
                            }
                            break;
                        case Global.RecoveryStream.B:
                            if (Properties.Settings.Default.Restore_StreamBIntervalPeriod.TotalSeconds > 0)
                            {
                                IntervalPeriod = Properties.Settings.Default.Restore_StreamBIntervalPeriod;
                            }
                            break;
                        case Global.RecoveryStream.C:
                            if (Properties.Settings.Default.Restore_StreamCIntervalPeriod.TotalSeconds > 0)
                            {
                                IntervalPeriod = Properties.Settings.Default.Restore_StreamCIntervalPeriod;
                            }
                            break;
                        case Global.RecoveryStream.D:
                            if (Properties.Settings.Default.Restore_StreamDIntervalPeriod.TotalSeconds > 0)
                            {
                                IntervalPeriod = Properties.Settings.Default.Restore_StreamDIntervalPeriod;
                            }
                            break;
                        case Global.RecoveryStream.E:
                            if (Properties.Settings.Default.Restore_StreamEIntervalPeriod.TotalSeconds > 0)
                            {
                                IntervalPeriod = Properties.Settings.Default.Restore_StreamEIntervalPeriod;
                            }
                            break;
                    }

                    if (IntervalPeriod.TotalSeconds > 0)
                    {
                        Item = new ListViewItem();
                        Item.Text = "Interval";

                        Item.ImageIndex = 6;
                        CumulativeTime += IntervalPeriod;

                        Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(IntervalPeriod));
                        Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(CumulativeTime));

                        lvwRunbook.Items.Add(Item);
                    }

                    #endregion

                }
            }

            this.Activate();
        }

        private void frmRecoveryEstimates_Resize(object sender, EventArgs e)
        {
            lvwRunbook.Columns[0].Width = lvwRunbook.Size.Width - lvwRunbook.Columns[1].Width - lvwRunbook.Columns[2].Width - 28;
        }

        private void lvwRunbook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem SelectedItem = null;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                SelectedItem = lvwRunbook.SelectedItems[0];

                if (SelectedItem.Tag != null)
                {
                    if (SelectedItem.Tag is BusinessApplication)
                    {
                        BusinessApplication Service = (BusinessApplication)SelectedItem.Tag;

                        //frmMain MainForm = (frmMain)_ParentForm;

                        if (Service.Drawing != null)
                        {
                            Main.CreateOrSelectBusinessApplicationForm(Service.Drawing.Name, Service);
                        }
                        else
                        {
                            Main.CreateOrSelectBusinessApplicationForm(Service.Name, Service);
                        }
                    }

                    else if (SelectedItem.Tag is Server)
                    {
                        Server Server = (Server)SelectedItem.Tag;

                        //frmMain MainForm = (frmMain)_ParentForm;

                        Main.CreateOrSelectServerForm(ref Server);
                    }

                    else if (SelectedItem.Tag is List<RecoveryTask>)
                    {
                        frmRecoveryTasks RecoveryTasksForm = new frmRecoveryTasks();

                        RecoveryTasksForm.Tasks = (List<RecoveryTask>)SelectedItem.Tag;

                        RecoveryTasksForm.Caption = "Disaster Recovery Tasks: " + SelectedItem.Text;

                        RecoveryTasksForm.Show(this);
                    }
                }
            }
        }
    }
}
