using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace HeroBot.Plugins.RP.Entities
{
    public enum Jobs
    {
        [Description("Chômeur")]
        NO_JOB,
        [Description("I work at the HeroBot's Staff <a:partner:529984152494407700>")]
        HEROBOT_WORKER,
        [Description("I'm a partener :) <:partener:618824069751767052>")]
        PARTENER
    }

    public static class EnumExtension {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
