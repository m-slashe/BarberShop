namespace BarberShop.Clientes.Dominio.Entidades
{
    public class Servico: Entity
    {
        public Servico(string nome, double valor)
        {
            Nome = nome;
            Valor = valor;
        }

        public string Nome { get; protected set; }
        public double Valor { get; protected set; }
    }
}
