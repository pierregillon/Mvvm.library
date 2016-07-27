using System;
using System.Threading;

namespace Mvvm
{
    public class UISynchronizedNotifiableObject : NotifiableObject
    {
        private readonly SynchronizationContext _context;

        public UISynchronizedNotifiableObject()
        {
            _context = SynchronizationContext.Current;
        }

        protected override void RaisePropertyChanged(string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (_context == null) {
                base.RaisePropertyChanged(propertyName);
            }
            else {
                _context.Send(state => { base.RaisePropertyChanged(propertyName); }, null);
            }
        }
    }
}