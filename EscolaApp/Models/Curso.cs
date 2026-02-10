using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaApp.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CargaHoraria { get; set; }
    }
}
