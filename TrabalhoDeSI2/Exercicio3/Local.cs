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
    
    public partial class Local
    {
        public Local()
        {
            this.Aluguer = new HashSet<Aluguer>();
            this.Aluguer1 = new HashSet<Aluguer>();
        }
    
        public int codigo { get; set; }
        public string nome { get; set; }
        public string morada { get; set; }
    
        public virtual ICollection<Aluguer> Aluguer { get; set; }
        public virtual ICollection<Aluguer> Aluguer1 { get; set; }
    }
}