using Exchange.Mobile.Core.Constants;

namespace Exchange.Mobile.Core.Enums
{
    public enum CategoryEnum
    {

        None = 0,
        [EnumDescriptor(Descriptor = Constant.CategoryIcon.TOY_ICON)]
        Toy,
        [EnumDescriptor(Descriptor = Constant.CategoryIcon.EAT_ICON)]
        Eat,
        [EnumDescriptor(Descriptor = Constant.CategoryIcon.TECH_ICON)]
        Tech,
        [EnumDescriptor(Descriptor = Constant.CategoryIcon.AUTO_ICON)]
        Auto,
        [EnumDescriptor(Descriptor = Constant.CategoryIcon.MONEY_ICON)]
        Money,
        [EnumDescriptor(Descriptor = Constant.CategoryIcon.OTHER_ICON)]
        Other
    }
}
