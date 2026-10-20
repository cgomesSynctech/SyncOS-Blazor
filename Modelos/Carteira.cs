namespace Modelos
{
    public class Carteira
    {
        public int Id { get; set; }
        public string Convenio { get; set; }
        public string DVConvenio { get; set; }
        public string Descricao { get; set; }
        public string TipoCarteira { get; set; }
        public string Variacao { get; set; }
        public string Agencia { get; set; }
        public string DVAgencia { get; set; }
        public string Banco { get; set; }
        public string DVBanco { get; set; }
        public string Conta { get; set; }
        public string DVConta { get; set; }
        public int CedenteId { get; set; }
        public Cedente Cedente { get; set; }

    }
}