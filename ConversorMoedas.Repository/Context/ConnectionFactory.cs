using ConversorMoedas.Repository.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorMoedas.Repository.Context
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly MongoSettings _mongoSettings;
        public ConnectionFactory(IOptions<MongoSettings> mongoSettings)
        {
            _mongoSettings = mongoSettings.Value;
        }

        public IMongoDatabase GetDatabase()
        {
            var mongoClient = new MongoClient(_mongoSettings.ConnectionString);
            return mongoClient.GetDatabase(_mongoSettings.DatabaseName);
        }
    }
}
