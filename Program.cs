using PEATabuSearch.Algorithm;
using PEATabuSearch.Entities;
using PEATabuSearch.Functionalities;

namespace PEATabuSearch
{
    public class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello, World!");
            FileReader fileReader = new();
            Matrix matrix = fileReader.ReadFile(@"C:\Users\kamil\Desktop\PROJEKTY XDD\PEATabuSearch\Files\ftv47.atsp");
            var x = new TabuSearch(matrix);
            x.ExecuteTabuSearch();

        }
    }
}