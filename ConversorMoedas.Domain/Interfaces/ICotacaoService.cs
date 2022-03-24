﻿using ConversorMoedas.Common.Enums;

namespace ConversorMoedas.Domain.Interfaces
{
    public interface ICotacaoService
    {
        Task<decimal> Converter(Moeda moedaOrigem, Moeda moedaDestino, decimal valor);
        void Iniciar(Guid usuarioId);
        Task<Guid> SalvarCotacao();
    }
}