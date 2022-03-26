namespace Api.Autenticacao.Models
{
    public class ValidacaoModel
    {
        private string Mensagem { get; set; }
        private string[] Detalhes { get; set; } = new string[] { };

        public ValidacaoModel(string mensagem, string[] detalhes)
        {
            Mensagem = mensagem;
            Detalhes = detalhes;
        }
    }
}
