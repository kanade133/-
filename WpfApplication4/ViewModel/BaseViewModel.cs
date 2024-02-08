using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication4.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyBindingProperties()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                var attribute = property.GetCustomAttribute<BindingPropertyAttribute>();
                if (attribute != null && attribute.NeedNotify)
                {
                    Notify(property.Name);
                }
            }
        }
        protected void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        [AttributeUsage(AttributeTargets.Property)]
        protected class BindingPropertyAttribute : Attribute
        {
            public bool NeedNotify { get; }
            /// <summary>
            /// Identify this is a binding property
            /// </summary>
            /// <param name="needNotify">Need notify this property</param>
            public BindingPropertyAttribute(bool needNotify = true)
            {
                NeedNotify = needNotify;
            }
        }
    }
}
