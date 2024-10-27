using Bizacademia.Interview.Business;

namespace Bizacademia.Interview
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ExcelToJsonService? service = new ExcelToJsonService();
            string filePath = "products.xlsx"; // Path to your Excel file
            string json = service.ConvertToJson(filePath);
            Console.WriteLine(json);
        }
    }
}