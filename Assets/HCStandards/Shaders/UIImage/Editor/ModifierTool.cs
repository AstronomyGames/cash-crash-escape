using System;
using System.Reflection;
using System.Collections.Generic;

namespace UnityEngine.UI.Procedural
{
    public static class ModifierTool
    {
        /// Gets the instance with identifier specified in a ModifierID Attribute.
        /// <returns>The instance with identifier.</returns>
        public static BaseModifier GetInstanceWithId(string id)
        {
            return (BaseModifier)Activator.CreateInstance(GetTypeWithId(id));
        }
        /// <summary>
        /// Gets the type with specified in a ModifierID Attribute.
        /// </summary>
        /// <returns>The type with identifier.</returns>
        /// <param name="id">Identifier.</param>
        public static Type GetTypeWithId(string id)
        {
            foreach (Type type in Assembly.GetAssembly(typeof(BaseModifier)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(BaseModifier)))
                {
                    if (((ModifierID[])type.GetCustomAttributes(typeof(ModifierID), false))[0].Name == id)
                    {
                        return type;
                    }
                }
            }
            return null;
        }
        /// Gets a list of Attributes of type ModifierID.
        /// <returns>The attribute list.</returns>
        public static List<ModifierID> GetAttributeList()
        {
            List<ModifierID> l = new List<ModifierID>();
            foreach (Type type in Assembly.GetAssembly(typeof(BaseModifier)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(BaseModifier)))
                {
                    l.Add(((ModifierID[])type.GetCustomAttributes(typeof(ModifierID), false))[0]);
                }
            }
            return l;
        }
    }
}