namespace Lista_de_Compras;

public class Program
{
    static int idArquivoTxt = 1;

    static void Main(string[] args)
    {      
        while (true)
        {
            Console.WriteLine("1 - Nova Lista de Compras\n" +
                              "2 - Mostrar Lista de Compras");

            Console.Write("\nSelecione uma opção:");
            int opcao = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            switch (opcao)
            {
                case 1:
                    NovaLista();
                    break;
                case 2:
                    ImprimirLista();
                    break;
            }
        }
    }


    static void NovaLista()
    {
        List<Produto> lista = new();

        while (true)
        {
            Console.Write("Digite o nome do produto: ");
            string nomeProduto = Console.ReadLine()!;
            Console.Write("\nDigite a unidade do produto (kg, pacote, caixa etc.): ");
            string unidade = Console.ReadLine()!;
            Console.Write("\nDigite a quantidade do produto: ");
            double quantidade = Convert.ToDouble(Console.ReadLine());

            lista.Add(new Produto { Nome = nomeProduto, Quantidade = quantidade, Unidade = unidade });
            Console.Clear();

            Console.WriteLine("Produto Adicionado! Deseja adicionar mais um produto (s/n): ");
            char maisUmProduto = Convert.ToChar(Console.ReadLine()!);

            if(maisUmProduto == 'n')
            {
                break;
            }

            Console.Clear();
        }

        string diretorioBase = Directory.GetCurrentDirectory();
        string pastaListas = Path.Combine(diretorioBase, "Lista de Compras");

        if (!Directory.Exists(pastaListas))
        {
            Directory.CreateDirectory(pastaListas);
        }

        string nomeArquivo = $"produtos_{idArquivoTxt}.txt";
        string caminhoArquivo = Path.Combine(pastaListas, nomeArquivo);

        using (StreamWriter sw = new StreamWriter(caminhoArquivo))
        {
            foreach (Produto produto in lista)
            {
                sw.WriteLine($"{produto.Nome},{produto.Quantidade},{produto.Unidade}");
            }
        }

        Console.WriteLine($"Lista de produtos foi salva no arquivo '{nomeArquivo}'.");

        idArquivoTxt++;

        Thread.Sleep(3000);
        Console.Clear();
    }

    static void ImprimirLista()
    {       
        string diretorioBase = AppDomain.CurrentDomain.BaseDirectory;
        string pastaListas = Path.Combine(diretorioBase, "Lista de Compras");
      
        if (Directory.Exists(pastaListas))
        {            
            string[] arquivos = Directory.GetFiles(pastaListas, "*.txt");

            if (arquivos.Length > 0)
            {
                Console.WriteLine("Listas Salvas:\n");
                
                foreach (string arquivo in arquivos)
                {
                    Console.WriteLine($"--- {Path.GetFileName(arquivo)} ---\n");
                   
                    string[] linhas = File.ReadAllLines(arquivo);
                    foreach (string linha in linhas)
                    {
                        Console.WriteLine(linha);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Nenhuma lista foi encontrada.");
            }
        }
        else
        {
            Console.WriteLine("O diretório de listas não existe.");
        }

        Console.Write("Pressione qualquer tecla...");
        Console.ReadKey();
        Console.Clear();
    }
}