using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CardService.Model
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
    [XmlRoot(ElementName = "neuqpay")]
    public class NeuqPayResponse<T>
    {
        /// <summary>
        ///
        /// </summary>
        [XmlElement(ElementName = "responsedata")]
        public T responsedata
        {
            get;
            set;
        }
    }
    public class CardInfo
    {
        [XmlElement(ElementName = "RETURNCODE")]
        public string RETURNCODE { get; set; } 
        [XmlElement(ElementName = "SCARDNO")]
        public string SCARDNO { get; set; } = "";
        [XmlElement(ElementName = "NAME")]
        public string NAME { get; set; } = "";
    }
    
}