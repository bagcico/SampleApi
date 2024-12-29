int accountBalance = 100000; // Hesap bakiyesi

//Bir kullanıcının hesabında 100.000 TL var,
//32 kişiye aynı anda rastgele bir tutarda para transferi yapılacak
//Transfer methodunun içerisindeki hata nedir?
        
Parallel.For(0, 32, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount * 2}, i =>
{
    Transfer(Random.Shared.Next(1, 10) * 10);
});

Console.WriteLine($"Final: {accountBalance}");

void Transfer(object obj)
{
    int amount = (int)obj;
    Console.WriteLine($"{amount}");
    accountBalance -= amount;
}