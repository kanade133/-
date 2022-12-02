using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApplication4
{
    static class ControlExtenion
    {
        public static T GetVisualChild<T>(this DependencyObject obj) where T : FrameworkElement
        {
            return GetVisualChild<T>(obj, a => true);
        }

        public static T GetVisualChild<T>(this DependencyObject obj, string name) where T : FrameworkElement
        {
            return GetVisualChild<T>(obj, a => a.Name == name);
        }

        public static T GetVisualChild<T>(this DependencyObject obj, Predicate<T> predicate) where T : FrameworkElement
        {
            if (obj == null)
            {
                return null;
            }
            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                T child = VisualTreeHelper.GetChild(obj, i) as T;
                if (child != null && predicate(child))
                {
                    return child;
                }
                else
                {
                    T childOfChild = GetVisualChild(child, predicate);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        public static List<T> GetVisualChildren<T>(this DependencyObject obj) where T : FrameworkElement
        {
            List<T> list = new List<T>();
            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null)
                {
                    if (child is T)
                    {
                        list.Add((T)child);
                    }
                    List<T> childOfChildren = GetVisualChildren<T>(child);
                    list.AddRange(childOfChildren);
                }
            }
            return list;
        }

        public static T GetVisualParent<T>(this DependencyObject obj) where T : FrameworkElement
        {
            return GetVisualParent<T>(obj, a => true);
        }

        public static T GetVisualParent<T>(this DependencyObject obj, string name) where T : FrameworkElement
        {
            return GetVisualParent<T>(obj, a => a.Name == name);
        }

        public static T GetVisualParent<T>(this DependencyObject obj, Predicate<T> predicate) where T : FrameworkElement
        {
            if (obj == null)
            {
                return null;
            }
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                T parentT = parent as T;
                if (parentT != null && predicate(parentT))
                {
                    return parentT;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }
    }
}
