using PEATabuSearch.Algorithm;
using PEATabuSearch.Entities;
using PEATabuSearch.Functionalities;

namespace PEATabuSearch
{
    public class Program
    {
        private static void Main()
        {
            Console.WriteLine("Algorytm Tabu Search");
            Console.WriteLine("Domyślne opcje 2 minuty, 2-opt");

            double stopTime = 120;
            int neighbourChoice = 1;
            Matrix? matrix = null;
            TabuSearch tabuSearch;
            ConsoleKeyInfo choice;
            do
            {
                Menu();
                choice = Console.ReadKey();
                Console.WriteLine();
                try
                {
                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            FileReader fileReader = new();
                            Console.WriteLine("Podaj ścieżkę do pliku.");
                            matrix = fileReader.ReadFile(Console.ReadLine() ?? string.Empty);
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine("Ustaw czas stopu.");
                            stopTime = int.Parse(Console.ReadLine() ?? "120");
                            break;
                        case ConsoleKey.D3:
                            Console.WriteLine("Wybór sąsiedztwa:\n1. 2-opt swap\n2. Przez wstawianie.\n3. Przez zamiane dwóch krawędzi");
                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            switch (keyInfo.Key)
                            {
                                case ConsoleKey.D1:
                                    neighbourChoice = 1;
                                    break;
                                case ConsoleKey.D2:
                                    neighbourChoice = 2;
                                    break;
                                case ConsoleKey.D3:
                                    neighbourChoice = 3;
                                    break;
                                default:
                                    Console.WriteLine("\nBłędny klawisz. Wybierz 1, 2 lub 3.");
                                    break;
                            }
                            Console.WriteLine();
                            break;
                        case ConsoleKey.D4:
                            if (matrix != null)
                            {
                                tabuSearch = new TabuSearch(matrix, stopTime, neighbourChoice);
                                tabuSearch.ExecuteTabuSearch();
                            }

                            break;
                        default:
                            Console.WriteLine("Brak opcji.");
                            break;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            } while (!(choice.Equals(ConsoleKey.D6)));

        }

        private static void Menu()
        {

            Console.WriteLine("Wybierz opcje:\n1. Pobierz dane z pliku.\t2. Wprowadz kryterium stopu w sekundach.\n3. Wybierz sąsiedztwo\t\t 4. Uruchom algorytm.");
        }
    }
}