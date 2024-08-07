using System;
using System.Collections.Generic;
using System.Linq;

namespace Estacionamento
{
    class Program
    {
        static List<Veiculo> veiculos = new List<Veiculo>();
        static decimal PrecoPorHora = 7.0m;  // Preço por hora atualizado para 7 reais

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nSistema de Estacionamento");
                Console.WriteLine("1. Adicionar Veículo");
                Console.WriteLine("2. Remover Veículo");
                Console.WriteLine("3. Listar Veículos");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarVeiculo();
                        break;
                    case "2":
                        RemoverVeiculo();
                        break;
                    case "3":
                        ListarVeiculos();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarVeiculo()
        {
            Console.Write("Digite a placa do veículo: ");
            var placa = Console.ReadLine();

            Console.Write("Digite o preço inicial cobrado pelo estacionamento: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal precoInicial))
            {
                Console.WriteLine("Preço inicial inválido. O veículo não foi adicionado.");
                return;
            }

            var veiculo = new Veiculo
            {
                Placa = placa ?? string.Empty,
                Entrada = DateTime.Now,
                PrecoInicial = precoInicial
            };

            veiculos.Add(veiculo);
            Console.WriteLine($"Veículo {placa} adicionado com sucesso às {veiculo.Entrada}.");
        }

        static void RemoverVeiculo()
        {
            // Lista os veículos atuais
            ListarVeiculos();

            Console.Write("Digite a placa do veículo que deseja remover: ");
            var placa = Console.ReadLine();

            var veiculo = veiculos.FirstOrDefault(v => v.Placa == placa);
            if (veiculo == null)
            {
                Console.WriteLine("Veículo não encontrado.");
                return;
            }

            veiculos.Remove(veiculo);
            var tempoPermanencia = DateTime.Now - veiculo.Entrada;
            var valorCobrado = veiculo.PrecoInicial + (decimal)tempoPermanencia.TotalHours * PrecoPorHora;

            Console.WriteLine($"Veículo {placa} removido. Tempo de permanência: {tempoPermanencia.TotalHours:F2} horas. Valor cobrado: R${valorCobrado:F2} Reais.");
        }

        static void ListarVeiculos()
        {
            if (veiculos.Count == 0)
            {
                Console.WriteLine("Nenhum veículo no estacionamento.");
                return;
            }

            Console.WriteLine("Veículos no estacionamento:");
            foreach (var veiculo in veiculos)
            {
                Console.WriteLine($"Placa: {veiculo.Placa}, Entrada: {veiculo.Entrada}, Preço Inicial: R${veiculo.PrecoInicial} Reais");
            }
        }
    }

    class Veiculo
    {
        public string Placa { get; set; } = string.Empty; // Inicializa com uma string vazia
        public DateTime Entrada { get; set; }
        public decimal PrecoInicial { get; set; } // Propriedade para armazenar o preço inicial
    }
}
