using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seguros.Models
{
    [Flags]
    public enum Danger
    {
        Bajo,
        Medio,
        MedioAlto,
        Alto
    }

    public class Policy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Period { get; set; }
        public Danger Danger { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

}
