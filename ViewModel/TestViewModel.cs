using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication4.ViewModel
{
    public class TestViewModel : BaseViewModel
    {
        #region UI Property
        [BindingProperty]
        public string Name { get => CogoPointModel.Name; }
        [BindingProperty]
        public string Q { get => GetDoubleText(nameof(CogoPointModel.Q)); set => SetPropertyValue(nameof(Q), nameof(CogoPointModel.Q), value, nameof(IsModifiedQ)); }
        [BindingProperty(false)]
        public ObservableCollection<string> ListPipeType { get; }
        [BindingProperty]
        public bool IsModifiedQ { get => _dicRecorders[nameof(IsModifiedQ)].IsModified; }
        #endregion

        public CogoPointModel CogoPointModel { get; private set; }

        private event Action ModifiedChanged;
        private const string DoubleFormat = "0.000";
        private const double DefaultDoubleValue = 0.000d;
        private readonly Dictionary<string, IsModifiedRecorder> _dicRecorders;

        public TestViewModel(CogoPointModel cogoPointModel, ObservableCollection<string> listPipeType, Action modifiedChanged)
        {
            this.CogoPointModel = cogoPointModel;
            this.ListPipeType = listPipeType;
            this.ModifiedChanged += modifiedChanged;
            if (!listPipeType.Contains(cogoPointModel.PipeName))
            {
                cogoPointModel.PipeName = null;
            }
            _dicRecorders = new Dictionary<string, IsModifiedRecorder>()
            {
                { nameof(IsModifiedQ), new IsModifiedRecorder(nameof(cogoPointModel.Q), cogoPointModel.Q) },
            };
        }
        private string GetDoubleText(string modelPropertyName)
        {
            var modelProperty = CogoPointModel.GetType().GetProperty(modelPropertyName);
            if (modelProperty != null && modelProperty.PropertyType == typeof(double))
            {
                double value = (double)modelProperty.GetValue(CogoPointModel);
                if (value != DefaultDoubleValue)
                {
                    return value.ToString(DoubleFormat);
                }
            }
            return null;
        }
        private void SetPropertyValue(string viewModelPropertyName, string modelPropertyName, string str, string modifiedPropertyName)
        {
            var modelProperty = CogoPointModel.GetType().GetProperty(modelPropertyName);
            if (modelProperty != null)
            {
                //Get invalid value
                object value = null;
                bool isValidValue = false;
                if (modelProperty.PropertyType == typeof(double))
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        value = DefaultDoubleValue;
                        isValidValue = true;
                    }
                    else if (double.TryParse(str, out double v))
                    {
                        value = v;
                        isValidValue = true;
                    }
                }
                else if (modelProperty.PropertyType == typeof(double?))
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        value = null;
                        isValidValue = true;
                    }
                    else if (double.TryParse(str, out double v))
                    {
                        value = v;
                        isValidValue = true;
                    }
                }
                else if (modelProperty.PropertyType == typeof(string))
                {
                    value = str;
                    isValidValue = true;
                }
                //The input value is invalid value && The value after input is not the same as the value before input
                if (isValidValue && !IsObjectEquals(modelProperty.GetValue(CogoPointModel), value))
                {
                    modelProperty.SetValue(CogoPointModel, value);
                    if (!string.IsNullOrEmpty(modifiedPropertyName))//Set modified
                    {
                        //Set Recorder IsModified
                        var recorder = _dicRecorders[modifiedPropertyName];
                        recorder.IsModified = !IsObjectEquals(recorder.LastValue, value);
                        Notify(modifiedPropertyName);
                        ModifiedChanged?.Invoke();
                    }
                }
            }
            Notify(viewModelPropertyName);
        }
        private static bool IsObjectEquals(object obj1, object obj2)
        {
            if (obj1 is null && obj2 is null)
                return true;
            else if (!(obj1 is null))
                return obj1.Equals(obj2);
            else
                return obj2.Equals(obj1);
        }

        private class IsModifiedRecorder
        {
            public string ModelPropertyName { get; }
            public bool IsModified { get; set; }
            public object LastValue { get; set; }

            public IsModifiedRecorder(string modelPropertyName, object lastValue)
            {
                ModelPropertyName = modelPropertyName;
                LastValue = lastValue;
            }
        }
    }
}
