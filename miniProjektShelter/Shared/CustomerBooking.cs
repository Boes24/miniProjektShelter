using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace miniProjektShelter.Shared{

    [BsonIgnoreExtraElements]
    public class CustomerBooking{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? MongoId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Dit navn kan ikke indeholde mere en 50 bogstaver")]
        [BsonElement("Kundenavn")]
        public string? CustomerName { get; set; }

        [Required]
        [EmailAddress]
        [BsonElement("Email")]
        public string? Email { get; set; }

        [Required]
        [Range(minimum: 1, maximum: System.Int32.MaxValue, ErrorMessage = "Et telefonnummer kan ikke indeholde bogstaver")]
        [BsonElement("Telefon")]
        public int PhoneNumber { get; set; }

        [BsonElement("Date1")]
        public int Date1 { get; set; }

        [BsonElement("Date2")]
        public int Date2 { get; set; }

        [BsonElement("ShelterID")]
        public string ShelterID { get; set; }

        public CustomerBooking(string costumerName, string email, int phoneNumber, int date1, int date2, string shelterID)
        {
            this.CustomerName = costumerName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Date1 = date1;
            this.Date2 = date2;
            this.ShelterID = shelterID;
        }

        public CustomerBooking()
        {

        }



        public Shelter FindShelterInList(string shelterMongoID, List<Shelter> ListOfShelters)
        {
            foreach (Shelter tmpShelter in ListOfShelters)
            {
                if (tmpShelter.MongoId == shelterMongoID)
                {
                    return tmpShelter;
                }
            }
            return new Shelter(navn: "FINDES IKKE!!! IDIOT");
        }

    }
}