using ConversorMoedas.Domain.Entities;
using ConversorMoedas.Domain.Interfaces;
using ConversorMoedas.Repository.Context;
using MongoDB.Driver;

namespace ConversorMoedas.Repository.Repository
{
    public class CotacaoRepository : ICotacaoRepository
    {
        private const string CollectionName = "Cotacoes";
        private readonly IMongoCollection<Cotacao> _cotacoesCollection;

        public CotacaoRepository(IConnectionFactory connectionFactory)
        {
            _cotacoesCollection = connectionFactory.GetDatabase().GetCollection<Cotacao>(CollectionName);
        }

        public async Task<Guid> SalvarCotacao(Cotacao cotacao)
        {
            await _cotacoesCollection.InsertOneAsync(cotacao); 
            return cotacao.Id;
        }
    }
}
