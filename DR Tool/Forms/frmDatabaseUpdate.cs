using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DRPlanningTool
{
    public partial class frmDatabaseUpdate : Form
    {
        private bool _LoadComplete = false;
        public frmDatabaseUpdate()
        {
            InitializeComponent();
        }

        private void radBusinessApplications_CheckedChanged(object sender, EventArgs e)
        {
            if (radBusinessApplications.Checked && _LoadComplete)
            {
                GetPropertyNames(typeof(ISA.Dependency.BusinessApplication));
            }
        }

        private void radServers_CheckedChanged(object sender, EventArgs e)
        {
            if (radServers.Checked && _LoadComplete)
            {
                GetPropertyNames(typeof(ISA.Dependency.Server));
            }
        }

        private void GetPropertyNames(Type Type)
        {
            PropertyInfo[] Properties = null;

            cmbProperties.Items.Clear();

            switch (Type.FullName)
            {
                case "ISA.Dependency.BusinessApplication":

                    Properties = typeof(ISA.Dependency.BusinessApplication).GetProperties();
                    break;
                case "ISA.Dependency.Server":

                    Properties = typeof(ISA.Dependency.Server).GetProperties();
                    break;
            }

            System.Collections.Specialized.OrderedDictionary SortedList = new System.Collections.Specialized.OrderedDictionary();

            // Sort the Property list
            foreach (PropertyInfo Property in Properties)
            {
                if (Property.PropertyType.IsValueType)
                {
                    SortedList.Add(Property.Name, Property);
                    //Debug.WriteLine(Property.PropertyType.UnderlyingSystemType.Name);
                }
            }

            // Retrieve property list and put into combobox
            foreach (PropertyInfo Property in SortedList.Values)
            {
                ComboBoxItem Item = new ComboBoxItem(Property.Name, Property);
                cmbProperties.Items.Add(Item);
            }
        }

        private void frmDatabaseUpdate_Shown(object sender, EventArgs e)
        {
            radBusinessApplications.Checked = false;
            radServers.Checked = false;

            _LoadComplete = true;

            radBusinessApplications.Checked = true;
        }

        public class ComboBoxItem
        {
            private object _Value = null;
            private string _Key = "";

            public ComboBoxItem()
            {

            }

            public ComboBoxItem(object Value)
            {
                _Value = Value;
            }

            public ComboBoxItem(string Key, object Value)
            {
                _Key = Key;
                _Value = Value;
            }

            public object Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    _Value = value;
                }
            }

            public override string ToString()
            {
                return _Key;
            }
        }

        private void cmbProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem Item = (ComboBoxItem)cmbProperties.SelectedItem;
            PropertyInfo Property = (PropertyInfo)Item.Value;

            switch (Property.PropertyType.UnderlyingSystemType.Name)
            {
                case "RecoveryTier":
                    break;
                case "RecoveryStream":
                    break;
                case "Int32":
                    break;
                case "Boolean":
                    break;
                case "TimeSpan":
                    break;
                case "Guid":
                    break;
                case "HealthState":
                    break;
                case "AvailabilityPredictionMethod":
                    break;
                default:
                    Debug.WriteLine(Property.PropertyType.UnderlyingSystemType.Name);
                    break;
            }
        }
    }
}
