namespace UniSys.Database.Data.Models.Users.Interfaces
{
    public interface IUser
    {
        #region Properties

        string FirstName { get; set; }

        string LastName { get; set; }

        string PersonalNumber { get; set; }

        string BirthDate { get; set; }

        string Gender { get; set; }

        string Faculty { get; set; }

        string Region { get; set; }

        string Image { get; set; }

        #endregion
    }
}
