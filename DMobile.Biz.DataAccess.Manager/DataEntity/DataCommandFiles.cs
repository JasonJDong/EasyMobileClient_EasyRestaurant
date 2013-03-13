using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace DMobile.Biz.DataAccess.Manager.DataEntity
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "files", Namespace = "http://www.easyrest.com/DataCommandFiles", IsNullable = false)]
    public class DataCommandFiles
    {
        [XmlElement(ElementName = "file")]
        public FileMajor[] Files { get; set; }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class FileMajor
    {
        [XmlAttribute(AttributeName = "useFor")]
        public string UseFor { get; set; }

        [XmlAttribute(AttributeName = "location")]
        public string Location { get; set; }
    }
}