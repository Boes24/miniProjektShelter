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
        public string? CustomerEmail { get; set; }

        [Required]
        [Range(minimum: 1, maximum: System.Int32.MaxValue, ErrorMessage = "Et telefonnummer kan ikke indeholde bogstaver")]
        [BsonElement("Telefon")]
        public int CustomerPhoneNumber { get; set; }

        [BsonElement("Date1")]
        public int BookedDate1 { get; set; }

        [BsonElement("Date2")]
        public int BookedDate2 { get; set; }

        [BsonElement("ShelterID")]
        public string BookedShelterID { get; set; }

        public CustomerBooking(string costumerName, string email, int phoneNumber, int date1, int date2, string shelterID)
        {
            this.CustomerName = costumerName;
            this.CustomerEmail = email;
            this.CustomerPhoneNumber = phoneNumber;
            this.BookedDate1 = date1;
            this.BookedDate2 = date2;
            this.BookedShelterID = shelterID;
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