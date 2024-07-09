using System;

namespace PasswordSecurityExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cadastro de Usuário:");
            Console.Write("Digite uma senha: ");
            string password = Console.ReadLine();

            // Verificar a força da senha
            if (!PasswordManager.IsValidPassword(password))
            {
                Console.WriteLine("A senha deve ter no mínimo 6 caracteres e pelo menos 1 número, 1 letra e 1 caracter especial.");
                return;
            }

            // Gerar o hash e salt da senha
            var (hash, salt) = PasswordManager.HashPassword(password);
            Console.WriteLine($"Senha Hash: {hash}");
            Console.WriteLine($"Salt: {salt}");

            // Simular armazenamento no banco de dados
            // Normalmente você armazenaria hash e salt no banco de dados aqui

            // Validação de senha
            Console.WriteLine("\nValidação de Usuário:");
            Console.Write("Digite a senha novamente: ");
            string enteredPassword = Console.ReadLine();

            bool isValid = PasswordManager.ValidatePassword(enteredPassword, hash, salt);
            if (isValid)
            {
                Console.WriteLine("Senha válida!");
            }
            else
            {
                Console.WriteLine("Senha inválida!");
            }
        }
    }
}
