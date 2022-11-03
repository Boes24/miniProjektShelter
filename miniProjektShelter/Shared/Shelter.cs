using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using miniProjektShelter.Shared;
namespace miniProjektShelter.Shared
{
    [BsonIgnoreExtraElements]
    public class Shelter
    {
        public Shelter()
        {
        }
        public Shelter(string navn = "")
        {
            this.Properties = new ShelterProperties();
            this.Properties.ShelterName = navn;
            this.Properties.MunicipalName = "";
            this.Properties.LongDescription = "";
            this.Properties.Capacity = 0;
            this.Properties.Handicap = "";
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? MongoId { get; set; }

        [BsonElement("properties")]
        public ShelterProperties Properties { get; set; }

        [BsonIgnoreExtraElements]
        public class ShelterProperties
        {
            [BsonElement("kommunekode")]
            public int? MunicipalCode { get; set; }

            [BsonElement("cvr_navn")]
            public string? MunicipalName { get; set; }

            [BsonElement("navn")]
            public string? ShelterName { get; set; }

            [BsonElement("facil_ty")]
            public string? FacilityType { get; set; }

            [BsonElement("lang_beskr")]
            public string? LongDescription { get; set; }

            [BsonElement("handicap")]
            public string? Handicap { get; set; }

            [BsonElement("antal_pl")]
            public int? Capacity { get; set; }

        }

        [BsonIgnoreExtraElements]
        public class BookingObject{

            [BsonElement("KundeNavn")]
            public string? CostumerName { get; set; }

            [BsonElement("Email")]
            public string? Email { get; set; }

            [BsonElement("Telefon")]
            public int? Phone { get; set; }

            [BsonElement("AntalPersoner")]
            public int? NumberOfPersons { get; set; }

            [BsonElement("Datoer")]
            public int[]? Dates { get; set; }

            [BsonElement("id")]
            public int ID { get; set; }
        }

    }
}


