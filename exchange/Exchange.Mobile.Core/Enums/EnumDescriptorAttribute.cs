using System;

namespace Exchange.Mobile.Core.Enums
{
    public class EnumDescriptorAttribute : Attribute
    {
        public string Descriptor { get; set; }
        public EnumDescriptorAttribute(string descriptor = null)
        {
            Descriptor = descriptor is null ? string.Empty : descriptor;
        }
    }
}
