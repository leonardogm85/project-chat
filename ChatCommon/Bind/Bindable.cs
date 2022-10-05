using ChatCommon.Events;

namespace ChatCommon.Bind
{
    public abstract class Bindable
    {
        public event EventHandler<ErrorChangedEventArgs>? ErrorChanged;

        private readonly Dictionary<string, List<string>> _errors = new();

        public bool HasErrors(string propertyName)
        {
            return GetErrors(propertyName).Any();
        }

        public List<string> GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _errors.SelectMany(error => error.Value).ToList();
            }

            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }

            return Enumerable.Empty<string>().ToList();
        }

        public void AddError(string message, string propertyName)
        {
            AddErrors(new() { message }, propertyName);
        }

        public void AddErrors(List<string> propertyErrors, string propertyName)
        {
            _errors[propertyName] = propertyErrors;
            OnErrorChanged(propertyName);
        }

        public void RemoveErrors(string propertyName)
        {
            _errors.Remove(propertyName);
            OnErrorChanged(propertyName);
        }

        private void OnErrorChanged(string propertyName)
        {
            ErrorChanged?.Invoke(this, new ErrorChangedEventArgs(propertyName));
        }
    }
}
