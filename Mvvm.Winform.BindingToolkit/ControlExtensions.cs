using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace Mvvm.Winform.BindingToolkit
{
    public static class ControlExtensions
    {
        public static void BindVisibility<T>(this Control control, T viewModel, Expression<Func<T, bool>> property, bool reverse = false) where T : INotifyPropertyChanged
        {
            var memberExpression = (MemberExpression) property.Body;
            var propertyInfo = (PropertyInfo) memberExpression.Member;
            viewModel.PropertyChanged += (sender, args) => {
                if (args.PropertyName == memberExpression.Member.Name) {
                    var isVisible = (bool) propertyInfo.GetValue(viewModel);
                    if (isVisible) {
                        control.Visible = !reverse;
                    }
                    else {
                        control.Visible = reverse;
                    }
                }
            };
        }
    }
}