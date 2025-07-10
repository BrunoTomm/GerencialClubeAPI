namespace GerencialClube.Dominio.Entidades
{
    public class Endereco
    {
        public Guid Id { get; private set; }

        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Cidade { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }

        public Guid SocioId { get; private set; }
        public Socio Socio { get; private set; }

        protected Endereco() { }

        public Endereco(
            string cep,
            string logradouro,
            string cidade,
            string numero,
            string complemento)
        {
            Id = Guid.NewGuid();
            Cep = cep;
            Logradouro = logradouro;
            Cidade = cidade;
            Numero = numero;
            Complemento = complemento;
        }

        public Endereco AtualizarEndereco(Endereco endereco)
        {
            Cep = endereco.Cep;
            Logradouro = endereco.Logradouro;
            Cidade = endereco.Cidade;
            Numero = endereco.Numero;
            Complemento = endereco.Complemento;
            return this;
        }

        public void DefinirSocio(Guid clienteId)
        {
            SocioId = clienteId;
        }
    }
}
