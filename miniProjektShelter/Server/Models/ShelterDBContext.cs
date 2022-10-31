using System;
using MongoDB.Driver;
using miniProjektShelter.Shared;


namespace miniProjektShelter.Server.Models{
    public class ShelterDBContext{

        private readonly IMongoDatabase mongoDatabase;
        public ShelterDBContext()
        {
            var client = new
            MongoClient("mongodb+srv://Boes24:Hejmeddig12@cluster0.u5a07y6.mongodb.net/test");
            mongoDatabase = client.GetDatabase("shelterdb");
        }
        public IMongoCollection<Shelter> Items
        {
            get
            {
                return mongoDatabase.GetCollection<Shelter>("ShelterAlle");
            }
        }
    }
}

