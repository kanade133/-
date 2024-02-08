using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication4
{
    public delegate Point GetDragDropPosition(IInputElement theElement);

    class UITools
    {
        private static bool IsTheMouseOnTargetRow(Visual theTarget, GetDragDropPosition pos)
        {
            Rect posBounds = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point theMousePos = pos((IInputElement)theTarget);
            return posBounds.Contains(theMousePos);
        }

        private static DataGridRow GetDataGridRowItem(int index, DataGrid dgEmployee)
        {
            if (dgEmployee.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                return null;
            return dgEmployee.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
        }

        public static int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos, DataGrid dgEmployee)
        {
            int curIndex = -1;
            for (int i = 0; i < dgEmployee.Items.Count; i++)
            {
                DataGridRow itm = GetDataGridRowItem(i, dgEmployee);
                if (IsTheMouseOnTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }

        public static DataGridRow GetDataGridItemCurrentRow(GetDragDropPosition pos, DataGrid dgEmployee)
        {
            DataGridRow curRow = null;
            for (int i = 0; i < dgEmployee.Items.Count; i++)
            {
                DataGridRow itm = GetDataGridRowItem(i, dgEmployee);
                if (IsTheMouseOnTargetRow(itm, pos))
                {
                    curRow = itm;
                    break;
                }
            }
            return curRow;
        }
    }
}
