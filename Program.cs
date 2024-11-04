using System;
using System.Threading;

class Program
{
    private static Mutex mutex = new Mutex();
    private static int sharedResource = 0;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Thread[] threads = new Thread[5];
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(AccessResource);
            threads[i].Name = $"Thread {i + 1}";
            threads[i].Start();
        }

        
        foreach (Thread t in threads)
        {
            t.Join();
        }

        Console.WriteLine("Усі потоки завершено.");
    }

    
    static void AccessResource()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} очікує доступ до ресурсу.");

        
        mutex.WaitOne();

        try
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} отримав доступ до ресурсу.");
            sharedResource++;
            Console.WriteLine($"{Thread.CurrentThread.Name} змінив ресурс: {sharedResource}");
            Thread.Sleep(1000); 
        }
        finally
        {
            
            Console.WriteLine($"{Thread.CurrentThread.Name} звільнив ресурс.");
            mutex.ReleaseMutex();
        }
    }
}
