namespace University_System.ViewModel
{
    using Contracts;
    using System.Collections.Generic;
    using System.ComponentModel;
    using UniSys.Database.Data;
    using UniSys.Database.Data.Services.Interfaces;
    using UniSys.Database.Data.Xml.Services;

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public IDbService databaseService = SystemDatabase.GetDb();
        public IXmlService xmlService = XmlDatabase.GetXmlDatabase();

        #region Properties
        public IView View { get; set; }
        // колекция от полове
        public List<string> GenderTypes { get; } = 
            new List<string>(CollectionDataService.Instance.GenderTypes);
        // колекция от градове
        public List<string> RegionCollection { get; } =
            new List<string>(CollectionDataService.Instance.RegionCollection);
        #endregion

        // използва се за да известява нещата, които са bind-нати за класа, че има някакви промено
        public event PropertyChangedEventHandler PropertyChanged; 

        protected void OnPropertyChanged(string propertyName = null) // проверява дали е null, тоест дали нещо се е закачило
        {
            //извиква класа
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
