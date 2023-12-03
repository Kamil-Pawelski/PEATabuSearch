using PEATabuSearch.Entities;
using PEATabuSearch.Functionalities;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        FileReader fileReader = new();
        Matrix matrix = fileReader.ReadFile(@"C:\Users\kamil\Desktop\PROJEKTY XDD\PEATabuSearch\Files\ftv47.atsp");
        matrix.Print();
    }
}