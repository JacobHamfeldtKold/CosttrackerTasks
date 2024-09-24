using System.ComponentModel.DataAnnotations;

namespace DotnetWebApiWithEFCodeFirst.Models
{
    public class HistoricRate
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string BaseCurrency { get; set; }

        public double USD { get; set; }

        public double EUR { get; set; }

        public double AUD { get; set; }

        public double CAD { get; set; }

        public double PLN { get; set; }

        public double MXN { get; set; }
    }
}
