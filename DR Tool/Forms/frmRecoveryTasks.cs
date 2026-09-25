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
    public partial class frmRecoveryTasks : Form
    {
        private List<RecoveryTask> _Tasks = new List<RecoveryTask>();

        public List<RecoveryTask> Tasks
        {
            get
            {
                return _Tasks;
            }

            set
            {
                _Tasks = value;
            }
        }

        public string Caption
        {
            set
            {
                this.Text = value;
            }
        }

        public frmRecoveryTasks()
        {
            InitializeComponent();
        }

        private void frmRecoveryTasks_Load(object sender, EventArgs e)
        {
            TimeSpan TotalTime = new TimeSpan();
            ListViewItem Item = new ListViewItem();

            foreach (RecoveryTask Task in _Tasks)
            {
                Item = new ListViewItem();

                if (Task != null)
                {
                    Item.Text = Task.Name;

                    if (!Task.Completed)
                    {
                        Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(Task.Time));

                        TotalTime += Task.Time;
                    }
                    else
                    {
                        Item.ForeColor = Color.LightGray;
                        Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(TimeSpan.Zero));
                    }

                    Item.Tag = Task;

                    lvwTasks.Items.Add(Item);

                    Task.Completed = false; // <--- this is required so that we can view these tasks normally elsewhere
                }
            }

            Item = new ListViewItem();

            if (TotalTime.TotalSeconds > 0)
            {
                Item.Text = "Total Recovery Time";
                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(TotalTime));
                Item.Font = new Font(this.Font, this.Font.Style | FontStyle.Bold | FontStyle.Underline);
            }
            else
            {
                Item.Text = "No Recovery Time";
                Item.Font = new Font(this.Font, this.Font.Style | FontStyle.Bold | FontStyle.Underline);
            }

            lvwTasks.Items.Add(Item);

            this.Activate();
        }

        private void lvwTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void frmRecoveryTasks_Resize(object sender, EventArgs e)
        {
            lvwTasks.Columns[0].Width = lvwTasks.Size.Width - lvwTasks.Columns[1].Width - 28;
        }

    }
}
