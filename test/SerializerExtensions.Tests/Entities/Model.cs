using System;
using System.Collections.Generic;

namespace SerializerExtensions.Tests.Entities
{
    [Serializable]
    public class Model
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<ModelDetail> Details { get; set; }

        public Model()
        {
            Details = new List<ModelDetail>();
        }
    }
}
