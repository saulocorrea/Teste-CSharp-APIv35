namespace Questao1
{
    class ContaBancaria
    {
        private readonly double TAXA_SAQUE = 3.50;

        private int _numeroConta;
        private double _saldo;
        private string _nomeTitular;

        public ContaBancaria()
        {
            _numeroConta = 0;
            _saldo = 0;
        }

        public ContaBancaria(int numeroConta, string nomeTitular, double depositoInicial = 0) : this()
        {
            _numeroConta = numeroConta;
            _saldo = depositoInicial;
            _nomeTitular = nomeTitular;
        }

        public override string ToString()
        {
            return $"Conta {_numeroConta}, Titular: {_nomeTitular}, Saldo: $ {_saldo:0.00}";
        }

        internal void Deposito(double quantia)
        {
            _saldo += quantia;
        }

        internal void Saque(double quantia)
        {
            _saldo = _saldo - quantia - TAXA_SAQUE;
        }
    }
}
