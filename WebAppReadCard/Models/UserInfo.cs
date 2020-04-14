using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebAppReadCard.Models
{
    public class UserInfo
    {
        [XmlElement(ElementName = "VERSION")] 
        public string VerSion { get; set; } = "1.0";
        [XmlElement(ElementName = "BUSICODE")]
        public string BUSICODE { get; set; } = "00";
        [XmlElement(ElementName = "YLJGBM")]
        public string YLJGBM { get; set; } 
        [XmlElement(ElementName = "DKLXDM")]
        public string DKLXDM { get; set; }
        [XmlElement(ElementName = "OPERNO")]
        public string OperNo { get; set; } 
        [XmlElement(ElementName = "OPERNAME")]
        public string OperName { get; set; } 
    }
}