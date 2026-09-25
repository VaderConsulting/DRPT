using ISA.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DRPlanningTool
{
    public interface IDrawingForm
    {
        void SetupContextMenuStrip();

        void SetupListView();

        void SetupCheckboxes();

        void DisableGUI();

        void BeginUpdate();

        void EndUpdate();

        void EnableGUI();

        void FilterList();

        void Reset();

        void Refresh();

    }
}
