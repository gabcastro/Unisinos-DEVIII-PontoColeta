using Flunt.Validations;
using Flunt.Notifications;

namespace PontoColeta.ViewModels.CoordinateViewModels
{
    public class EditorCoordinateViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string NameOfPlace { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMinLen(Latitude, 2, "Latitude", "A latitude deve conter no mínimo 2 caracteres")
                    .HasMinLen(Longitude, 2, "Longitude", "A longitude deve conter no mínimo 2 caracteres")
                    .HasMaxLen(Latitude, 11, "Latitude", "A latitude deve conter até 11 caracteres")
                    .HasMaxLen(Longitude, 11, "Longitude", "A longitude deve conter até 11 caracteres")
            );
        }
    }
}