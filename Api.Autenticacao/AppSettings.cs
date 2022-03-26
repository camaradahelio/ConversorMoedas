namespace Api.Autenticacao
{
    public class AppSettings
    {
        public string Chave { get; set; }
        public string Emissor { get;set; }
        public string ValidoEm { get; set; }
        public int ExpiracaoHoras { get; set; }
    }
}
