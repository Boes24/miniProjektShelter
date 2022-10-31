using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace miniProjektShelter.Shared{
    [BsonIgnoreExtraElements]
    public class Shelter
    {
        public Shelter()
        {
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
            public int? KommuneKode { get; set; }

            [BsonElement("cvr_navn")]
            public string? KommuneNavn { get; set; }

            [BsonElement("navn")]
            public string? Navn { get; set; }

            [BsonElement("facil_ty")]
            public string? Facil_ty { get; set; }

            [BsonElement("lang_beskr")]
            public string? Lang_beskrivelse { get; set; }

            [BsonElement("handicap")]
            public string? Handicap { get; set; }

            [BsonElement("antal_pl")]
            public int? Antal_pl { get; set; }
        }
    }
}
    

