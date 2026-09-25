using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRPlanningTool
{
    public interface IMainForm
    {
        void UpdateCreateManagementPacksToolstrip();

        void UpdateLoadDependencyDataToolstripForReadWrite();

        void UpdateLoadDependencyDataToolstripForReadOnly();

        void EnableDependencyMapToolstrip();

        void UpdateSystemCenterToolStrip();

        void DisableGUI();

        void EnableGUI();

        void Reset();

        void SetLocation(Point Location);

        void SetSize(Size Size);

        void SetHeight(int Height);

        void EnableSystemCenterToolStrip();

    }
}
