using System;
using System.Collections.Generic;
using System.Text;

namespace NOB.Generator.Information
{
    public class ClassInformation
    {
        public string Name { get; set; }

        public string NameSpace { get; set; }

        public IEnumerable<FieldInformation> FieldsInformation { get; set; }

        public IEnumerable<MethodInformation> MethodsInformation { get; set; }

        public void Deconstruct(out string name,
                                out string nameSpace,
                                out IEnumerable<FieldInformation> fieldsInformation,
                                out IEnumerable<MethodInformation> methodsInformation)
        {
            name = Name;
            nameSpace = NameSpace;
            fieldsInformation = FieldsInformation;
            methodsInformation = MethodsInformation;
        }
    }
}
