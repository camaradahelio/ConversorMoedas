using MongoDB.Driver;

namespace ConversorMoedas.Repository.Context
{
    public interface IConnectionFactory
    {
        IMongoDatabase GetDatabase();
    }
}
