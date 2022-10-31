using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace miniProjektShelter.Shared{
    public class Shelter{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? MongoId { get; set; }

        [BsonElement("properties.kommunekode")]
        public int KommuneKode { get; set; }

        [BsonElement("properties.navn")]
        public int Navn { get; set; }

        [BsonElement("properties.facil_ty")]
        public int Facil_ty { get; set; }

        [BsonElement("properties.lang_beskr")]
        public int Lang_beskrivelse { get; set; }

        [BsonElement("properties.handicap")]
        public int Handicap { get; set; }

        [BsonElement("properties.antal_pl")]
        public int Antal_pl { get; set; }


        public Shelter()
        {
        }
    }
}

