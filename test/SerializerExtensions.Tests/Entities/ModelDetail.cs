using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializerExtensions.Tests.Entities
{
    [Serializable]
    [XmlRoot(Namespace ="http://www.domain.com")]
    public class ModelDetail
    {
        public int Id { get; set; }
        public string Description1 { get; set; }
        public long Value1 { get; set; }
        public DateTime? Date { get; set; }

    }
}
