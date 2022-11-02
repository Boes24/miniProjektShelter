using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace miniProjektShelter.Shared{

    [BsonIgnoreExtraElements]
    public class CostumerBooking{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? MongoId { get; set; }

        [BsonElement("Kundenavn")]
        public string? CostumerName { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("Telefon")]
        public int? PhoneNumber { get; set; }

        [BsonElement("Date1")]
        public int Date1 { get; set; }

        [BsonElement("Date2")]
        public int Date2 { get; set; }

        [BsonElement("ShelterID")]
        public string ShelterID { get; set; }

        public CostumerBooking(string costumerName, string email, int phoneNumber, int date1, int date2, string shelterID)
        {
            this.CostumerName = costumerName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Date1 = date1;
            this.Date2 = date2;
            this.ShelterID = shelterID;
        }

        public CostumerBooking()
        {

        }





    }
}