//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exercicio3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Veiculo
    {
        public Veiculo()
        {
            this.Aluguer = new HashSet<Aluguer>();
        }
    
        public int @ref { get; set; }
        public string rentacar { get; set; }
        public System.DateTime data_registo { get; set; }
        public string matricula { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public Nullable<System.DateTime> ano_modelo { get; set; }
        public string cor { get; set; }
        public int nr_portas { get; set; }
        public float kilom_actual { get; set; }
        public string grupo { get; set; }
    
        public virtual ICollection<Aluguer> Aluguer { get; set; }
        public virtual Empresa_Aluguer Empresa_Aluguer { get; set; }
        public virtual Grupo Grupo1 { get; set; }
    }
}
