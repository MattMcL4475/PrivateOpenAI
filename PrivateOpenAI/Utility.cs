namespace PrivateOpenAI
{
    public class Utility
    {
        public void InvokeControlMethod<T1, T2>(Control control, Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));

            if (action == null)
                throw new Exception("Action is null");

            var method = () => action(arg1, arg2);

            if (control.InvokeRequired)
            {
                control.Invoke(method);
                return;
            }

            method();
        }

        public void UpdateControlProperty<T>(Control control, string propertyName, T value)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));

            var propertyInfo = typeof(Control).GetProperty(propertyName);

            if (propertyInfo == null)
                throw new Exception($"Property {propertyName} does not exist");

            if (!propertyInfo.PropertyType.IsAssignableFrom(typeof(T)))
                throw new Exception($"Input value of type {typeof(T).Name} cannot be assigned to property {propertyName}");

            var setProperty = () => propertyInfo.SetValue(control, value, null);

            if (control.InvokeRequired)
            {
                control.Invoke(setProperty);
                return;
            }

            setProperty();
        }

        public string ConvertToWindowsLineEndings(string text)
        {
            return text.Replace("\n", "\r\n");
        }

        public string ConvertToUnixLineEndings(string text)
        {
            return text.Replace("\r\n", "\n");
        }
    }
}
