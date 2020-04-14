using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebAppReadCard.Models
{
    [XmlRoot(ElementName = "neuqpay")]
    public class NeuqPay<T>
    {
        /// <summary>
        ///
        /// </summary>
        [XmlElement(ElementName = "requestdata")]
        public T requestdata
        {
            get;
            set;
        }
    }
}