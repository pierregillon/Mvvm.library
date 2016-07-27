using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace Mvvm.Winform.BindingToolkit
{
    public static class FormExtensions
    {
        public static void BindPopupVisibility<T>(this Form form, T viewModel, Expression<Func<T, bool>> property) where T : INotifyPropertyChanged
        {
            var memberExpression = (MemberExpression) property.Body;
            var propertyInfo = (PropertyInfo) memberExpression.Member;
            viewModel.PropertyChanged += (sender, args) => {
                if (args.PropertyName == memberExpression.Member.Name) {
                    var isVisible = (bool) propertyInfo.GetValue(viewModel);
                    if (isVisible) {
                        form.ShowDialog();

                        // Fin d'exécution de la pompe à message ShowDialog(), 
                        // la fenêtre est donc fermée, on remet la propriété à false.
                        propertyInfo.SetValue(viewModel, false);
                    }
                    else {
                        if (form.Visible) {
                            form.Visible = false;
                        }
                    }
                }
            };
        }
    }
}