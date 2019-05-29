namespace University_System.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Windows.Data;
    using UniSys.Database.Data.Models.Users;

    class StringsCollectionConverter : IValueConverter
    {
        // Конвертира колекцията от студенти за view-то
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            List<Student> list = value as List<Student>;
            StringBuilder result = new StringBuilder();

            foreach (var student in list)
            {
                result.AppendLine(student.Names);
            }
            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
