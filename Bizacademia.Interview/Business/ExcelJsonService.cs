using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Bizacademia.Interview.Business
{
    public class ExcelToJsonService
    {
        public string ConvertToJson(string filePath)
        {
            var productMap = ExtractSheet1(filePath);
            var discountMap = ExtractSheet2(filePath);

            // Match discount percentages with products
            foreach (Product product in productMap.Values)
            {
                if (discountMap.TryGetValue(product.DiscountCode, out double discountPercentage))
                {
                    product.DiscountPercentage = discountPercentage;
                    product.DiscountPrice = product.Cost - (product.Cost * discountPercentage / 100);
                }
            }

            // Serialize to JSON and print
            string jsonOutput = JsonConvert.SerializeObject(productMap.Values, Formatting.Indented);
            return jsonOutput;
        }

        private Dictionary<string, Product> ExtractSheet1(string excelPath)
        {
            var productMap = new Dictionary<string, Product>();

            using (var fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet1 = workbook.GetSheetAt(0);

                for (int i = 1; i <= sheet1.LastRowNum; i++) // Skip header row
                {
                    IRow row = sheet1.GetRow(i);
                    string productId = row.GetCell(0).ToString();
                    string productName = row.GetCell(1).ToString();
                    double cost = double.Parse(row.GetCell(2).ToString());
                    string discountCode = row.GetCell(3).ToString();

                    var product = new Product(productId, productName, cost, discountCode, 0);
                    productMap[productId] = product;
                }
            }
            return productMap;
        }
        private Dictionary<string, double> ExtractSheet2(string excelPath)
        {
            var discountMap = new Dictionary<string, double>();

            using (var fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet2 = workbook.GetSheetAt(1);

                for (int i = 1; i <= sheet2.LastRowNum; i++) // Skip header row
                {
                    IRow row = sheet2.GetRow(i);
                    string discountCode = row.GetCell(1).ToString();
                    double discountPercentage = double.Parse(row.GetCell(2).ToString());

                    discountMap[discountCode] = discountPercentage;
                }
            }
            return discountMap;
        }
    }
}
